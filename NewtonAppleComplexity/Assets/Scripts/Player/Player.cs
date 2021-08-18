using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    /***** Public values *****/
    public PlayerData playerData;

    public Text textScore;
    public Text scoreData;

    public Text textHighestTimes;
    public Text highestTimesData;

    public float moveSpeed;

    /***** Private values *****/
    private float inputX;

    private int score;
    private int timesOfHighest;
    private readonly int goalScore = 100;

    private Rigidbody2D rigidBody;
    private Animator anime;
    private AudioSource sound;

    private bool hasAcceleratedSpeed;
    private bool hasReducedSpeed;
    private bool hasHeartCard;
    private bool hasBitCoinCard;
    private bool hasRottenCard;
    private bool hasRottenToxicCard;
    private bool hasToxicCard;

    private void Awake()
    {
        this.playerData = new PlayerData();
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.anime = GetComponent<Animator>();
        this.sound = GetComponent<AudioSource>();

        this.score = 0;

        InitializeAllTags();
    }

    private void Start()
    {
        this.playerData.AddTheNewFlowPoint(0, score);

        this.textScore.text = score.ToString();
        this.scoreData.text = "0";
        this.textHighestTimes.text = "0";
    }

    private void InitializeAllTags()
    {
        this.hasAcceleratedSpeed = false;
        this.hasReducedSpeed = false;
        this.hasBitCoinCard = false;
        this.hasRottenCard = false;
        this.hasRottenToxicCard = false;
        this.hasToxicCard = false;
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

    public PlayerData GetPlayerData()
    {
        return playerData;
    }

    public int GetScores()
    {
        return score;
    }

    public bool NoMorePoints()
    {
        return score == 0;
    }

    public bool ReachHighestPoints()
    {
        return score == goalScore;
    }

    public void AddScores(int newPoints)
    {
        sound.Play();

        score += newPoints;

        if (score >= goalScore)
        {
            score = goalScore;
            timesOfHighest++;
            textHighestTimes.text = timesOfHighest.ToString();
            highestTimesData.text = timesOfHighest.ToString();

        }
        
        textScore.text = score.ToString();
        scoreData.text = score.ToString();
    }

    public void MinuScoresPoints(int negPoints)

    {
        sound.Play();

        if (score > negPoints)
            score -= negPoints;

        else if (score <= negPoints)
            score = 0;
        
        textScore.text = score.ToString();
        scoreData.text = score.ToString();
    }

    public bool HasGettenTheTargetItem(string tagName)
    {
        if (tagName.Equals("R"))
            return hasRottenCard;
        else if (tagName.Equals("RT"))
            return hasRottenToxicCard;
        else if (tagName.Equals("T"))
            return hasToxicCard;
        else if (tagName.Equals("H"))
            return hasHeartCard;
        else if (tagName.Equals("B"))
            return hasBitCoinCard;
        else if (tagName.Equals("+"))
            return hasAcceleratedSpeed;
        else if (tagName.Equals("-"))
            return hasReducedSpeed;
        else
            return false;
    }

    public void UpdateTheTagOfItems(string tagName, bool newTag)
    {
        if (tagName.Equals("R"))
            hasRottenCard = newTag;
        else if (tagName.Equals("RT"))
            hasRottenToxicCard = newTag;
        else if (tagName.Equals("T"))
            hasToxicCard = newTag;
        else if (tagName.Equals("H"))
            hasHeartCard = newTag;
        else if (tagName.Equals("B"))
            hasBitCoinCard = newTag;
        else if (tagName.Equals("+"))
            hasAcceleratedSpeed = newTag;
        else if (tagName.Equals("-"))
            hasReducedSpeed = newTag;
        else
            return;
    }

    public void UpdateTheMovingSpeed(float newMovingSpeed)
    {
        moveSpeed = newMovingSpeed;
    }
}
