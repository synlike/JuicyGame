using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField]
    private float time = 2f;

    void Start()
    {
        Destroy(gameObject, time);
    }
    
    void Update()
    {
        
    }
}
