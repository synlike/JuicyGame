using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScreenShake : MonoBehaviour
{

    public void ShakeCamera(float explosionForce)
    {
        Vector3 shakeVector = new Vector3(explosionForce, explosionForce, 0);
        Camera.main.transform.DOShakePosition(1f, shakeVector, 5, 10f, false, true);
    }

}
