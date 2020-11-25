using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float moveSpeed = 5.0f;
    private int totalcorner;
    private int cornerleft;
    bool coneOn = false;


    public Animator playerAnim;
    public GameObject plank;
    // Start is called before the first frame update
    void Start()
    {
        totalcorner = GameObject.FindGameObjectsWithTag("TagCorner").Length;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        if (cornerleft == totalcorner)
        {
            coneOn = true;
        }
    }
    void PlayerMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnim.SetTrigger("triggJump");
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
    void StartRun()
    {
        playerAnim.SetBool("isRun", true);
        playerAnim.SetFloat("startRun", 0.26f);
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TagCone"))
        {
            if (coneOn == true)
            {
                Debug.Log("Cone is ON");
                plank.transform.Rotate(0f,90f ,0f);
            }
            else
            {
                Debug.Log("Cone is OFF");
            }
        }

        if (other.gameObject.CompareTag("TagCorner"))
        {
            cornerleft++;
            Destroy(other.gameObject);
        }
    }
}
