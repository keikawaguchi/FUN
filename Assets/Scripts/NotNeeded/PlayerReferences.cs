// PlayerReference.cs
// Holds references to all players in a game.

using UnityEngine;
using System.Collections;

public class PlayerReferences : MonoBehaviour {

	GameObject[] players;

	void Start () {
		players = GameObject.FindGameObjectsWithTag("Player");
	}
	
	public GameObject getPlayer(int playerID) {
		return players[playerID];
	}
}
