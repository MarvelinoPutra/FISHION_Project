using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Tombol Play
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Tombol Quit
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game ditutup");
    }
}
