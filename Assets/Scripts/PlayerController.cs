using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    float moveSpeed = 10.0f;
    private int totalcorner;
    private int cornerleft;
    bool coneOn = false;
    float currentTime = 0f;
    float startTime = 10f;

    bool onGround;

    public bool isHitBox;
   
    float fTimerCount;
    float iCount;

    bool TimerStarted = false;

    public Animator playerAnim;
    public GameObject plank;
    public Text CountDown;

    public Transform plankOB;
    public Material[] PlayerMaterial;

    Renderer PlayerRenderer;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
        //plankOB.GetComponent<Transform>();
        totalcorner = GameObject.FindGameObjectsWithTag("TagCorner").Length;
        PlayerRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        if (transform.position.y < -5)
        {
            SceneManager.LoadScene("LoseScene");
        }
        CountDownController();
    }

    private void PlayerMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnim.SetTrigger("triggJump");
            PlayerRenderer.material.color = PlayerMaterial[1].color;
        }


        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

            StartRun();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);

            StartRun();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);

            StartRun();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);

            StartRun();
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            playerAnim.SetBool("isRun", false);
        }


    }
    private void StartRun()
    {
        playerAnim.SetBool("isRun", true);
        playerAnim.SetFloat("startRun", 0.26f);
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (cornerleft == totalcorner)
        {
            coneOn = true;
        }

        if (other.gameObject.CompareTag("TagCone"))
        {
            if (coneOn == true)
            {
                Debug.Log("Cone is ON");
                plankOB.GetComponent<Transform>().Rotate(0f, 90f, 0f);
                TimerStarted = true;
            }
            else 
            {
                TimerStarted = false;
                Debug.Log("Cone is OFF");
            }
        }

        if (other.gameObject.CompareTag("TagCorner"))
        {
            cornerleft++;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("CubeTouch"))
        {
            Debug.Log("OI TOUCH");
            isHitBox = true;
        }
    }

    private void CountDownController()
    {
        if (fTimerCount < 10 && TimerStarted == true)
        {
            fTimerCount += Time.deltaTime;
            iCount = Mathf.RoundToInt(fTimerCount);
            CountDown.GetComponent<Text>().text = "Timer Countdown: " + iCount.ToString();
            
        }
        else if(fTimerCount >= 10 && TimerStarted == true)
        {
            TimerStarted = false;
            plankOB.GetComponent<Transform>().Rotate(0, 90, 0);
            Debug.Log("Done");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayPlaneA"))
        {
            Debug.Log("PLayPLanA");
            PlayerRenderer.material.color = PlayerMaterial[0].color;
        }
    }
}
