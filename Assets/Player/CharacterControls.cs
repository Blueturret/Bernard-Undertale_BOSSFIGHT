using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControls : MonoBehaviour
// Mouvements du personnage
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

        [Header("GroundCheck")]
    float coyoteTime = .12f;
    float coyoteTimeCounter;
    Transform groundCheck;  
    [SerializeField] LayerMask groundMask;

    // Crouch
    float defaultX;
    float defaultY;

    // ColorChange
    bool isBlue = false;
    Animation colorFlickerAnim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        colorFlickerAnim = GetComponent<Animation>(); 
        groundCheck = transform.GetChild(2).transform;

        defaultX = transform.localScale.x;
        defaultY = transform.localScale.y;
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
            rb.linearVelocityX = horizontalInput * speed * Time.deltaTime;
        }
    }

    private void Update()
    // Cette fonction est dans Update() pour permettre au joueur de continuellement sauter s'il maintient la touche
    // D'ou les deux variables wantsToJump et canJump
    {
        if (isGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; // Decreases the coyoteTimeCounter
        }

        if (wantsToJump && coyoteTimeCounter > 0 && canJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            StartCoroutine(JumpCooldown());
        }
    }

    public void Move(InputAction.CallbackContext context)
    // Movement input
    {
        horizontalInput = context.ReadValue<Vector2>().x;
        verticalInput = context.ReadValue<Vector2>().y;
    }

    bool isGrounded()
    // GroundCheck
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.15f, groundMask);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(!isBlue) return;

        if (context.performed)
        {
            wantsToJump = true;
        }

        if (context.canceled)
        // Au plus on maintient la touche, au plus on saute haut.
        // Si on arrete prematurement de presser le touche, on diminue la force du saut
        {
            if(rb.linearVelocity.y > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            }
            
            wantsToJump = false;

            coyoteTimeCounter = 0;
        }
    }

    IEnumerator JumpCooldown()
    {
        canJump = false;

        yield return new WaitForSeconds(jumpCooldown);

        canJump = true;
    }

    // Handling heart color
    public IEnumerator ChangeState(int stateIndex, bool playAnim)
    // Coroutine pour alterner entre coeur bleu et coeur rouge
    // Le deuxieme parametre controle si oui ou non on veut jouer une animation de telegraphing, 'false' change
    // la couleur instantanement
    {   
        if(playAnim)
        {
            colorFlickerAnim.Play();

            yield return new WaitUntil(() => !colorFlickerAnim.isPlaying);
            yield return new WaitForSeconds(0.3f);
        }
        
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
    // Change le coeur en coeur bleu
    {
        rb.gravityScale = gravityScale;
        isBlue = true;

        SpriteRenderer sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        sprite.color = new Color(0, 0, 255);
    }
    void TurnRed()
    // Change le coeur en coeur rouge
    {
        rb.gravityScale = 0f;
        isBlue = false;

        SpriteRenderer sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        sprite.color = new Color(255, 0, 0);
    }

    public void Crouch(InputAction.CallbackContext context)
    // Pourquoi j'ai fait ca...
    {
        if (context.performed)
        {
            transform.localScale = new Vector3(defaultX * 1.3f, defaultY / 2, transform.localScale.z);
            AudioManager.instance.PlaySound("PlayerCrouch");
        }

        if(context.canceled)
        {
            transform.localScale = new Vector3(defaultX, defaultY, transform.localScale.z);
        }
    }
}