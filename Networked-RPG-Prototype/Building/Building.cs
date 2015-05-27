using UnityEngine;
using System.Collections;

public abstract class Building {
	private string name;

	string Id
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
		}
	}
}
