using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour, ICanReceiveDamage {

    public int maxHealth = 10;
    public float moveSpeed = 1;
    public float angularSpeed = 1200;
    public int damage = 1;
    public float attackRange = 2;
    public int attacksPerSecond = 1;
    public float postAttackWaitDuration = 1f;
    public float awarenessRadius = 5f;
    public float deathTime = 0.1f;

    [Header("Debugging")]
    public bool DebugCols;
    public bool DebugState;
    public Renderer MainMesh;

    protected NavMeshAgent navAgent;
    protected GameManager GM;
    protected float lastAttackTime;
    protected int currentHealth;

    public enum eState { Idle, Moving, Attacking, Waiting }
    public eState EnemyState;

    public float soundTimer;
    public float nearSoundDistance;
    public float farSoundDistance;
    public AudioClip nearSound;
    public AudioClip farSound;
    AudioSource thisAudio;
    float maxSoundTimer;

	// Sprite that the user sees when getting attacked or when this dies
	public Transform enemySprite;
	public float enemyAttackVisibilityTime = 0.1f;

    void Start () {
        Initialize();
	}

    void Initialize()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = moveSpeed;
        navAgent.angularSpeed = angularSpeed;
        currentHealth = maxHealth;

        ModifyState(eState.Idle);
        MainMesh.material.color = Color.clear;

        thisAudio = GetComponent<AudioSource>();
        maxSoundTimer = soundTimer;
    }
	
	void Update () {
        if(GM.GameState == GameManager.gState.play)
        {
            MovementLogic();
            AttackLogic();
        } else if(!navAgent.isStopped)
        {
            navAgent.isStopped = true;
            Idle();
        }

        checkSound();
	}

    void checkSound()
    {
        // Far Sound
        if (GM.GetDistanceToVRPlayer(transform.position) < farSoundDistance)
        {
            if (soundTimer < 0.0f)
            {
                thisAudio.clip = farSound;
                thisAudio.Play();
                soundTimer = maxSoundTimer;
            }
        }
        // Near Sound
        else if (GM.GetDistanceToVRPlayer(transform.position) < nearSoundDistance)
        {
            if (soundTimer < 0.0f)
            {
                thisAudio.clip = nearSound;
                thisAudio.Play();
                soundTimer = maxSoundTimer;
            }
        }

        // Decrease Timer
        soundTimer -= Time.deltaTime;
    }

    /// <summary>
    /// Houses all enemy move logic
    /// </summary>
    void MovementLogic()
    {
        if (EnemyState != eState.Attacking && EnemyState != eState.Waiting)
        {
            Move(GM.VRPlayer.transform.position); //Always move toward where player is if not attacking
        }
    }

    /// <summary>
    /// Move to a target location if able
    /// </summary>
    /// <param name="moveTarget">Place to move to</param>
    public void Move(Vector3 moveTarget)
    {
        if (GM.GetDistanceToVRPlayer(transform.position) < awarenessRadius)
        {
            if (ModifyState(eState.Moving))
            {
                navAgent.SetDestination(moveTarget);
            }
        }
    }

    /// <summary>
    /// Houses all enemy attack logic
    /// </summary>
    void AttackLogic()
    {
        if(GM.GetDistanceToVRPlayer(transform.position) < attackRange)
        {
            if (CanAttack()) {
                if(ModifyState(eState.Attacking))
                {
                    Attack(GM.VRPlayerScript, damage);
                }
            }
        }
    }

    /// <summary>
    /// Returns true if this enemy is able to attack right now
    /// </summary>
    /// <returns></returns>
    public bool CanAttack()
    {
        return Time.time - lastAttackTime > 1 / attacksPerSecond || lastAttackTime == 0;
    }

    /// <summary>
    /// Makes the enemy attack a given script that is able to receive damage
    /// </summary>
    /// <param name="targetScript">The script to trigger receive damage upon</param>
    /// <param name="damageToDeal">Amount of damage to deal</param>
    void Attack(ICanReceiveDamage targetScript, int damageToDeal)
    {
        targetScript.ReceiveDamage(damageToDeal, gameObject.name);
        lastAttackTime = Time.time;
        Wait(postAttackWaitDuration);
    }

    //Make the enemy wait for an amount of time before taking another action

    /// <summary>
    /// Make the enemy wait for an amount of time before taking another action
    /// </summary>
    /// <param name="duration">Amount of time to wait</param>
    public void Wait(float duration)
    {
        if (ModifyState(eState.Waiting))
        {
			// Flash the sprite.
			enemySprite.gameObject.SetActive (true);
            Invoke("Idle", duration);
			Invoke("ResetEnemySprite", enemyAttackVisibilityTime);
        }
    }

	// Reset the sprite after a short delay to hidden mode. 
	private void ResetEnemySprite () {
		enemySprite.gameObject.SetActive (false);
	}

    /// <summary>
    /// Attempt to update the enemies state to a new one
    /// </summary>
    /// <param name="desiredState">State </param>
    /// <returns></returns>
    public bool ModifyState(eState desiredState)
    {
        if (AllowedState(desiredState))
        {
            if (DebugState) { Debug.Log("Success changing to state: " + desiredState); }
            EnemyState = desiredState;
            if (DebugCols) { DebugStateCol(desiredState); }
        }
        return EnemyState == desiredState;
    }

    /// <summary>
    /// Make the enemy go Idle if able
    /// </summary>
    public void Idle()
    {
        ModifyState(eState.Idle);
    }

    /// <summary>
    /// Whether the Enemy can currently change to a given state
    /// </summary>
    /// <param name="desiredState">State the the Enemy should attempt to change to</param>
    /// <returns>True if the Enemy is allowed to change to the desired state</returns>
    public bool AllowedState(eState desiredState)
    {
        if (EnemyState == desiredState && desiredState != eState.Moving) { return false; } //Cannot enter the same state that we are already in, unless we want to walk somewhere new
        if (EnemyState == eState.Waiting && desiredState != eState.Idle) { return false; } //Cannot do anything except idle if we are waiting

        switch (desiredState)
        {
            case eState.Idle:
                ToggleNavAgent(true);
                break;
            case eState.Moving:
                if(GM.GetDistanceToVRPlayer(transform.position) < attackRange) { return false; } //Can't move if we are too close to the player
                ToggleNavAgent(true);
                break;
            case eState.Attacking:
                ToggleNavAgent(false);
                break;
            case eState.Waiting:
                ToggleNavAgent(false);
                break;
        }
        return true;
    }

    //Changes the mainMesh's colour depending on what state the enemy is currently in
    private void DebugStateCol(eState state)
    {
        switch (state)
        {
            case eState.Idle:
                MainMesh.material.color = GM.ColMgr.IdleCol;
                break;
            case eState.Moving:
                MainMesh.material.color = GM.ColMgr.MoveCol;
                break;
            case eState.Attacking:
                MainMesh.material.color = GM.ColMgr.AttackCol;
                break;
            case eState.Waiting:
                MainMesh.material.color = GM.ColMgr.WaitCol;
                break;
        }
    }

    /// <summary>
    /// Stops / starts the navAgent
    /// </summary>
    /// <param name="onOff"></param>
    public void ToggleNavAgent(bool onOff)
    {
        navAgent.isStopped = !onOff;
    }

    /// <summary>
    /// Damage this enemy
    /// </summary>
    /// <param name="damageDealt">Amount of damage to deal</param>
    /// <returns>Whether damage was successfully delivered</returns>
    public bool ReceiveDamage(int damageDealt, string damageDealer)
    {
        if(DebugState) { Debug.Log(gameObject.name + " received " + damageDealt + " damage from " + damageDealer); }
        currentHealth -= damageDealt;
        if(currentHealth <= 0) { Kill(damageDealer); }
        return true;
    }

    /// <summary>
    /// Kill this enemy
    /// </summary>
    /// <param name="killer">What killed the enemy</param>
    /// <returns>Whether the enemy died</returns>
    public bool Kill(string killer)
    {
        if(currentHealth <= 0)
        {
            if (DebugState) { Debug.Log(gameObject.name + " died"); }
			enemySprite.gameObject.SetActive (true);
            Destroy(gameObject, deathTime);
            return true;
        }
        return false;
    }
}
