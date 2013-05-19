using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	GUIContent[] comboBoxList;
	private ComboBox Player1;// = new ComboBox();
	private ComboBox Player2;
	private ComboBox Player3;
	private ComboBox Player4;
	private GUIStyle listStyle = new GUIStyle();
	// Use this for initialization
	void Start () {
		comboBoxList = new GUIContent[4];
		comboBoxList[0] = new GUIContent("Albion");
		comboBoxList[1] = new GUIContent("Fanndis");
		comboBoxList[2] = new GUIContent("Merlini");
		comboBoxList[3] = new GUIContent("Temptress");
 
		listStyle.normal.textColor = Color.white; 
		listStyle.onHover.background =
		listStyle.hover.background = new Texture2D(2, 2);
		listStyle.padding.left =
		listStyle.padding.right =
		listStyle.padding.top =
		listStyle.padding.bottom = 4;
 
		Player1 = new ComboBox(new Rect(Screen.height/35, (int)(Screen.width/5.6), 130, 20), comboBoxList[0], comboBoxList, "button", "box", listStyle);
		Player2 = new ComboBox(new Rect(Screen.height/35, (int)(Screen.width/1.9), 130, 20), comboBoxList[1], comboBoxList, "button", "box", listStyle);
		Player3 = new ComboBox(new Rect(Screen.height + 22, (int)(Screen.width/5.6), 130, 20), comboBoxList[2], comboBoxList, "button", "box", listStyle);
		Player4 = new ComboBox(new Rect(Screen.height + 22, (int)(Screen.width/1.9), 130, 20), comboBoxList[3], comboBoxList, "button", "box", listStyle);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI()
	{
		GUI.Box(new Rect(Screen.height/35,Screen.width/50,130,100),"Player 1");
		GUI.Box(new Rect(Screen.height/35,(int)(Screen.width/2.7),130,100),"Player 2");
		GUI.Box(new Rect(Screen.height + 22,Screen.width/50,130,100),"Player 3");
		GUI.Box(new Rect(Screen.height + 22,(int)(Screen.width/2.7),130,100),"Player 4");
		
		GUI.Box(new Rect((int)(Screen.height/3.3),Screen.width/50,360,200),"Champions");
		Player1.Show();
		Player2.Show();
		Player3.Show();
		Player4.Show();
	}
}
