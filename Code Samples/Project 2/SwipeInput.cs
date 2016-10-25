using UnityEngine;
using System.Collections;

public class SwipeInput : MonoBehaviour {

    private bool isMoving = false;
    private int swipeDir = 0;

    public void StartMoving()
    {
        isMoving = true;
    }
    public void stopMoving(int swpdr)
    {
        swipeDir = swpdr;
        isMoving = false;
    }

    public int getSwipeDir()
    {
        return swipeDir;
    }



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
