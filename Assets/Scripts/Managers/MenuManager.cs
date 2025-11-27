using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadGame(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
