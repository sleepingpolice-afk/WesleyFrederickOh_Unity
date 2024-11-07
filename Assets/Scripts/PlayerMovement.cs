using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 maxSpeed;
    [SerializeField] private Vector2 timeToFullSpeed;
    [SerializeField] private Vector2 timeToStop;
    [SerializeField] private Vector2 stopClamp;

    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Vector2 moveFriction;
    private Vector2 stopFriction;
    private Rigidbody2D rb;
    private Camera mainCamera;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        
        // Get object dimensions
        //SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            objectWidth = boxCollider.bounds.extents.x;
            objectHeight = boxCollider.bounds.extents.y;
        }
        
        moveVelocity = 2*maxSpeed/timeToFullSpeed;
        moveFriction = -2 * (maxSpeed / (timeToFullSpeed * timeToFullSpeed));
        stopFriction = -2 * (maxSpeed / (timeToStop * timeToStop));
    }

    public void Move()
    {
        // Existing movement code remains the same
        // 1. Get input and set move direction
        moveDirection = Vector2.zero;   
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += Vector2.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += Vector2.right;
        }
        moveDirection = moveDirection.normalized;

        // 2. Handle X and Y components separately
        // X-axis movement
        if (moveDirection.x != 0)
        {
            moveVelocity.x += moveDirection.x * -GetFriction().x * Time.deltaTime;
            moveVelocity.x = Mathf.Clamp(moveVelocity.x, -maxSpeed.x, maxSpeed.x);
        }
        else
        {
            float decelerationX = GetFriction().x;

            if (moveVelocity.x > 0)
            {
                moveVelocity.x = Mathf.Max(0, moveVelocity.x + decelerationX * Time.deltaTime);
            }
            else if (moveVelocity.x < 0)
            {
                moveVelocity.x = Mathf.Min(0, moveVelocity.x - decelerationX * Time.deltaTime);
            }

            if (Mathf.Abs(moveVelocity.x) < stopClamp.x)
            {
                moveVelocity.x = 0;
            }
        }

        // Y-axis movement
        if (moveDirection.y != 0)
        {
            moveVelocity.y += moveDirection.y * -GetFriction().y * Time.deltaTime;
            moveVelocity.y = Mathf.Clamp(moveVelocity.y, -maxSpeed.y, maxSpeed.y);
        }
        else
        {
            float decelerationY = GetFriction().y;

            if (moveVelocity.y > 0)
            {
                moveVelocity.y = Mathf.Max(0, moveVelocity.y + decelerationY * Time.deltaTime);
            }
            else if (moveVelocity.y < 0)
            {
                moveVelocity.y = Mathf.Min(0, moveVelocity.y - decelerationY * Time.deltaTime);
            }

            if (Mathf.Abs(moveVelocity.y) < stopClamp.y)
            {
                moveVelocity.y = 0;
            }
        }

        rb.velocity = moveVelocity;
        
        MoveBound();
    }

    public void MoveBound()
    {
        if (mainCamera == null) return;

        Vector3 viewPos = mainCamera.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp(viewPos.x, 0.0f + (objectWidth / (mainCamera.orthographicSize * mainCamera.aspect * 2)), 
                               1.0f - (objectWidth / (mainCamera.orthographicSize * mainCamera.aspect * 2)));
        viewPos.y = Mathf.Clamp(viewPos.y, 0.0f + (objectHeight / (mainCamera.orthographicSize * 2)), 
                               1.0f - (objectHeight / (mainCamera.orthographicSize * 2)));
        
        Vector3 worldPos = mainCamera.ViewportToWorldPoint(viewPos);
        transform.position = new Vector3(worldPos.x, worldPos.y, transform.position.z);
        
        // If object hits bounds, stop velocity in that direction
        if (viewPos.x <= 0.0f + (objectWidth / (mainCamera.orthographicSize * mainCamera.aspect * 2)) || 
            viewPos.x >= 1.0f - (objectWidth / (mainCamera.orthographicSize * mainCamera.aspect * 2)))
        {
            moveVelocity.x = 0;
        }
        
        if (viewPos.y <= 0.0f + (objectHeight / (mainCamera.orthographicSize * 2)) || 
            viewPos.y >= 1.0f - (objectHeight / (mainCamera.orthographicSize * 2)))
        {
            moveVelocity.y = 0;
        }
    }

    public Vector2 GetFriction()
    {
        return new Vector2(
            moveDirection.x != 0 ? moveFriction.x : stopFriction.x,
            moveDirection.y != 0 ? moveFriction.y : stopFriction.y
        );
    }

    public bool IsMoving()
    {
        return moveDirection != Vector2.zero;
    }
}