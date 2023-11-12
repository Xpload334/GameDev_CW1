using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSounds : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip selectSound;
    [SerializeField] private AudioClip blipSound;

    [SerializeField] private float pitchRandomRange = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        // Button[] buttonsInScene = FindObjectsOfType<Button>();
        // foreach (Button button in buttonsInScene)
        // {
        //     button.onClick.AddListener((() =>
        //     {
        //         PlaySelect();
        //         // button.interactable = false;
        //     }));
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySelect()
    {
        _audioSource.pitch = 1f;
        _audioSource.PlayOneShot(selectSound);
    }

    public void PlayBlip()
    {
        _audioSource.pitch = Random.Range((1 - (pitchRandomRange / 2)), (1 + (pitchRandomRange / 2)));
        _audioSource.PlayOneShot(blipSound, 0.5f);
    }
}
