using System.Collections;
using UnityEngine;

public class IntroSequence : MonoBehaviour
{
    public Transform playerTransform;
    public Transform introPositionMarker;
    public Camera playerCamera;
    public SceneFader sceneFader;
    public DialogueManager dialogueManager;
    public DialogueLine[] introDialogue;
    public bool playOnStart = true;
    public float delayBeforeFade = 0.5f;

    private bool _introPlayed = false;

    void Start()
    {
        if (playOnStart && !_introPlayed)
        {
            StartCoroutine(PlayIntroSequence());
        }
    }

    public IEnumerator PlayIntroSequence()
    {
        GameStateManager.Instance.SetState(GameState.Cutscene);
        _introPlayed = true;

        var playerMove = FindFirstObjectByType<PlayerMoveScript>();
        var playerCameraScript = FindFirstObjectByType<PlayerCameraScript>();

        if (playerMove != null)
            playerMove.enabled = false;
        if (playerCameraScript != null)
            playerCameraScript.enabled = false;

        if (introPositionMarker != null && playerTransform != null)
        {
            playerTransform.position = introPositionMarker.position;
            playerTransform.rotation = introPositionMarker.rotation;
        }

        yield return new WaitForSeconds(delayBeforeFade);

        if (sceneFader != null)
        {
            yield return StartCoroutine(sceneFader.FadeOut());
        }

        yield return new WaitForSeconds(0.5f);

        if (dialogueManager != null && introDialogue.Length > 0)
        {
            dialogueManager.StartDialogue(introDialogue);

            while (dialogueManager.IsDialogueActive())
            {
                yield return null;
            }
        }

        if (playerMove != null)
            playerMove.enabled = true;
        if (playerCameraScript != null)
            playerCameraScript.enabled = true;

        GameStateManager.Instance.SetState(GameState.Normal);
    }


}