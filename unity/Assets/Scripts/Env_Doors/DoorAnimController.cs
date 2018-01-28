using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimController : MonoBehaviour {

	// Priveate components
	private DoorLock doorLock;
	private Animator animator;

	// List of actors inside the door radius
	private List<int> actorsInProximity = new List<int>();

	void Awake () {
		doorLock = GetComponent<DoorLock> ();
		animator = GetComponent<Animator> ();
		doorLock.OnLockEvent.AddListener (OnLocked);
	}

	void OnLocked () {
		// Close the door with animation
		animator.SetBool("open", false);
		Debug.Log ("HELLO");
	}

	// Called when the trigger detects an object enter it's radius
	void OnTriggerEnter (Collider col) {
		if ((col.tag == "Player" || col.tag == "Enemy") && !actorsInProximity.Contains(col.transform.GetInstanceID())) {
			actorsInProximity.Add (col.transform.GetInstanceID());
			if (doorLock.locked == false && !animator.GetBool("open")) {
				// Open the door with animation
				animator.SetBool("open", true);
			}
		}
	}

	// Called when a player exits the door range
	void OnTriggerExit (Collider col) {
		if ((col.tag == "Player" || col.tag == "Enemy") && actorsInProximity.Contains (col.transform.GetInstanceID())) {
			actorsInProximity.Remove (col.transform.GetInstanceID());
			if (actorsInProximity.Count == 0) {
				// Close the door with animation
				animator.SetBool("open", false);
			}
		}
	}
}
