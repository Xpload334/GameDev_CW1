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
    [SerializeField] private int currentSceneIndex;
    
    private static MusicManager _instance;
    private bool _shouldPlay;
    [SerializeField] private int levelSelectSceneIndex = 0;
    [SerializeField] private int endScreenSceneIndex = 11;
    private void Awake()
    {
        // MusicManager[] musicObjs = FindObjectsOfType<MusicManager>();
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            if(_audioSource == null) _audioSource = GetComponent<AudioSource>();
            _shouldPlay = true;
            
            // _instance.SetMusicTrack();
        }
        else
        {
            _shouldPlay = false;
            Destroy(gameObject);
            
            // _instance.SetMusicTrack();
        }
    }

    // private void Start()
    // {
    //     _instance.SetMusicTrack();
    // }

    //OnEnable comes first
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        // Debug.Log("OnEnable");
    }
    
    //OnDisable last.
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        // Debug.Log("OnDisable");
    }
    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.isLoaded)
        {
            SetMusicTrack();
        }
    }

    public void SetMusicTrack()
    {
        if (!_shouldPlay) return;
        
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == levelSelectSceneIndex)
        {
            PlayLevelSelect();
        }
        else if(currentSceneIndex == endScreenSceneIndex)
        {
            PlayEndScreen();
        }
        else
        {
            PlayLevelNormal();
        }
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
