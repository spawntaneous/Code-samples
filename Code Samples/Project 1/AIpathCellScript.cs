using UnityEngine;
using System.Collections;

public class AIpathCellScript : MonoBehaviour {
GameObject doors= new Array();

 

void OnTriggerEnter ( Collider other  ){

    if (other.tag == "AIpathDoor")

        doors.Push(other.gameObject);

}
}

//Done By Sidharth Varma Kanumuri