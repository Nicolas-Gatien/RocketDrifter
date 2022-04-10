using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreIndicator : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = score.ToString();    
    }
}
