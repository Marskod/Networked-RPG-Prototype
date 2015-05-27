using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {

    public Texture2D cursorImage;


    void Start()
    {
        UnityEngine.Cursor.visible = false;
    }

    void OnGUI()
    {
        GUI.depth = -1;
        GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 32, 32), cursorImage);
    }
}
