using UnityEngine;

public class NoteScript : MonoBehaviour
{
    public GameObject noteUI; 
    public PlayerMoveScript playerMove;
    public PlayerCameraScript playerCamera;
    public KeyCode toggleKey = KeyCode.Q;

    private bool _isNoteOpen = false;

    void Start()
    {
        if (noteUI != null)
            noteUI.SetActive(false);
    }

    void Update()
    {
        if (GameStateManager.Instance == null) return;
        if (GameStateManager.Instance.IsBusy() && !_isNoteOpen) return;
        if (Input.GetKeyDown(toggleKey))
        {
            if (!_isNoteOpen)
                OpenNote();
            else
                CloseNote();
        }
    }

    void OpenNote()
    {
        GameStateManager.Instance.SetState(GameState.UIBlocked);
        _isNoteOpen = true;

        if (noteUI != null)
            noteUI.SetActive(true);

        if (playerMove != null)
            playerMove.enabled = false;

        if (playerCamera != null)
            playerCamera.enabled = false;
    }

    void CloseNote()
    {
        _isNoteOpen = false;

        if (noteUI != null)
            noteUI.SetActive(false);

        if (playerMove != null)
            playerMove.enabled = true;

        if (playerCamera != null)
            playerCamera.enabled = true;

        GameStateManager.Instance.SetState(GameState.Normal);
    }
}
