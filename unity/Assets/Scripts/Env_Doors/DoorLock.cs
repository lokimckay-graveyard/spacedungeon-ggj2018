using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorLock : MonoBehaviour {

	// Public variables
	// Is this door locked by default?
	public bool locked = false;
	public string unlockCode = "defaultCode";

	private NavMeshObstacle navModifier;

	void Awake () {
		navModifier = GetComponent<NavMeshObstacle> ();
		if (locked) {
			navModifier.carving = true;
		} else {
			navModifier.carving = false;
		}
	}

	public bool AttemptUnlock (string code) {
		if (code == unlockCode) {
			locked = false;
			navModifier.carving = false;
			return true;
		} else {
			return false;
		}
	}

	public void LockDoor () {
		locked = true;
		navModifier.carving = true;
	}
}
