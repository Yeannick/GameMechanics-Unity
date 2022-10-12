using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpinDash : MonoBehaviour
{

    public Rigidbody2D rb;
    public GameObject Spikes;
    public float RotateSpeed;
    public Cooldown SpinDashUI;
    [Header("Spin Dash Settings")]
    
    public float SpinDashCoolDown = 5f;
    float currentCooldown=0;
    bool IsSpinDashOnCoolDown = false;
   
    bool IsSpinDashing = false;
 

    public float SpinCharge = 2f;
    public float currentCharge = 0f;
    public float dashDistance = 10f;
   public bool IsSpinning = false;

    PlayerMovement script;
    ShapeShift Shapescript;
    public int direction;
    int shapeMode;
    private RotatePlayer rotation;
    // Start is called before the first frame update
    void Start()
    {
        script = GetComponent<PlayerMovement>();
        Shapescript = GetComponent<ShapeShift>();
        Spikes.SetActive(false);
        SpinDashUI.SetMaxCooldownTime(SpinDashCoolDown);


    }

    // Update is called once per frame
    void Update()
    {
        SpinDashUI.SetCoolDownTime(currentCooldown);
        if (IsSpinDashOnCoolDown)
        {
            currentCooldown += Time.deltaTime;

            if (currentCooldown >= SpinDashCoolDown)
            {
                currentCooldown = 0;
                IsSpinDashOnCoolDown = false;
            }
        }
        if (!IsSpinDashOnCoolDown)
        {
            direction = script.direction;
            shapeMode = Shapescript.ShapeMode;
            if (shapeMode == 3)
            {
                SpinDashUI.SetEnabled(true);
                if (IsSpinning)
                {
                   
                    Spikes.SetActive(true);
                    currentCharge += Time.deltaTime;

                    if (currentCharge >= SpinCharge)
                    {

                        StartCoroutine(SpinDashing(direction));

                    }
                }
                if (Input.GetButtonDown("GrappleHook"))
                {
                    soundManager.PlaySound("SpinDash");
                    script.SetMovementEnabled(false);
                    IsSpinning = true;

                }
            }
            else
            {
                SpinDashUI.SetEnabled(false); ;
            }
        }
       
        
    }
    IEnumerator SpinDashing(int direction)
    {
       
        rb.velocity = new Vector2(0, 0f);
        rb.AddForce(new Vector2(dashDistance*10 * direction, 0), ForceMode2D.Impulse);

        float gravity = rb.gravityScale;
        rb.gravityScale = 0;

        yield return new WaitForSeconds(0.2f);
        rb.gravityScale = gravity;
        IsSpinning = false;
        currentCharge = 0;
        script.SetMovementEnabled(true);
        Spikes.SetActive(false);
        IsSpinDashOnCoolDown = true;
    }
   
}
