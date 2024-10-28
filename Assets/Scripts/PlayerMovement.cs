using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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
        moveFriction = -2*(maxSpeed/(timeToFullSpeed*timeToFullSpeed));
        stopFriction = -2*(maxSpeed/(timeToStop*timeToStop));
    }

    // Update is called once per frame
    public void Move()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            moveDirection = moveDirection.normalized;
            moveDirection += Vector2.up;
            rb.velocity = moveDirection*moveVelocity;
        }
        else if(Input.GetButtonDown("S"))
    }

    public Vector2 GetFriction()
    {
        return moveFriction;
    }

    public void MoveBound()
    {   
        // untuk sementara dikosongkan dulu
    }

    public bool IsMoving()
    {
        if(moveVelocity == Vector2.zero)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
