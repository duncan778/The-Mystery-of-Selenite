using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private AudioSource menuSFX;
    public AudioClip clickSound;

    public GameObject startPanel;
    public GameObject gameOverPanel;
    private bool pauseMode = false;
    private bool creditsOn = false;
    public GameObject creditsPanel;

    void Start()
    {
        Time.timeScale = 0;
        startPanel.SetActive(true);
        menuSFX = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuSFX.PlayOneShot(clickSound, 0.7f);
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
        menuSFX.PlayOneShot(clickSound, 0.7f);
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void StartGame()
    {
        menuSFX.PlayOneShot(clickSound, 0.7f);
        Time.timeScale = 1;
        startPanel.SetActive(false);
    }

    public void ExitGame()
    {
        menuSFX.PlayOneShot(clickSound, 0.7f);
        Application.Quit();
    }

    public void Credits()
    {
        menuSFX.PlayOneShot(clickSound, 0.7f);
        if (creditsOn)
        {
            creditsPanel.SetActive(false);
            creditsOn = false;
        }
        else
        {
            creditsPanel.SetActive(true);
            creditsOn = true;
        }
    }
}
