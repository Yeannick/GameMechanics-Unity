using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public void EndGame()
    {
        StartCoroutine(navigateToMainMenu());
    }

    IEnumerator navigateToMainMenu()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Menu");
    }
}
