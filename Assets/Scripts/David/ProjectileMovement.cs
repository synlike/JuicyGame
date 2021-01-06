using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);

        if(other.CompareTag("Ennemy"))
        {
            Debug.Log("Walah");
            other.gameObject.GetComponent<Ennemy_Script>().DestroyEnnemy();
        }

        if (other.CompareTag("Sky"))
        {
            Debug.Log("Wesh");
        }
        Destroy(gameObject);
    }
}
