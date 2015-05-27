using UnityEngine;
using System.Collections;

[System.Serializable]

public class Structure{

	public GameObject structure;

	public string id = "";
    public int value;

	public Structure (string iden, int val) {
        id = iden;
        value = val;
	}

	public Structure()
    {

    }
}
