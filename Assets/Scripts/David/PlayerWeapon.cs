using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;

    [SerializeField]
    Transform shootOrigin;

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float shootRate = 2; // 2 per seconds

    private int shootCount = 0;
    [SerializeField]
    private int shootBeforeSpecial = 5;
    private bool canShoot = true;
    private float shootTimer = 0f;
    

    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            if(canShoot)
            {
                if(shootCount <= 5)
                {
                    GameObject projectileInstance = Instantiate(projectile, shootOrigin.position, Quaternion.identity);
                    shootCount++;
                }
                else
                {
                    // Tir Spécial
                    Debug.Log("Tir Spécial");
                    shootCount = 0;
                }
                canShoot = false;
            }
        }

        if(!canShoot)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= (1 / shootRate))
            {
                canShoot = true;
                shootTimer = 0f;
            }
        }
    }
}
