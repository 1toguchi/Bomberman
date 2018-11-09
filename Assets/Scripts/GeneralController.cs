using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public static bool IsInStage(Vector3 pos)
	{
		// confirm if object position in Stage
		
		float x = pos.x;
		float z = pos.z;
		bool isInStageX = (x >= -6 && x <= 6);
		bool isInStageZ = (z >= -6 && z <= 6);
		
		bool isXEven = (x % 2 == 0);
		bool isZEven = (z % 2 == 0);
		bool isNotInBlock = (isXEven ||isZEven);

		if (isInStageX && isInStageZ && isNotInBlock)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
