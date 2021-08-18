using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodApple : Item
{
    public int worthyPoints;

    private void OnTriggerEnter2D(Collider2D otherGameObj)
    { 
        if (otherGameObj.tag == "Player")
        {
           if (!FindObjectOfType<GameController>().HasGameOver())
            {

                if (playerScript.HasReachedGoalScore())
                    ShowPopUpText("Highest +1");
                else
                {
                    string popUpStr = "+" + worthyPoints.ToString();
                    ShowPopUpText(popUpStr);
                }

                playerScript.AddScores(worthyPoints);
            }

            GameObject bomb = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
            Destroy(bomb, 1f);

        } else if (otherGameObj.tag == "Ground")
        {
            GameObject bomb = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
            Destroy(bomb, 1f);
        }
    }
}
