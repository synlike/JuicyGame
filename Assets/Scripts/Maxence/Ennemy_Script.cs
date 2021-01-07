using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy_Script : MonoBehaviour
{
    private ScreenShake screenShake;

    private AudioManager audioM;

    private audioClipRandom audioRandom;

    private AudioSource audioS;

    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private GameObject particleAttractor;

    void Start()
    {
        screenShake = GameObject.Find("ScreenShake").GetComponent<ScreenShake>();

        audioM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioS = GameObject.Find("AudioSourceEnemyDead").GetComponent<AudioSource>();
        audioRandom = GameObject.Find("AudioManager").GetComponent<audioClipRandom>();
    }

    public void Shoot()
    {
        audioM.Play("Enemy_Shot");

        if (transform.GetSiblingIndex() + 1 == transform.parent.childCount)
        {
            Instantiate(IAMovement_Script.instance.prefabShoot, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
        }

        IAMovement_Script.instance.timeNextShoot = 1.0f;
    }

    public void DestroyEnnemy()
    {
        audioM.Play("EnemyBoom");

        audioS.clip = audioRandom.listSoundsEnemyDead[Random.Range(0, audioRandom.listSoundsEnemyDead.Length)];
        audioS.Play();

        IAMovement_Script.instance.FirstLineEnnemy.Remove(gameObject);
        IAMovement_Script.instance.IaEnnemy.Remove(gameObject);

        Instantiate(explosion, transform.position, Quaternion.identity);
        Instantiate(particleAttractor, new Vector3(transform.position.x - 0.8f, transform.position.y + 2f, transform.position.z), Quaternion.identity);

        Destroy(gameObject);

        if (transform.parent.childCount > 1) IAMovement_Script.instance.FirstLineEnnemy.Add(transform.parent.GetChild(transform.parent.childCount - 2).gameObject);
        else if (transform.parent.childCount == 1) IAMovement_Script.instance.FirstLineEnnemy.Add(transform.parent.GetChild(transform.parent.childCount - 1).gameObject);
        
        screenShake.ShakeCamera(0.1f);
    }
}
