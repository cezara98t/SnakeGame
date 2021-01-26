﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;
    public float snakeSpeed = 1;
    public BodyPart bodyPrefab = null;
    public GameObject rockPrefab = null;
    public GameObject eggPrefab = null;
    public GameObject goldEggPrefab = null;
    public Sprite tailSprite;
    public Sprite bodySprite;
    public SnakeHead snakeHead;
    const float width = 3.7f, height = 7f;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        CreateWalls();
        StartGame();
        CreateEgg();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame()
    {
        snakeHead.ResetSnake();
    }

    void CreateWalls()
    {
        Vector3 start = new Vector3(-width, -height, 0);
        Vector3 finish = new Vector3(-width, height, 0);
        CreateWall(start, finish);

        start = new Vector3(width, -height, 0);
        finish = new Vector3(width, height, 0);
        CreateWall(start, finish);

        start = new Vector3(-width, -height, 0);
        finish = new Vector3(width, -height, 0);
        CreateWall(start, finish);

        start = new Vector3(-width, height, 0);
        finish = new Vector3(width, height, 0);
        CreateWall(start, finish);
    }

    void CreateWall(Vector3 start, Vector3 finish)
    {
        float distance = Vector3.Distance(start, finish);
        int numRocks = (int)(distance * 3);
        Vector3 delta = (finish - start)/numRocks;
        Vector3 position = start;
        for(int i = 0; i <= numRocks; i++)
        {
            float rotation = Random.Range(0, 360);
            float scale = Random.Range(1.5f, 2f);
            CreateRock(position, scale, rotation);
            position = position + delta;
        }
    }

    void CreateRock(Vector3 position, float scale, float rotation)
    {
        GameObject rock = Instantiate(rockPrefab, position, Quaternion.Euler(0,0,rotation));
        rock.transform.localScale = new Vector3(scale, scale, 1);
    }

    void CreateEgg(bool golden = false)
    {
        Vector3 position;
        position.x = -width + Random.Range(1f, width * 2 - 2);
        position.y = -height + Random.Range(1f, height * 2 - 2);
        position.z = 0;
        if (golden)
        {
            Instantiate(goldEggPrefab, position, Quaternion.identity);
        }
        else
            Instantiate(eggPrefab, position, Quaternion.identity);
    }
}