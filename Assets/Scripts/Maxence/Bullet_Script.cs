using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    // Update is called once per frame
    void Update()
    {
        transform.position -= transform.up * Time.deltaTime * speed;

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.y < 0.0) //Bas de la caméra atteint
        {
            Destroy(gameObject); //Detruit la balle si elle touche le bas de l'écran
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Destroy(other.gameObject);
            Debug.Log("Une vie en moins");
            Destroy(gameObject);
        }
    }
}
