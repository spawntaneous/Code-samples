using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class InputTouch : MonoBehaviour {

    //Touch Input Variables
    public Touch buff;
    private float t_timer;
    public int state;
    public RaycastHit raybuff;
    public float minSwipeDist, maxSwipeTime;
    public bool couldBeSwipe = false;
    public Vector3 startPos;
    public float swipeStartTime;
    public bool dragging = false;
    public Vector3 myPos;

    //Swipe Vars
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    private List<Vector3> touchPositions = new List<Vector3>();

    //debug variables
    public string debug_var;

    public float timer
    {
        get { return this.t_timer; }
        set { this.t_timer = value; }
    }



    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0) { 
            
            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {

                    case TouchPhase.Began:
                        Debug.Log("Touch Began");
                        Ray ray = Camera.main.ScreenPointToRay(buff.position);
                        raybuff = new RaycastHit();
                        if (Physics.Raycast(ray, out raybuff))
                            switch (raybuff.transform.GetComponent<OurObject>().getType())
                            {

                                case "TAP_OBJ":
                                    state = 0;
                                    break;
                                case "DRAG_OBJ":
                                    state = 1;
                                    raybuff.transform.GetComponent<DragInput>().touchDown();
                                    break;
                                case "SWIPE_OBJ":
                                    state = 2;
                                   
                                    break;
                            }
                        break;
                    case TouchPhase.Ended:
                        switch (state)
                        {
                            case 0:
                                raybuff.transform.GetComponent<TapInput>().stopMoving();
                                break;
                            case 1:
                                raybuff.transform.GetComponent<DragInput>().touchUp();
                                break;
                            case 2:
                                fp = touchPositions[0]; //get first touch position from the list of touches
                                lp = touchPositions[touchPositions.Count - 1]; //last touch position 

                                //Check if drag distance is greater than 20% of the screen height
                                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                                {//It's a drag
                                 //check if the drag is vertical or horizontal 
                                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                                    {   //If the horizontal movement is greater than the vertical movement...
                                        if ((lp.x > fp.x))  //If the movement was to the right)
                                        {   //Right swipe
                                            Debug.Log("Right Swipe");
                                            raybuff.transform.GetComponent<SwipeInput>().stopMoving(1);
                                        }
                                        else
                                        {   //Left swipe
                                            Debug.Log("Left Swipe");
                                            raybuff.transform.GetComponent<SwipeInput>().stopMoving(2);
                                        }
                                    }
                                    else
                                    {   //the vertical movement is greater than the horizontal movement
                                        if (lp.y > fp.y)  //If the movement was up
                                        {   //Up swipe
                                            Debug.Log("Up Swipe");
                                            raybuff.transform.GetComponent<SwipeInput>().stopMoving(3);
                                        }
                                        else
                                        {   //Down swipe
                                            Debug.Log("Down Swipe");
                                            raybuff.transform.GetComponent<SwipeInput>().stopMoving(4);
                                        }
                                    }
                                }else { raybuff.transform.GetComponent<SwipeInput>().stopMoving(0); }
                                break;
                        }
                        break;
                    case TouchPhase.Moved:
                       
                        Debug.Log("Touch Moved");
                        switch (state)
                        {
                            case 1:
                                myPos = touch.position;
                                break;
                            case 2:
                                touchPositions.Add(touch.position);
                                break;
                        }


                        break;
                    case TouchPhase.Stationary:
                        Debug.Log("Touch Hold");
                        break;
                    case TouchPhase.Canceled:
                        Debug.Log("Touch Canceled");
                        break;

                    default:
                        break;

                }


            }

            

            
           
        }
        
	}
}
