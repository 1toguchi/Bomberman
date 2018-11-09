using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpeedUpController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			gameObject.SetActive(false);
		}
	}
}
