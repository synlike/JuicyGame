using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    public float soundDecrease = 0.1f;
    public float SceneNumber = 0;
    public float MusicLevel = 0;
    public string musicPlayingName;
    public float musicVolume;

    private void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }


        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            
        }
    }
    private void Start()
    {
        Play("Theme");
    }

    private void Update()
    {
        SceneNumber = SceneManager.GetActiveScene().buildIndex;
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        s.source.Play();

        if(s.CouldCrossfade)
            musicPlayingName = name;
    }

    public void PlayRandomlyEnemyDead(string name)
    {

    }

    public IEnumerator FadeOut(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        while (s.source.volume > 0)
        {
            s.source.volume -= soundDecrease;
            yield return new WaitForSeconds(0.1f);
        }
    }


    public IEnumerator Crossfade(string newMusic)
    {
        Sound sOld = Array.Find(sounds, sound => sound.name == musicPlayingName);
        Sound sNew = Array.Find(sounds, sound => sound.name == newMusic);

        if(sNew != null)
        { 

            sNew.source.volume = 0;
            sNew.source.Play();
            sNew.source.time = sOld.source.time;

            while (sOld.source.volume > 0 || sNew.source.volume < musicVolume)
            {
                sNew.source.volume += soundDecrease;
                sOld.source.volume -= soundDecrease;
                yield return new WaitForSeconds(0.1f);
            }

            sOld.source.volume = 0;
            sNew.source.volume = musicVolume;

            if (sNew.CouldCrossfade)
                musicPlayingName = newMusic;

            sOld.source.Stop();
            sOld.source.volume = musicVolume;

        }
        else
        {
            Debug.Log("No music having this name" + newMusic);
        }

    }



}
