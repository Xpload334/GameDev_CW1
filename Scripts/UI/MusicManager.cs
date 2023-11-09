using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MusicType
{
    START,
    LEVEL,
    END
}

public class MusicManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip levelSelectTrack;
    [SerializeField] private AudioClip levelTrack;
    [SerializeField] private AudioClip endingTrack;

    [SerializeField] private AudioClip currentlyPlaying;
    [SerializeField] private string currentScene;
    
    private static MusicManager _instance;
    private void Awake()
    {
        // MusicManager[] musicObjs = FindObjectsOfType<MusicManager>();
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            if(_audioSource == null) _audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
        // if (musicObjs.Length > 1)
        // {
        //     Destroy(gameObject);
        // }
        // else
        // {
        //     DontDestroyOnLoad(transform.gameObject);
        //     // currentlyPlaying = _audioSource.clip;
        // }
    }
    
    public void PlayLevelSelect()
    {
        if (currentlyPlaying.Equals(levelSelectTrack)) return;
        
        _audioSource.Pause();
        // _audioSource.Stop();
        Debug.Log("Switching music to Level Select Track");
        _audioSource.clip = levelSelectTrack;
        currentlyPlaying = levelSelectTrack;
        _audioSource.Play();
    }

    public void PlayEndScreen()
    {
        if (currentlyPlaying.Equals(endingTrack)) return;
        
        _audioSource.Pause();
        // _audioSource.Stop();
        Debug.Log("Switching music to Ending Track");
        _audioSource.clip = endingTrack;
        currentlyPlaying = endingTrack;
        _audioSource.Play();
    }

    public void PlayLevelNormal()
    {
        if (currentlyPlaying.Equals(levelTrack)) return;

        _audioSource.Pause(); 
        // _audioSource.Stop();
        Debug.Log("Switching music to Main Level Track");
        _audioSource.clip = levelTrack;
        currentlyPlaying = levelTrack;
        _audioSource.Play();
    }
}
