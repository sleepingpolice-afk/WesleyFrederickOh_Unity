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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        moveVelocity = 2*maxSpeed/timeToFullSpeed;
        moveFriction = -2 * (maxSpeed / (timeToFullSpeed * timeToFullSpeed));
        stopFriction = -2 * (maxSpeed / (timeToStop * timeToStop));
    }

    // Update is called once per frame
    public void Move()
    {
        // 1. Get input and set move direction
        moveDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            //moveDirection.y += 1;
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
            // Add velocity based on input and timeToFullSpeed
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

            // Stop if below threshold
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

            // Stop if below threshold
            if (Mathf.Abs(moveVelocity.y) < stopClamp.y)
            {
                moveVelocity.y = 0;
            }
        }

        rb.velocity = moveVelocity;
    }

    public Vector2 GetFriction()
    {
        return new Vector2(
            moveDirection.x != 0 ? moveFriction.x : stopFriction.x,
            moveDirection.y != 0 ? moveFriction.y : stopFriction.y
        );
    }

    public void MoveBound()
    {   
        // Empty for now
    }

    public bool IsMoving()
    {
        return moveDirection != Vector2.zero;
    }
}
