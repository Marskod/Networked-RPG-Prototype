using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EditModePlayer : MonoBehaviour {

    public GameObject player;

	public float speed = 10f;
	public float partDistance = 10f;
	public GameObject subject = null;

    /*Game Objects*/
	public GameObject siliconExtractor;

    public float yoffset = 50; 
    public GUISkin skin;

	private Vector2 scrollPosition;
	private BuildEditorManager buildEditor;
	private Dictionary<string, GameObject> structures = new Dictionary<string, GameObject>();
    private StructureDatabase database;
    private Inventory inv; 
    //private Vector3 baseSize = new Vector3(2,1,2);
	
	// Use this for initialization
	void Start () {
        buildEditor = gameObject.GetComponent<BuildEditorManager>();
		setSubject(GameObject.CreatePrimitive(PrimitiveType.Cube));

        database = GameObject.FindGameObjectWithTag("BuildManager").GetComponent<StructureDatabase>();

		structures.Add("SiliconExtractor", siliconExtractor);

        //Fill the list will empty elements 
	}

	// Update is called once per frame
	void Update () {
		InputMovement();
		defineStructure();
	}
	
	void OnGUI ()
	{
		//scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(200), GUILayout.Height(300));
		//List<string> structures = getStructures();

        GUILayout.BeginArea(new Rect((Screen.width/1.3f), Screen.height/2.3f, 250, 200));

        int i = 0, curr = 0;
        GUI.skin = skin;

        if (player != null)
        {
            print("works");
            inv = player.GetComponent<Inventory>();
        }

        for (i = 0; i < database.buildings.Count; i++)
        {
            if (GUILayout.Button(database.buildings[i].id + "  Cost: " + database.buildings[i].value, "Button") && (inv.monies >= database.buildings[i].value))
            {
                setSubject(database.buildings[i].structure);
                curr = i;
            }
            i++;
        }


		//GUILayout.EndScrollView();
        if (GUILayout.Button("Place Structure","Button"))
        {
            //CREATE COLLISION DETECTION BEFORE PLACE XD
            buildEditor.instantiatePart(subject);
            inv.monies -= database.buildings[curr].value;
        }

        GUILayout.EndArea();
	}

	private void InputMovement () {
		if (Input.GetKey (KeyCode.W))
			GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.forward * speed * Time.deltaTime);
		if (Input.GetKey (KeyCode.S))
			GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position - transform.forward * speed * Time.deltaTime);
		if (Input.GetKey (KeyCode.D))
			GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.right   * speed * Time.deltaTime);
		if (Input.GetKey (KeyCode.A))
			GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position - transform.right   * speed * Time.deltaTime);
		if (Input.GetKey (KeyCode.E))
			GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.up      * speed * Time.deltaTime);
		if (Input.GetKey (KeyCode.Q))
			GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position - transform.up      * speed * Time.deltaTime);
	}
	
	public void setSubject (GameObject _subject) {
		// Initialize subject on player event update
		
		// If transformparameter is null then assign subject to first item in inventory
		if (!_subject) {
			//Debug.Log (structures.Values.First ());
		}

		Destroy(subject);
        //float temp = transform.position.y;
        //Vector3 location = new Vector3(transform.position.x, temp += yoffset, transform.position.z);
		subject = Instantiate(_subject, transform.position + transform.forward * partDistance, Quaternion.identity) as GameObject;
	}
	
	private void defineBase () {
		partDistance += Input.GetAxis("Mouse ScrollWheel") * speed;
		Vector3 baseSize = subject.transform.localScale;
		
		float[] corners = new float[4];
		
		// Check terrain height of all x, z component vectors of base
		corners[0] = Terrain.activeTerrain.SampleHeight(new Vector3( subject.transform.position.x+baseSize.x, 0, subject.transform.position.z));
		corners[1] = Terrain.activeTerrain.SampleHeight(new Vector3( subject.transform.position.x+baseSize.x, 0, subject.transform.position.z+baseSize.z));
		corners[2] = Terrain.activeTerrain.SampleHeight(new Vector3( subject.transform.position.x,            0, subject.transform.position.z+baseSize.z));
		corners[3] = Terrain.activeTerrain.SampleHeight(subject.transform.position);
		
		subject.transform.localScale = new Vector3(baseSize.x,
		                                           corners.Max() - corners.Min(),
		                                           baseSize.z);
		
		//subject.transform.position = new Vector3(gameObject.transform.position.x, Terrain.activeTerrain.SampleHeight(part.position), part.position.z) + transform.forward * partDistance
		Vector3 temp = gameObject.transform.position + transform.forward * partDistance;
		temp.y = Terrain.activeTerrain.SampleHeight(subject.transform.position);
		subject.transform.position = temp;
	}
	
	private void defineStructure () {
		// Update distance from subject based on mouse scroll
		partDistance += Input.GetAxis("Mouse ScrollWheel") * speed;
		Vector3 temp = new Vector3(0,0,0);

		// Update rotation of subject
		if (Input.GetKeyDown(KeyCode.LeftControl)) {
			subject.transform.Rotate(0, 10, 0);
		}
		else if (Input.GetKeyDown(KeyCode.RightControl)) {
			subject.transform.Rotate(0, -10, 0);
		}

		// Update subject position considering terrain elevation and direction of player camera
		temp = gameObject.transform.position + transform.forward * partDistance;
		temp.y = Terrain.activeTerrain.SampleHeight(subject.transform.position) + yoffset;
		subject.transform.position = temp;
	}
	
	public void updateBuildObject () {
		partDistance += Input.GetAxis("Mouse ScrollWheel") * speed;
		subject.transform.position = gameObject.transform.position + transform.forward * partDistance;
	}
	
	public void editingControl () {
		if (Input.GetKeyUp (KeyCode.Mouse0))
			buildEditor.instantiatePart(subject);
		if (Input.GetKeyUp (KeyCode.T))
			//buildEditor.instantiateBase(subject, new Vector3(2,1,2));
			buildEditor.instantiatePart(subject);
	}
}
