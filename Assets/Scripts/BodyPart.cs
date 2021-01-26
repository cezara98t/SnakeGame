using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    Vector2 deltaPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMovement(Vector2 movement)
    {
        deltaPosition = movement;
    }

    public void UpdatePosition()
    {
        gameObject.transform.position += (Vector3)deltaPosition;
    }

    public void UpdateDirection()
    {
        if(deltaPosition.y > 0) // up
        {
            gameObject.transform.localEulerAngles = new Vector3(0,0,0);
        }
        else if(deltaPosition.y < 0) // down
        {
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 180);
        }
        else if(deltaPosition.x < 0) // left
        {
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 90);
        }
        else if(deltaPosition.x > 0) // right
        {
            gameObject.transform.localEulerAngles = new Vector3(0, 0, -90);
        }
    }

}