using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    GameObject projectileSpecial;

    [SerializeField]
    private VisualEffect orb;

    [SerializeField]
    Transform shootOrigin;

    [SerializeField]
    private float speed = 5f;

    public int shootsBeforeSpecial = 5;
    [HideInInspector]
    public bool canShoot = true, soundreleased = false;

    [HideInInspector]
    public int shootCount = 0;

    private Vector3 endPosition;

    public bool activateRecoil = false;

    private AudioManager audioM;

    private audioClipRandom audioRandom;

    private AudioSource audioS;

    [HideInInspector]
    public bool activateElectricity;

    public MeshRenderer playerMeshRenderer;

    void Start()
    {
        audioM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioS = GameObject.Find("AudioSourceAllyShots").GetComponent<AudioSource>();
        audioRandom = GameObject.Find("AudioManager").GetComponent<audioClipRandom>();

        orb.SetFloat("Size", 0);
        
    }

    void Update()
    {
        if(shootCount == shootsBeforeSpecial && !soundreleased)
        {
            audioM.Play("BeforeMegaShoot");
            soundreleased = true;
        }

        if(Input.GetKey(KeyCode.Space))
        {
            if(canShoot)
            {
                if(activateRecoil)
                {
                    StartCoroutine(HandleKnockback(0.1f));
                }

                if (shootCount < shootsBeforeSpecial)
                {
                    if(shootCount == 0)
                    {
                        playerMeshRenderer.enabled = false;
                    }
                    //Tir normal
                    audioS.clip = audioRandom.listAllyShots[Random.Range(0, audioRandom.listAllyShots.Length)];
                    audioS.Play();

                    GameObject projectileInstance = Instantiate(projectile, shootOrigin.position, Quaternion.identity) as GameObject;
                    projectileInstance.GetComponent<ProjectileMovement>().AssignPlayerWeapon(this);

                    shootCount++;
                }
                else
                {
                    // Tir Spécial
                    playerMeshRenderer.enabled = false;

                    GameObject projectileInstance = Instantiate(projectileSpecial, shootOrigin.position, Quaternion.identity);
                    projectileInstance.GetComponent<ProjectileMovement>().AssignPlayerWeapon(this);
                    audioM.Play("MegaShoot");
                    soundreleased = false;
                    shootCount = 0;

                    ResetOrb();
                }
                canShoot = false;
            }
        }
    }

    public void ResetShootCount()
    {
        shootCount = 0;
        playerMeshRenderer.enabled = false;
    }

    public void IncreaseOrb()
    {
        orb.SetFloat("Size", orb.GetFloat("Size") + 0.1f);
    }

    public void ResetOrb()
    {
        orb.SetFloat("Size", 0);
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
