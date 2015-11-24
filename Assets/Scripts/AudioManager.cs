using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class AudioManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    private float musicVolume;
    public float MusicVolume
    {
        get { return MusicVolume; }
        set
        {
            musicVolume = value;
            if (music1Source != null && !crossFading)
            {
                music1Source.volume = musicVolume;
                music2Source.volume = musicVolume;
            }
        }
    }

    public float soundVolume
    {
        get { return AudioListener.volume; }
        set { AudioListener.volume = value; }
    }

    public bool soundMute
    {
        get { return AudioListener.pause; }
        set { AudioListener.pause = value; }
    }

    public bool musicMute
    {
        get
        {
            if (music1Source != null)
            {
                return music1Source.mute;
            }
            return false;
        }
        set
        {
            if (music1Source != null)
            {
                music1Source.mute = value;
                music2Source.mute = value;
            }
        }
    }

    [SerializeField]
    private AudioSource music1Source;
    [SerializeField]
    private AudioSource music2Source;
    [SerializeField]
    private string introBGMusic;
    [SerializeField]
    private string levelBGMusic;
    [SerializeField]
    private AudioSource soundSource;

    private AudioSource activeMusic;
    private AudioSource inactiveMusic;

    public float crossFadeRate = 1.5f;
    private bool crossFading;


    public void Startup()
    {
        Debug.Log("Audio manager starting...");

        music1Source.ignoreListenerVolume = true;
        music1Source.ignoreListenerPause = true;
        music2Source.ignoreListenerVolume = true;
        music2Source.ignoreListenerPause = true;

        soundVolume = 1f;
        musicVolume = 1f;

        activeMusic = music1Source;
        inactiveMusic = music2Source;

        status = ManagerStatus.Started;
    }

    public void PlayIntroMusic()
    {
        PlayMusic(Resources.Load("Music/" + introBGMusic) as AudioClip);
    }

    public void PlayLevelMusic()
    {
        PlayMusic(Resources.Load("Music/" + levelBGMusic) as AudioClip);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (crossFading)
        {
            return;
        }

        StartCoroutine(CrossFadeMusic(clip));
    }

    public void StopMusic()
    {
        activeMusic.Stop();
        inactiveMusic.Stop();
    }

    public void PlaySound(AudioClip clip)
    {
        soundSource.PlayOneShot(clip);
    }

    private IEnumerator CrossFadeMusic(AudioClip clip)
    {
        crossFading = true;

        inactiveMusic.clip = clip;
        inactiveMusic.volume = 0;
        inactiveMusic.Play();

        float scaledRate = crossFadeRate * musicVolume;
        while (activeMusic.volume > 0)
        {
            activeMusic.volume -= scaledRate * Time.deltaTime;
            inactiveMusic.volume += scaledRate * Time.deltaTime;

            yield return null;
        }

        AudioSource temp = activeMusic;

        activeMusic = inactiveMusic;
        activeMusic.volume = musicVolume;

        inactiveMusic = temp;
        inactiveMusic.Stop();

        crossFading = false;
    }


}