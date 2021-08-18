using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private PlayerData playerData;

    private float inputX;
    public float moveSpeed;

    private int score;
    private int reachHighestNum;
    private readonly int goalScore = 100;

    public Text textScores;
    public Text textHightestTimes;

    public Text dataScores;
    public Text dataReachHighestTimes;

    private Rigidbody2D rigidBody;
    private Animator anime;
    private AudioSource sound;

    private void Awake()
    {
        this.playerData = new PlayerData();
        this.score = 0;
        this.reachHighestNum = 0;
        this.sound = GetComponent<AudioSource>();
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.anime = GetComponent<Animator>();
        this.textScores.text = score.ToString();
    }

    private void Start()
    {
        this.playerData.AddTheNewFlowPoint(0, score);
    }

    private void Update()
    {
        if (inputX == 1)
        {
            anime.SetBool("isWalkingLeft", false);
            anime.SetBool("isWalkingRight", true);
        }
        else if (inputX == -1)
        {
            anime.SetBool("isWalkingLeft", true);
            anime.SetBool("isWalkingRight", false);
        }
        else
        {
            anime.SetBool("isWalkingLeft", false);
            anime.SetBool("isWalkingRight", false);
        }
    }

    
    private void FixedUpdate()
    {        
        inputX = Input.GetAxisRaw("Horizontal");
        rigidBody.velocity = new Vector2(inputX * moveSpeed, rigidBody.velocity.y);
    }

    public bool HasReachedGoalScore()
    {
        return score == goalScore;
    }

    public bool NoMoreScore()
    {
        return score == 0;
    }

    public bool NoMoreScores()
    {
        return this.score == 0;
    }

    public int GetScores()
    {
        return this.score;
    }

    public PlayerData GetPlayerData()
    {
        return this.playerData;
    }

    public void AddScores(int newPoints)
    {
        this.sound.Play();
        score += newPoints;

        if (score >= goalScore)
        {
            score = goalScore;
            reachHighestNum++;

            textHightestTimes.text = reachHighestNum.ToString();
            dataReachHighestTimes.text = reachHighestNum.ToString();
        }

        textScores.text = score.ToString();
        dataScores.text = score.ToString();
    }
    
    public void MinuScoresPoints(int negPoints)

    { 
        this.sound.Play();

        if (score >= negPoints)        
            score-= negPoints;

        else if (score < negPoints)
            score = 0;

        textScores.text = score.ToString();
        dataScores.text = score.ToString();
    }
}
