using System.Collections;
using UnityEngine;

public class WheelSpinScript : MonoBehaviour
{
    private Animator _anim;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject[] _day1Items;
    [SerializeField] private GameObject[] _day2Items;
    [SerializeField] private GameObject[] _day3Items;
    [SerializeField] private GameObject[] _day4Items;
    private int _currentIndex = 0;
    private GameObject _currentItem;

    void Start()
    {
      _anim = this.GetComponent<Animator>();  
    }

    public void SpinWheel()
    {
        _anim?.SetBool("isSpinning", true);
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

    public void OnSpinEnd()
    {
        if (_anim.GetBool("hasQuest")) return;

        _anim?.SetBool("isSpinning", false);

        int day = _anim.GetInteger("day");
        int spinNumber = _anim.GetInteger("spinNumber");
        GameObject prefab = GetRewardByIndex(day, spinNumber);

        _currentItem = Instantiate(prefab, _spawnPoint.position, Quaternion.identity);
        StartCoroutine(RiseItem(_currentItem.transform));

        _anim.SetInteger("spinNumber", ++spinNumber);
        _anim.SetBool("hasQuest", true);
    }
    private GameObject GetRewardByIndex(int day, int spinNum)
    {
        switch (day)
        {
            case 1:
                _currentIndex = spinNum - 1;
                return _currentIndex < _day1Items.Length ? _day1Items[_currentIndex] : null;

            case 2:
                _currentIndex = spinNum - 1;
                return _currentIndex < _day2Items.Length ? _day2Items[_currentIndex] : null;

            case 3:
                _currentIndex = spinNum - 1;
                return _currentIndex < _day3Items.Length ? _day3Items[_currentIndex] : null;

            case 4:
                _currentIndex = spinNum - 1;
                return _currentIndex < _day4Items.Length ? _day4Items[_currentIndex] : null;

            default:
                return null;
        }
    }

    private IEnumerator RiseItem(Transform item)
    {
        Vector3 start = item.position - Vector3.up * 1.5f;
        Vector3 end = item.position;
        float t = 0f;
        float duration = 1f;

        item.position = start;
        while (t < duration)
        {
            t += Time.deltaTime;
            item.position = Vector3.Lerp(start, end, t / duration);
            yield return null;
        }
    }
}
