using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour

{
    private PlayerData playerData;

    public Transform[] spawnerPoints;

    public Text sloganText;
    public Text delayText;
    public Text secondText;

    public GameObject gamePlayer;
    public GameObject endPanel;

    public GameObject[] phase1;
    public GameObject[] phase2;
    public GameObject[] phase3;
    public GameObject[] phase4;
    public GameObject[] phase5;
    private GameObject[] itemSet;

    public float spawnTime;

    private float waitFiveSeconds;

    private int playerScores;
    private int elaspsedSecond;

    private float newRottenSpeed;
    private float newToxicSpeed;

    private bool hasBegun;
    private bool hasGameOver;

    private readonly int targetSecond = 300;
    private readonly float delayTime = 1.0f;

    private void Awake()
    {
        this.waitFiveSeconds = 5.0f;
        this.newRottenSpeed = 5.0f;
        this.newToxicSpeed = 5.0f;

        this.hasBegun = false;
        this.hasGameOver = false;

        this.playerScores = 0;
        this.elaspsedSecond = 0;

        this.itemSet = new GameObject[5];

        this.secondText.text = "0";
    }

    private void Start()
    {
        this.endPanel.SetActive(false);
        StartCoroutine(Spawn());
    }

    private void Update()
    {
        if (elaspsedSecond > targetSecond)
            DisplayThePanel();
        
    }

    private IEnumerator Spawn()
    {
        while (waitFiveSeconds > 0)
        {
            delayText.text = waitFiveSeconds.ToString();
            yield return new WaitForSeconds(delayTime);
            waitFiveSeconds--;
        }

        if (!hasBegun)
        {
            delayText.text = "Ready ?";
            yield return new WaitForSeconds(delayTime);
            delayText.text = "Go !!!";
            yield return new WaitForSeconds(delayTime);
            delayText.gameObject.SetActive(false);
            hasBegun = true;
        }

        while (elaspsedSecond <= targetSecond)
        {
            ShuffleTheIndexSet();

            playerScores = gamePlayer.GetComponent<Player>().GetScores();
            playerData = gamePlayer.GetComponent<Player>().GetPlayerData();

            GetTargetSpawnItems(playerScores);
            ChangeTheFallenSpeed();
            SpawnTheItems();

            UpdateThePlayerData();

            elaspsedSecond++;

            if (elaspsedSecond <= targetSecond)
                secondText.text = elaspsedSecond.ToString();

            yield return new WaitForSeconds(delayTime);
       }
        
    }

    private void ShuffleTheIndexSet()
    {
        for (int i = 0; i < spawnerPoints.Length; i++)
        {
            int randomIndex = Random.Range(0, spawnerPoints.Length);
            Transform tempTrans = spawnerPoints[i];
            spawnerPoints[i] = spawnerPoints[randomIndex];
            spawnerPoints[randomIndex] = tempTrans;
        }
    }

    private void GetTargetSpawnItems(int scorePoints)
    {
        if (scorePoints >= 80)
        {
            newRottenSpeed = 9.5f;
            newToxicSpeed = 10f;
            itemSet = phase5;

        }
        else if (scorePoints >= 60)

        {
            newRottenSpeed = 9f;
            newToxicSpeed = 9.5f;
            itemSet = phase4;

        }
        else if (scorePoints >= 40)
        {
            newRottenSpeed = 8.5f;
            newToxicSpeed = 9f;
            itemSet = phase3;

        }
        else if (scorePoints >= 20)
        {
            newRottenSpeed = 8f;
            newToxicSpeed = 8.5f;
            itemSet = phase2;
        }
        else
        {
            newRottenSpeed = 7f;
            itemSet = phase1;
        }
    }

    private void ChangeTheFallenSpeed()
    {
        for (int i = 0; i < itemSet.Length; i++)
        {
            GameObject gameOBJ = itemSet[i];

            if (gameOBJ.tag == "RottenApple")
                gameOBJ.GetComponent<BadApple>().ChangeTheFallSpeed(newRottenSpeed);

            else if (gameOBJ.tag == "ToxicApple")
                gameOBJ.GetComponent<BadApple>().ChangeTheFallSpeed(newToxicSpeed);

        }
    }

    private void SpawnTheItems()
    {
        Instantiate(itemSet[0], spawnerPoints[0].position, Quaternion.identity);
        Instantiate(itemSet[1], spawnerPoints[1].position, Quaternion.identity);
        Instantiate(itemSet[2], spawnerPoints[2].position, Quaternion.identity);
        Instantiate(itemSet[3], spawnerPoints[3].position, Quaternion.identity);
        Instantiate(itemSet[4], spawnerPoints[4].position, Quaternion.identity);
    }

    public bool HasGameOver()
    {
        return this.hasGameOver;
    }

    private void DisplayThePanel()
    {
        hasGameOver = true;

        this.sloganText.text = "Good Effort! :)";
        this.sloganText.GetComponent<Text>().color = new Color32(146, 246, 255, 200);
        this.endPanel.GetComponent<Image>().color = new Color32(255, 50, 50, 200);

        playerData.UpdateTheLastFlowPoint(playerScores);

        SetUpJSONFile();
        StopAllCoroutines();
        this.endPanel.SetActive(true);
    }

    private void SetUpJSONFile()
    {
        string jsonStr = JsonUtility.ToJson(playerData);

        File.WriteAllText(Application.dataPath + "GameData1.json", jsonStr);
    }

    private void UpdateThePlayerData()
    {
        FlowPoint lastPoint = playerData.GetTheLastFlowPoint();
        int secondPoint = lastPoint.GetSecondPoint();
       
        if (elaspsedSecond % 10 == 0)
        {
            if (elaspsedSecond == secondPoint)
                playerData.UpdateTheLastFlowPoint(playerScores);
            else
                playerData.AddTheNewFlowPoint(elaspsedSecond, playerScores);
        }
    }
}
