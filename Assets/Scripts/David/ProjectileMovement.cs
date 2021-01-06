﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private PlayerWeapon playerWeapon;

    private int hitToDestroy = 1;
    private int hitCount = 0;

    private void Start()
    {
        if(gameObject.CompareTag("Special"))
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
        Debug.Log(other.gameObject.tag);
        hitCount++;

        if(other.CompareTag("Ennemy"))
        {
            other.gameObject.GetComponent<Ennemy_Script>().DestroyEnnemy();
            if(hitCount == hitToDestroy)
            {
                DestroySelf();
            }
        }

        if (other.CompareTag("Sky"))
        {
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