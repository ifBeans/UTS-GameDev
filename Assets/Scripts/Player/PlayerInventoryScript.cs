using UnityEngine;

public class PlayerInventoryScript : MonoBehaviour
{
    public static PlayerInventoryScript Instance;

    [SerializeField] private Transform _holdPoint;
    private GameObject _currentItem;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (_holdPoint == null && Camera.main != null)
        {
            _holdPoint = new GameObject("HoldPoint").transform;
            _holdPoint.SetParent(Camera.main.transform);
            _holdPoint.localPosition = new Vector3(0.5f, -0.5f, 1f);
            _holdPoint.localRotation = Quaternion.identity;
        }
    }

    public void PickUpItem(GameObject item)
    {
        _currentItem = item;
        //Rigidbody rb = item.GetComponent<Rigidbody>();
        //if (rb) rb.isKinematic = true;

        Collider col = item.GetComponent<Collider>();
        if (col) col.enabled = false;

        // Attach item to hold point
        item.transform.SetParent(_holdPoint);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;

        // Optional: scale it down if needed
        //item.transform.localScale = Vector3.one * 0.5f;
    }

    public bool HasItem()
    {
        if(_currentItem == null)
        {
            return false;
        }

        else return true;
    }

    public void DeliverItem()
    {
        if (_currentItem != null)
        {
            _currentItem.transform.SetParent(null);
            Destroy(_currentItem);
            _currentItem = null;
        }

        Animator wheelAnim = GameObject.FindWithTag("Wheel").GetComponent<Animator>();
        wheelAnim.SetBool("hasQuest", false);
    }
}
