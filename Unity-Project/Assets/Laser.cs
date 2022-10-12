using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject PlayerObject;
    int shapeMode;
    private ShapeShift script;
    // Start is called before the first frame update
    void Start()
    {
        script = PlayerObject.GetComponent<ShapeShift>();
    }

    // Update is called once per frame
    void Update()
    {
        shapeMode = script.ShapeMode;
        if (shapeMode == 2)
        {
            Physics2D.IgnoreCollision(PlayerObject.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>(), true);
        }
        else
        {
            Physics2D.IgnoreCollision(PlayerObject.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>(), false);
        }
    }

}
