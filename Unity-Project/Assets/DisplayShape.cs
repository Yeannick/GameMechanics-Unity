using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayShape : MonoBehaviour
{
    public GameObject Square;
    public GameObject Triangle;
    public GameObject Circle;
    public int shapeModeToChange;
    // Start is called before the first frame update
    void Start()
    {
        switch (shapeModeToChange)
        {
            case 1:
                Square.SetActive(true);
                Triangle.SetActive(false);
                Circle.SetActive(false);
                break;
            case 2:
                Square.SetActive(false);
                Triangle.SetActive(true);
                Circle.SetActive(false);
                break;
            case 3:
                Square.SetActive(false);
                Triangle.SetActive(false);
                Circle.SetActive(true);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
