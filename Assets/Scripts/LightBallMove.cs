using Cinemachine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBallMove : MonoBehaviour
{
    public float lightSpeed = 10f;
    public bool isIn = false;
    private Vector2 lastClickedPos;
    public bool isNotColliding = true;
    bool moving;
    private int cameraListNumber;
    public GameObject guy;
    private Vector3 prevPos;
    public bool isOnBack = false;
    public bool movingBack = false;

    private void Start()
    {
        prevPos = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moving = true;
        }

        if (!movingBack)
        {
            prevPos = new Vector3(transform.position.x, transform.position.y + 3f, 0.0f);
        }
        

        if (movingBack)
        {
            float step = lightSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, prevPos, step);
            if (Vector2.Distance(transform.position, prevPos) < 0.4f)
            {
                movingBack = false;
                moving = false;
                
            }
        }
        else if (moving && (Vector2)transform.position != lastClickedPos)
        {
            float step = lightSpeed * Time.deltaTime;
            if (isNotColliding)
            {
                
                transform.position = Vector2.MoveTowards(transform.position, lastClickedPos, step);
            }
            else
            {
                transform.position = transform.position;
                if (Input.GetMouseButtonDown(0))
                {
                    isNotColliding = true;
                }
            }
            
            
        }
        else
        {
            moving = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "CharacterBack")
        {
            isOnBack = true;
            PlayerTouchedBack();

        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Inter.Obj")
        {
            isIn = true;
            other.GetComponent<LightUp>().LightNow();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Inter.Obj")
        {
            isNotColliding = false;
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Inter.Obj")
        {
            isNotColliding = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Inter.Obj")
        {
            isNotColliding = true;
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Inter.Obj")
        {
            isIn = false;
            other.GetComponent<LightUp>().LightOff();
        }
        if (other.gameObject.tag == "CharacterBack")
        {
            isOnBack = false;
            PlayerTouchedBack();

        }
    }

    void PlayerTouchedBack()
    {
        movingBack = true;  
    }
}
