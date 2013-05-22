using UnityEngine;
using System.Collections;
using System.Collections.Specialized;

public class PopupText : MonoBehaviour {
	
	// Textures
	const string PLUS_SPEED_TEXTURE = "Textures/Text/plusSpeed";
	const string MINUS_SPEED_TEXTURE = "Textures/Text/minusSpeed";
	const string PLUS_BOMBS_TEXTURE = "Textures/Text/plusBombs";
	const string MINUS_BOMBS_TEXTURES = "Textures/Text/minusBombs";
	const string PLUS_EXPLOSION_TEXTURE = "Textures/Text/plusExplosion";
	const string MINUS_EXPLOSION_TEXTURE = "Textures/Text/minusExplosion";
	const string STUN_TEXTURE = "Textures/Text/stun";
	const string SLOWED_TEXTURE = "Textures/Text/slowed";
	
	// Texture factory
	OrderedDictionary textureFactory;
	
	private float duration = 1.0f;
	private float sizeScale = 1.0f;
	private bool shouldFade = true;
	private float startTime;
	
	private Texture texture;
	
	void Update () {
		if (Time.time - startTime > duration) {
			Destroy(gameObject);
		}
	}
	
	public void initialize() {
		startTime = Time.time;
		textureFactory = new OrderedDictionary();
		textureFactory["PlusSpeed"] = PLUS_SPEED_TEXTURE;
		textureFactory["MinusSpeed"] = MINUS_SPEED_TEXTURE;
		textureFactory["PlusBombs"] = PLUS_BOMBS_TEXTURE;
		textureFactory["MinusBombs"] = MINUS_BOMBS_TEXTURES;
		textureFactory["PlusExplosion"] = PLUS_EXPLOSION_TEXTURE;
		textureFactory["MinusExplosion"] = MINUS_EXPLOSION_TEXTURE;
		textureFactory["Stun"] = STUN_TEXTURE;
		textureFactory["Slowed"] = SLOWED_TEXTURE;
	}
	
	public void setPredefinedText(string text) {
		string texturePath = (string)textureFactory[text];
		if (texturePath == "") {
			return;
		}
		texture = Resources.Load(texturePath) as Texture;
		renderer.material.mainTexture = texture;
	}
	
	public void setDuration(float time) {
		duration = time;
	}
	
	public void setPosition(float x, float y) {
		Vector3 newPos = new Vector3(x, 15.0f, y);
		transform.position = newPos;
	}
	
	public void setSizeScale(float scale) {
		sizeScale = scale;
	}
	
	public void setShouldFade(bool fade) {
		shouldFade = fade;
	}
}
