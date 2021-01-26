using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : BodyPart
{
    Vector2 movement;
    private BodyPart tail = null;
    const float TIMETOADDBODYPART = 0.1f;
    float addTimer = TIMETOADDBODYPART;
    public int partsToAdd = 0;

    // Start is called before the first frame update
    void Start()
    {
        SwipeControls.OnSwipe += SwipeDetection;
    }

    // Update is called once per frame
    override public void Update()
    {
        base.Update();
        SetMovement(movement * Time.deltaTime);
        UpdateDirection();
        UpdatePosition();
        if(partsToAdd > 0)
        {
            addTimer -= Time.deltaTime;
            if(addTimer <= 0)
            {
                addTimer = TIMETOADDBODYPART;
                AddBodyPart();
                partsToAdd--;
            }
        }
    }

    public void ResetSnake()
    {
        tail = null;
        MoveUp();
        partsToAdd = 5;
        addTimer = TIMETOADDBODYPART;
    }

    void AddBodyPart()
    {
        if (tail)
        {
            Vector3 newPosition = tail.transform.position;
            newPosition.z = newPosition.z + 0.01f;
            BodyPart newPart = Instantiate(GameController.instance.bodyPrefab, newPosition, Quaternion.identity);
            newPart.following = tail;
            newPart.TurnIntoTail();
            tail.TurnIntoBodyPart();
            tail = newPart;
        }
        else // only a head
        {
            Vector3 newPosition = transform.position;
            newPosition.z = newPosition.z + 0.01f;
            BodyPart newPart = Instantiate(GameController.instance.bodyPrefab, newPosition, Quaternion.identity);
            newPart.following = this;
            tail = newPart;
            newPart.TurnIntoTail();
        }
    }

    void SwipeDetection(SwipeControls.SwipeDirection direction)
    {
        switch (direction)
        {
            case SwipeControls.SwipeDirection.up:
                MoveUp();
                break;
            case SwipeControls.SwipeDirection.down:
                MoveDown();
                break;
            case SwipeControls.SwipeDirection.left:
                MoveLeft();
                break;
            case SwipeControls.SwipeDirection.right:
                MoveRight();
                break;
        }
    }

    void MoveUp()
    {
        movement = Vector2.up * GameController.instance.snakeSpeed;
    }

    void MoveDown()
    {
        movement = Vector2.down * GameController.instance.snakeSpeed;
    }

    void MoveLeft()
    {
        movement = Vector2.left * GameController.instance.snakeSpeed;
    }

    void MoveRight()
    {
        movement = Vector2.right * GameController.instance.snakeSpeed;
    }
}
