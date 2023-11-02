using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void Resume(int i)
    {
        Time.timeScale = 1.0f;
        PlayerPrefs.SetFloat("PlayerHP", 50f);
        PlayerPrefs.SetFloat("PlayerBulletSpeed", 10f);
        SceneManager.LoadScene(i);
    }
}
