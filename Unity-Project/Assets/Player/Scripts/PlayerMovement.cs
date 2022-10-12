using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Standard Settings")]
    public float speed = 10f;
    public float jumpPower;
    bool shouldMove = true;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform feet;
    [SerializeField] Transform head;
    [SerializeField] Transform left;
    [SerializeField] Transform right;

  

    private ShapeShift shapeShiftScript;
    private MirroredBehaviour MirrorScript;
    [SerializeField] GrapplingGun gun;
    private SpinDash spindash;

    [Header("Double Jump Settings")]
    public int extraJumps = 1;
    private int jumpCount = 0;
    bool IsGrounded;
    float mx;
    float jumpCoolDown;
    bool CanDoubleJump = false;
    public Cooldown JumpUI;
    private bool IsDoubleJumpOnCooldown;

    [Header("Dash Settings")]
    public float dashCooldown = 2f;
    float CurrentDashCooldown = 0f;
    bool IsDashOnCoolDown = false;
    public Cooldown DashUI;

    bool CanDash = false;
    public float dashDistance = 15f;
    bool isDashing;

    [HideInInspector] public int direction = 1; // -1 for left , 1 for right;

    [Header("Grappling gun Settings")]
    public Cooldown GunUI;

    [Header("WallClimb Settings")]
    public bool IsAtWall;
    public bool LeftSide;
    public bool RightSide;
    public bool canWallClimb;
    private bool wallGrabbed = false;

    int shapeMode;
    bool IsMirrored;
    float gravity;
    void Start()
    {
       shapeShiftScript = GetComponent<ShapeShift>();
        MirrorScript = GetComponent<MirroredBehaviour>();
        DashUI.SetMaxCooldownTime(dashCooldown);
        JumpUI.SetMaxCooldownTime(1f);
        spindash = GetComponent<SpinDash>();
        
    }
   
    // Update is called once per frame
    private void Update()
    {
        // UI 
        DashUI.SetCoolDownTime(CurrentDashCooldown);
        JumpUI.SetCoolDownTime(IsDoubleJumpOnCooldown);
        IsMirrored = MirrorScript.IsMirrored;
        shapeMode = shapeShiftScript.ShapeMode;

        jumpPower = rb.gravityScale * 6;

        if (jumpPower == 0 )
        {
            if (IsMirrored)
            {
                jumpPower = -6;
            }
            else 
            {
                jumpPower = 6;
            }
        }
        // CoolDown chevks
        if (IsGrounded)
        {
            IsDoubleJumpOnCooldown = true;
        }
        else if (jumpCount >= extraJumps)
        {
            IsDoubleJumpOnCooldown = false;
        }

        if (IsDashOnCoolDown)
        {
            CurrentDashCooldown += Time.deltaTime;
            if (CurrentDashCooldown >= dashCooldown)
            {
                IsDashOnCoolDown = false;
                CurrentDashCooldown = 0;
            }
        }
        if (IsMirrored)
        { 
            if (shapeMode == 2)
            {
                transform.rotation = new Quaternion(- 180, transform.rotation.y, transform.rotation.z, 0);
            }
           
        }
        else if (!IsMirrored)
        {
            jumpPower = Mathf.Abs(jumpPower);
            if (shapeMode == 2)
            {
                transform.rotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, 0);
            }
        }

        if (shapeMode == 1)
        {
            CanDash = true;
            CanDoubleJump = true;
            DashUI.SetEnabled(true);
            JumpUI.SetEnabled(true);
            gun.GetComponent<SpriteRenderer>().enabled = false;
            canWallClimb = false;
            //gun.SetActive(false);

        }
        if (shapeMode == 2)
        {
            gun.GetComponent<SpriteRenderer>().enabled = true;
            DashUI.SetEnabled(false);
            CanDash = false;
            CanDoubleJump = false;
            JumpUI.SetEnabled(false);
            canWallClimb = false;
            
        }
        if (shapeMode == 3)
        {
            gun.GetComponent<SpriteRenderer>().enabled = false;
            CanDoubleJump = false;
            CanDash = false;
            canWallClimb = true;
            DashUI.SetEnabled(false);
            JumpUI.SetEnabled(false);
        }
      
        mx = Input.GetAxis("Horizontal");
        if (mx > 0)
        {
            direction = 1;
        }
        else if (mx < 0)
        {
            direction = -1;
        }
        if (Input.GetButtonDown("Jump"))
        {
            soundManager.PlaySound("Jump");
            Jump();
        }
        if (Input.GetButtonDown("Dash"))
        {
            if (CanDash)
            {
                if (!IsDashOnCoolDown)
                {
                    soundManager.PlaySound("Dash");
                    StartCoroutine(Dash(direction));
                }
            }
           
        }
        if (canWallClimb)
        {
            if ( IsAtWall)
            {
               wallGrabbed = true;
                
            }
            if(!IsAtWall )
            {
                wallGrabbed = false;
            }
            if (wallGrabbed)
            {
              
              rb.gravityScale = 0;
                
                float  Vx = Input.GetAxis("Vertical");
                rb.velocity = new Vector2(rb.velocity.x, Vx*speed);
            }
            else
            {
                if (MirrorScript.IsMirrored)
                {
                    rb.gravityScale = -1;
                }
                if (!MirrorScript.IsMirrored)
                {
                    rb.gravityScale = 1;
                }

            }
        }
   
       
        
       
        
        CheckGround();
        CheckWall();
    }
    private void FixedUpdate()
    {
        if (shouldMove)
        {
            if (!isDashing)
            {
                rb.velocity = new Vector2(mx * speed, rb.velocity.y);
            }
        }
      
    }
 
    void Jump()
    {
        if (CanDoubleJump)
        {
         
            if (IsGrounded || jumpCount < extraJumps)
            {
                
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                jumpCount++;
            }
           
        }
        else
        {
            if (IsGrounded)
            {
                
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                jumpCount++;
            }
            
        }
      
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Destructable")&& spindash.IsSpinning)
        {
            
            Destroy(collision.gameObject);
        }
    }
    IEnumerator Dash(int direction)
    {
        isDashing = true;
       
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(dashDistance * direction, 0),ForceMode2D.Impulse);

        float gravity = rb.gravityScale;
        rb.gravityScale = 0;
       
        yield return new WaitForSeconds(0.4f);
        isDashing = false;
        rb.gravityScale = gravity;
        IsDashOnCoolDown = true;
        //currentSpinDashCharge = 0;
    }
    
    void CheckGround()
    {
        if (Physics2D.OverlapCircle(feet.position,0.1f,groundLayer) || Physics2D.OverlapCircle(head.position, 0.1f, groundLayer))
        {
            IsGrounded = true;
            jumpCount = 0;
            jumpCoolDown = Time.time + 0.2f;

        }
        else if (Time.time < jumpCoolDown)
        {
            IsGrounded = true;
           
        }
        else
        {
            IsGrounded = false;
        }
    }
    void CheckWall()
    {
        if (Physics2D.OverlapCircle(left.position,0.2f,groundLayer) || Physics2D.OverlapCircle(right.position, 0.1f, groundLayer))
        {
            IsAtWall = true;
            if (Physics2D.OverlapCircle(left.position, 0.2f, groundLayer))
            {
                LeftSide = true; 
            }
            else
            {
                RightSide = true;
            }
        }
        else
        {
            IsAtWall = false;
            LeftSide = false;
            RightSide = false;
        }
    }
    public void SetMovementEnabled(bool statement)
    {
        shouldMove = statement;
    }
}
