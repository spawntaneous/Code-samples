using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {

public static bool  reloading = false;
public static Animation reloadAnimation;
public static AudioSource reloadSound;
public static string reloadAnimationString;

public static bool  beingHeld = false;
public static GameObject outsideBox;
int countToThrow = -1;
Transform playerTransform;
PlayerMovementScript playerMovementScript;
GameObject cameraObject;
Transform cameraTransform;
float targetXRotation;
float targetYRotation;
float targetXRotationV;
float targetYRotationV;

public static float rotateSpeed = 0.3f;
public static float holdHeight = -0.5f;
public static float holdSide = 0.3f;

public static float ratioHipHold = 1;
public static float hipToAimSpeed = 0.1f;
float ratioHipHoldV;  

public static float aimRatio = 0.4f;

public static float zoomAngle = 30;
public static float fireSpeed = 15;
float waitTillNextFire = 0;
public static GameObject bullet;
public static GameObject bulletSpawn;

public static float shootAngleRandomizationAiming = 5;
public static float shootAngleRandomizationNotAiming = 15;

public static float recoilAmount = 0.5f;
public static float recoilRecoverTime = 0.2f;
float currentRecoilZPos;
float currentRecoilZPosV;

public static GameObject bulletSound;
public static GameObject muzzleFlash;

public static float gunbobAmountX = 0.5f;
public static float gunbobAmountY = 0.5f;
public static float currentGunbobX;
public static float currentGunbobY;
public static GameObject[] gunModelObjects;
public static int clipSize = 25;
public static int currentClip = 25;
public static int maxExtraAmmo = 100;
public static int currentExtraAmmo = 100;
public static int ammoType = 0;

public static Texture bulletHudTexture;
public static Rect ammoCountRect = new Rect(25,25,50,25);
public static int ammoStartX = 100;
public static int ammoY = 25;
public static Vector2 ammosize = Vector2(10,25);
public static int ammoSpacing = 4;
public static Rect ammoDecorationHudRect = new Rect(25,25,50,25);
public static Texture ammoDecorationTexture;

void Awake (){
	cameraTransform = GameObject.FindWithTag("MainCamera").transform;
	countToThrow = -1;
	playerTransform = GameObject.FindWithTag("Player").transform;
	playerMovementScript = GameObject.FindWithTag("Player").GetComponent<PlayerMovementScript>();
	cameraObject = GameObject.FindWithTag("MainCamera");
}

void LateUpdate (){
	if(currentClip > clipSize)
	currentClip = clipSize;
	if(currentExtraAmmo > maxExtraAmmo)
	currentExtraAmmo = maxExtraAmmo;
	if(currentClip < 0)
	currentClip = 0;
	if(currentExtraAmmo < 0)
	currentExtraAmmo = 0;

  if(beingHeld)
 { 
 	if(!reloading && Input.GetButtonDown("Reload") && currentClip < clipSize && currentExtraAmmo > 0)
 	{
 	reloading = true;
 	reloadAnimation.Play(reloadAnimationString);
 	reloadSound.Play();
 	} 	
 		if(!reloading && Input.GetButtonDown("Fire1") && currentClip == 0 && currentExtraAmmo > 0)
 		{
 		reloading = true;
 		reloadAnimation.Play(reloadAnimationString);
 		reloadSound.Play();
 		} 	
 	if(reloading && !reloadAnimation.IsPlaying(reloadAnimationString))
 	{
 		if(currentExtraAmmo >= clipSize - currentClip)
 		{
 			currentExtraAmmo -= clipSize - currentClip;
 			currentClip = clipSize;
 		}
 		
 		
 		if(currentExtraAmmo < clipSize - currentClip)
 		{
 			currentClip += currentExtraAmmo;
 			currentExtraAmmo = 0;
 		}
 		
 		reloading = false;
 	}
 	foreach(GameObject modelObject in gunModelObjects)
 	{
 	 modelObject.layer = 8;
 	}
 	rigidbody.useGravity = false;
 	outsideBox.GetComponent<Collider>().enabled = false;
 	currentGunbobX = Mathf.Sin(cameraObject.GetComponent<MouseLookScript>().headbobStepCounter) * gunbobAmountX * ratioHipHold;
 	currentGunbobY = Mathf.Sin(cameraObject.GetComponent<MouseLookScript>().headbobStepCounter * 2) * gunbobAmountY *  ratioHipHold;
 	
 
 	GameObject holdSound;
 	GameObject holdMuzzleFlash;
 	if(Input.GetButton("Fire1") && currentClip > 0 && !reloading)
 	{
 		if(waitTillNextFire <= 0)
 		{
 			currentClip -= 1;
 		
 		 	if(bullet)
 		 	{
 		 	Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
 		 	if(bulletSound)
 		 		{holdSound = Instantiate(bulletSound, bulletSpawn.transform.position, bulletSpawn.transform.rotation);}
 		 	if(muzzleFlash)
 		 		{holdSound = Instantiate(muzzleFlash, bulletSpawn.transform.position, bulletSpawn.transform.rotation);}	
 		 	targetXRotation += (Random.value - 0.5f) * Mathf.Lerp(shootAngleRandomizationAiming, shootAngleRandomizationNotAiming, ratioHipHold);
 		 	targetYRotation += (Random.value - 0.5f) * Mathf.Lerp(shootAngleRandomizationAiming, shootAngleRandomizationNotAiming, ratioHipHold);
 			 currentRecoilZPos -= recoilAmount;
 			 waitTillNextFire = 1;
 			 
 			}
 		}
 	}
 	waitTillNextFire -= Time.deltaTime * fireSpeed;
 	
 	if(holdSound)
 		holdSound.transform.parent = transform;
 	
 		
 	if(holdMuzzleFlash)
 		holdMuzzleFlash.transform.parent = transform;
 	
 	
 	currentRecoilZPos = Mathf.SmoothDamp( currentRecoilZPos, 0, currentRecoilZPosV, recoilRecoverTime);
 	
 	
 	cameraObject.GetComponent<MouseLookScript>().currentTargetCameraAngle = zoomAngle;
 	
 	if(Input.GetButton("Fire2") && !reloading)
 	{
 		cameraObject.GetComponent<MouseLookScript>().currentAimRatio = aimRatio;
 		ratioHipHold = Mathf.SmoothDamp(ratioHipHold, 0, ratioHipHoldV, hipToAimSpeed);
 	}
 		if(Input.GetButton("Fire2") == false || reloading)
 		{
 		ratioHipHold = Mathf.SmoothDamp(ratioHipHold, 1, ratioHipHoldV, hipToAimSpeed);	
 		cameraObject.GetComponent<MouseLookScript>().currentAimRatio = 1;
 		}
 		
	transform.position = cameraObject.transform.position + (Quaternion.Euler(0,targetYRotation,0) * Vector3(holdSide * ratioHipHold + currentGunbobX, holdHeight * ratioHipHold + currentGunbobY, 0) + Quaternion.Euler(targetXRotation, targetYRotation, 0) * Vector3(0,0,currentRecoilZPos));
	
	targetXRotation = Mathf.SmoothDamp( targetXRotation, cameraObject.GetComponent<MouseLookScript>().xRotation, targetXRotationV, rotateSpeed);
    targetYRotation = Mathf.SmoothDamp( targetYRotation, cameraObject.GetComponent<MouseLookScript>().yRotation, targetYRotationV, rotateSpeed);
    
    transform.rotation = Quaternion.Euler(targetXRotation, targetYRotation, 0);
 }  
 if(!beingHeld)
 {
 	foreach(GameObject modelObject in gunModelObjects)
 	{
 	 modelObject.layer = 0;
 	}
 	rigidbody.useGravity = true;
 	outsideBox.GetComponent<Collider>().enabled = true;
 	
 	countToThrow -= 1;
 	if (countToThrow == 0)
 		rigidbody.AddRelativeForce(0, playerMovementScript.throwGunUpForce, playerMovementScript.throwGunForwardForce);
    float angle = Vector3.Angle(outsideBox.transform.position - cameraTransform.position, outsideBox.transform.position + (cameraTransform.right * outsideBox.transform.localScale.magnitude) - cameraTransform.position);
 	if(Vector3.Angle(outsideBox.transform.position - cameraTransform.position, cameraTransform.forward) <= angle)
 	if(Vector3.Distance(transform.position, playerTransform.position) < playerMovementScript.distToPickGun && Input.GetButtonDown("Use Key") && playerMovementScript.waitFrameForSwitchGuns <= 0)
 	{
 		playerMovementScript.waitToPickupAmmo = 2;
 		playerMovementScript.currentGun.GetComponent<GunScript>().beingHeld = false;
 		playerMovementScript.currentGun.GetComponent<GunScript>().countToThrow = 2;
  		playerMovementScript.currentGun = gameObject;
  		beingHeld = true;
  		targetYRotation = cameraObject.GetComponent<MouseLookScript>().yRotation = 180;
  		playerMovementScript.waitFrameForSwitchGuns = 2;
  		}
 	
 		
 }   
 }
 
 void OnGUI (){
 	if(beingHeld)
 	{
 		for(int i = 1; i <= currentClip; i++)
 		{
 			GUI.DrawTexture( new Rect(ammoStartX + ((i - 1) * (ammosize.x + ammoSpacing)), ammoY, ammosize.x, ammosize.y), bulletHudTexture);
 		}
 	 
 	   GUI.Label(ammoCountRect, currentExtraAmmo.ToString ());
 		GUI.DrawTexture( ammoDecorationHudRect, ammoDecorationTexture);
 	}
 	
 }
}
//Done By Sidharth Varma Kanumuri