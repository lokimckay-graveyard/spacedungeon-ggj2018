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
		animator.SetBool("Open", false);
		Debug.Log ("HELLO");
	}

	// Called when the keycard enters the trigger zone
	void OpenDoor () {
		// Open the door with animation
		animator.SetBool("Open", true);
	}

	// Called when the trigger detects an object enter it's radius
	void OnTriggerEnter (Collider col) {
		if ((col.gameObject.name == "[VRTK][AUTOGEN][HeadsetColliderContainer]" || col.tag == "Player" || col.tag == "Enemy" || col.tag == "KeyCard") && !actorsInProximity.Contains(col.transform.GetInstanceID())) {
			actorsInProximity.Add (col.transform.GetInstanceID());
			if (doorLock.locked == false && !animator.GetBool("Open")) {
				// Open the door with animation
				animator.SetBool("Open", true);
                GetComponent<AudioSource>().Play();
            }
		}
	}

	// Called when a player exits the door range
	void OnTriggerExit (Collider col) {
		if ((col.gameObject.name == "[VRTK][AUTOGEN][HeadsetColliderContainer]" || col.tag == "Player" || col.tag == "Enemy" || col.tag == "KeyCard") && actorsInProximity.Contains (col.transform.GetInstanceID())) {
			actorsInProximity.Remove (col.transform.GetInstanceID());
			if (actorsInProximity.Count == 0) {
				// Close the door with animation
				animator.SetBool("Open", false);
                GetComponent<AudioSource>().Play();
            }
		}
	}
}
