using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimController : MonoBehaviour {

	private DoorLock doorLock;

	// List of actors inside the door radius
	private List<Transform> actorsInProximity;

	void Awake () {
		doorLock = GetComponent<DoorLock> ();
	}

	// Called when the trigger detects an object enter it's radius
	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player" || col.tag == "Enemy") {
			if (doorLock.locked == false) {
				// Open the door with animation

			}
		}
	}
}
