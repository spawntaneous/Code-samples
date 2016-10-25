using UnityEngine;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour 

{

	public static GameObject currentGun;
	public static float distToPickGun  = 6f;
	public static float throwGunUpForce = 100f;
	public static float throwGunForwardForce = 300f;
	public static int waitFrameForSwitchGuns = -1;

	public static float walkAcceleration = 5f;
	public static float walkAccelAirRatio = 0.1f;
	public static float walkDeacceleration = 5f;
	public static float walkDeaccelerationVolx;
	public static float walkDeaccelerationVolz;
	public static GameObject cameraObject;
	public static float maxWalkSpeed = 20f;
	public static Vector2 horizontalMovement;
	public static float jumpVelocity = 20f;
	public static bool  grounded = false;
	public static float maxSlope =60f;

	public static float crouchRatio =0.3f;
	public static float transitionToCrouchSec = 0.2f;
	public static float crouchingVelocity;
	public static float currentCrouchRatio = 1f;
	public static float originalLocalScaleY;
	public static float crouchLocalScaleY;
	public static GameObject collisionDetectionSphere;
	public static GameObject currentCell;

	public static float health = 100f;
	public static GameObject deadBodyPrefab;
	
	public static float waitToPickupAmmo = 0f;

void Awake ()
	{
		currentCrouchRatio = 1f;
		originalLocalScaleY = transform.localScale.y;
		crouchLocalScaleY = transform.localScale.y * crouchRatio;
	}

void LateUpdate ()
	{
		waitFrameForSwitchGuns -= 1;
		waitToPickupAmmo -= Time.deltaTime;
  
		originalLocalScaleY = Mathf.Lerp(crouchLocalScaleY, originalLocalScaleY, currentCrouchRatio);
  if(Input.GetButton("Crouch"))
  	currentCrouchRatio = Mathf.SmoothDamp(currentCrouchRatio, 0, ref crouchingVelocity, transitionToCrouchSec);
  if(Input.GetButton("Crouch") == false && collisionDetectionSphere.GetComponent<CollisionDetectionSphereScript>().collisionDetected == false)
  	currentCrouchRatio = Mathf.SmoothDamp(currentCrouchRatio, 1, ref crouchingVelocity, transitionToCrouchSec);
  
      
  horizontalMovement = Vector2(rigidbody.velocity.x, rigidbody.velocity.z);
  if(horizontalMovement.magnitude > maxWalkSpeed)
	{
	  horizontalMovement =horizontalMovement.normalized;
	  horizontalMovement *= maxWalkSpeed;
	  
	} 
	 rigidbody.velocity.x = horizontalMovement.x;
	 rigidbody.velocity.z = horizontalMovement.y;
	 
	 
	 if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") ==0 && grounded)
	 {
	 	rigidbody.velocity.x = Mathf.SmoothDamp(rigidbody.velocity.x, 0, walkDeaccelerationVolx, walkDeacceleration);
	 	rigidbody.velocity.z = Mathf.SmoothDamp(rigidbody.velocity.z, 0, walkDeaccelerationVolz, walkDeacceleration);
	 }
      
     transform.rotation =Quaternion.Euler(0, cameraObject.GetComponent<MouseLookScript>().currentYRotation, 0);
     
     /*if(grounded)
      rigidbody.AddRelativeForce(Input.GetAxis("Horizontal") * walkAcceleration * Time.deltaTime, 0,Input.GetAxis("Vertical") * walkAcceleration * Time.deltaTime);
      else
      rigidbody.AddRelativeForce(Input.GetAxis("Horizontal") * walkAcceleration * walkAccelAirRatio * Time.deltaTime, 0,Input.GetAxis("Vertical") * walkAcceleration * walkAccelAirRatio * Time.deltaTime);
         */
	if(Input.GetButtonDown("Jump") && grounded)
	rigidbody.AddForce(0,jumpVelocity,0);
	
	
	if(health <= 0f)
	{
		currentGun.GetComponent<GunScript>().beingHeld = false;
		currentGun.rigidbody.AddRelativeForce(Vector3(0, throwGunUpForce, throwGunForwardForce));
		Instantiate(deadBodyPrefab, transform.position, transform.rotation);
		collider.enabled = false;
		cameraObject.GetComponent<AudioListener>().enabled = false;
		enabled = false;
	}
	

  }
  void FixedUpdate (){
  if (grounded)
  rigidbody.AddRelativeForce(Input.GetAxis("Horizontal") * walkAcceleration , 0,Input.GetAxis("Vertical") * walkAcceleration );
      else
      rigidbody.AddRelativeForce(Input.GetAxis("Horizontal") * walkAcceleration * walkAccelAirRatio, 0,Input.GetAxis("Vertical") * walkAcceleration * walkAccelAirRatio);
  
  }
  
void OnCollisionStay ( Collision collision  ){
	foreach(ContactPoint contact in collision.contacts)
	{
		if(Vector3.Angle(contact.normal, Vector3.up) < maxSlope)
		{
		grounded = true;
		}
		
}
}

void OnCollisionExit (){
	grounded = false;
}

void OnTriggerExit (){
	currentCell = null;
}
void OnTriggerStay ( Collider hitTrigger  ){
if(hitTrigger.tag == "AIpathCell")
	currentCell = hitTrigger.gameObject;
if (hitTrigger.transform.tag == "StringGoingUp")
if (!Input.GetButton("Jump") && Vector3.Angle(rigidbody.velocity, hitTrigger.transform.forward) < 90)
if (rigidbody.velocity.y > 0)
    rigidbody.velocity.y = 0;
    if (hitTrigger.transform.tag == "StairGoingDown")
 if (!Input.GetButton("Jump") && Vector3.Angle(rigidbody.velocity, hitTrigger.transform.forward) < 90) 
 rigidbody.AddForce(0,-100,0);
        
        
        
        
	GunScript current = null;
		if(currentGun)
		{
			current = currentGun.GetComponent<GunScript>();
		}
	ammopickupScript ammo = null;
	GunScript gun = null;
		if(hitTrigger.tag == "AmmoPickup" && waitToPickupAmmo <= 0)
		{
		ammo = hitTrigger.gameObject.GetComponent<ammopickupScript>();
		
		
			if(current.currentExtraAmmo < current.maxExtraAmmo)
			{
				if(ammo.fromGun)
				{
					gun=ammo.gun.GetComponent<GunScript>();
					if(gun.currentExtraAmmo > 0 && gun.ammoType == current.ammoType && ammo.gun != currentGun)
					{
						if(gun.currentExtraAmmo >= current.maxExtraAmmo - current.currentExtraAmmo)
						{
							gun.currentExtraAmmo -= current.maxExtraAmmo - current.currentExtraAmmo;
							current.currentExtraAmmo = current.maxExtraAmmo;
							
						
						}
						if(gun.currentExtraAmmo < current.maxExtraAmmo - current.currentExtraAmmo)
						{
							current.currentExtraAmmo += gun.currentExtraAmmo; 
							gun.currentExtraAmmo = 0;
						}
			  	         if(ammo.pickupSound)
			 			 ammo.gameObject.GetComponent<AudioSource>().Play();
					}		
				}
					if (!ammo.fromGun)
					{
						if(current.ammoType == ammo.ammoType || ammo.ammoType == -1)
						{
							if(ammo.ammoAmount > 0 && !ammo.unlimitedAmmo)
							{
								if(ammo.ammoAmount >= current.maxExtraAmmo - current.currentExtraAmmo)
								{
										ammo.ammoAmount -= current.maxExtraAmmo - current.currentExtraAmmo;
										current.currentExtraAmmo = current.maxExtraAmmo;
							
						
								}
								if(ammo.ammoAmount < current.maxExtraAmmo - current.currentExtraAmmo)
								{
									current.currentExtraAmmo += gun.currentExtraAmmo; 
									ammo.ammoAmount = 0;
								
								}
								if(ammo.pickupSound)
						         ammo.gameObject.GetComponent<AudioSource>().Play();
							} 
								if(ammo.unlimitedAmmo)
							    {
									 current.currentExtraAmmo = current.maxExtraAmmo;
									 if(ammo.pickupSound)
							          ammo.gameObject.GetComponent<AudioSource>().Play();
									 
								}
							}
						}
			
					}
		
		
			}
}





}

//Done By Sidharth Varma Kanumuri