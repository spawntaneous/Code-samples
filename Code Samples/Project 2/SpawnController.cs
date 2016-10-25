using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour
{
    //  public int randomSpawn;
    public List<GameObject> spawns;
    public GameObject[] car;
    public GameObject[] biker;
    public GameObject[] brawler;
    public GameObject[] bully;
    public GameObject[] bicycle;
    public GameObject[] cart;
    public GameObject[] cops;
    public GameObject[] drunkard; 
    public GameObject[] dumpster;
    public GameObject[] mascot;
    public GameObject[] nerd;
    public GameObject[] pedestrian;
    public GameObject[] rickshaw;
    public GameObject[] security;
    public GameObject[] skateboard;
    public GameObject[] trashCan;
    public GameObject[] wetSignBoard;
    public GameObject[] toyTrain;
    public GameObject[] skateboarder;
    public GameObject[] manHole;
   
    public void Awake()
    {
        //  randomSpawn = Random.Range(0, spawns.Count - 1);
        foreach (var currentSpawn in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            
            {
                if (currentSpawn.name == "SpawnPoint")
                    spawns.Add(currentSpawn.gameObject);  // Add current spawnpoint
                
            }
        }
    }


    public void SpawnRelay(int triggerNo)  // relay triger information to specific waypoint
    {
        
        foreach (var currentSpawn in spawns)
        {
            if (currentSpawn.GetComponent<Spawner>().spawnPointno == triggerNo)
            {
                Debug.Log("SpawnRelay working");
                currentSpawn.GetComponent<Spawner>().SpawnEnemy();
            }
        }
        

    }


    public GameObject CarSpawn()
    {
        //Debug.Log("Car Spawn() working");
        int randomSpawn;
        randomSpawn = Random.Range(0, car.Length);
        return car[randomSpawn];
    }
    public GameObject BikerSpawn()
    {
        //Debug.Log("Biker Spawn() working");
        int randomSpawn;
        randomSpawn = Random.Range(0, biker.Length);
        return biker[randomSpawn];
    }
    public GameObject BrawlerSpawn()
    {
        //Debug.Log("Brawler Spawn() working");
        int randomSpawn;
        randomSpawn = Random.Range(0, brawler.Length);
        return brawler[randomSpawn];
    }
    public GameObject BullySpawn()
    {
        //Debug.Log("Bully Spawn() working");
        int randomSpawn;
        randomSpawn = Random.Range(0, bully.Length);
        return bully[randomSpawn];
    }
    public GameObject BicycleSpawn()
    {
        //Debug.Log("Bicycle Spawn() working");
        int randomSpawn;
        randomSpawn = Random.Range(0, bicycle.Length);
        return bicycle[randomSpawn];
    }
    public GameObject CartSpawn()
    {
       // Debug.Log("Cart Spawn() working");
        int randomSpawn;
        randomSpawn = Random.Range(0, cart.Length);
        return cart[randomSpawn];
    }
    public GameObject CopsSpawn()
    {
        //Debug.Log("Cops Spawn() working");
        int randomSpawn;
        randomSpawn = Random.Range(0, cops.Length);
        return cops[randomSpawn];
    }
    public GameObject DrunkardSpawn()
    {
        //Debug.Log("Drunkard Spawn() working");
        int randomSpawn;
        randomSpawn = Random.Range(0, drunkard.Length);
        return drunkard[randomSpawn];
    }
    public GameObject DumpsterSpawn()
    {
        //Debug.Log("Dumpster Spawn() working");
        int randomSpawn;
        randomSpawn = Random.Range(0, dumpster.Length);
        return dumpster[randomSpawn];
    }
    public GameObject MascotSpawn()
    {
        //Debug.Log("Mascot Spawn() working");
        int randomSpawn;
        randomSpawn = Random.Range(0, mascot.Length);
        return mascot[randomSpawn];
    }
    public GameObject NerdSpawn()
    {
        //Debug.Log("Nerd Spawn() working");
        int randomSpawn;
        randomSpawn = Random.Range(0, nerd.Length);
        return nerd[randomSpawn];
    }
    public GameObject PedestrianSpawn()
    {
        //Debug.Log("Pedestrian Spawn() working");
        int randomSpawn;
        randomSpawn = Random.Range(0, pedestrian.Length);
        return pedestrian[randomSpawn];
    }
    public GameObject RickshawSpawn()
    {
        //Debug.Log("Rickshaw Spawn() working");
        int randomSpawn;
        randomSpawn = Random.Range(0, rickshaw.Length);
        return rickshaw[randomSpawn];
    }
    public GameObject SecuritySpawn()
    {
        //Debug.Log("Security Spawn() working");
        int randomSpawn;
        randomSpawn = Random.Range(0, security.Length);
        return security[randomSpawn];
    }
    public GameObject SkateboardSpawn()
    {
        //Debug.Log("Skateboard Spawn() working");
        int randomSpawn;
        randomSpawn = Random.Range(0, skateboard.Length);
        return skateboard[randomSpawn];
    }
    public GameObject TrashCanSpawn()
    {
       // Debug.Log("TrashCan Spawn() working");
        int randomSpawn;
        randomSpawn = Random.Range(0, trashCan.Length);
        return trashCan[randomSpawn];
    }
    public GameObject WetSignBoardSpawn()
    {
       // Debug.Log("WetSignBoard Spawn() working");
        int randomSpawn;
        randomSpawn = Random.Range(0, wetSignBoard.Length);
        return wetSignBoard[randomSpawn];
    }
    public GameObject ToyTrainSpawn()
    {
      //  Debug.Log("ToyTrain Spawn() working");
        int randomSpawn;
        randomSpawn = Random.Range(0, toyTrain.Length);
        return toyTrain[randomSpawn];
    }
    public GameObject SkateboarderSpawn()
    {
      //  Debug.Log("Skateboarder Spawn() working");
        int randomSpawn;
        randomSpawn = Random.Range(0, skateboarder.Length);
        return skateboarder[randomSpawn];
    }
    public GameObject ManHoleSpawn()
    {
      //  Debug.Log("ManHole Spawn() working");
        int randomSpawn;
        randomSpawn = Random.Range(0, manHole.Length);
        return manHole[randomSpawn];
    }
}