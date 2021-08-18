using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public float fallSpeed;

    protected Player playerScript;

    public GameObject explosion;

    [SerializeField] protected GameObject popUpText;

    private void Awake()
    {
        this.playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }

    protected void ShowPopUpText(string textStr)
    {
        if (popUpText)
        {
            popUpText.GetComponentInChildren<TextMesh>().color = new Color32(255,255,255,255);
            popUpText.GetComponentInChildren<TextMesh>().text = textStr;
            GameObject popUpTextOBJ = Instantiate(popUpText, transform.position, Quaternion.identity);
        }
    }
}
