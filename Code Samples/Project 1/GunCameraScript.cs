using UnityEngine;
using System.Collections;

public class GunCameraScript : MonoBehaviour {
GameObject cameraObject;
void Awake (){
	cameraObject = GameObject.FindWithTag("MainCamera");
}
void Update (){
	camera.fieldOfView = cameraObject.camera.fieldOfView;
}
}

//Done By Sidharth Varma Kanumuri