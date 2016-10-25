using UnityEngine;
using System.Collections;

public class EnemyBodyPartScript : MonoBehaviour {
public static GameObject enemyBody;

public static Vector3 addForceVector;

public static float damageMultiplyer = 1;

 

void Update (){

    if (addForceVector != Vector3.zero && !enemyBody.GetComponent<EnemyBodyScript>().enabled)

    {

        if (rigidbody)

            rigidbody.AddForce(addForceVector);

        else

            transform.parent.rigidbody.AddForce(addForceVector);

        addForceVector = Vector3.zero;

    }

}
}
//Done By Sidharth Varma Kanumuri