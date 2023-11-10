using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathCounterLevel : MonoBehaviour
{
    private int _deathCount;
    [SerializeField] private TMP_Text countText;
    // Start is called before the first frame update
    void Start()
    {
        //Deathcount
        _deathCount = PlayerPrefs.GetInt("playerDeaths", 0);
        countText.text = "Injuries = " + _deathCount;
    }
    
}
