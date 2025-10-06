using UnityEngine;

public class ButtonPressedScript : MonoBehaviour
{
    private Animator _anim;

    void Start()
    {
        _anim = this.GetComponent<Animator>();
    }

    public void AnimateButtonPressed()
    {
        _anim?.SetBool("isPressed", true);
    }

    public void AnimateButtonPressedStop()
    {
        _anim?.SetBool("isPressed", false);
    }
}
