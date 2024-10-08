using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private PlayerSounds _playerSounds;
    private LevelSceneManager _levelSceneManager;

    private bool isDead;
    public int health;
    public GameObject deathAnim;

    private void Start()
    {
        _levelSceneManager = FindObjectOfType<LevelSceneManager>();
        _playerSounds = FindObjectOfType<PlayerSounds>();
    }

    public void TakeDamage(int h)
    {
        health -= h;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Instantiate(deathAnim, transform.position, Quaternion.identity);
        if (_playerSounds != null)
        {
            _playerSounds.PlayDeathSound();
        }
        
        //Update player prefs
        int deathCount = PlayerPrefs.GetInt("playerDeaths", 0);
        PlayerPrefs.SetInt("playerDeaths", deathCount+1);
        Debug.Log("Died "+(deathCount+1)+" times");
        
        
        gameObject.SetActive(false);
        
        //Reload current scene if player dies
        _levelSceneManager.PlayerDeath();
    }

    // Update is called once per frame
    void Update()
    {
    }
}