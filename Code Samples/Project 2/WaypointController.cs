using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointController : MonoBehaviour {

    public List<GameObject> wayPoints;
    public Transform playerTransform;
   
    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
       wayPoints.AddRange(GameObject.FindGameObjectsWithTag("Waypoint"));
        wayPoints.AddRange(GameObject.FindGameObjectsWithTag("WaypointEdge"));
        StartCoroutine(WaitAndUpdate());

    }

   public LayerMask mask = 1 << 8;

    public int randomWaypoint;
    public GameObject GetRandomWaypoint() // return a random waypoints game object
    {
        
        randomWaypoint = Random.Range(0, wayPoints.Count - 1); // return a random wapoint's gameobject

        return wayPoints[randomWaypoint];
    }

     IEnumerator WaitAndUpdate()  // find the closest waypoint to the player and initialize it 
    {
        while (true)
        {
            GameObject closestToPlayer = FindClosestWaypoint(playerTransform);
            closestToPlayer.GetComponent<Waypoint>().initilalizeData();
            yield return new WaitForSeconds(3);
        }
    }
   

    public GameObject FindClosestWaypoint(Transform inTransform) // retreives transforms of waypoints, Function to find closest waypoint
    
    {
    // Vector3 inPosition; // position of the waypoints
   
        Collider[] closeWaypoints;
        int sphereDistance = 1;
        int breakWhile = 1;




        var inPosition = inTransform.position;

        closeWaypoints = Physics.OverlapSphere(inPosition, 1, mask);
        while (closeWaypoints.Length < 1 && breakWhile < 10)
            {
                closeWaypoints = Physics.OverlapSphere(inPosition, sphereDistance, mask);
                sphereDistance += 1;
                breakWhile++;
            }
        
          if (closeWaypoints.Length > 0)
            {
                 return closeWaypoints[0].gameObject;
            }
             else
              {
                  return null;
              }

    }


    void Update()
    {

    }
}
