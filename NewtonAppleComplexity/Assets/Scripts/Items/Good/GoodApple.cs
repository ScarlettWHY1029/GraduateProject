using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodApple : Items
{
    public int worthyPoints;

    private void OnTriggerEnter2D(Collider2D otherGameObj)
    {
        GameObject bomb = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;

        if (otherGameObj.tag == "Player")
        {
            if (!FindObjectOfType<GameController>().HasGameOver())
            {
                string popupStr;

                if (playerScript.ReachHighestPoints())
                    popupStr = "Highest +1";
                else
                    popupStr = "+" + worthyPoints.ToString();

                ShowPopUpText(popupStr);
                playerScript.AddScores(worthyPoints);
            }

            bomb.GetComponent<AudioSource>().Stop();
            Destroy(gameObject);
            Destroy(bomb, 1.5f);

        }
        else if (otherGameObj.tag == "Ground")
        {
            Destroy(gameObject);
            Destroy(bomb, 1.5f);
        }
    }
}
