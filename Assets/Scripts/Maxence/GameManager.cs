﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public int playerLife;
    [SerializeField] private Text winText;
    [SerializeField] private Text gameOverText;
    public Text lifeText;

    public static GameManager instance;

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

        playerLife = PlayerPrefs.GetInt("Player Life", 3);

        lifeText.text = "LIFE : " + playerLife;
    }

    // Start is called before the first frame update
    void Start()
    {

        winText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (playerLife <= 0) StartCoroutine(GameOver());

        if (IAMovement_Script.instance.IaEnnemy.Count <= 0) StartCoroutine(Win());
    }

    IEnumerator Win()
    {
        winText.gameObject.SetActive(true);
        DOTween.KillAll();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        PlayerPrefs.DeleteKey("Player Life");
        DOTween.KillAll();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}