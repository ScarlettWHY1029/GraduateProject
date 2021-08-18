using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    
    private void Awake()
    {
        GameObject[] playList = GameObject.FindGameObjectsWithTag("BGM");

        if (playList.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
}
