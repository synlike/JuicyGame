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
    public bool canShoot = true, soundreleased = false;

    private int shootCount = 0;

    private Vector3 endPosition;

    private AudioSource audioS;

    private audioClipRandom audioRandom;

    private AudioManager audioM;

    void Start()
    {
        audioS = GameObject.Find("AudioSourceAllyShots").GetComponent<AudioSource>();

        audioRandom = GameObject.Find("AudioManager").GetComponent<audioClipRandom>();

        audioM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        if(shootCount == shootsBeforeSpecial && !soundreleased)
        {
            Debug.Log("coucou chef");
            audioM.Play("BeforeMegaShoot");
            soundreleased = true;
        }

        if(Input.GetKey(KeyCode.Space))
        {
            if(canShoot)
            {
                StartCoroutine(HandleKnockback(0.1f));

                if (shootCount <= shootsBeforeSpecial)
                {
                    //tir normal
                    audioS.clip = audioRandom.listAllyShots[Random.Range(0, audioRandom.listAllyShots.Length)];
                    audioS.Play();

                    GameObject projectileInstance = Instantiate(projectile, shootOrigin.position, Quaternion.identity) as GameObject;
                    projectileInstance.GetComponent<ProjectileMovement>().AssignPlayerWeapon(this);
                    shootCount++;
                }
                else
                {
                    // Tir Spécial
                    audioM.Play("MegaShoot");

                    GameObject projectileInstance = Instantiate(projectileSpecial, shootOrigin.position, Quaternion.identity);
                    projectileInstance.GetComponent<ProjectileMovement>().AssignPlayerWeapon(this);
                    soundreleased = false;
                    shootCount = 0;
                }
                canShoot = false;
            }
        }
    }

    public void ResetShootCount()
    {
        shootCount = 0;
    }

    private IEnumerator HandleKnockback(float duration)
    {
        float time = 0;
        Vector3 startPos = transform.position;
        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
        StartCoroutine(ReverseKnockback(duration));
    }

    private IEnumerator ReverseKnockback(float duration)
    {
        float time = 0;
        Vector3 startPos = transform.position;
        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
    }
}
