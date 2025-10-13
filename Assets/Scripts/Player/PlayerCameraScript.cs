using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerCameraScript : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject _target;
    [SerializeField] private Camera _camera;
    [SerializeField] private Animator _wheelAnim;
    private float _MouseYRotation = 0f;

    [Header("Raycast Settings")]
    public float interactionDistance = 3.0f;

    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        _camera = Camera.main;
        _wheelAnim = GameObject.FindWithTag("Wheel").GetComponent<Animator>();
        AlignCameraFirstPerson();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryInteract();
        }
    }

    void LateUpdate()
    {
        RotateCamera();
        _camera.transform.position = _target.transform.position;
    }

    private void AlignCameraFirstPerson()
    {
        _camera.transform.rotation = _target.transform.rotation;
        _camera.transform.position = _target.transform.position;
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * 3f;
        float mouseY = Input.GetAxis("Mouse Y") * 3f;

        _MouseYRotation -= mouseY;
        _MouseYRotation = Mathf.Clamp(_MouseYRotation, -90f, 90f);

        _target.transform.Rotate(Vector3.up * mouseX);
        _camera.transform.localRotation = Quaternion.Euler(_MouseYRotation, 0f, 0f);
    }

    void TryInteract()
    {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // Cast the ray and check if it hits something within the interactionDistance
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            // Check if the object hit has the "Button" tag
            if (hit.collider.CompareTag("Button"))
            {
                // Try to get the ButtonEventTrigger component from the hit object
                ButtonEventTrigger button = hit.collider.GetComponent<ButtonEventTrigger>();

                if (button != null && !_wheelAnim.GetBool("isSpinning"))
                {
                    // Trigger the public method on the button
                    button.PressButton();
                }
            }
            // You can add other interaction logic here (e.g., opening doors)
        }
    }
}
