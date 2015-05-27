using UnityEngine;
using System.Collections;

public class Extractor : Building {
	private string name;
	private int rank = 0;

	public Extractor()
	{

	}

	public int promote()
	{
		return ++rank;
	}

	public int extract()
	{
		switch(rank)
		{
		case 0:
			return 10;
		case 1:
			return 100;
		case 2:
			return 1000;
		case 3:
			return 5000;
		case 4:
			return 10000;
		}

		return 0;
	}
}
