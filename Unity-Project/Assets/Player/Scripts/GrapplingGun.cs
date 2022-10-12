using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GrapplingGun : MonoBehaviour
{
    public GameObject playerObject;
    private MirroredBehaviour script;
    private ShapeShift shape;
    int shapeMode;
    bool IsMirrored;

    public float CoolDown = 10f;
    private float currentCoolDowmTime = 0;
    bool canShoot = true;
    public Cooldown cooldownUI;
    bool hasGrappled = false;

    public GrapplingRope grapplingRope;

    [SerializeField] private bool grappleToAll = false;
    [SerializeField] private int grappableLayerNumber = 9;

    public Camera mainCamera;

    public Transform GunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    public SpringJoint2D springJoint;
    public Rigidbody2D rb;

    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistance = 20;

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float launchSpeed = 1;

    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequency = 1;

    public Vector2 grapplePoint;
    public Vector2 grappleDistanceVector;

    // Start is called before the first frame update
 
    private void Start()
    {
        
        grapplingRope.enabled = false;
        springJoint.enabled = false;
        cooldownUI.SetMaxCooldownTime(CoolDown);
    }

    // Update is called once per frame
    private void Update()
    {
        cooldownUI.SetCoolDownTime(currentCoolDowmTime);
        if (!canShoot)
        {
            currentCoolDowmTime += Time.deltaTime;

            if (currentCoolDowmTime >= CoolDown)
            {
                canShoot = true;
                currentCoolDowmTime = 0;
                hasGrappled = false;
            }
        }
        script = playerObject.GetComponent<MirroredBehaviour>();
        shapeMode = playerObject.GetComponent<ShapeShift>().ShapeMode;
        IsMirrored = script.IsMirrored;
        if (shapeMode == 1 || shapeMode ==3 )
        {
            cooldownUI.SetEnabled(false);
        }
        if (shapeMode == 2)
        {
            cooldownUI.SetEnabled(true);
            if (canShoot)
            {
               

                Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    RotateGun(mousePos, true);
                
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    SetGrapplePoint();
                  
                }
                else if (Input.GetKey(KeyCode.Mouse0))
                {
                    if (grapplingRope.enabled)
                    {
                        RotateGun(grapplePoint, false);
                    }
                    else
                    {
                        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                        RotateGun(mousePos, true);
                    }
                    if (launchToPoint && grapplingRope.isGrappling)
                    {
                        if (launchType == LaunchType.Transform_Launch)
                        {
;
                            Vector2 firePointDistance = firePoint.position - GunHolder.localPosition;
                            Vector2 targetPos = grapplePoint - firePointDistance;
                            GunHolder.position = Vector2.Lerp(GunHolder.position, targetPos, Time.deltaTime * launchSpeed);
                            hasGrappled = true;
                        }
                    }
                }
                else if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    grapplingRope.enabled = false;
                    springJoint.enabled = false;
                    rb.gravityScale = 1;
                    if (IsMirrored)
                    {
                        rb.gravityScale = -1;
                    }
                    if (hasGrappled)
                    {
                        canShoot = false;
                    }
                    



                }
                //else
                //{
                //    Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                //    RotateGun(mousePos, true);
                //}

            }
            else
            {
                Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                RotateGun(mousePos, true);
            }


        }
    }

    void RotateGun(Vector3 lookPoint , bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;
        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;

        if (rotateOverTime && allowRotationOverTime)
        {
            gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
        else
        {
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void SetGrapplePoint()
    {
        Vector2 distanceVector = mainCamera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;

        if (Physics2D.Raycast(firePoint.position,distanceVector.normalized))
        {
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized);

            if (hit.transform.gameObject.layer == grappableLayerNumber || grappleToAll)
            {
                if (Vector2.Distance(hit.point,firePoint.position) <= maxDistance || !hasMaxDistance)
                {
                    grapplePoint = hit.point;
                    grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                    grapplingRope.enabled = true;
                }
            }
        }
    }
    public void Grapple()
    {
        soundManager.PlaySound("Grappling");
        springJoint.autoConfigureDistance = false;
        if (!launchToPoint && !autoConfigureDistance)
        {
            springJoint.distance = targetDistance;
            springJoint.frequency = targetFrequency;
        }
        if (!launchToPoint)
        {
            if (autoConfigureDistance)
            {
                springJoint.autoConfigureDistance = true;
                springJoint.frequency = 0;
            }

            springJoint.connectedAnchor = grapplePoint;
            springJoint.enabled = true;
        }
        else
        {
            switch (launchType)
            {
                case LaunchType.Transform_Launch:
                    rb.gravityScale = 0;
                    rb.velocity = Vector2.zero;
                    break;
                case LaunchType.Physics_Launch:
                    springJoint.connectedAnchor = grapplePoint;
                    Vector2 distanceVector = firePoint.position - GunHolder.position;

                    springJoint.distance = distanceVector.magnitude;
                    springJoint.frequency = launchSpeed;
                    springJoint.enabled = true;
                    break;
                default:
                    break;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistance);
        }
    }
    public void SetActive(bool statement)
    {
        gameObject.SetActive(statement);
    }
}
