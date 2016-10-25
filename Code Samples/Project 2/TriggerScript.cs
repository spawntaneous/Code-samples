
using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour
{
    
 
    GameObject spawnControllerObj;
    public  int triggerNo;

    void Awake()
    {
        spawnControllerObj = GameObject.FindGameObjectWithTag("SpawnController");
    }


   public void OnTriggerEnter(Collider Player)
    {
        if (Player.gameObject.tag == "Player")
        {
            Debug.Log("Triggered");
            spawnControllerObj.GetComponent<SpawnController>().SpawnRelay(triggerNo);
        }
    }
 
}
