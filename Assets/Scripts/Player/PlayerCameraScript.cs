using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerCameraScript : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private Camera _camera;
    private float _MouseYRotation = 0f;

    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        _camera = Camera.main;
        AlignCameraFirstPerson();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
}
