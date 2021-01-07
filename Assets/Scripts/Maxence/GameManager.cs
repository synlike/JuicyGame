using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public int playerLife;

    public static GameManager instance;

    private AudioManager audioM;

    private bool endJingle;

    public GameObject health;
    private Image[] healthImages;

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
        healthImages = health.GetComponentsInChildren<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (playerLife <= 0) StartCoroutine(GameOver());

        if (IAMovement_Script.instance.IaEnnemy.Count <= 0) StartCoroutine(Win());
    }

    public void RemoveLife()
    {
        playerLife--;
        healthImages[playerLife].enabled = false;
    }

    IEnumerator Win()
    {
        if(!endJingle)
        {
            audioM.Play("Win");
            endJingle = true;
        }
        
        DOTween.KillAll();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator GameOver()
    {
        if (!endJingle)
        {
            audioM.Play("Lose");
            endJingle = true;
        }
        
        PlayerPrefs.DeleteKey("Player Life");
        DOTween.KillAll();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
