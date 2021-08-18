using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : Items
{
    private int damageScorePoints;

    public void OnTriggerEnter2D(Collider2D OtherGameObj)
    {
        GameObject bomb = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;

        if (OtherGameObj.tag.Equals("Player")) {

            if (!FindObjectOfType<GameController>().HasGameOver())
            {
                if (playerScript.NoMorePoints())
                    ShowPopUpText("Score = 0");
                else
                {
                    damageScorePoints = playerScript.GetScores() / 2;
                    string popupStr = "-" + damageScorePoints.ToString();
                    ShowPopUpText(popupStr);
                    playerScript.MinuScoresPoints(damageScorePoints);
                }
            }

            bomb.GetComponent<AudioSource>().Stop();
            Destroy(gameObject);
            Destroy(bomb, 1.5f);

        } else if (OtherGameObj.tag.Equals("Ground"))
        {

            Destroy(gameObject);
            Destroy(bomb, 1.5f);
        }
    }
}
