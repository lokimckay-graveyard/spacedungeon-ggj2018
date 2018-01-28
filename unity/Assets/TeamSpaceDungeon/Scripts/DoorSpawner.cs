using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSpawner : MonoBehaviour {

    public GameObject doorPrefabToSpawn;

	void Start () {
        SpawnDoor();
	}

    private void SpawnDoor()
    {
        GameObject newDoor = Instantiate(doorPrefabToSpawn, transform.position, transform.rotation);
        newDoor.transform.SetParent(transform);
    }
}
