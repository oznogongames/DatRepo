﻿using UnityEngine;
using System.Collections;

public class BackButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Input.simulateMouseWithTouches = true;
    }
    public void onClick(){
         Application.LoadLevel(0);
    }
}
