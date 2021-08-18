using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInSeconds : MonoBehaviour
{
    private float DamageSeconds = 1f;

    private void Start()
    {
        Destroy(gameObject, DamageSeconds);
    }
}
