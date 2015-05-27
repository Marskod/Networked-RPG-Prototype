using UnityEngine;
using System.Collections;

public class BuildEditorManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject instantiatePart (GameObject part) {
		Debug.Log (part.transform.position);
		Debug.Log(part.transform.localScale);

		GameObject a = Instantiate(part, part.transform.position, Quaternion.identity) as GameObject;
		a.transform.rotation = part.transform.rotation;
		return a;
	}

	public Transform instantiateBase (Transform part, Vector3 size) {
		//Debug.Log("Instantiating base...");
		// Create base game object
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		// Write an algorithm to check the corners of the base and use the min and max terrain hieght to determine the height of the base.
		cube.transform.localScale = size;

		return Instantiate(cube, new Vector3(part.position.x, Terrain.activeTerrain.SampleHeight(part.position), part.position.z), Quaternion.identity) as Transform;
	}
}
