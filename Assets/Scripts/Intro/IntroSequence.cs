using System.Collections;
using UnityEngine;

public class IntroSequence : MonoBehaviour
{
    public Transform playerTransform;
    public Transform introPositionMarker; // Empty Object untuk posisi player intro
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
        _introPlayed = true;

        // Disable player control
        var playerMove = FindFirstObjectByType<PlayerMoveScript>();
        var playerCameraScript = FindFirstObjectByType<PlayerCameraScript>();

        if (playerMove != null)
            playerMove.enabled = false;
        if (playerCameraScript != null)
            playerCameraScript.enabled = false;

        // LANGSUNG teleport player ke posisi intro marker
        if (introPositionMarker != null && playerTransform != null)
        {
            // Teleport player ke posisi dan rotasi marker
            playerTransform.position = introPositionMarker.position;
            playerTransform.rotation = introPositionMarker.rotation;
        }

        // Tunggu sebentar
        yield return new WaitForSeconds(delayBeforeFade);

        // Fade Out (dari hitam ke terlihat)
        if (sceneFader != null)
        {
            yield return StartCoroutine(sceneFader.FadeOut());
        }

        yield return new WaitForSeconds(0.5f);

        // Mulai dialogue
        if (dialogueManager != null && introDialogue.Length > 0)
        {
            dialogueManager.StartDialogue(introDialogue);

            // Tunggu sampai dialogue selesai
            while (dialogueManager.IsDialogueActive())
            {
                yield return null;
            }
        }

        // Enable player control kembali (tanpa return camera karena sudah di posisi)
        if (playerMove != null)
            playerMove.enabled = true;
        if (playerCameraScript != null)
            playerCameraScript.enabled = true;
    }


}