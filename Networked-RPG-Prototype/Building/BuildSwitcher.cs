using UnityEngine;
using System.Collections;

public class BuildSwitcher : MonoBehaviour {

    public GameObject mainCam;
    public GameObject buildCam;

    private Transform cam;

    public bool def = true;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        buildCam = GameObject.FindGameObjectWithTag("BuildCam");
        
        buildCam.SetActive(false);
        buildCam.GetComponent<EditModePlayer>().player = gameObject;
    }

    void Update()
    {
        Transform player = transform;
        GameObject camInst = buildCam;
        //cam = buildCam.transform;

        if (def)
        {
            buildCam.GetComponent<EditModePlayer>().setSubject(null);
            player.GetComponent<CharacterController>().enabled = true;
            Vector3 pos = camInst.transform.position;
            pos.x = player.position.x;
            pos.z = player.position.z;
            buildCam.transform.position = pos;
        }
        else
        {
            if (player != null)
            {
                player.GetComponent<CharacterController>().enabled = false;
            }
        }
    }

	void OnGUI()
    {
        GUILayout.BeginArea(new Rect(2, (Screen.height/2)-50, 160, 100));
        if(GUILayout.Button("ITS BUILDING TIME!!!"))
        {
            swapCams(!def);
        }
        GUILayout.EndArea();
    }

    void swapCams(bool active)
    {
        if(active)
        {
            transform.GetComponent<PlayerCombat>().enabled = true;
            mainCam.SetActive(true);
            buildCam.SetActive(false);
            def = true;
        }
        else
        {
            transform.GetComponent<PlayerCombat>().enabled = false;
            mainCam.SetActive(false);
            buildCam.SetActive(true);
            def = false;
        }
    }
}
