using UnityEngine;
using System.Collections;

public class MouseLookScript : MonoBehaviour {

public static float defaultCameraAngle = 60f;
public static float currentTargetCameraAngle = 60f;
public static float racioZoom = 1f;
public static float racioZoomV;
public static float racioZoomSpeed = 0.2f;
public static float looksensitivity = 5f;

private float xRotation;

private float yRotation;

private float currentXRotation;

private float currentYRotation;
private float xRotationV;
private float yRotationV;
float lookSmoothDamp=0.1f;
private float currentAimRatio = 1f;

public static float headbobSpeed = 1f;
float headbobStepCounter;
public static float headbobAmountX=1f;
public static float headbobAmountY=1f;
Vector3 parentLastPos;
public static float eyeHeightRatio = 0.9f;

	void Awake (){
	parentLastPos = transform.parent.position;
}

void Update (){
 	if(transform.parent.GetComponent<PlayerMovementScript>().grounded)
 		headbobStepCounter += Vector3.Distance(parentLastPos, transform.parent.position) * headbobSpeed;
 		transform.localPosition.x = Mathf.Sin(headbobStepCounter) * headbobAmountX * currentAimRatio;
 		transform.localPosition.y = (Mathf.Cos(headbobStepCounter * 2) * headbobAmountY * -1 * currentAimRatio) + (transform.parent.localScale.y * eyeHeightRatio) - (transform.parent.localScale.y/2);
 	
 	parentLastPos = transform.parent.position;
 	
 	if(currentAimRatio == 1)
 		racioZoom = Mathf.SmoothDamp(racioZoom, 1, racioZoomV, racioZoomSpeed);
 	else
 		racioZoom = Mathf.SmoothDamp(racioZoom, 0, racioZoomV, racioZoomSpeed);
 		
 	camera.fieldOfView = Mathf.Lerp(currentTargetCameraAngle, defaultCameraAngle, racioZoom);
 
  xRotation -= Input.GetAxis("Mouse Y") * looksensitivity * currentAimRatio;
  yRotation += Input.GetAxis("Mouse X") * looksensitivity * currentAimRatio;
  
  xRotation = Mathf.Clamp(xRotation, -90, 90);
  
  currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, xRotationV, lookSmoothDamp);
  currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, yRotationV, lookSmoothDamp);
  
 transform.rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);
 }
}

//Done By Sidharth Varma Kanumuri