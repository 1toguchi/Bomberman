using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityScript.Lang;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using UnityEditor;
using UnityEngine.SocialPlatforms;
using Random = System.Random;

public class BombController : MonoBehaviour
{
	public bool isExplode = false;
	public float explodePower = 3.0f;
	//private float pBombPower = player.GetComponent<PlayerController>().explodePower;

	public int itemPossibility = 3;
	
	public GameObject ExplodePowerUpObj;
	public GameObject BombSpeedUpObj;
	public GameObject ExplodeObj;
	
	Dictionary<string,decimal> explodeDistanceDict = new Dictionary<string, decimal>()
	{
		{"xPlus", 3},{"xMinus", 3},{"zPlus", 3 },{"zMinus", 3}
	};
	
	private void Start()
	{
		StartCoroutine(WaitForThreeSeconds());
	}

	// Update is called once per frame
	void Update()
	{
		if (this.isExplode)
		{
			RaycastForExplode();
			Explode(explodeDistanceDict);
		}
	}

	private IEnumerator WaitForThreeSeconds()
	{
		yield return new WaitForSeconds(3);
		isExplode = true;
	}

	private IEnumerator WaitForASecond()
	{
		yield return new WaitForSeconds(1);
	}
	
	
	
	public void RaycastForExplode()
	{
		//// right
		Ray rayRight = new Ray(transform.position, Vector3.right);
		RaycastHit raycastHitRight;
		DrawRedRay(rayRight);
		if (Physics.Raycast(rayRight.origin, Vector3.right, out raycastHitRight, explodePower))
		{
			RaycastJudge(raycastHitRight);
			explodeDistanceDict["xPlus"] = Math.Round((decimal)raycastHitRight.distance, MidpointRounding.AwayFromZero) -1;
			Debug.Log(String.Format("xplus is {0}", explodeDistanceDict["xPlus"]));
		}
		else
		{
			Debug.Log("raycast right is not hit");
		}
		/////		

		//// forward
		Ray rayForward = new Ray(transform.position, Vector3.forward);
		RaycastHit raycastHitForward;
		DrawRedRay(rayForward);
		
		if (Physics.Raycast(rayForward.origin, Vector3.forward, out raycastHitForward, explodePower))
		{
			RaycastJudge(raycastHitForward);
			explodeDistanceDict["zPlus"] = Math.Round((decimal)raycastHitForward.distance, MidpointRounding.AwayFromZero) - 1;
			Debug.Log(String.Format("zplus is {0}", explodeDistanceDict["zPlus"]));
//			Blaze(rayForward.origin, Vector3.forward, Mathf.RoundToInt(raycastHitForward.distance));
		}
		else
		{
			Debug.Log("raycast forward is not hit");
		}
		////
		
		//// back
		Ray rayBack = new Ray(transform.position, Vector3.back);
		RaycastHit raycastHitBack;
		DrawRedRay(rayBack);
		if (Physics.Raycast(rayBack.origin, Vector3.back, out raycastHitBack, explodePower))
		{
			RaycastJudge(raycastHitBack);
			explodeDistanceDict["zMinus"] = Math.Round((decimal)raycastHitBack.distance, MidpointRounding.AwayFromZero) -1 ;
			Debug.Log(String.Format("zminus is {0}", explodeDistanceDict["zMinus"]));
//			Blaze(rayBack.origin, Vector3.back, Mathf.RoundToInt(raycastHitBack.distance));
		}
		else
		{
			Debug.Log("raycast back is not hit");
		}
		////
		
		//// left
		Ray rayLeft = new Ray(transform.position, Vector3.left);
		RaycastHit 	raycastHitLeft;
		DrawRedRay(rayLeft);
		if (Physics.Raycast(rayLeft.origin, Vector3.left, out raycastHitLeft, explodePower))
		{
			RaycastJudge(raycastHitLeft);
			explodeDistanceDict["xMinus"] = Math.Round((decimal)raycastHitLeft.distance, MidpointRounding.AwayFromZero) -1;
			Debug.Log(String.Format("xminus is {0}", explodeDistanceDict["xMinus"]));
//			Blaze(rayLeft.origin, Vector3.left, Mathf.RoundToInt(raycastHitLeft.distance));
		}
		else
		{
			Debug.Log("raycast left is not hit");
		}
		////
	}
	
	private void RaycastJudge(RaycastHit raycastHit)
	{
//		Debug.Log("raycasthit.point is");
//		Debug.Log(raycastHit.point);
//		Debug.Log("raycasthit.transform is");
//		Debug.Log(raycastHit.transform.position);
//		Debug.Log("raycast hit name is");
//		Debug.Log(raycastHit.collider.name);
		Collider hitObjectCollider = raycastHit.collider; 
		switch (hitObjectCollider.tag)
		{
			case "Bomb":
				Debug.LogWarning("raycast hit bomb");
				raycastHit.collider.GetComponent<BombController>().isExplode = true;
				break;
			case "Player":
				Debug.LogWarning("raycast hit player");
				// todo game over
				break;
			case "Block":
				Debug.LogWarning("raycast hit block");
				var blaze = Instantiate(ExplodeObj, hitObjectCollider.transform.position, Quaternion.identity);
				Destroy(blaze, 1.0f);
				PutItem(hitObjectCollider.transform.position);
				hitObjectCollider.enabled = false;
				hitObjectCollider.gameObject.SetActive(false);
				// todo item
				break;
			case "Stage":
				Debug.LogWarning("raycast hit stage");
				break;
			case "FirePowerUp":
				var blazefire = Instantiate(ExplodeObj, hitObjectCollider.transform.position, Quaternion.identity);
				Destroy(blazefire, 1.0f);
				Debug.LogWarning("raycast hit FirePowerUp");
				hitObjectCollider.enabled = false;
				hitObjectCollider.gameObject.SetActive(false);
				break;
			case "BombSpeedUp":
				var b = Instantiate(ExplodeObj, hitObjectCollider.transform.position, Quaternion.identity);
				Destroy(b, 1.0f);
				Debug.LogWarning("raycast hit FirePowerUp");
				hitObjectCollider.enabled = false;
				hitObjectCollider.gameObject.SetActive(false);
				break;
				
			default:
				// nothing to do
				break;
		}
	}

	private void PutItem(Vector3 pos)
	{
		int num = UnityEngine.Random.Range(0, itemPossibility);
		StartCoroutine(WaitForASecond());

		if (num == 0)
		{
			Instantiate(ExplodePowerUpObj, pos, Quaternion.identity);
			Debug.Log("explode obj created");
		}
		else if (num == 1)
		{
			Instantiate(BombSpeedUpObj, pos, Quaternion.identity);
			Debug.Log("bomb power up obj created");
		}
	}

	private void Explode(Dictionary<string, decimal> explodeDistanceDict)
	{
		bool isXEven = (transform.position.x % 2 == 0);
		bool isZEven = (transform.position.z % 2 == 0);

		Destroy(gameObject);
		var blaze = Instantiate(ExplodeObj, transform.position, Quaternion.identity);
		Destroy(blaze, 1.0f);
		
		BlazeInLine(explodeDistanceDict);
	}

	private void BlazeInLine(Dictionary<string, decimal> distanceDict)
	{
		Vector3 dir = new Vector3();
		Vector3 p;

		Vector3 pos = transform.position;

		foreach (var d in explodeDistanceDict)
		{

			Debug.LogWarning(String.Format("dict key is {0} and dict value is {1}", d.Key, d.Value));
			for (int j = 1; j <= d.Value; j++)
			{
				switch (d.Key)
				{
					case "xPlus":
						dir = new Vector3(j, 0, 0);
						p = pos + dir;
						Debug.Log(String.Format("blaze is at {0}", p));
						Debug.Log(String.Format("xplus is in stage :{0} ", GeneralController.IsInStage(p)));

						if (GeneralController.IsInStage(p))
						{
							var blaze = Instantiate(ExplodeObj, p, Quaternion.identity);
							Destroy(blaze, 1.0f);
						}

						break;
					case "xMinus":
						dir = new Vector3(j, 0, 0);
						p = pos - dir;
						Debug.Log(String.Format("blaze is at {0}", p));
						Debug.Log(String.Format("xminus is in stage :{0} ", GeneralController.IsInStage(p)));
						if (GeneralController.IsInStage(p))
						{
							var blaze = Instantiate(ExplodeObj, p, Quaternion.identity);
							Destroy(blaze, 1.0f);
						}

						break;
					case "zPlus":
						dir = new Vector3(0, 0, j);
						p = pos + dir;
						Debug.Log(String.Format("blaze is at: {0}", p));
						Debug.Log(String.Format("zplus is in stage :{0} ", GeneralController.IsInStage(p)));

						if (GeneralController.IsInStage(p))
						{
							var blaze = Instantiate(ExplodeObj, p, Quaternion.identity);
							Destroy(blaze, 1.0f);
						}

						break;
					case "zMinus":
						dir = new Vector3(0, 0, j);
						p = pos - dir;
						Debug.Log(String.Format("blaze is at: {0}", p));
						Debug.Log(String.Format("zminus is in stage :{0} ", GeneralController.IsInStage(p)));
						if (GeneralController.IsInStage(p))
						{
							var blaze = Instantiate(ExplodeObj, p, Quaternion.identity);
							Destroy(blaze, 1.0f);
						}

						break;
					default:
						break;

				}
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Explode")
		{	
			isExplode = true;
			
		}
		
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Explode")
		{	
			this.isExplode = true;
		}
	}

	private static void DrawRedRay(Ray ray)
	{
//		Debug.Log("ray origin is ");
//		Debug.Log(ray.origin);
		//開始点　終点を指定して飛ぶ
//		Debug.DrawLine(ray.origin, ray.direction, Color.red, 5.0f);
		// 相対的に四方に飛ぶ
		// こっちっぽい
//		Debug.DrawRay(ray.origin, ray.direction,  Color.red, 5.0f);
	}
}
