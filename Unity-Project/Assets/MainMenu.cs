using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame()
    {
        PlayerPrefs.SetInt("PlayerLives", 3);
        PlayerPrefs.SetInt("Cherries", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
