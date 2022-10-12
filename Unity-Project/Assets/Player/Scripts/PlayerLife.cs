using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public GameObject SpawnPoint;
    public int PlayerLives =0;
    public int MaxPlayerLives = 3;
    public Image[] heartsSquare;
    public Image[] heartsTriangle;
    public Image[] heartsCirlce;
    public Sprite FullHeartSquare;
    public Sprite FullHeartTriangle;
    public Sprite FullHeartCircle;

    public Sprite EmptyHeart;

    private Animator p_Anim;
    private Rigidbody2D p_RigidBody;
    public GameObject PlayerGun;
    private SpriteRenderer p_RespawnSpriteRenderer;
    private Sprite p_RespawnSprite;
    private float p_ShapeMode;

    float timeSinceLastCall= 0;
    bool startCounting;

  
    // Start is called before the first frame update
    private void Start()
    {
        PlayerLives = PlayerPrefs.GetInt("PlayerLives");
        p_Anim = GetComponent<Animator>();
        p_RigidBody = GetComponent<Rigidbody2D>();
        p_RespawnSpriteRenderer = GetComponent<SpriteRenderer>();
       
     
    }
    private void Update()
    {
        PlayerLives = PlayerPrefs.GetInt("PlayerLives");
        if (startCounting)
        {
            timeSinceLastCall += Time.deltaTime;
        }
       

        if (timeSinceLastCall >= 1f)
        {
            startCounting = false;
        }
        p_ShapeMode = GetComponent<ShapeShift>().ShapeMode;

        switch (p_ShapeMode)
        {
            case 1:
                for (int i = 0; i < heartsSquare.Length; i++)
                {
                    if (i < PlayerLives)
                    {
                        heartsSquare[i].sprite = FullHeartSquare;
                    }
                    else
                    {
                        heartsSquare[i].sprite = EmptyHeart;
                    }
                    if (i <= MaxPlayerLives)
                    {
                        heartsSquare[i].enabled = true;
                    }
                    else if (i > MaxPlayerLives)
                    {
                        heartsSquare[i].enabled = false;
                    }
                }
                for (int i = 0; i < heartsTriangle.Length; i++)
                {
                   heartsTriangle[i].enabled = false;
                }
                for (int i = 0; i < heartsCirlce.Length; i++)
                {
                    heartsCirlce[i].enabled = false;
                }
                break;
            case 2:
                for (int i = 0; i < heartsTriangle.Length; i++)
                {
                    if (i < PlayerLives)
                    {
                        heartsTriangle[i].sprite = FullHeartTriangle;
                    }
                    else
                    {
                        heartsTriangle[i].sprite = EmptyHeart;
                    }
                    if (i <= MaxPlayerLives)
                    {
                        heartsTriangle[i].enabled = true;
                    }
                    else if (i > MaxPlayerLives)
                    {
                        heartsTriangle[i].enabled = false;
                    }
                }
                for (int i = 0; i < heartsSquare.Length; i++)
                {
                    heartsSquare[i].enabled = false;
                }
                for (int i = 0; i < heartsCirlce.Length; i++)
                {
                    heartsCirlce[i].enabled = false;
                }
                break;
            case 3:
                for (int i = 0; i < heartsCirlce.Length; i++)
                {
                    if (i < PlayerLives)
                    {
                        heartsCirlce[i].sprite = FullHeartCircle;
                    }
                    else
                    {
                        heartsCirlce[i].sprite = EmptyHeart;
                    }
                    if (i <= MaxPlayerLives)
                    {
                        heartsCirlce[i].enabled = true;
                    }
                    else if (i > MaxPlayerLives)
                    {
                        heartsCirlce[i].enabled = false;
                    }
                }
                 for (int i = 0; i < heartsSquare.Length; i++)
                {
                    heartsSquare[i].enabled = false;
                }
                for (int i = 0; i < heartsTriangle.Length; i++)
                {
                    heartsTriangle[i].enabled = false;
                }
                break;
            
        }
        
        
        
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.CompareTag("Traps"))
        {

            if (!startCounting)
            {
                Die();
                soundManager.PlaySound("Death");
            }
           
            startCounting = true;
        }
    }
    private void Die()
    {
      
        PlayerLives -= 1;
        PlayerPrefs.SetInt("PlayerLives", PlayerLives);
        p_RespawnSprite = p_RespawnSpriteRenderer.sprite;
        p_RigidBody.bodyType = RigidbodyType2D.Static;
        
        p_Anim.SetInteger("ShapeMode", 0);
        p_Anim.SetTrigger("Death");

        PlayerGun.GetComponent<SpriteRenderer>().enabled = false;
        if (PlayerLives > 0)
        {
            p_Anim.SetBool("Respawning", false);
            StartCoroutine( Respawn());
        }
        if (PlayerLives == 0)
        {
            p_Anim.SetBool("Respawning", false);
            
            StartCoroutine(RestartLevel());
        }
        
      
      
    }
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0.4f);
      
       
        p_RigidBody.bodyType = RigidbodyType2D.Dynamic;
        p_Anim.SetBool("Respawning", true);
        p_Anim.ResetTrigger("Death");
        this.transform.position = SpawnPoint.transform.position;
        p_RespawnSpriteRenderer.sprite = p_RespawnSprite;
        
        GetComponent<ShapeShift>().ShapeMode = 1;
        p_Anim.SetInteger("ShapeMode", GetComponent<ShapeShift>().ShapeMode);
        p_RigidBody.gravityScale = 1;
        GetComponent<PlayerMovement>().jumpPower = 6;
        GetComponent<MirroredBehaviour>().IsMirrored = false;
        Debug.Log(p_RespawnSprite);
       
    }
   IEnumerator RestartLevel()
    {
       // Debug.Log("RestartLevel");
        yield return new WaitForSeconds(0.3f);
        PlayerPrefs.SetInt("PlayerLives", 3);
        SceneManager.LoadScene("GameOver");
    }
}
