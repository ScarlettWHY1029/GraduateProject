using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadApple : Item
{
    public int damageScoresPoints;

    public void ChangeTheFallSpeed(float newFallSpeed) {
        this.fallSpeed = newFallSpeed;
    }

    private void OnTriggerEnter2D(Collider2D OtherGameObj)
    {
        if (OtherGameObj.tag.Equals("Player"))
        {
          
            if (!FindObjectOfType<GameController>().HasGameOver())
            {
                if (playerScript.NoMoreScore())
                    ShowPopUpText("Score = 0");
                else
                {
                    string popUpStr = "-" + damageScoresPoints.ToString();
                    ShowPopUpText(popUpStr);
                }

                playerScript.MinuScoresPoints(damageScoresPoints);
            }

            GameObject bomb = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
            Destroy(bomb, 1f);
        }
        
        else if (OtherGameObj.tag == "Ground")
        {
            GameObject bomb = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
            Destroy(bomb, 1f);
        }
    }
}
