using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health;
    public GameObject deathAnim;

    private void Start()
    {
    }

    public void TakeDamage(int h)
    {
        health -= h;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathAnim, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }
}