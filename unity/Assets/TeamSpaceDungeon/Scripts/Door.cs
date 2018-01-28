using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour {

    public float openDist = 3;
    public float speed = 0.1f;

    protected NavMeshObstacle[] navObst;
    protected Transform[] childTrans;
    protected Vector3[] startPos = new Vector3[3];
    protected float offset;

    private bool open;
    public enum dState { open, closed }
    public dState desiredState = dState.closed;

    private void Awake() { Initialize(); }

    void Initialize()
    {
        navObst = GetComponentsInChildren<NavMeshObstacle>();
        childTrans = GetComponentsInChildren<Transform>();
        startPos[1] = childTrans[1].position;
        startPos[2] = childTrans[2].position;
    }

    private void Update()
    {
        switch (desiredState)
        {
            case dState.open:
                if(!open) { IncrementDoors(speed); }
                if (offset >= openDist - 0.01f) { open = true; }
                break;
            case dState.closed:
                if(open) { IncrementDoors(-speed); }
                if (offset <= 0.01f) { open = false; }
                break;
        }
    }

    public void Open()
    {

        // Play Sound 
        GetComponent<AudioSource>().Play();

        

        desiredState = dState.open;
    }

    public void Close()
    {
        // Play Sound 
        GetComponent<AudioSource>().Play();

        desiredState = dState.closed;
    }

    private void IncrementDoors(float amt)
    {
        Vector3[] currentPos = new Vector3[2];

        childTrans[1].Translate(amt * Time.deltaTime, 0, 0, Space.Self);
        childTrans[2].Translate(-1 * amt * Time.deltaTime, 0, 0, Space.Self);

        offset += amt * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "[VRTK][AUTOGEN][HeadsetColliderContainer]")
        {
            Open();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "[VRTK][AUTOGEN][HeadsetColliderContainer]")
        {
            Close();
        }
    }
}
