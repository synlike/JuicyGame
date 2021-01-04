using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class IAMovement_Script : MonoBehaviour
{

    [SerializeField] private List<GameObject> LineIA;
    [SerializeField] private List<GameObject> IaEnnemy;

    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveX(5, 3f).SetId("goMove");
        AddListGameObject();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject ennemy in IaEnnemy)
        {
            pos = Camera.main.WorldToViewportPoint(ennemy.transform.position);

            if (pos.x < 0.05f) //Côté gauche de la caméra atteint
            {
                transform.DOMoveX(5, 3f).SetId("goMove");
                transform.DOMoveY(transform.position.y + (-1), 0.5f).SetId("goDown").OnPlay(() => DOTween.Pause("goMove")).OnComplete(() => { DOTween.Play("goMove"); DOTween.Kill("goDown"); });
            }

            if (0.95f < pos.x) //Côté droit de la caméra atteint
            {
                transform.DOMoveX(-5, 3f).SetId("goMove");
                transform.DOMoveY(transform.position.y + (-1), 0.5f).SetId("goDown").OnPlay(() => DOTween.Pause("goMove")).OnComplete(() => { DOTween.Play("goMove"); DOTween.Kill("goDown"); });
            }

            if (pos.y < 0.05f) //Bas de la caméra atteint
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
    }
}
