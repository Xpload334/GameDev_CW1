using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private PlayerController _playerController;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip flipSound;

    [SerializeField] private AudioClip flipReverseSound;

    [SerializeField] private AudioClip landSound;

    [SerializeField] private AudioClip winSound;

    [SerializeField] private AudioClip deathSound;
    // Start is called before the first frame update
    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlipSound()
    {
        if (_playerController.desiredGravity.y > 0)
        {
            _audioSource.PlayOneShot(flipSound);
        }
        else
        {
            _audioSource.PlayOneShot(flipReverseSound);
        }
    }
}
