using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    public Text finalScore;
    public Text bestScore;

    void Awake()
    {
        finalScore.text = PlayerPrefs.GetInt("LastScore").ToString();
        bestScore.text = PlayerPrefs.GetInt("Score").ToString() ;
    }

    

    public void BackToTittleScreen()
    {
        SceneManager.LoadScene("Title Screen");
    }
}
