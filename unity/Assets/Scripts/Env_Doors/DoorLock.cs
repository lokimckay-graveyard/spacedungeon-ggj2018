using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class DoorLock : MonoBehaviour {

	// Public variables
	// Is this door locked by default?
	public bool locked = false;
	public string unlockCode = "defaultCode";
	public bool useKeyCard = false;

	// Private components
	private NavMeshObstacle navModifier;
	private BoxCollider boxCollider;

	// Event dispatcher
	[HideInInspector]
	public UnityEvent OnLockEvent;

	void Awake () {
		navModifier = GetComponent<NavMeshObstacle> ();
		boxCollider = GetComponent<BoxCollider> ();
		OnLockEvent = new UnityEvent ();
		if (locked) {
			navModifier.carving = true;
			boxCollider.enabled = true;
		} else {
			navModifier.carving = false;
			boxCollider.enabled = false;
		}
	}

	public bool AttemptUnlock (string code) {
		if (code == unlockCode) {
			locked = false;
			navModifier.carving = false;
			boxCollider.enabled = false;
			return true;
		} else {
			return false;
		}
	}

	public void LockDoor () {
		locked = true;
		navModifier.carving = true;
		boxCollider.enabled = true;
		OnLockEvent.Invoke ();
	}

	void OnTriggerEnter (Collider col) {
		if (useKeyCard && !AttemptUnlock (col.transform.name)) {
			Debug.Log ("Wrong KeyCard");
		} else {			
			
		}
	}
}
