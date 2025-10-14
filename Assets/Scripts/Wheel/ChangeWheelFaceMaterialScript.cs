using UnityEngine;

public class ChangeWheelFaceMaterialScript : MonoBehaviour
{
    [SerializeField] private MeshRenderer _wheelFaceRenderer;
    [SerializeField] private Texture _day1IdleTexture;
    [SerializeField] private Texture _day2IdleTexture;
    private Animator _wheelAnim;
    private Transform _wheelTransform;

    void Start()
    {
        _wheelAnim = GameObject.FindWithTag("Wheel").GetComponent<Animator>();
        _wheelTransform = GameObject.FindWithTag("Wheel").transform;
        _wheelFaceRenderer.material.mainTexture = _day1IdleTexture;
    }

    public void UpdateFaceMaterial()
    {
        switch (_wheelAnim?.GetInteger("day"))
        {
            case 2:
                _wheelFaceRenderer.material.mainTexture = _day2IdleTexture;
                return;
        }
    }

    public void ResetWheelFaceRotation()
    {
        _wheelTransform.localRotation = Quaternion.identity;
        Debug.Log("Y = " + _wheelTransform.rotation.y);
    }
}
