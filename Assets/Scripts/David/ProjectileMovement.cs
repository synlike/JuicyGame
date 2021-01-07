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
        if(gameObject.CompareTag("Special"))
        {
            hitToDestroy = 2;
        }

        audioM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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
