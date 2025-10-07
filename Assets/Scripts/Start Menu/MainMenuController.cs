using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void OnStartButtonPressed()
    {
        if (SceneFader.Instance != null)
        {
            SceneFader.Instance.FadeToScene("Level");
        }
        else
        {
            Debug.LogWarning("SceneFader not found, loading directly");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level");
        }
    }
}
