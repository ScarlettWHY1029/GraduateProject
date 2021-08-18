using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float fallSpeed = 5.0f;

    protected Player playerScript;

    public GameObject explosion;

    [SerializeField] protected GameObject popUpText;

    private void Start()
    {
        this.playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }

    public float GetFallSpeed()
    {
        return this.fallSpeed;
    }

    protected void ShowPopUpText(string pop_up_text)
    {
        if (popUpText)
        {
            GameObject popUpTextObj = Instantiate(popUpText, transform.position, Quaternion.identity);
            popUpTextObj.GetComponentInChildren<TextMesh>().text = pop_up_text;
            popUpTextObj.GetComponentInChildren<TextMesh>().color = new Color32(0, 0, 0, 255);
        }
    }
}
