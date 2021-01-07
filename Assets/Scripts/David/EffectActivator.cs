using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EffectActivator : MonoBehaviour
{
    public ScreenShake screenShake;
    public ParticleSystem playerShipTrail;
    public PlayerWeapon playerWeaponScript;
    public ShipTilt shipTilt;
    public VisualEffect orb;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Debug.Log("hello");
            screenShake.activateScreenshake = !screenShake.activateScreenshake;
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
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

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            playerWeaponScript.activateRecoil = !playerWeaponScript.activateRecoil;
            Debug.Log("Ship Recoil = " + playerWeaponScript.activateRecoil);
        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            shipTilt.tiltActivated = !shipTilt.tiltActivated;
            Debug.Log("Ship Tilt = " + shipTilt.tiltActivated);
        }

        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            if(orb.isActiveAndEnabled)
            {
                orb.enabled = false;
                Debug.Log("Orb Activated");
            }
            else
            {
                orb.enabled = true;
                Debug.Log("Orb Deactivated");
            }
        }
    }
}
