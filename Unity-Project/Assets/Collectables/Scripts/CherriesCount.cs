using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CherriesCount : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textMesh;
    int AmountOfCherries = 0;
    void Start()
    {
        AmountOfCherries = PlayerPrefs.GetInt("Cherries");
        textMesh.text = "X " + AmountOfCherries;   
    }

    // Update is called once per frame
    void Update()
    {
        AmountOfCherries = PlayerPrefs.GetInt("Cherries");
        textMesh.text = "X " + AmountOfCherries;
    }
}
