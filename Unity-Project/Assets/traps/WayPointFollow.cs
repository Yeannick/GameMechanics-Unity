using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollow : MonoBehaviour
{
    [SerializeField] private GameObject[] WayPoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float Speed = 2.0f;

    [SerializeField] private bool Looping;
    private bool isReturning = false;
   
   private void Update()
    {
        if (!Looping)
        {
            if (Vector2.Distance(WayPoints[currentWaypointIndex].transform.position, transform.position) < 0.1f)
            {

                currentWaypointIndex++;

                if (currentWaypointIndex >= WayPoints.Length)
                {
                    currentWaypointIndex = 0;
                    
                }
            }
        }
        if (Looping)
        {
            LoopingWayPoint();
        }
       
        transform.position = Vector2.MoveTowards(transform.position, WayPoints[currentWaypointIndex].transform.position,Time.deltaTime * Speed);
    }
    private void LoopingWayPoint()
    {
        if (Vector2.Distance(WayPoints[currentWaypointIndex].transform.position, transform.position) < 0.1f)
        {
            if (!isReturning)
            {
                currentWaypointIndex++;
            }
            else if (isReturning)
            {
                currentWaypointIndex = Decrement(currentWaypointIndex);
            }
            if (currentWaypointIndex >= WayPoints.Length)
            {
                currentWaypointIndex = Decrement(currentWaypointIndex);
                isReturning = true;
            }
            if (currentWaypointIndex <= 0)
            {
                
                isReturning = false;
            }
        }

    }
    private int Decrement(int index)
    {
       return  --index;
    }
   
}
