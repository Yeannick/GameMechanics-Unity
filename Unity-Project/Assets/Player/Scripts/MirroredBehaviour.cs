using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirroredBehaviour : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    public bool IsMirrored = false;

    
    private CircleCollider2D circle;
    // Start is called before the first frame update
    private void Start()
    {
        
        circle = GetComponent<CircleCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mirror"))
        {
            
            Debug.Log("HitMirror");
            if (IsMirrored)
            {
                rb.gravityScale *= -1;
                rb.velocity = new Vector2(rb.velocity.x, 10);
               
                IsMirrored = false;
            }
            else if (!IsMirrored)
            {
                rb.velocity = new Vector2(rb.velocity.x, -10);
                
                rb.gravityScale *= -1;
                IsMirrored = true;
               
            }
            
           

        }
    }
    
}
