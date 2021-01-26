using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public bool isAlive = true;
    public bool waitingToPlay = true;
    List<Egg> eggs = new List<Egg>();
    int level = 0;
    int noOfEggsForNextLevel = 0;
    public int score = 0, highscore = 0;
    public Text scoreText = null, highscoreText = null;
    public Text gameOverText = null, tapToPlayText = null;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        CreateWalls();
        isAlive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitingToPlay)
        {
            foreach(Touch touch in Input.touches)
            {
                if(touch.phase == TouchPhase.Ended)
                {
                    StartGameplay();
                }
            }
            if (Input.GetMouseButtonUp(0))
                StartGameplay();
        }
    }


    void StartGameplay()
    {
        score = 0;
        scoreText.text = "Score: " + score;
        highscoreText.text = "Highcore: " + highscore;
        gameOverText.gameObject.SetActive(false);
        tapToPlayText.gameObject.SetActive(false);
        waitingToPlay = false;
        isAlive = true;
        KillAllEggs();
        LevelUp();
    }

    void LevelUp()
    {
        level++;
        noOfEggsForNextLevel = 4 + 2 * level;
        snakeSpeed = 1f + level/4;
        if (snakeSpeed > 6) snakeSpeed = 6;
        snakeHead.ResetSnake();
        CreateEgg();
    }

    public void GameOver()
    {
        isAlive = false;
        waitingToPlay = true;
        gameOverText.gameObject.SetActive(true);
        tapToPlayText.gameObject.SetActive(true);
    }

    public void EggEaten(Egg egg)
    {
        score++;
        noOfEggsForNextLevel--;
        if (noOfEggsForNextLevel == 0)
        {
            score += 10;
            LevelUp();
        }
        else if (noOfEggsForNextLevel == 1) // last egg
        {
            CreateEgg(true);
        }
        else
        {
            CreateEgg();
        }

        if (score > highscore)
        {
            highscore = score;
            highscoreText.text = "Highcore: " + highscore;
        }
        scoreText.text = "Score: " + score;

        eggs.Remove(egg);
        Destroy(egg.gameObject);
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
        Egg egg = null;
        if (golden)
        {
            egg = Instantiate(goldEggPrefab, position, Quaternion.identity).GetComponent<Egg>();
        }
        else
            egg = Instantiate(eggPrefab, position, Quaternion.identity).GetComponent<Egg>();
        eggs.Add(egg);
    }

    void KillAllEggs()
    {
        foreach(Egg egg in eggs)
        {
            Destroy(egg.gameObject);
        }
        eggs.Clear();
    }
}