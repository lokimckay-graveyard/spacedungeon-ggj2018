using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBehaviour : MonoBehaviour {

    private GameManager GM;

    void Awake() { GM = GameObject.Find("GameManager").GetComponent<GameManager>(); }

	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "[VRTK][AUTOGEN][HeadsetColliderContainer]")
        {
            GM.victory = true;
            GM.GoToGameOver();
        }
    }
}
