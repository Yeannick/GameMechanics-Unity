using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateShape : MonoBehaviour
{
    public float RotationX;
    public float RotationY;
    public float RotationZ;
    public float Speed = 2f;
    // 1 == square , 2 == triangle , 3 == circle 

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotationX * 360 * Speed * Time.deltaTime, RotationY * 360 * Speed * Time.deltaTime, RotationZ * 360 * Speed * Time.deltaTime);
    }
}
