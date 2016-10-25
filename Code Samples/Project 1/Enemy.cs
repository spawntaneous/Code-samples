using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
    public Transform playerTransform;
    public WaypointController waypointControllerScript;
    public GameObject closestWaypoint;
    public List<Transform> playerLocation = new List<Transform>();
    public bool decoy = false;
    public Transform targetTransform;
   
    // Use this for initialization
    void Awake ()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        waypointControllerScript = GameObject.FindGameObjectWithTag("WaypointController").GetComponent<WaypointController>();
        closestWaypoint = waypointControllerScript.FindClosestWaypoint(transform);
        if(decoy == false)
        {
            targetTransform = playerTransform;
        }
    }
	void Start ()
    {
       playerLocation = closestWaypoint.GetComponent<Waypoint>().GetPlayerLocation();
    }

    // Update is called once per frame

     
   
    void Update ()
    {
        float distanceEnemyWaypoint = Vector3.Distance(transform.position, closestWaypoint.transform.position);

       if(distanceEnemyWaypoint < distanceToWaypoint && playerLocation.Count > 1)
        {
           closestWaypoint = playerLocation[playerLocation.Count -2].gameObject;
           playerLocation = closestWaypoint.GetComponent<Waypoint>().GetPlayerLocation();
        }

        if (playerLocation.Count > 1)
        {
          MoveAndRotate(closestWaypoint.transform.position);
           
        }
        else
        {
          
            MoveAndRotate(targetTransform.position);
        }

       

    }
    public float smoothTime = 2.5f;
    public float distanceToWaypoint;
    public float maxRotationSpeed = 0.1f;
    Vector3 velocity = Vector3.zero;
    public float maxSpeed = 0f;

    void MoveAndRotate(Vector3 targetPosition)
    {

        Vector3 thisPosition;
        Quaternion toRotation;
        float deltaTime = Time.deltaTime;

        thisPosition = transform.position;

        targetPosition = new Vector3(targetPosition.x,thisPosition.y, targetPosition.z);

        transform.position = Vector3.SmoothDamp(thisPosition, targetPosition, ref velocity, smoothTime, maxSpeed, deltaTime);

        if (targetPosition - thisPosition != Vector3.zero)
        {
            toRotation = Quaternion.LookRotation(targetPosition - thisPosition);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, maxRotationSpeed);
        }
       

       
    }


}
