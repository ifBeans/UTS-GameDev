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
    }
}
