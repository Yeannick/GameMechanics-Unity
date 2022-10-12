using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float Speed = 2f;
    int shapeMode;
    public ShapeShift script;

    // Update is called once per frame
    void Update()
    {
       shapeMode = script.ShapeMode;
       if (shapeMode == 3)
         {
            transform.Rotate(0, 0, 360 * Speed * Time.deltaTime);
         }
        
    }
}
