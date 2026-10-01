using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //fungsi ngeload ke scene gamenya
    public GameObject Menu, Setting, Credits_UI;
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("MainGame");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Settings()
    {
        Menu.SetActive(false);
        Setting.SetActive(true);
        //AudioManager.Instance.PlaySFX("Click");
    }

    public void BackToMenu()
    {
        Menu.SetActive(true);
        Setting.SetActive(false);
        Credits_UI.SetActive(false);
        //AudioManager.Instance.PlaySFX("Click");
    }

    public void Credits()
    {
        Credits_UI.SetActive(true);
        Menu.SetActive(false);
        //AudioManager.Instance.PlaySFX("Click");
    }
}
