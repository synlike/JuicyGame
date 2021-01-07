using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    private AudioManager audioM;

    public void Start()
    {
        audioM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

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

            if(GameObject.Find("GameManager").GetComponent<GameManager>().playerLife != 0)
            {
                audioM.Play("Ally_Hit");
            }
            else
            {
                audioM.Play("Ally_Boom");
            }

            Destroy(gameObject);
        }

        if(other.CompareTag("Bottom"))
        {
            Destroy(gameObject);
        }
    }
}
