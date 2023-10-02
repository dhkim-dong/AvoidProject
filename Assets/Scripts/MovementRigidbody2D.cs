using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementRigidbody2D : MonoBehaviour
{
    [Header("수평 이동")]
    [SerializeField] private float moveSpeed = 8f;

    [Header("점프")]
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float lowGravity = 2;
    [SerializeField] private float highGravity = 3;
    [SerializeField] private int maxJumpCount = 2;
    private int currentJumpCount = 2;

    [Header("충돌")]
    [SerializeField] private LayerMask groundLayer;

    private bool isGrounded;
    private Vector2 footPosition;
    private Vector2 footArea;

    private Rigidbody2D rigid2D;
    private new Collider2D collider2D;

    public bool leftMove = false;
    public bool rightMove = false;

    public bool IsLongJump { set; get; } = false;
    public float MoveSpeed { set => moveSpeed = Mathf.Max(0, value); }

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (leftMove)
        {
            Vector3 moveVec = new Vector3(-1f, 0, 0);
            transform.position += moveVec * moveSpeed * Time.deltaTime;
        }
        if (rightMove)
        {
            Vector3 moveVec = new Vector3(1f, 0, 0);
            transform.position += moveVec * moveSpeed * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Bounds bounds = collider2D.bounds;

        footPosition = new Vector2(bounds.center.x, bounds.min.y);

        footArea = new Vector2((bounds.max.x - bounds.min.x) * 0.5f, 0.1f);

        isGrounded = Physics2D.OverlapBox(footPosition, footArea, 0, groundLayer);

        if(isGrounded == true && rigid2D.velocity.y <= 0)
        {
            currentJumpCount = maxJumpCount;
        }

        if(IsLongJump && rigid2D.velocity.y > 0)
        {
            rigid2D.gravityScale = lowGravity;
        }
        else
        {
            rigid2D.gravityScale = highGravity;
        }
    }

    private void LateUpdate()
    {
        float x = Mathf.Clamp(transform.position.x, Constrants.min.x, Constrants.max.x);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
    public void MoveTo(float x)
    {
        rigid2D.velocity = new Vector2(x * moveSpeed, rigid2D.velocity.y);
    }

    public bool JumpTo()
    {
        if(currentJumpCount > 0)
        {
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, jumpForce);
            currentJumpCount--;

            return true;
        }

        return false;
    }

    public void ResetPos()
    {
        transform.position = new Vector3(0, -3.75f, 0);
    }
}
