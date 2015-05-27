using UnityEngine;
using System.Collections;

public class PlayerNAme : MonoBehaviour {

    public string getPlayerName;
    public TextMesh NAMEPLATE;

    void Awake()
    {
        if (GetComponent<NetworkView>().isMine)
        {
            GameObject netset = GameObject.Find("NetworkSettings");
            NAMEPLATE = gameObject.GetComponent<TextMesh>();
            GetComponent<NetworkView>().RPC("getName", RPCMode.AllBuffered, netset.GetComponent<Mpbase>().playerName);
            GameObject cam = GameObject.Find("MainCamera"); 
            transform.LookAt(cam.transform);
        }
    }

    [RPC]
    public void getName(string name)
    {
        NAMEPLATE.text = name;
    }
}

