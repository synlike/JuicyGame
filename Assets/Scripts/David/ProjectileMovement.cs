using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private PlayerWeapon playerWeapon;

    private int hitToDestroy = 1;
    private int hitCount = 0;

    private AudioManager audioM;

    private void Start()
    {
        audioM = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        if (gameObject.CompareTag("Special"))
        {
            hitToDestroy = 2;
        }
    }

    void Update()
    {
        transform.position += transform.up * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ennemy"))
        {
            hitCount++;
            if(gameObject.tag != "Special")
            {
                playerWeapon.IncreaseOrb();
            }
            Debug.Log("Hitcount : " + hitCount + " | " + hitToDestroy);
            other.gameObject.GetComponent<Ennemy_Script>().DestroyEnnemy();
            if(hitCount == hitToDestroy)
            {
                DestroySelf();
            }
        }

        if (other.CompareTag("Sky"))
        {
            audioM.Play("DontTouchEnemies");
            playerWeapon.ResetShootCount();
            playerWeapon.ResetOrb();
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        playerWeapon.canShoot = true;
        Destroy(gameObject);
    }

    public void AssignPlayerWeapon(PlayerWeapon pw)
    {
        playerWeapon = pw;
    }
}
