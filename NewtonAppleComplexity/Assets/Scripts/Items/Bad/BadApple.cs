using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadApple : Items
{
    public int damageScoresPoints;

    private void OnTriggerEnter2D(Collider2D OtherGameObj)
    {
        GameObject bomb = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;

        if (OtherGameObj.tag == "Player")
        {
            if (!FindObjectOfType<GameController>().HasGameOver())
            {
                string popupStr;

                if (playerScript.NoMorePoints())
                    popupStr = "Score = 0";
                else
                    popupStr = "-" + damageScoresPoints.ToString();
                    
                ShowPopUpText(popupStr);
                playerScript.MinuScoresPoints(damageScoresPoints);
            }

            bomb.GetComponent<AudioSource>().Stop();
            Destroy(gameObject);
            Destroy(bomb, 1.5f);

        }
        else if (OtherGameObj.tag == "Ground")
        {
            Destroy(gameObject);
            Destroy(bomb, 1.5f);
        }
    }
}
