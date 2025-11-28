using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject gameOverUI;
    public GameObject youwinUI;
    public bool isPaused = false;
    public Shooting player;
    public MouseController mouseController;
    public bool isGameOver = false;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        mouseController.SetCustomCursor();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        // Activar input del player
        player.canShoot = true;

        mouseController.SetCustomCursor();
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        // Desactivar input del player
        player.canShoot = false;

        //Cursor.visible = true;
        mouseController.ResetCursor();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
    }

    public void Victory()
    {
        Time.timeScale = 0f;
        mouseController.ResetCursor();
        youwinUI.SetActive(true);
    }
}
