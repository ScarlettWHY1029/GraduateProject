using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInSeconds : MonoBehaviour
{
    private float damageInSeconds = 1.0f;

    void Start()
    {
        Destroy(gameObject, damageInSeconds);
    }
}
