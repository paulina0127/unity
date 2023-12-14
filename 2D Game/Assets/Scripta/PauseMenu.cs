using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
  public GameObject PausePanel;
  public static bool isPaused;

  void Start()
  {
    PausePanel = GameObject.Find("PausePanel");
    if (PausePanel != null)
    {
      PausePanel.SetActive(false);
    }
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      if (isPaused)
      {
        ResumeGame();
      }
      else
      {
        PauseGame();
      }
    }
  }

  public void PauseGame()
  {
    PausePanel.SetActive(true);
    Time.timeScale = 0f;
    isPaused = true;
  }

  public void ResumeGame()
  {
    PausePanel.SetActive(false);
    Time.timeScale = 1f;
    isPaused = false;
  }

  public void RestartGame()
  {
    SceneManager.LoadScene(0);
  }
}
