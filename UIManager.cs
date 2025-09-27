using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject infoUI;
    public TMP_Text scoreText;
    public AudioSource audioSource;

    //Load main game scene
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    //Show info panel
    public void InfoDisplay()
    {
        infoUI.SetActive(true);
    }

    //Hide info panel
    public void Back()
    {
        infoUI.SetActive(false);
    }

    //Exit the game
    public void QuitButton()
    {
        Debug.Log("Bye see ya");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    //Show pause menu
    public void Menu()
    {
        menuUI.SetActive(true);
    }

    //Resume game by hiding the menu
    public void Resume()
    {
        menuUI.SetActive(false);
    }

    //Reload the current scene and resume time
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Load main menu and resume time
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    //Display formatted time on screen
    public void ShowScore(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        scoreText.text = $"Time: {minutes:00}:{seconds:00}";
    }

    //toggle background audio on/off
    public void ToggleAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.Play();
        }
    }
}