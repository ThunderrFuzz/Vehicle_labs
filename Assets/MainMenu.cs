using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
   public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Retry()
    {
        SceneManager.LoadScene("Challenge1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
