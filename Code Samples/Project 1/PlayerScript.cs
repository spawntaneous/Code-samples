using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

    public Transform playerTransform;
    public GameObject closestWaypoint;
    public WaypointController waypointControllerScript;
    public List<Transform> playerPath = new List<Transform>();
    public int playerPathNo;
    public Transform targetTransform;
	public float smoothTime = 2f;
	public float minSpeed,maxSpeed; //minSpeed - higher number but low speed(eg. 3), maxSpeed - lower number max speed (eg.1)
    // Use this for initialization
    void Start ()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        waypointControllerScript = GameObject.FindGameObjectWithTag("WaypointController").GetComponent<WaypointController>();
        closestWaypoint = waypointControllerScript.FindClosestWaypoint(transform);

    }
	
	// Update is called once per frame
	void Update ()
    {
        float distanceEnemyWaypoint = Vector3.Distance(transform.position, closestWaypoint.transform.position);

        if (distanceEnemyWaypoint < distanceToWaypoint && playerPath.Count != playerPathNo - 1)
        {
            closestWaypoint = playerPath[playerPath.Count - playerPathNo].gameObject;
            getPlayerPathNo();
			if (closestWaypoint.GetComponent<Waypoint> ().speedUp)
			{
				smoothTime -= 0.9f;
				if(smoothTime>=minSpeed)
					smoothTime=minSpeed;
			}
			else if (closestWaypoint.GetComponent<Waypoint> ().speedDown)
			{	
				smoothTime += 0.9f;
				if(smoothTime<= maxSpeed)
					smoothTime=maxSpeed;
			}
        }

        if(playerPath.Count != playerPathNo -1)
             {
                 MoveAndRotate(closestWaypoint.transform.position);
             }
            else
            {
              MoveAndRotate(targetTransform.position);
            }
        }

    void getPlayerPathNo()
    {
        playerPathNo = playerPathNo + 1;
    }

  
    public float distanceToWaypoint;
    public float maxRotationSpeed = 0.1f;
    Vector3 velocity = Vector3.zero;

    void MoveAndRotate(Vector3 targetPosition)
    {

        Vector3 thisPosition;
        Quaternion toRotation;

        thisPosition = transform.position;
        float deltaTime = Time.deltaTime;
        

        targetPosition = new Vector3(targetPosition.x, thisPosition.y, targetPosition.z);

        transform.position = Vector3.SmoothDamp(thisPosition, targetPosition, ref velocity, smoothTime,maxSpeed,deltaTime);

        if (targetPosition - thisPosition != Vector3.zero)
        {
            toRotation = Quaternion.LookRotation(targetPosition - thisPosition);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, maxRotationSpeed);
        }



    }
}
