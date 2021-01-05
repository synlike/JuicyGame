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
        Vector2 movement = Vector2.zero;

        if(Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
        {
            movement.x = (transform.right * Time.deltaTime * -speed).x;
        }
        
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            movement.x = (transform.right * Time.deltaTime * speed).x;
        }

        movement = movement + (Vector2)transform.position;

        rb.MovePosition(movement);

    }
}
