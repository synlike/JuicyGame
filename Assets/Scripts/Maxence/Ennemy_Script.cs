using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy_Script : MonoBehaviour
{
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
        IAMovement_Script.instance.FirstLineEnnemy.Add(transform.parent.GetChild(transform.parent.childCount - 2).gameObject);

        IAMovement_Script.instance.FirstLineEnnemy.Remove(gameObject);
        IAMovement_Script.instance.IaEnnemy.Remove(gameObject);

        Destroy(gameObject);
    }
}
