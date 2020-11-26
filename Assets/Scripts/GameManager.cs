using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject gameOverPanel;
    private bool pauseMode = false;

    void Start()
    {
        Time.timeScale = 0;
        startPanel.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMode)
            {
                Time.timeScale = 0;
                gameOverPanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = "*** Pause ***";
                gameOverPanel.SetActive(true);
                pauseMode = true;
            }
            else
            {
                Time.timeScale = 1;
                gameOverPanel.SetActive(false);
                pauseMode = false;
            }
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        startPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
       //
    }
}
