using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{

    [SerializeField]
    private Transform targetToFollow;
    [SerializeField]
    Vector2 LevelLimitX;
    [SerializeField]
    Vector2 LevelLimitY;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(targetToFollow.position.x, LevelLimitX.x, LevelLimitX.y),
                                        Mathf.Clamp(targetToFollow.position.y, LevelLimitY.x, LevelLimitY.y),
                                        transform.position.z);
    }
}
