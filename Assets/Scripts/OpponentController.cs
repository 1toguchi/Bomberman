using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpponentController : MonoBehaviour
{
	public Transform target;
	public Transform homeTarget;
 	
	private NavMeshAgent agent;

	// Use this for initialization
	void Start ()
	{
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (PlayerController.isPowerUp)
		{
			agent.SetDestination(homeTarget.position);
		}
		else
		{
			agent.SetDestination(target.position);
		}

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			if (PlayerController.isPowerUp)
			{
				gameObject.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
			}
		}
	}
}
