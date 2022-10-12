using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeShift : MonoBehaviour
{
    
    private Animator anim;
    public int ShapeMode = 1;
    public int previous = 1;
    bool hasChanged = false;

    public int ShapeToChange;
        // 1 == square , 2 triangle , 3 == cirlce;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        previous = 1;
    }

    // Update is called once per frame
    void Update()
    {
       
        //if (Input.GetButtonDown("ShapeShift"))
        //{
        //    previous = ShapeMode;
        //    //anim.SetInteger("FromShape", ShapeMode);
        //    ShapeMode++;
        //    if (ShapeMode > 3)
        //    {
        //        ShapeMode = 1;
        //    }
        //    ShapeToChange = ShapeMode;
        //    anim.SetInteger("FromShape", previous);
        //    anim.SetInteger("ShapeMode", ShapeToChange);
        //}
       
    }
    void SetShapeMode(int mode)
    {
        previous = ShapeMode;
        anim.SetInteger("FromShape", previous);
        ShapeMode = mode;
        ShapeToChange = ShapeMode;
        anim.SetInteger("ShapeMode", ShapeToChange);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
   
            if (collision.gameObject.CompareTag("ShapeChanger"))
            {
                if (collision.gameObject.GetComponent<DisplayShape>().shapeModeToChange != ShapeMode)
                {
                previous = ShapeMode;
                ShapeToChange = collision.gameObject.GetComponent<DisplayShape>().shapeModeToChange;
                SetShapeMode(collision.gameObject.GetComponent<DisplayShape>().shapeModeToChange);
               
                }
              
            }
      }
        
    
  
}
