using UnityEngine;

public enum GameState
{
    Normal,
    Dialogue,
    Cutscene,
    Paused,
    Transition,
    UIBlocked
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    private GameState currentState = GameState.Normal;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public GameState GetState()
    {
        return currentState;
    }

    public void SetState(GameState newState)
    {
        currentState = newState;
    }

    public bool IsBusy()
    {
        return currentState != GameState.Normal;
    }

    public bool Is(GameState state)
    {
        return currentState == state;
    }

}
