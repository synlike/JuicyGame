using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectActivator : MonoBehaviour
{
    public ScreenShake screenShake;
    public ParticleSystem playerShipTrail;
    public PlayerWeapon playerWeaponScript;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("hello");
            screenShake.activateScreenshake = !screenShake.activateScreenshake;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            if (playerShipTrail.isPlaying)
            {
                playerShipTrail.Stop();
                Debug.Log("Player Trail deactivated");
            }
            else
            {
                playerShipTrail.Play();
                Debug.Log("Player Trail activated");
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            playerWeaponScript.activateRecoil = !playerWeaponScript.activateRecoil;
            Debug.Log("Ship Recoil = " + playerWeaponScript.activateRecoil);
        }
    }
}
