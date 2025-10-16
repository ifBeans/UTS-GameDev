using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string name;
    [TextArea(3, 5)]
    public string sentence;
    public bool requireInteraction = false; 
    public string interactionPrompt; 
}

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject nextIndicator;
    public GameObject interactionIndicator; 
    public TextMeshProUGUI interactionText;
    public GameObject crosshair;

    private Queue<DialogueLine> dialogueLines;
    private bool isDialogueActive = false;
    private bool isTyping = false;
    private bool waitingForInteraction = false;
    private string currentSentence = "";

    void Start()
    {
        dialogueLines = new Queue<DialogueLine>();
        dialoguePanel.SetActive(false);

        if (interactionIndicator != null)
            interactionIndicator.SetActive(false);

        if (crosshair == null)
            crosshair = GameObject.Find("Crosshair");
    }

    void Update()
    {
        if (waitingForInteraction && Input.GetKeyDown(KeyCode.E))
        {
            waitingForInteraction = false;
            interactionIndicator.SetActive(false);
            DisplayNextSentence();
            return;
        }

        if (isDialogueActive && !waitingForInteraction && Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = currentSentence;
                isTyping = false;
                nextIndicator.SetActive(true);
            }
            else
            {
                DisplayNextSentence();
            }
        }
    }

    public void StartDialogue(DialogueLine[] lines)
    {
        GameStateManager.Instance.SetState(GameState.Dialogue);
        isDialogueActive = true;
        dialoguePanel.SetActive(true);

        if (crosshair != null)
            crosshair.SetActive(false);

        var playerMove = FindFirstObjectByType<PlayerMoveScript>();
        var playerCamera = FindFirstObjectByType<PlayerCameraScript>();

        if (playerMove != null)
            playerMove.enabled = false;
        if (playerCamera != null)
            playerCamera.enabled = false;

        dialogueLines.Clear();

        foreach (DialogueLine line in lines)
        {
            dialogueLines.Enqueue(line);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (dialogueLines.Count == 0)
        {
            EndDialogue();
            return;
        }

        dialoguePanel.SetActive(true);
        DialogueLine line = dialogueLines.Dequeue();

        if (line.requireInteraction)
        {
            waitingForInteraction = true;
            nextIndicator.SetActive(false);
            interactionIndicator.SetActive(true);

            if (interactionText != null)
                interactionText.text = line.interactionPrompt;

            dialoguePanel.SetActive(false);
            dialogueText.text = "";
            nameText.text = "";
            return;
        }

        nameText.text = line.name;
        currentSentence = line.sentence;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(line.sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        nextIndicator.SetActive(false);
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }

        isTyping = false;
        nextIndicator.SetActive(true);
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);

        var playerMove = FindFirstObjectByType<PlayerMoveScript>();
        var playerCamera = FindFirstObjectByType<PlayerCameraScript>();

        if (playerMove != null)
            playerMove.enabled = true;
        if (playerCamera != null)
            playerCamera.enabled = true;

        if (crosshair != null)
            crosshair.SetActive(true);
        GameStateManager.Instance.SetState(GameState.Normal);
    }

    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }
}