using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
//		Debug.Log("explode on trigger enter");
		if (other.gameObject.tag == "Stage")
		{
//			Debug.Log("explode stage on trigger enter");
			//gameObject.SetActive(false);
		}
	}

//	private void OnCollisionEnter(Collision other)
//	{
//		Debug.Log("explode on collision enter");
//		if (other.gameObject.tag == "Stage")
//		{
//			Debug.Log("explode stage on collision enter");
//				gameObject.SetActive(false);
//		}
//		
//	}
}
