using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathCountText : MonoBehaviour
{
    private SortedDictionary<int, string> subtitleList = new SortedDictionary<int, string>()
    {
        {5,"What a champion!"},
        {10,"Didn't break a sweat!"},
        {15,"Whatever doesn't kill you, makes you stronger!"},
        {20,"A bit rocky, but still made it!"},
        {30,"Too many workplace hazards!"},
        {50,"HR will be informed."},
        {100,"HR will be informed."}
    };
    private int _deathCount;
    private readonly string _countTextDefault = "He has experienced ### injuries.";
    [SerializeField] private TMP_Text countText;

    [SerializeField] private TMP_Text subtitleText;
    // Start is called before the first frame update
    void Start()
    {
        //Deathcount
        _deathCount = PlayerPrefs.GetInt("playerDeaths", 0);
        countText.text = "He has experienced " + _deathCount + " injuries.";
        
        //Subtitle
        subtitleText.text = GetSubtitle(_deathCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Get the best subtitle
    string GetSubtitle(int deaths)
    {
        string s = "2 weeks late, but not to worry...";
        foreach (var (i, subtitle) in subtitleList)
        {
            if (deaths <= i) return subtitle;
        }

        return s;
    }
}
