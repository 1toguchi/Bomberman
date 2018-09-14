using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
//using UnityEditor.Experimental.Build.AssetBundle;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speedX;
    public float speedZ;
//    public float powerUpSpan;
    private float time = 0.0f;
//    public Transform respawnPoint;

    public float warpOffset;
    
//    public Text countText;
    static public int energyBallCount = 0;
    static public int allEnergyBallCount;
    
//    public Text winText;
    static public bool stageClear;
//    public Text stageNameText;

    private Rigidbody rb;
    static public int count;

    private string direction;
    private Vector3 dirVector;
    static public bool isPowerUp;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
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
    }

    void PutBomb()
    {
        if (Input.GetKey(KeyCode.B))
        {
            
        }
    }


    private void OnTriggerEnter(Collider other)
    {
    }
}