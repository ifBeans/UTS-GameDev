using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 5f;

    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Vector3 move = new Vector3(h, 0f, v) * _playerSpeed * Time.deltaTime;

        transform.Translate(move, Space.Self);
    }
}
