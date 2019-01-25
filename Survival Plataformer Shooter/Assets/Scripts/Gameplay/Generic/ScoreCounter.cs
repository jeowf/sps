using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{

    public Text scoreText;

    private float _score = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _score += Time.deltaTime;
        scoreText.text = "Score: " + ((int)_score).ToString();
        PlayerPrefs.SetInt("LastScore", (int)_score);

        int best_score = PlayerPrefs.GetInt("Score", 0);
        Debug.Log(best_score);
        if ((int)_score > best_score)
            PlayerPrefs.SetInt("Score", (int) _score);
    }
}
