using UnityEngine;
using UnityEngine.Events;

public class ButtonEventTrigger : MonoBehaviour
{
    [Header("Event triggered when the button is pressed")]
    public UnityEvent OnButtonPressed;

    public void PressButton()
    {
        Debug.Log(gameObject.name + " was pressed!");
        OnButtonPressed?.Invoke();
    }

}
