using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControls : MonoBehaviour
{
    Vector2 swipeStart, swipeEnd;
    const float minDistance = 10;
    public static event System.Action<SwipeDirection> OnSwipe = delegate { };
    public enum SwipeDirection
    {
        up, down, left, right
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Touch touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Began)
            {
                swipeStart = touch.position;
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                swipeEnd = touch.position;
                processSwipe();
            }
        }
        // mouse touch simulation
        if (Input.GetMouseButtonDown(0))
        {
            swipeStart = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            swipeEnd = Input.mousePosition;
            processSwipe();
        }
    }

    private void processSwipe()
    {
        float distance = Vector2.Distance(swipeStart, swipeEnd);
        if(distance > minDistance)
        {
            if (isVerticalSwipe())
            {
                if(swipeEnd.y > swipeStart.y) // up
                {
                    OnSwipe(SwipeDirection.up);
                }
                else // down
                {
                    OnSwipe(SwipeDirection.down);
                }
            }
            else
            {
                if(swipeEnd.x > swipeStart.x) // right
                {
                    OnSwipe(SwipeDirection.right);
                }
                else // left
                {
                    OnSwipe(SwipeDirection.left);
                }
            }
        }
    }

    private bool isVerticalSwipe()
    {
        float vertical = Mathf.Abs(swipeEnd.y - swipeStart.y),
              horizontal = Mathf.Abs(swipeEnd.x - swipeStart.x);
        if (vertical > horizontal)
            return true;
        else
            return false;

    }
}
