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

    void FixedUpdate()
    // Mouvements de base
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
    // Cette fonction est dans Update() pour permettre au joueur de continuellement sauter s'il maintient la touche
    // D'ou les deux variables wantsToJump et canJump
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
    // GroundCheck
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
        // Au plus on maintient la touche, au plus on saute haut. Si on arrete prematurement de presser le touche, on diminue la force du saut
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
    // Fonction pour alterner entre coeur bleu et coeur rouge
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