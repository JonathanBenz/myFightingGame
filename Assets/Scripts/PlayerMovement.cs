using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float horizontalMovementSpeed = 5f;

    [SerializeField] float jumpSpeed = 5f;
    
    Vector2 rawInput;

    public bool attackButtonPressed;

    Collider2D myCollider;
    Rigidbody2D rb;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        Move();
        FlipSprite();
    }


    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void Move()
    {       
        Vector2 delta = new Vector2(rawInput.x * horizontalMovementSpeed, rb.velocity.y);
        rb.velocity += delta * Time.deltaTime;
    }

    void OnJump(InputValue value)
    {
        //float jumpDistanceToPeak = jumpDistance / 2;
        //float jumpGravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpPeak, 2);
        //float jumpSpeed = Mathf.Abs(jumpGravity) * timeToJumpPeak;
        //float yPosition = rb.position.y;
        //float xPosition = rb.position.x;
        //float yVelocity = rb.velocity.y;
        //step updates
        //Vector2 stepMovement = (new Vector2(rb.velocity.x, jumpSpeed) + Vector2.up * jumpGravity * Time.deltaTime * 0.5f) * Time.deltaTime;
        //transform.Translate(stepMovement);
        //xPosition = (horizontalMovementSpeed * Time.deltaTime);
        //rb.position += new Vector2(xPosition, yPosition);
        //rb.velocity += new Vector2(0f, jumpGravity * Time.deltaTime);
        //yVelocity += jumpGravity * Time.deltaTime;
        if (value.isPressed)
            if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
                return;
            else
            {
                //yPosition = (jumpSpeed * Time.deltaTime) + (0.5f * jumpGravity * Time.deltaTime * Time.deltaTime);
                //yVelocity = jumpSpeed;
                Vector2 jump = new Vector2(0, jumpSpeed);
                rb.AddForce(jump, ForceMode2D.Impulse);
            }
    }
    void FlipSprite()
    {
        bool playerHasHorizontalMovement = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalMovement)
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
    }

    void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            attackButtonPressed = true;
        }
    }
}
