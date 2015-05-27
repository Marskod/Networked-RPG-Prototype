using UnityEngine;
using System.Collections;

public class MiniCamFollow : MonoBehaviour {

    public Texture2D frame;
    private GameObject player;
    private float camHeight = 30;
    public float defaultheight = 100;

    public GUISkin skin;

    private Rect text = new Rect(941f, 12.5f, 275.8f, 206.3f);

    void LateUpdate()
    {
        player = GameObject.Find("Player(Clone)");
        transform.position = new Vector3(player.transform.position.x, defaultheight, player.transform.position.z);

        if (camHeight > 50)
            camHeight = 50;
        else if (camHeight < 10)
            camHeight = 10;
    }

    void OnGUI()
    {
        GUI.skin = skin; 

        Camera camera = gameObject.GetComponent<Camera>();
        bool active = camera.enabled;

        camera.orthographicSize = camHeight;

        if (GUI.Button(new Rect(Screen.width * .92f, 0, 40, 10), "+", skin.GetStyle("Slot")))
            camHeight -= 5;
        else if (GUI.Button(new Rect(Screen.width * .873f, 0, 40, 10), "-", skin.GetStyle("Slot")))
            camHeight += 5;

        if (GUI.Button(new Rect(Screen.width * .965f, 0, 40, 10), "-", skin.GetStyle("Slot")))
            camera.enabled = !active;

        //if (active)
          //  GUI.DrawTexture(text, frame);
    }
}
