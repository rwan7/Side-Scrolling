using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinUIManager : MonoBehaviour
{
    [SerializeField] private GameObject winUI; 

    private void Start()
    {
        winUI.SetActive(false); 
    }

    public void ShowWinUI()
    {
        winUI.SetActive(true); 
        Time.timeScale = 0f; 
    }

    public void OnRestartButton()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void OnContinueButton()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Level 2");
    }

    public void OnBackToMenuButton()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu"); 
    }
}

