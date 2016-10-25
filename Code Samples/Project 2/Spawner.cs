using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject spawnObject;
    public GameObject spawnControllerObj;
    public int spawnPointno;
    public bool setSpawnDecoy;
    public Transform DecoyTarget;

    public void Awake()
    {
        spawnControllerObj = GameObject.FindGameObjectWithTag("SpawnController");
        


    }

    public void Start()
    {

        switch (this.gameObject.tag)
        {
            case "CarSpawn":
                spawnObject = spawnControllerObj.GetComponent<SpawnController>().CarSpawn();
                //Debug.Log("Car Switch");
                break;
            case "BikerSpawn":
                spawnObject = spawnControllerObj.GetComponent<SpawnController>().BikerSpawn();
                //Debug.Log("Biker Switch");
                break;
            case "BrawlerSpawn":
                spawnObject = spawnControllerObj.GetComponent<SpawnController>().BrawlerSpawn();
                //Debug.Log("Brawler Switch");
                break;
            case "BullySpawn":
                spawnObject = spawnControllerObj.GetComponent<SpawnController>().BullySpawn();
                //Debug.Log("Bully Switch");
                break;
            case "BicycleSpawn":
                spawnObject = spawnControllerObj.GetComponent<SpawnController>().BicycleSpawn();
                //Debug.Log("Bicycle Switch");
                break;
            case "CartSpawn":
                spawnObject = spawnControllerObj.GetComponent<SpawnController>().CartSpawn();
                //Debug.Log("Cart Switch");
                break;
            case "CopsSpawn":
                spawnObject = spawnControllerObj.GetComponent<SpawnController>().CopsSpawn();
                //Debug.Log("Cops Switch");
                break;
            case "DrunkardSpawn":
                spawnObject = spawnControllerObj.GetComponent<SpawnController>().DrunkardSpawn();
                //Debug.Log("Drunkard Switch");
                break;
            case "DumpsterSpawn":
                spawnObject = spawnControllerObj.GetComponent<SpawnController>().DumpsterSpawn();
                //Debug.Log("Dumpster Switch");
                break;
            case "MascotSpawn":
                spawnObject = spawnControllerObj.GetComponent<SpawnController>().MascotSpawn();
                //Debug.Log("Mascot Switch");
                break;
            case "NerdSpawn":
                spawnObject = spawnControllerObj.GetComponent<SpawnController>().NerdSpawn();
                //Debug.Log("Nerd Switch");
                break;
            case "PedestrianSpawn":
                spawnObject = spawnControllerObj.GetComponent<SpawnController>().PedestrianSpawn();
                //Debug.Log("Pedestarian Switch");
                break;
            case "RickshawSpawn":
                spawnObject = spawnControllerObj.GetComponent<SpawnController>().RickshawSpawn();
                //Debug.Log("Rickshaw Switch");
                break;
            case "SecuritySpawn":
                spawnObject = spawnControllerObj.GetComponent<SpawnController>().SecuritySpawn();
                //Debug.Log("Security Switch");
                break;
            case "SkateboardSpawn":
                spawnObject = spawnControllerObj.GetComponent<SpawnController>().SkateboardSpawn();
                //Debug.Log("Skateboard Switch");
                break;
            case "WetSignBoardSpawn":
                spawnObject = spawnControllerObj.GetComponent<SpawnController>().WetSignBoardSpawn();
                //Debug.Log("WetSignBoard Switch");
                break;
            case "ToyTrainSpawn":
                spawnObject = spawnControllerObj.GetComponent<SpawnController>().ToyTrainSpawn();
               // Debug.Log("ToyTrain Switch");
                break;
            case "SkateboarderSpawn":
                spawnObject = spawnControllerObj.GetComponent<SpawnController>().SkateboarderSpawn();
                //Debug.Log("Skateboarder Switch");
                break;
            case "ManHoleSpawn":
                spawnObject = spawnControllerObj.GetComponent<SpawnController>().ManHoleSpawn();
                //Debug.Log("ManHole Switch");
                break;
            case "TrashCanSpawn":
                spawnObject = spawnControllerObj.GetComponent<SpawnController>().TrashCanSpawn();
              //  Debug.Log("TrashCan Switch");
                break;

            default:
                break;
                
        }


    }

    public void SpawnEnemy()        // function to instantiate enemy
    {
  
     //   if(targetTrigger.GetComponent<TriggerScript>().triggerMovement == true)
        {
         Instantiate(spawnObject, this.transform.position, this.transform.rotation) ;
            if (setSpawnDecoy == true)
            {
                spawnObject.GetComponent<Enemy>().decoy = true;
                spawnObject.GetComponent<Enemy>().targetTransform = DecoyTarget;
            }
            else
            {
                spawnObject.GetComponent<Enemy>().decoy = false;
            }
          
        }
        

    }



}