using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 4;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        float input = Input.GetAxis("Horizontal");

        if(input != 0)
        {
            transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);
        }
    }
}
