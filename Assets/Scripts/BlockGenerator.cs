using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class BlockGenerator : MonoBehaviour
{
	public GameObject block;
	public int BlockDensity; //0~10;
	
	// Update is called once per frame
	private void Start()
	{
		for (int x = -6; x <= 6; x++)
		{
			for (int z = -6; z <= 6; z++)
			{
				bool presence = (Random.Range(1, BlockDensity)) != 1; 
				Vector3 pos = new Vector3(x,0.5f,z);
				bool isInStage = GeneralController.IsInStage(pos);
				if (isInStage && !IsInCorner(pos) && presence)
				{
					Instantiate(block, pos, Quaternion.identity);
				}
			}
		}
	}

	private bool IsInCorner(Vector3 pos)
	{
		var x = new[] {-6, -5, 6, 5};
		var z = new[] {-6, -5, 6, 5};

		bool result = (x.Contains((int)pos.x) && z.Contains((int)pos.z)) ? true : false;
		return result;
	}
}