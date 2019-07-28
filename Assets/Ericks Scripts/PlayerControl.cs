using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerControl : MonoBehaviour
{
    #region Variables
    //Player Properties
    public Animator animator;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool dashing = false;
    float gravity;

    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    [SerializeField]
    float buttonCooler = .5F;

    int buttonCount = 0;

    //Serializable fields
    [Header("Serializable")]
    [SerializeField] private float jumpForce = 400f;                          // Amount of force added when the player jumps.
    [SerializeField] private float dashForce = 500f;                          // Amount of force added when the player dashes.
    [Range(0, 1)] [SerializeField] private float crouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool airControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask groundLayerMask;                          // A mask determining what is ground to the character
    [SerializeField] private Transform groundPosition;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform ceilingPosition;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D crouchDisabler;                        // A collider that will be disabled when crouching
    int facingDir;

    //State variables
    const float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool isGrounded;            // Whether or not the player is grounded.
    const float ceilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D rb;
    private bool isFacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 velocity = Vector3.zero;
    private bool wasCrouching = false;

    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    #endregion

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        gravity = rb.gravityScale;

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAirDash();

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (horizontalMove < .01 && horizontalMove > -0.01)
            animator.SetBool("IsRunning", false);
        else
            animator.SetBool("IsRunning", true);

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        if (Input.GetButton("Vertical"))
        {
            if(Input.GetAxis("Vertical") > 0)
                animator.SetBool("IsAimingUp", true);
        }
        else
            animator.SetBool("IsAimingUp", false);

    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        dashing = false;
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    void FixedUpdate()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;

        CheckForGroundLayers(wasGrounded);

        // Move our character
        Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    private void CheckForGroundLayers(bool wasGrounded)
    {
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundPosition.position, groundedRadius, groundLayerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

    public void Move(float move, bool crouch, bool jump)
    {

        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(ceilingPosition.position, ceilingRadius, groundLayerMask))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (isGrounded || airControl)
        {
            // If crouching
            if (crouch)
            {
                if (!wasCrouching)
                {
                    wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                //move *= crouchSpeed;

                // Disable one of the colliders when crouching
                //if (crouchDisabler != null)
                    //crouchDisabler.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (crouchDisabler != null)
                    crouchDisabler.enabled = true;

                if (wasCrouching)
                {
                    wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }


            }

            // Move the character by finding the target velocity
            Vector2 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
            // And then smoothing it out and applying it to the character
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !isFacingRight) // ... flip the player.
                Flip();
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && isFacingRight)// ... flip the player.
                Flip();

        }
        // If the player should jump...
        if (isGrounded && jump)
        {
            // Add a vertical force to the player.
            isGrounded = false;
            rb.AddForce(new Vector2(0f, jumpForce));
            animator.SetBool("IsJumping", true);
        }
    }

    private void CheckAirDash()
    {
        if (isFacingRight)
            facingDir = 1;
        else
            facingDir = -1;

        if(Input.GetKeyDown(KeyCode.E) && direction ==0)
        {
            direction = facingDir;
        }

        if (direction != 0)
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;

                if (direction == 1)
                {
                    rb.velocity = Vector2.right * dashSpeed;
                }
                else if (direction == -1)
                {
                    rb.velocity = Vector2.left * dashSpeed;
                }
            }
        }

        /*if (Input.GetKeyDown(KeyCode.E) && dashing == false)
        {
            if (buttonCooler > 0 && buttonCount == 1)
            {
                //has double tapped
                //rb.gravityScale = 1;
                dashing = true;
                rb.AddForce((new Vector2(facingDir * Time.deltaTime, 0f) * dashForce), ForceMode2D.Impulse);
            }
            else
            {
                buttonCooler = .5F;
                buttonCount += 1;
            }
        }

        if (buttonCooler > 0)
        {
            buttonCooler -= 1 * Time.deltaTime;
        }
        else
        {
            buttonCount = 0;
        }*/
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        isFacingRight = !isFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
