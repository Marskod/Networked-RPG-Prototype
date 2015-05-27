using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StructureDatabase : MonoBehaviour {

    public List<Structure> buildings = new List<Structure>();
	
	void Awake()
	{
        //buildings.Add(new Structure("SiliconExtractor",300));
	}
}
