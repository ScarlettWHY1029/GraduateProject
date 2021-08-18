using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Items
{
    public string tagName;

    public void OnTriggerEnter2D(Collider2D otherGameObj)
    {
        GameObject bomb = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;

        if (otherGameObj.tag.Equals("Player"))
        {
            if (!FindObjectOfType<GameController>().HasGameOver())
            {
                playerScript.UpdateTheTagOfItems(tagName, true);
                ShowPopUpText("+5 Red");
            }

            bomb.GetComponent<AudioSource>().Stop();
            Destroy(gameObject);
            Destroy(bomb, 1.5f);
        }
        else if (otherGameObj.tag.Equals("Ground"))
        {
            Destroy(gameObject);
            Destroy(bomb, 1.5f);
        }
    }
}
