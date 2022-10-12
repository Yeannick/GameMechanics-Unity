using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    int PlayerCherries;
    public ParticleSystem particle;
    void Start()
    {
        PlayerCherries = PlayerPrefs.GetInt("Cherries");
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCherries = PlayerPrefs.GetInt("Cherries");
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            soundManager.PlaySound("Cherry");
            PlayerPrefs.SetInt("Cherries", PlayerCherries + 1);
            particle.transform.position = transform.position;
            Instantiate(particle);
            Destroy(gameObject);
        }
    }
}
