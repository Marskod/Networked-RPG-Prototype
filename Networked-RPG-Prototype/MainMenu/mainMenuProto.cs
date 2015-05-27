using UnityEngine;
using System.Collections;

public class mainMenuProto : MonoBehaviour {

	void OnMouseEnter()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(1.1f, 0.3f, 0.3f);
    }

    void OnMouseExit()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    void OnMouseDown()
    {
        Debug.Log("Pressed");
        Application.LoadLevel("playerCustomizationMenu");
    }
}
