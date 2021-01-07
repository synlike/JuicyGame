using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTilt : MonoBehaviour
{
    [SerializeField]
    private float tilAngle = 30f;
    [SerializeField]
    private float rotationSpeed = 5f;

    void Update()
    {
        EulerRotate();
    }

    void EulerRotate()
    {
        var input = Input.GetAxis("Horizontal");
        var currAngles = transform.eulerAngles;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(currAngles.x, tilAngle * -input + 180, currAngles.z), Time.deltaTime * rotationSpeed);
    }
}
