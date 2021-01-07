using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class IAMovement_Script : MonoBehaviour
{

    [SerializeField] private List<GameObject> LineIA;
    public List<GameObject> FirstLineEnnemy;
    public List<GameObject> IaEnnemy;

    public GameObject prefabShoot;

    private Vector3 pos;

    public float speedEnnemy;

    public static IAMovement_Script instance;

    public bool canShoot;
    private bool waitMove;
    private bool LeftRight;

    public float timeNextShoot;

    [HideInInspector]
    public bool activateExplosion;
    [HideInInspector]
    public bool activateAttractor;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        speedEnnemy = 1f;
        LeftRight = true;

        AddListGameObject();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.playerLife > 0)
        {
            CheckPosEnnemy();
            ShootEnnemy();

            if (waitMove == false)
            {
                if (LeftRight) transform.position += transform.right * Time.deltaTime * speedEnnemy;
                else transform.position -= transform.right * Time.deltaTime * speedEnnemy;
            }
        }
    }

    void AddListGameObject()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            LineIA.Add(transform.GetChild(i).gameObject);
            for (int j = 0; j < LineIA[i].transform.childCount; j++)
            {
                IaEnnemy.Add(LineIA[i].transform.GetChild(j).gameObject);
            }
        }

        foreach(GameObject line in LineIA)
        {
            if(line.gameObject.tag != "Light")
            {
                FirstLineEnnemy.Add(line.transform.GetChild(line.transform.childCount - 1).gameObject);
            }
        }
    }

    public void ShootEnnemy()
    {
        if (timeNextShoot <= 0.0f) 
        {
            int value = Random.Range(0, FirstLineEnnemy.Count);
            if(FirstLineEnnemy[value] != null)
            {
                FirstLineEnnemy[value].GetComponent<Ennemy_Script>().Shoot();
            }
        }
        else timeNextShoot -= Time.deltaTime;
    }

    void CheckPosEnnemy()
    {
        foreach (GameObject ennemy in IaEnnemy)
        {
            pos = Camera.main.WorldToViewportPoint(ennemy.transform.position);

            if (pos.x < 0.05f) //Côté gauche de la caméra atteint
            {
                GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Alarm");

                LeftRight = true;
                
                transform.DOMoveY(transform.position.y + (-0.5f), 0.5f).SetId("GoDown").OnComplete(() => {
                     DOTween.Kill("GoDown");
                });
            }

            if (0.95f < pos.x) //Côté droit de la caméra atteint
            {
                GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Alarm");

                LeftRight = false;
                
                transform.DOMoveY(transform.position.y + (-0.5f), 0.5f).SetId("GoDown").OnComplete(() => {
                     DOTween.Kill("GoDown");
                });
            }

            if (pos.y < 0.2f) //Bas de la caméra atteint
            {
                StartCoroutine(GameManager.instance.GameOver());
            }
        }
    }
}
