using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool _isPaused = false;
    private PlayerCameraScript _playerCameraScript;

    private void Awake()
    {
        _isPaused = false;
       pauseMenuUI?.SetActive(false);
    }

    private void Start()
    {
        _playerCameraScript = GameObject.FindWithTag("Player").GetComponent<PlayerCameraScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _playerCameraScript.enabled = true;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _playerCameraScript.enabled = false;
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
