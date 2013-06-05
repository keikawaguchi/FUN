using UnityEngine;
using System.Collections;

public class ChampInfo : MonoBehaviour {
	private string[] champNames;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		champNames[0] = "Albion";
		champNames[1] = "Fanndis";
		champNames[2] = "Kirito";
		champNames[3] = "Merlini";
		champNames[4] = "Temptress";
	}
	
	public string getChampName(int index) {
		return champNames[index];
	}
	
//	public string getSkillDescription(string championName) {
//		if (championName == "Albion")
			
//	}
}
