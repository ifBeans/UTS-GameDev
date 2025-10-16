using UnityEngine;

public class ResetSceneScript : MonoBehaviour
{
    private Animator _wheelAnim;

    private void Awake()
    {
        _wheelAnim = GameObject.FindWithTag("Wheel").GetComponent<Animator>();
        _wheelAnim.SetBool("isSpinning", false);
        _wheelAnim.SetBool("hasQuest", false);
        _wheelAnim.SetInteger("day", 1);
        _wheelAnim.SetInteger("spinNumber", 1);
    }

    public void ResetNPCs()
    {
        NPCScript[] npcs = FindObjectsOfType<NPCScript>();
        foreach (NPCScript npc in npcs)
        {
            npc.ResetNPC();
        }
    }
}
