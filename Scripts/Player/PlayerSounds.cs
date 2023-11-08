using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource footstepsSource;
    [SerializeField] private AudioSource objectSource;

    
    [SerializeField] private AudioClip flipSound;
    [SerializeField] private AudioClip landSound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip footstepSound;

    private bool isGrounded;
    private bool isLastGrounded = true;

    private bool makeFootsteps;
    private bool isFootstepsLoopActive;

    [SerializeField] private float footstepsDelay = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        _audioSource = GetComponent<AudioSource>();
        
        // playerController.flipGravityAction.started += _ =>
        // {
        //     PlayFlipSound();
        // };

        StartCoroutine(FootstepsLoop());

    }
    // Update is called once per frame

    void FixedUpdate()
    {
        isGrounded = playerController.IsGrounded();

        //Make footsteps
        if (isGrounded && playerController.horizontalInput != 0)
        {
            makeFootsteps = true;
            if (!isFootstepsLoopActive) StartCoroutine(FootstepsLoop());
        }
        else
        {
            makeFootsteps = false;
        }
        
        
        //Landing
        if (!isLastGrounded && isGrounded)
        {
            isLastGrounded = true;
            PlayLandSound();
        }

        //Last check
        isLastGrounded = isGrounded;
    }

    public void PlayFlipSound()
    {
        _audioSource.PlayOneShot(flipSound);
    }

    public void PlayDeathSound()
    {
        _audioSource.PlayOneShot(deathSound);
    }

    public void PlayWinSound()
    {
        _audioSource.PlayOneShot(winSound);
    }

    public void PlayLandSound()
    {
        _audioSource.PlayOneShot(landSound);
    }

    
    //Objects
    public void PlayCollectSound()
    {
        objectSource.PlayOneShot(collectSound);
    }

    IEnumerator FootstepsLoop()
    {
        isFootstepsLoopActive = true;
        while (makeFootsteps)
        {
            footstepsSource.PlayOneShot(footstepSound);
            yield return new WaitForSeconds(footstepsDelay);
        }
        isFootstepsLoopActive = false;
    }
}
