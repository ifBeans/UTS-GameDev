using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueLine[] dialogueLines;
    public KeyCode interactKey = KeyCode.E;
    public float triggerDistance = 3f;
    public GameObject crosshair;
    public GameObject interactPrompt; // UI "Press E to Talk"

    private DialogueManager _dialogueManager;
    private Transform _player;
    private bool _playerInRange = false;

    void Start()
    {
        _dialogueManager = FindFirstObjectByType<DialogueManager>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        if (interactPrompt != null)
            interactPrompt.SetActive(false);

        if (crosshair == null)
            crosshair = GameObject.Find("Crosshair");
    }

    void Update()
    {
        // Cek jarak player dengan trigger
        float distance = Vector3.Distance(_player.position, transform.position);
        _playerInRange = distance <= triggerDistance;

        if (interactPrompt != null)
            interactPrompt.SetActive(_playerInRange && !_dialogueManager.IsDialogueActive());

        if (_playerInRange && Input.GetKeyDown(interactKey) && !_dialogueManager.IsDialogueActive())
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        _dialogueManager.StartDialogue(dialogueLines);

        // Opsional: freeze player saat dialogue
        FindFirstObjectByType<PlayerMoveScript>().enabled = false;
        FindFirstObjectByType<PlayerCameraScript>().enabled = false;
        if (crosshair != null)
            crosshair.SetActive(false);
    }

    // Visualisasi range di Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, triggerDistance);
    }
}