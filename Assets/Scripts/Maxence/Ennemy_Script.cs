using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy_Script : MonoBehaviour
{
    private ScreenShake screenShake;

    public AudioManager audioM;

    private audioClipRandom audioRandom;

    private AudioSource audioS;

    void Start()
    {
        screenShake = GameObject.Find("ScreenShake").GetComponent<ScreenShake>();

        audioM = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        audioS = GameObject.Find("AudioSourceEnemyDead").GetComponent<AudioSource>();

        audioRandom = GameObject.Find("AudioManager").GetComponent<audioClipRandom>();
    }

    public void Shoot()
    {
        if (transform.GetSiblingIndex() + 1 == transform.parent.childCount)
        {
            Instantiate(IAMovement_Script.instance.prefabShoot, gameObject.transform);
        }

        IAMovement_Script.instance.timeNextShoot = 3.0f;
    }

    public void DestroyEnnemy()
    {
        audioM.Play("EnemyBoom");

        audioS.clip = audioRandom.listSoundsEnemyDead[Random.Range(0, audioRandom.listSoundsEnemyDead.Length)];
        audioS.Play();

        IAMovement_Script.instance.FirstLineEnnemy.Remove(gameObject);
        IAMovement_Script.instance.IaEnnemy.Remove(gameObject);

        Destroy(gameObject);

        if (transform.parent.childCount > 1) IAMovement_Script.instance.FirstLineEnnemy.Add(transform.parent.GetChild(transform.parent.childCount - 2).gameObject);
        else if (transform.parent.childCount == 1) IAMovement_Script.instance.FirstLineEnnemy.Add(transform.parent.GetChild(transform.parent.childCount - 1).gameObject);

        screenShake.ShakeCamera(0.1f);
    }
}
