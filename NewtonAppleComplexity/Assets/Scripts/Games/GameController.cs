using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour

{
    /***** Public variables *****/
    public Text sloganText;
    public Text delayText;
    public Text timeText;

    public GameObject gamePlayer;
    public GameObject endPanel;

    public GameObject[] appleList;   
    public GameObject[] goodList;  
    public GameObject[] badList;
    private GameObject[] itemSet;
    public Transform[] spawnerPoints;

    /***** Private variables *****/
    private PlayerData playerData;
    private int playerScores;
    
    private float delaySeconds;
    private readonly float waitSeconds = 1.0f;
    private readonly float defaultMovingSpeed = 10.0f;

    private int fastSecond;
    private int slowSecond;
    private int gameSeconds;
    private readonly int goalSeconds = 300;
    private readonly int speedTimer = 6;

    private bool hasGameOver;
    private bool hasGameBegin;
    private bool hasReward;

    private Dictionary<string, bool> badRecord;
    

    private void Awake()
    {
        this.playerData = gamePlayer.GetComponent<Player>().GetPlayerData();

        this.delaySeconds = 5.0f;
        this.fastSecond = 0;
        this.slowSecond = 0;
        this.gameSeconds = 0;

        this.itemSet = new GameObject[5];

        this.hasGameBegin = false;
        this.hasGameOver = false;
        this.hasReward = false;

        this.badRecord = new Dictionary<string, bool>();
        this.badRecord.Add("BroomCard", false);
        this.badRecord.Add("RottenAppleCard", false);
        this.badRecord.Add("RottenToxicAppleCard", false);
        this.badRecord.Add("SlowMeterCard", false);
        this.badRecord.Add("ToxicAppleCard", false);
    }

    private void Start()
    {
        this.endPanel.SetActive(false);
        StartCoroutine(BeginToSpawn());
    }

    private void Update()
    {
        if (gameSeconds > goalSeconds)
            DisplayTheDataPanel(); 
    }

    private IEnumerator BeginToSpawn()
    {
        while (delaySeconds > 0.0f)
        {
            delayText.text = delaySeconds.ToString();
            yield return new WaitForSeconds(waitSeconds);
            delaySeconds--;
        }

        if (!hasGameBegin)
        {
            delayText.text = "Ready ?";
            yield return new WaitForSeconds(waitSeconds);
            delayText.text = "Go !!!";
            yield return new WaitForSeconds(waitSeconds);
            hasGameBegin = true;
            delayText.gameObject.SetActive(false);
        }

        if (gamePlayer != null)
        {
            while (gameSeconds <= goalSeconds)
            {
                
                ShuffleTheIndexSet();
                
                playerScores = gamePlayer.GetComponent<Player>().GetScores();
                playerData = gamePlayer.GetComponent<Player>().GetPlayerData();

                GetFallingItems();
                SpawnTheItems();
                CheckThePlayerSpeed();

                UpdateTheRecordList();

                if (gameSeconds == goalSeconds)
                    gameSeconds = goalSeconds;

                timeText.text = gameSeconds.ToString();

                gameSeconds++;

                yield return new WaitForSeconds(waitSeconds);
            }
        }
    }

    private void DisplayTheDataPanel()
    {
        hasGameOver = true;

        this.sloganText.text = "Good Effort! :)";
        this.sloganText.GetComponent<Text>().color = new Color32(125, 230, 255, 255);
        this.endPanel.GetComponent<Image>().color = new Color32(11, 104, 0, 205);

        SetUpJSONFile();
        StopAllCoroutines();
        this.endPanel.SetActive(true);
    }

    private void SetUpJSONFile()
    {
        string jsonStr = JsonUtility.ToJson(playerData);

        File.WriteAllText(Application.dataPath + "GameData2.json", jsonStr);

    }

    private void UpdateTheRecordList()
    {
        int time_point = gameSeconds;
        int last_time_point = playerData.GetTheLastFlowPoint().GetSecondPoint();

        if (time_point % 10 == 0)
        {
            if (time_point == last_time_point)
                playerData.UpdateTheLastScorePoint(playerScores);
            else
                playerData.AddTheNewFlowPoint(time_point, playerScores);
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

    private void GetFallingItems()
    {
        if (FindObjectOfType<Player>().HasGettenTheTargetItem("R"))
        {
            GetTheSpecialSpawnItems("R");
            FindObjectOfType<Player>().UpdateTheTagOfItems("R", false);
        }
        else if (FindObjectOfType<Player>().HasGettenTheTargetItem("RT"))
        {
            GetTheSpecialSpawnItems("RT");
            FindObjectOfType<Player>().UpdateTheTagOfItems("RT", false);
        }
        else if (FindObjectOfType<Player>().HasGettenTheTargetItem("T"))
        {
            GetTheSpecialSpawnItems("T");
            FindObjectOfType<Player>().UpdateTheTagOfItems("T", false);
        }
        else if (FindObjectOfType<Player>().HasGettenTheTargetItem("H"))
        {
            GetTheSpecialSpawnItems("H");
            FindObjectOfType<Player>().UpdateTheTagOfItems("H", false);
        }
        else if (FindObjectOfType<Player>().HasGettenTheTargetItem("B"))
        {
            GetTheSpecialSpawnItems("B");
            FindObjectOfType<Player>().UpdateTheTagOfItems("B", false);
        }
        else
            GetTargetSpawnItems(playerScores);
    }

    private void GetTheSpecialSpawnItems(string tagName)
    {
        if (tagName.Equals("R"))
        {
            for (int i = 0; i < 5; i++)
                itemSet[i] = appleList[2];

        }else if (tagName.Equals("RT"))
        {
            for (int i = 0; i < 3; i++)
                itemSet[i] = appleList[2];

            itemSet[3] = appleList[3];
            itemSet[4] = appleList[3];

        } else if (tagName.Equals("T"))
        {
            for (int i = 0; i < 5; i++)
                itemSet[i] = appleList[3];

        } else if (tagName.Equals("H"))
        {
            for (int i = 0; i < 5; i++)
                itemSet[i] = appleList[0];
        }
        else if (tagName.Equals("B"))
        {
            for (int i = 0; i < 5; i++)
                itemSet[i] = appleList[1];
        }
    }

    private void GetTargetSpawnItems(int scorePoints)
    {
        if (scorePoints >= 80)
        {
            itemSet[0] = appleList[1];

            for (int i = 1; i < 3; i++)
                itemSet[i] = appleList[3];
        }
        else if (scorePoints >= 60)

        {
            itemSet[0] = appleList[0];
            itemSet[1] = appleList[2];
            itemSet[2] = appleList[3];
        }
        else if (scorePoints >= 40)
        {
            itemSet[0] = appleList[0];

            for (int i = 1; i < 3; i++)
                itemSet[i] = appleList[2];
        }
        else if (scorePoints >= 20)
        {
            for (int i = 0; i < 2; i++)
                itemSet[i] = appleList[0];
            
            itemSet[2] = appleList[2];
        }
        else
        {
            for (int i = 0; i < 3; i++)
                itemSet[i] = appleList[0];
        }


        if (!HasGottenTheRewards(3))
            GetItemsOfPunishments(3);

        if (!hasReward)
        {
            if (!HasGottenTheRewards(4))
                GetItemsOfPunishments(4);
        } else
            GetItemsOfPunishments(4);

        RefreshTheBadRecord();
    }

    private void GetItemsOfPunishments(int index)
    {
        do
        {
            itemSet[index] = badList[Random.Range(0, badList.Length)];

        } while (badRecord[itemSet[index].tag]);

        badRecord[itemSet[index].tag] = true;
    }

    private void RefreshTheBadRecord()
    {
        List<string> keySet = new List<string>(badRecord.Keys);

        foreach (string k in keySet)
            badRecord[k] = false;
    }

    private bool HasGottenTheRewards(int index)
    {
        if (index < 0 || index >= itemSet.Length)
            return false;

        int number = Random.Range(0, 101);

        if (number == 100)
        {
            hasReward = true;
            itemSet[index] = goodList[2];
            return true;

        }
        else if (number >= 30 && number <= 35)
        {
            hasReward = true;
            itemSet[index] = goodList[0];
            return true;
        }
        else if (number >= 0 && number < 3)
        {
            hasReward = true;
            itemSet[index] = goodList[1];
            return true;
        }
        else
        {
            hasReward = false;
            return false;
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

    private void CheckThePlayerSpeed()
    {
        if (gamePlayer.GetComponent<Player>().HasGettenTheTargetItem("+"))
        {
            if (fastSecond < (speedTimer / 2))
                fastSecond++;
            else
            {
                gamePlayer.GetComponent<Player>().UpdateTheMovingSpeed(defaultMovingSpeed);
                gamePlayer.GetComponent<Player>().UpdateTheTagOfItems("+", false);
                fastSecond = 0;
            }

        }


        else if (gamePlayer.GetComponent<Player>().HasGettenTheTargetItem("-"))
        {
            if (slowSecond < speedTimer)
                slowSecond++;  
            else
            {
                gamePlayer.GetComponent<Player>().UpdateTheMovingSpeed(defaultMovingSpeed);
                gamePlayer.GetComponent<Player>().UpdateTheTagOfItems("-", false);
                slowSecond = 0;
            }
        }
    }

    public bool HasGameOver()
    {
        return this.hasGameOver;
    }
}
