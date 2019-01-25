using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TittleScreen : MonoBehaviour
{


    public void PlayGame()
    {
        SceneManager.LoadScene("Level 0");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
