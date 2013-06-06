using UnityEngine;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

public class ChampInfo : MonoBehaviour {
	// champion texture path
	private const string CHAMP_BOX_IMAGE_PATH = "Textures/Menu/ChampBox";
	private const string ALBION_IMAGE_PATH = "Textures/Champions/AlbionIcon";
	private const string FANNDIS_IMAGE_PATH = "Textures/Champions/FanndisIcon";
	private const string KIRITO_IMAGE_PATH = "Textures/Champions/KiritoIcon";
	private const string MERLINI_IMAGE_PATH = "Textures/Champions/MerliniIcon";
	private const string TEMPTRESS_IMAGE_PATH = "Textures/Champions/TemptressIcon";
	
	private int TOTAL_AVAILABLE_CHAMPS = 5;
	private int MAX_UNIQUE_SKILLS = 2;
	
//	private TextAsset champFile;
	private Texture2D[] champTextures;
	private string[] champNames;
//	private string[] skillsNames;
//	private string[] skillsDescrip;
//	private int [] skillsCDs;
	private int index;
	
	// Use this for initialization
	void Start () {
		initializeVariables ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
//	public void constructChampInfo(int index) {
//		champFile = loadChampFile (index);
//		StringReader reader;
//		if (champFile.text == null)
//			Debug.Log ("it's null");
//		reader = new StringReader(champFile.text);
//		if (reader == null) {
//			Debug.Log ("File not found or not readable");
//			return;
//		}
//		
//		loadTexture (champFile, index);
//	}
//	
	public void setCurrentIndex(int index) {
		this.index = index;
	}
	
	public int getCurrentIndex() {
		return index;
	}
	
	public Texture2D getChampTexture(int index) {
		return champTextures[index];
	}
	
	public void showChampTexture(Rect rect, int index) {
		
		GUI.Box (rect, champTextures[index]);
	}
	
	public string getChampName(int index) {
		return champNames[index];
	}
	
	public string getSkillDescription(int index) {
		switch(index) {
		case 1:
			return "Holy Trap - CD: 10 sec\n" +
				"Albion places a trap on the map that is visible for 1 second " +
				"by everyone, to stun whoever walks through it except the hero places the trap.\n" +
				"Holy Blink - CD: 10 sec\n" +
				"Albion teleports 3 squares to the front direction of the hero.";
		default:
			return "No skills";
		}
	}
//	
	public void initializeVariables() {
		champTextures = new Texture2D[TOTAL_AVAILABLE_CHAMPS + 1];
		champNames = new string[TOTAL_AVAILABLE_CHAMPS + 1];
//		skillsNames = new string[MAX_UNIQUE_SKILLS];
//		skillsDescrip = new string[MAX_UNIQUE_SKILLS];
//		skillsCDs = new int[MAX_UNIQUE_SKILLS];
		
		index = 1;  // 0 is not used
		
		setChampNames();
		
	}
//	
	private void setChampNames() {
		champNames[1] = "Albion";
		champNames[2] = "Fanndis";
		champNames[3] = "Kirito";
		champNames[4] = "Merlini";
		champNames[5] = "Temptress";
	}
	
	private void setTextures() {
		champTextures[0] = Resources.Load (CHAMP_BOX_IMAGE_PATH) as Texture2D;
		champTextures[1] = Resources.Load (ALBION_IMAGE_PATH) as Texture2D;
		champTextures[2] = Resources.Load (FANNDIS_IMAGE_PATH) as Texture2D;
		champTextures[3] = Resources.Load (KIRITO_IMAGE_PATH) as Texture2D;
		champTextures[4] = Resources.Load (MERLINI_IMAGE_PATH) as Texture2D;
		champTextures[5] = Resources.Load (TEMPTRESS_IMAGE_PATH) as Texture2D;
	}
//	
//	private TextAsset loadChampFile(int index) {
////		string mapToLoad = maps [mapID];
////		if (mapToLoad == null) {
////			Debug.Log ("MapID " + mapID + " does not exist.");
////		}	
////		return Resources.Load (mapToLoad, typeof(TextAsset)) as TextAsset;
//		
//		string fileToLoad = "Resources/Champions Info/" + champNames[index];
//		Debug.Log ("File path: " + fileToLoad);
//		return Resources.Load (fileToLoad, typeof(TextAsset)) as TextAsset;
//	}
//	
//	private void loadTexture(TextAsset file, int index) {
//		Match match;
//	
//		match = Regex.Match(file.text, @"Path:(.*)");
//		if (match.Success) {
//			champTexture = Resources.Load(match.Groups[1].ToString()) as Texture2D;
//			Debug.Log("Champion Texture: " + match.Groups[1].ToString ());
//		}
//	}
}
