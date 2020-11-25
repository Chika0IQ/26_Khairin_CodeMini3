using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlane : MonoBehaviour
{
    float speed = 5.0f;
    float zlimit;
    bool OnLimit;
    float currentPost;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentPost = transform.position.z;
        zlimit = 36.50f;

        if (currentPost < zlimit && OnLimit)
        {
            MoveForward();
        }
        else if (currentPost > 27.45f && !OnLimit)
        {
            MoveBackward();
        }
        else
        {
            OnLimit = !OnLimit;
        }
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
    void MoveBackward()
    {
        transform.Translate(Vector3.back * Time.deltaTime * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player.transform.parent = null;
    }
}
