using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectActivator : MonoBehaviour
{
    public ScreenShake screenShake;
    public ParticleSystem playerShipTrail;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            screenShake.activateScreenshake = !screenShake.activateScreenshake;
            Debug.Log("Screenshake = " + screenShake.activateScreenshake);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            if (playerShipTrail.isPlaying)
                playerShipTrail.Play();
            else
                playerShipTrail.Stop();
        }

    }
}
