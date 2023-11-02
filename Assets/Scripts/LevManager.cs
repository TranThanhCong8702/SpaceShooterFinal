using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevManager : MonoBehaviour
{
    ScoreKeeper scoreKeeper;
    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1.0f;
        StartCoroutine( WaitATime(sceneName));
    }

    IEnumerator WaitATime(string sceneName)
    {
        yield return new WaitForSecondsRealtime(1);
        if((sceneName == "MainMenu" || sceneName == "SampleScene") && scoreKeeper != null)
        {
            scoreKeeper.Reset();
        }
        SceneManager.LoadScene(sceneName);
        Debug.Log(sceneName);
        Time.timeScale = 1.0f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
