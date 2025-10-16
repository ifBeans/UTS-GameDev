using System.Collections;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    [SerializeField] private Transform _walkPoint;
    [SerializeField] private float _walkSpeed = 1f;
    private Vector3 _originalPosition;
    private Quaternion _originalRotation;

    void Start()
    {
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;
    }

    public void ReceiveItem()
    {
        if (PlayerInventoryScript.Instance.HasItem())
        {
            PlayerInventoryScript.Instance.DeliverItem();
            Debug.Log("Quest item delivered!");
            StartCoroutine(GoHome());
        }
        else
        {
            Debug.Log("You don’t have an item to deliver!");
        }
    }

    private IEnumerator GoHome()
    {
        this.GetComponent<Collider>().enabled = false;

        while (Vector3.Distance(transform.position, _walkPoint.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _walkPoint.position, _walkSpeed * Time.deltaTime);
            transform.LookAt(_walkPoint);
            yield return null;
        }
    }

    public void ResetNPC()
    {
        StopAllCoroutines();
        transform.position = _originalPosition;
        transform.rotation = _originalRotation;
    }
}

