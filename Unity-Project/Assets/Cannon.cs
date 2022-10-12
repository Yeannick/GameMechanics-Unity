using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    bool IsActive = false;

    public GameObject PlayerObject;
    private ShapeShift script;
    int ShapeMode;
    // Start is called before the first frame update
    void Start()
    {
       script =  PlayerObject.GetComponent<ShapeShift>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive)
        {
            Vector2 CannonPosition = transform.position;
            Vector2 mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 Direction = mouseposition - CannonPosition;
            transform.right = Direction;

        }
       // ShapeMode = script.ShapeMode;
       
    }
}
