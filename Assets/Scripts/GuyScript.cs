using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GuyScript : MonoBehaviour
{
    [Header("Setup (empty)")]
    //Variable for the characters fysics controller
    private Rigidbody2D rb;

    [Header("Character Stats")]
    //Variable for the characters movement speed
    public float speed;
    //Variables for wait time until the character can start moving to the next move point
    public float waitTime = 2f;
    private float waitTimer;

    //Checks the current jump point's jump force
    private float jumpPointJumpForce;

    [Header("Move Points With Conditions")]
    //Move points with conditions
    public Transform[] conditionMovementPoints;

    [Header("Move Points WithOut Conditions")]
    //Move points which are either on the #1 or #2 camera
    public Transform[] screenMovePoints;
    
    //Variable which check if characters last move point had a condition
    private bool justFromCondition;
    //Checks if character is ready to start... (does nothing at the moment)
    private bool readyToStart = true;

    [Header("Variables... which need to be accessed from other scripts")]
    //Checks if light is on (it is set from the Player Script/ LightBallMove Script)
    public bool LightIsOn;
    //Variables which help calculating character movement
    public Transform currentMovePoint;
    private Vector2 movePointVector;

    //Variables which tell what move target/camera is now/next
    private int MovePointNumber = 0;
    private int currentConditionMovePoint = 0;
    //private int currentJumpPosition = 0;

    

    //Start is called once in the beginning
    private void Start()
    {
        //Fetches the characters fysics controller
        rb = GetComponent<Rigidbody2D>();
        //Sets the current move point to the first move point
        currentMovePoint = screenMovePoints[0];
        //Sets the move timer to the wait value
        waitTimer = waitTime;
    }


    //Update is called once every frame
    private void Update()
    {
        //If character is ready to start moving
        if (readyToStart)
        {
            //Move target is only tracked in the x axis
            movePointVector = new Vector2(currentMovePoint.position.x, rb.position.y);
            //character moves to the current move target
            transform.position = Vector2.MoveTowards(transform.position, movePointVector, speed * Time.deltaTime);


        }        
        //If target is closer than 0.3 units to the target
        if (Vector2.Distance(transform.position, currentMovePoint.position) < 0.3f)
        {
            //If character wait timer is 0 or less
            if (waitTimer <= 0)
            {
                //Set wait timer equal to the wait time that has been set in the public variable
                waitTimer = waitTime;

                //If current move point number is not equal to the amount of points on the first screen transform list - 1 
                if (MovePointNumber != screenMovePoints.Length - 1)
                {
                    //If current move point is a move point with a condition
                    if (currentMovePoint == conditionMovementPoints[currentConditionMovePoint])
                    {
                        //Set new bool list to the interMode variable from the script which is on the current move point
                        bool[] interMode = currentMovePoint.GetComponent<InterPoint>().interMode;

                        //If the new interMode variable's second boolean is true
                        if (interMode[1])
                        {
                            //If the LightIsOn variable is not true which means the player has not activated the corresponding light
                            if (!LightIsOn)
                            {
                                //Set the character's next move target to current move target + 2 from the screenMovePoints list
                                MovePointNumber += 2;
                                currentMovePoint = screenMovePoints[MovePointNumber];
                                //Set the next move target with a condition to the next move point with a condition
                                currentConditionMovePoint++;
                                //Here you don't need to set the justFromCondition to true because the player is going to die anyway
                            }
                            else //Else if the LightIsOn variable IS true
                            {
                                //Set the character's next move target to the current move target + 1 from the screenMovePoints list
                                MovePointNumber++;
                                //Fetch the corresponding jump force from the move point's script
                                jumpPointJumpForce = currentMovePoint.GetComponent<InterPoint>().jumpForce;
                                //Add the specified jump force to the character to make him jump to get to the next move point
                                rb.AddForce(transform.up * jumpPointJumpForce, ForceMode2D.Impulse);
                                //Here the character's move target is set to the mentioned transform point
                                currentMovePoint = screenMovePoints[MovePointNumber];
                                //Set the next move target with a condoition to the next move point with a condition
                                currentConditionMovePoint++;
                                //Set the justFromCondition boolean to true
                                justFromCondition = true;
                            }
                        }
                        //If  the new interMode variable's first boolean is true
                        if (interMode[0])
                        {
                            //Set the character's next move target to the current move target + 1 from the screenMovePoints list
                            MovePointNumber++;
                            //Fetch the corresponding jump force from the move point's script
                            jumpPointJumpForce = currentMovePoint.GetComponent<InterPoint>().jumpForce;
                            //Add the specified jump force to the character to make him jump to get to the next move point
                            rb.AddForce(transform.up * jumpPointJumpForce, ForceMode2D.Impulse);
                            //Here the character's move target is set to the mentioned transform point
                            currentMovePoint = screenMovePoints[MovePointNumber];
                            //Set the next move target with a condition to the next move point with a condition
                            currentConditionMovePoint++;
                        }
                    }
                    else //Else if current move point does not have a condition
                    {
                        //If the justFromCondition boolean is true
                        if (justFromCondition)
                        {
                            //Set the characters next move target to the current move target + 2 from the screenMovePoints list
                            MovePointNumber += 2;
                            currentMovePoint = screenMovePoints[MovePointNumber];
                            //Set the justFromCondition boolean back to false
                            justFromCondition = false;
                        }
                        else //Else if the character's last move target was not one with a condition
                        {
                            //Set the characters next move target to the current move target + 1 from the screenMovePoints list
                            MovePointNumber++;
                            currentMovePoint = screenMovePoints[MovePointNumber];
                        }
                    }
                }
                else //Else if current move point is the last one in the screenMovePoints list
                {
                    //Make the character stop moving
                    readyToStart = false;
                }
            }
            else //Else if the characters wait timer has not reached zero
            {
                //Make the wait timer count down
                waitTimer -= Time.deltaTime;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //If the character has collided with the FallDeath object
        if (other.tag == "FallDeath")
        {
            //Activate the Death function
            Death();
        }
    }

    //When the Death function is called
    void Death()
    {
        //Reload the current scene
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
