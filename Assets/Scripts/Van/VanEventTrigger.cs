using UnityEngine;
using UnityEngine.Events;

public class VanEventTrigger : MonoBehaviour
{
    [Header("Event triggered when the van is pressed")]
    public UnityEvent OnVanPressed;

    public void PressVan()
    {
        Debug.Log(gameObject.name + " was pressed!");
        OnVanPressed?.Invoke();
    }
}
