using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEditor;
//using UnityEditor.Experimental.Build.AssetBundle;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.WSA;

public class PlayerController : MonoBehaviour
{
    public float speedX;
    public float speedZ;

    static public bool stageClear;
    public GameObject bomb;

    private Rigidbody rb;
    static public int count;

    public float explodePower = 3.0f;

    private string direction;
    private Vector3 dirVector;
    static public bool isPowerUp;

    public float BombSpan { get; set; } = 0.8f;
    public float 
        Delta { get; set; }  = 0.0f;


    private int PlayerLayer = 9;
    private int BombLayer = 10;

    //public GameObject obstacleObject;
    
    
    void Start()
    {
        this.gameObject.layer = PlayerLayer;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ManipulatePlayer();
//        if (tag == "Player")
//        {
//            ManipulatePlayer();
//        }
    }

    private void ManipulatePlayer()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector3(speedX, 0.0f, 0.0f);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector3(-speedX, 0.0f, 0.0f);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.velocity = new Vector3(0.0f, 0.0f, speedZ);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.velocity = new Vector3(0.0f, 0.0f, -speedZ);
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }

        if (Delta > BombSpan)
        {
            if (Input.GetKey(KeyCode.B))
            {
                PutBomb();
                Delta = 0.0f;
            }
        }

        Delta += Time.deltaTime;
    }

    private void LogPosition()
    {
        int playerPosX = Mathf.RoundToInt(transform.position.x);
        int playerPosZ = Mathf.RoundToInt(transform.position.z);
        string pos = String.Format("posX is{0}, posZ is {1}", playerPosX, playerPosZ);
        Debug.Log(pos);
    }

    public void PutBomb()
    {
        int playerPosX = Mathf.RoundToInt(transform.position.x);
        int playerPosZ = Mathf.RoundToInt(transform.position.z);
        var position = new Vector3(playerPosX, 0.5f, playerPosZ);
//        var o = Instantiate(obstacleObject, position, Quaternion.identity);
//        Destroy(o, 3.0f);
        GameObject b = Instantiate(bomb, position, Quaternion.identity);
        b.GetComponent<BombController>().explodePower = explodePower;
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.GetComponent<CapsuleCollider>())
        {
            other.gameObject.GetComponent<CapsuleCollider>().isTrigger = false;        
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FirePowerUp")
        {
            explodePower += 1.0f;
        }
        else if (other.tag == "BombSpeedUp")
        {
            Debug.Log(String.Format("bomb speed up is {0}", BombSpan));
            BombSpan -= 0.2f;
            Debug.Log(String.Format("bomb speed up is {0}", BombSpan));
        } else if (other.tag == "Explode")
        {
            Debug.Log("burned!!!!");
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Bomb")
        {
            other.gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
        }    
    }
    
}