using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    private TMP_Text _dayCountTextBox;
    private TMP_Text _spinCountTextBox;
    private Animator _wheelAnim;

    private void Start()
    {
        _dayCountTextBox = this.transform.Find("Day Count").GetComponent<TMP_Text>();
        _spinCountTextBox = this.transform.Find("Spin Count").GetComponent<TMP_Text>();
        _wheelAnim = GameObject.FindWithTag("Wheel").GetComponent<Animator>();
        _dayCountTextBox.SetText("Day 1");
        _spinCountTextBox.SetText("Spins Left: 3/3");
    }

    public void UpdateDayCount()
    {
        _dayCountTextBox.SetText("Day " + _wheelAnim?.GetInteger("day"));
    }

    public void UpdateSpinCount()
    {
        int spinsLeft = 3 - _wheelAnim.GetInteger("spinNumber");
        _spinCountTextBox.SetText("Spins Left: " + spinsLeft + "/3");
    }

    public void ResetSpinCount()
    {
        _spinCountTextBox.SetText("Spins Left: 3/3");
    }
}
