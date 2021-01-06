using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    GameObject projectileSpecial;

    [SerializeField]
    Transform shootOrigin;

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private int shootsBeforeSpecial = 5;
    [HideInInspector]
    public bool canShoot = true;

    private int shootCount = 0;


    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            if(canShoot)
            {
                HandleKnockback();

                if (shootCount <= shootsBeforeSpecial)
                {
                    GameObject projectileInstance = Instantiate(projectile, shootOrigin.position, Quaternion.identity);
                    projectileInstance.GetComponent<ProjectileMovement>().AssignPlayerWeapon(this);
                    shootCount++;
                }
                else
                {
                    // Tir Spécial
                    GameObject projectileInstance = Instantiate(projectileSpecial, shootOrigin.position, Quaternion.identity);
                    projectileInstance.GetComponent<ProjectileMovement>().AssignPlayerWeapon(this);
                    
                    shootCount = 0;
                }
                canShoot = false;
            }
        }
    }

    private void HandleKnockback()
    {
        Debug.Log("KnockBack");
        GetComponent<Rigidbody>().AddForce(-transform.up * -1, ForceMode.Impulse);
    }

    public void ResetShootCount()
    {
        shootCount = 0;
    }
}
