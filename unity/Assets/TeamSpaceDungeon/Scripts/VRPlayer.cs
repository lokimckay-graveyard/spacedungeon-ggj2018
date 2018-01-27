using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayer : MonoBehaviour, ICanReceiveDamage {

    public int maxHealth = 10;

    public Transform anchor;

    [Header("Debug")]
    public bool debugging;

    protected int currentHealth;
    private GameManager GM;

    private void Awake() { Initialize(); }

    void Initialize()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        UpdatePositionToAnchor();
    }

    /// <summary>
    /// Damage the VR Player
    /// </summary>
    /// <param name="damageDealt">Amount of damage to deal to the VRPlayer</param>
    /// <returns>Whether or not dealing the damage was successful</returns>
	public bool ReceiveDamage(int damageDealt, string damageDealer)
    {
        if (debugging) { Debug.Log("Player receiving damage from: " + damageDealer); }
        currentHealth -= damageDealt;
        if (currentHealth <= 0) { Kill(damageDealer); }
        if (debugging) { Debug.Log("Health is now " + currentHealth); }
        return true;
    }

    /// <summary>
    /// Kill this enemy
    /// </summary>
    /// <param name="killer">What killed the enemy</param>
    /// <returns>Whether the enemy died</returns>
    public bool Kill(string killer)
    {
        if (currentHealth <= 0)
        {
            GM.GoToGameOver();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Anchors the player pos to anchor transform
    /// </summary>
    private void UpdatePositionToAnchor()
    {
        transform.position = anchor.position;
    }
}
