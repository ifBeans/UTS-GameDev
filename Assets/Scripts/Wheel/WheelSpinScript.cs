using UnityEngine;

public class WheelSpinScript : MonoBehaviour
{
    private Animator _anim;

    void Start()
    {
      _anim = this.GetComponent<Animator>();  
    }

    public void SpinWheel()
    {
        _anim?.SetBool("isSpinning", true);
    }

    public void StopSpinning()
    {
        _anim?.SetBool("isSpinning", false);
        _anim?.SetBool("hasQuest", true);
        int spinNumber = _anim.GetInteger("spinNumber");
        _anim.SetInteger("spinNumber", ++spinNumber);
        Debug.Log("Spin number is now " + spinNumber);
    }

    public void AddDay()
    {
        int day = _anim.GetInteger("day");
        _anim?.SetInteger("day", ++day);
    }

    public void ResetSpinNumber()
    {
        _anim?.SetInteger("spinNumber", 1);
    }
}
