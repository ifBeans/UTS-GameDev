using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI;

    private bool _isPaused = false;
    private PlayerCameraScript _playerCameraScript;
    private GameState _previousState;

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
        if (GameStateManager.Instance == null) return;

        // Hapus baris ini supaya pause bisa diakses kapanpun
        // if (GameStateManager.Instance.IsBusy()) return;

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
        GameStateManager.Instance.SetState(_previousState);
        if (!GameStateManager.Instance.IsBusy())
            _playerCameraScript.enabled = true;

        // Kembalikan ke state sebelumnya (misal Dialogue atau Cutscene)
    }

    public void Pause()
    {
        _previousState = GameStateManager.Instance.GetState();

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _playerCameraScript.enabled = false;

        // Set state khusus agar UI lain tahu game sedang dipause
        GameStateManager.Instance.SetState(GameState.Paused);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameStateManager.Instance.SetState(GameState.Normal);
    }
}
