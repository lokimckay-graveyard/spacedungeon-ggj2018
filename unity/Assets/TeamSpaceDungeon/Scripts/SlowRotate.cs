﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowRotate : MonoBehaviour {

    public float speed = 10f;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
		
	}
}
