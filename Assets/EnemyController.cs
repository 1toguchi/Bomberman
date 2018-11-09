using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemyController : MonoBehaviour
{
	public Transform _target;
	private NavMeshAgent _navAgent;
	private int _num = 0;
	private Collider bombCollider;
	private float _delta = 0.0f;
	private float _timeSpan = 0.0f;
	
	// Use	 this for initialization
	void Start()
	{
		_navAgent = GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
	void Update()
	{
		Rigidbody rb = GetComponent<Rigidbody>();


		var closestTarget = FindClosestTarget("Block");
		if (closestTarget.gameObject.activeSelf) 
		{
			Debug.Log($"destanation is  {_navAgent.destination}");
			_navAgent.destination = closestTarget.transform.position;

		}
		else
		{
			
		}

		//navmesh がissleepではない？
		//そっちのほうが楽？
		//raycast都バス？
		//bombは障害物扱いされているのか？
		//障害物はobstacleつけるべきなのか？
		//pathないときってどうするの？


		Attack();
	}
	
	GameObject FindClosestTarget(string trgt)
	{
		var closestGameObject = GameObject.FindGameObjectsWithTag(trgt)
			.OrderBy(go => Vector3.Distance(go.transform.position, transform.position)).FirstOrDefault();
		Debug.Log($"closest gameobect {closestGameObject.transform.position}");

		return closestGameObject ;
	}
	
	void Attack()
	{
		float bombSpan = GetComponent<PlayerController>().BombSpan;

		const MidpointRounding away = MidpointRounding.AwayFromZero;
		decimal distance = (decimal)(_navAgent.destination - transform.position).magnitude;
		distance = Math.Round(distance, 1, away);
		Debug.Log($"delta {_delta}");
		Debug.Log($"bombspan {bombSpan}");


		Debug.Log($"distance is {distance}");
		
		if (distance == 0.5m)
			{
			Debug.Log($"0.5m is {true}");

			if (_delta > bombSpan)
			{
				Debug.Log($"(delta > bombSpan {_delta > bombSpan}");

				GetComponent<PlayerController>().PutBomb();
				_delta = 0.0f;
				Debug.LogWarning($"_delta has been reset {_delta}");
			}
		_delta += Time.deltaTime;
		}
	}
	
	void DeleteBlock()
	{
		//
	}	
	
	void Avoid()
	{
	}

	void Escape(Vector3 pos)
	{
		bombCollider = GetComponent<SphereCollider>();
		var bombs = GameObject.FindGameObjectsWithTag("Bomb");
		var enemyPos = transform.position;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Bomb")
		{
			Debug.Log($"bomb searched {other.transform.position}");
//			transform.Translate();
		}
	}
}
