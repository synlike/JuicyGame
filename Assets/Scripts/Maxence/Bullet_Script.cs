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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.playerLife--;
            PlayerPrefs.SetInt("Player Life", GameManager.instance.playerLife);

            GameManager.instance.lifeText.text = "LIFE : " + GameManager.instance.playerLife;

            Destroy(gameObject);
        }

        if(other.CompareTag("Bottom"))
        {
            Destroy(gameObject);
        }
    }
}
