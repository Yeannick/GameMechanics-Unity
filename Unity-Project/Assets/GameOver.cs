using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void Quit()
    {
        StartCoroutine(Exit());
    }
    public void Menu()
    {
        StartCoroutine(navigateToMainMenu());
    }
    public void Retry()
    {
        StartCoroutine(RestartGame());
    }
    IEnumerator navigateToMainMenu()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Menu");
    }
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerPrefs.SetInt("PlayerLives", 3);
        PlayerPrefs.SetInt("Cherries", 0);
        SceneManager.LoadScene("Level1");

    }
    IEnumerator Exit()
    {
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
}
