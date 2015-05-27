using UnityEngine;
using System.Collections;

public class hoverOver : MonoBehaviour {

    private Color dColor;

    void OnMouseOver()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
    }

    void OnMouseExit()
    {
        gameObject.GetComponent<Renderer>().material.color = dColor;
    }
}
