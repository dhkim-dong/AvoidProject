using UnityEngine;

public class MovementTransform2D : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector3 moveDirection;
    private float originSpeed;

    private void Awake()
    {
        originSpeed = moveSpeed;
    }

    public void ResetSpeed()
    {
        moveSpeed = originSpeed;
    }

    public float MoveSpeed(float modify)
    {
        moveSpeed += modify;

        return moveSpeed;
    }

    public Vector3 MoveDirection => moveDirection;

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
}
