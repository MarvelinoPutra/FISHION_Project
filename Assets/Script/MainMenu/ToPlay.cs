using UnityEngine;
using UnityEngine.SceneManagement;

public class ToPlay : MonoBehaviour
{
    // Tombol Play
    public void PlayGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    // Tombol Quit
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game ditutup");
    }
}