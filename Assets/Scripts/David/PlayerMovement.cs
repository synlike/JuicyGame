using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 4;

    float tilAngle = 30f;
    float rotationSpeed = 5f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        EulerRotate();
    }

    void FixedUpdate()
    {
        float input = Input.GetAxis("Horizontal");

        if (input != 0)
        {
            transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);
        }
    }

    void EulerRotate()
    {
        var input = Input.GetAxis("Horizontal");
        var currAngles = transform.eulerAngles;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(currAngles.x, tilAngle * -input, currAngles.z), Time.deltaTime * rotationSpeed);
    }
}
