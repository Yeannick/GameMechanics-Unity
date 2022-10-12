using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableHeart : MonoBehaviour
{
    // Start is called before the first frame update
    int playerHearts;
    public ParticleSystem particle;
    
    void Start()
    {
        playerHearts = PlayerPrefs.GetInt("PlayerLives");
    }

    // Update is called once per frame
    void Update()
    {
        playerHearts = PlayerPrefs.GetInt("PlayerLives");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerHearts < 3 )
            {
                soundManager.PlaySound("Heart");
                playerHearts += 1;
            
           

            PlayerPrefs.SetInt("PlayerLives", playerHearts);
                particle.transform.position = transform.position;
                Instantiate(particle);
            Destroy(gameObject);
            }
        }
    }
}
