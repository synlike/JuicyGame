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
    
    private ScreenShake screenShake;

    private void Start()
    {
        audioM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        screenShake = GameObject.Find("ScreenShake").GetComponent<ScreenShake>();

        if (gameObject.CompareTag("Special"))
        {
            hitToDestroy = 4;
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
                screenShake.ShakeCamera(0.1f);
            }
            else
            {
                screenShake.ShakeCamera(0.4f);
            }
            Debug.Log("Hitcount : " + hitCount + " | " + hitToDestroy);
            other.gameObject.GetComponent<Ennemy_Script>().DestroyEnnemy();
            

            if (playerWeapon.shootCount == playerWeapon.shootsBeforeSpecial)
            {
                if (playerWeapon.activateElectricity)
                {
                    playerWeapon.playerMeshRenderer.enabled = true;
                }
            }

            if (hitCount == hitToDestroy)
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
