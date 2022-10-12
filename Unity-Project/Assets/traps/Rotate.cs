using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float Speed = 2f;
    int direction=1;
    public PlayerMovement movement;
    public bool isPlayer;
    void Update()
    {
        if (isPlayer)
        {
            direction = movement.direction;
        }
       
        transform.Rotate(0, 0, 360 * Speed* direction * Time.deltaTime);

    }
}
