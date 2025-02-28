using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    public TMP_Text score_text;
    public int score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void IncreamentScore(){
        score++;
        UpdateText();
    }

    void UpdateText() {
        score_text.text = "Score: " + score.ToString();
    }
}
