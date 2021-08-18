using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speeder : Items
{
    
    public string tagName;

    public float newMovingSpeed;

    private void OnTriggerEnter2D(Collider2D OtherGameObj)
    {
        GameObject bomb = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;

        if (OtherGameObj.tag.Equals("Player"))
        {   
            if (!FindObjectOfType<GameController>().HasGameOver())
            {
                if (tagName == "+")
                    ShowPopUpText("Speed: +50%");
                else if (tagName == "-")
                    ShowPopUpText("Speed: -50%");

                playerScript.UpdateTheTagOfItems(tagName, true);
                playerScript.UpdateTheMovingSpeed(newMovingSpeed);    
            }

            bomb.GetComponent<AudioSource>().Stop();
            Destroy(gameObject);
            Destroy(bomb, 1.5f);
        }
        // If hits the ground
        else if (OtherGameObj.tag.Equals("Ground"))
        {
            Destroy(gameObject);
            Destroy(bomb, 1.5f);
        }
    }
}
