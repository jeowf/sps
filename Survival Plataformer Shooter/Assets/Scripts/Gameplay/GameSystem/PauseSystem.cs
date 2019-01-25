using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSystem : MonoBehaviour
{

    public GameObject pausePanel;

    private bool paused = false;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                paused = false;
                Time.timeScale = 1;
                pausePanel.SetActive(false);

            }
            else
            {
                paused = true;
                Time.timeScale = 0;
                pausePanel.SetActive(true);

            }


        }
    }

    public void BackToTittleScreen()
    {
        SceneManager.LoadScene("Title Screen");
    }
}
