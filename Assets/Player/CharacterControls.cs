using System.Collections;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class CharacterControls : MonoBehaviour
{
    Rigidbody2D rb;
    
    float horizontalInput, verticalInput;

        [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float gravityScale = 6;
    [SerializeField] float jumpCooldown = 0.5f;
    bool wantsToJump = false;
    bool canJump = true;
    bool isBlue = false;


        [Header("GroundCheck")]
    Transform groundCheck;  
    [SerializeField] LayerMask groundMask;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = transform.GetChild(1).transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isBlue)
        {
            rb.linearVelocity = new Vector2 (horizontalInput, verticalInput).normalized * speed * Time.deltaTime;
        }
        else
        {
            rb.linearVelocityX = horizontalInput* speed * Time.deltaTime;
        }
    }

    private void Update()
    {
        if(wantsToJump && isGrounded() && canJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            StartCoroutine(JumpCooldown());
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalInput = context.ReadValue<Vector2>().x;
        verticalInput = context.ReadValue<Vector2>().y;
    }

    bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundMask);
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if(!isBlue)
        {
            return;
        }

        if (context.performed)
        {
            wantsToJump = true;
        }

        if (context.canceled)
        {
            if(rb.linearVelocity.y > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            }
            
            wantsToJump = false;
        }
    }

    IEnumerator JumpCooldown()
    {
        canJump = false;

        yield return new WaitForSeconds(jumpCooldown);

        canJump = true;
    }

    public void ChangeState(int stateIndex)
    {
        switch (stateIndex)
        {
            case 0:
                TurnRed();
                break;
            case 1:
                TurnBlue();
                break;
        }
    }
    void TurnBlue()
    {
        rb.gravityScale = gravityScale;
        isBlue = true;

        SpriteRenderer sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        sprite.color = new Color(0, 0, 255);
    }
    void TurnRed()
    {
        rb.gravityScale = 0f;
        isBlue = false;

        SpriteRenderer sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        sprite.color = new Color(255, 0, 0);
    }

    
}