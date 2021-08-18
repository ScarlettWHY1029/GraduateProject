using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : Items
{
    public string tagName;
    
    private void OnTriggerEnter2D(Collider2D OtherGameObj)
    {
        GameObject bomb = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;

        if (OtherGameObj.tag.Equals("Player"))
        {
            if (!FindObjectOfType<GameController>().HasGameOver())
                playerScript.UpdateTheTagOfItems(tagName, true);
            

            if (gameObject.tag == "RottenAppleCard")
                ShowPopUpText("+5 Rotten");
            else if (gameObject.tag == "RottenToxicAppleCard")
                ShowPopUpText("+3 Rotten \n +2 Toxic");
            else if (gameObject.tag == "ToxicAppleCard")
                ShowPopUpText("+5 Toxic");

            bomb.GetComponent<AudioSource>().Stop();
            Destroy(gameObject);
            Destroy(bomb, 1.5f);
        }
        else if (OtherGameObj.tag.Equals("Ground"))
        {
            Destroy(gameObject);
            Destroy(bomb, 1.5f);
        }
    }
}
