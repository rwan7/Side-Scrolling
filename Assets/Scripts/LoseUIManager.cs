using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseUIManager : MonoBehaviour
{
    public static LoseUIManager instance; 

    [SerializeField] private GameObject loseUI;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        loseUI.SetActive(false); 
    }

    public void ShowLoseUI()
    {
        loseUI.SetActive(true); 
        Time.timeScale = 0f; 
    }

    public void OnRestartButton()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnBackToMenuButton()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu"); 
    }
}
