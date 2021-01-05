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

    private bool canShoot = true;
    private float shootTimer = 0f;
    

    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            if(canShoot)
            {
                GameObject projectileInstance = Instantiate(projectile, shootOrigin.position, Quaternion.identity);
                canShoot = false;
            }
        }

        if(!canShoot)
        {
            shootTimer += Time.deltaTime;
            Debug.Log(shootTimer);
            if (shootTimer >= (1 / shootRate))
            {
                canShoot = true;
                shootTimer = 0f;
            }
        }
    }
}
