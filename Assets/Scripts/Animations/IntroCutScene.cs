using UnityEngine;
using System.Collections;

public class IntroCutScene : MonoBehaviour {
	
	const int numOfCharacters = 5;
	
	// 0:Albion, 1:Fanndis, 2:Merlini, 3:Temptress, 4:Kirito
	GameObject[] Characters;


	void Start () {
		Characters = new GameObject[numOfCharacters];
		findAllGameObjects();
		loadSpritesheets();
		setStartingPositions();	
	}
	
	void Update () {
		Vector3 pos;
		for (int i = 0; i < numOfCharacters; i++) {
			pos = Characters[i].transform.position;
			pos.x += 1;
			if (pos.x >= 160) {
				pos.x = -160;
			} 
			
			Characters[i].transform.position = pos;
		}
	}
	
	private void findAllGameObjects() {
		Characters[0] = GameObject.Find ("Albion") as GameObject;
		Characters[1] = GameObject.Find ("Fanndis") as GameObject;
		Characters[2] = GameObject.Find ("Merlini") as GameObject;
		Characters[3] = GameObject.Find ("Temptress") as GameObject;
		Characters[4] = GameObject.Find ("Kirito") as GameObject;
	}
	
	private void loadSpritesheets() {
		Characters[0].renderer.material.mainTexture = 
			Resources.Load("Textures/SpriteSheets/Characters/Albion/AlbionRunningSpritesheet") as Texture;
		Characters[1].renderer.material.mainTexture = 
			Resources.Load("Textures/SpriteSheets/Characters/Fanndis/FanndisRunningSpritesheet") as Texture;
		Characters[2].renderer.material.mainTexture = 
				Resources.Load("Textures/SpriteSheets/Characters/Merlini/MerliniRunningSpritesheet") as Texture;
		Characters[3].renderer.material.mainTexture = 
				Resources.Load("Textures/SpriteSheets/Characters/Temptress/TemptressRunningSpritesheet") as Texture;
		Characters[4].renderer.material.mainTexture = 
				Resources.Load("Textures/SpriteSheets/Characters/Ninja/NinjaRunningSpritesheet") as Texture;
	}
	
	private void setStartingPositions() {
		Vector3 position = new Vector3(-100, 0, 0);
		for (int i = 0; i < numOfCharacters; i++) {
			position.x -= 60;
			Characters[i].transform.position = position;
		}
	}
}
