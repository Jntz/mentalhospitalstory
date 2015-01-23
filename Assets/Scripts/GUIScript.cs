using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

	public Texture[] textures;
	private Texture currentTexture; //texture which use in background
	public Rect optionRect1, optionRect2, optionRect3;
	public string optionText1, optionText2, optionText3;
	public GUIStyle buttonStyle;

	//Static definitions
	private float width = 100f, height = 30f, buttonStartingPos = 0f;


	// Use this for initialization
	void Start () {
		buttonStartingPos = Screen.height - (3 * (height + 15f));
		initializeOptions ();
		loadAllBackground ();
		currentTexture = textures [0];
	}

	void initializeOptions() {
		float x = 20f, y = buttonStartingPos;

		optionRect1 = new Rect (x, y, width, height);
		y += height + 15f;
		optionText1 = "Teksti1";

		optionRect2 = new Rect (x, y, width, height);
		y += height + 15f;
		optionText2 = "Teksti2";

		optionRect3 = new Rect (x, y, width, height);
		optionText3 = "Teksti3";
	}


	void loadAllBackground() {
		Object[] obj_textures = Resources.LoadAll("Backgrounds", typeof(Texture));
		
		textures = new Texture[obj_textures.Length];
		for (int i = 0; i < obj_textures.Length; i++) {
			textures[i] = obj_textures[i] as Texture;
		}
	}
	
	void selectOption1() {
		Debug.Log ("Press button one");		
	}
	void selectOption2() {
		Debug.Log ("Press button two");
	}
	void selectOption3() {
		Debug.Log ("Press button three");
	}

	void OnGUI() {
		GUI.DrawTexture (new Rect (0f, 0f, Screen.width, Screen.height), currentTexture, ScaleMode.ScaleToFit); //texture fill camera area
					
		if (GUI.Button (optionRect1, optionText1)) {
			selectOption1();
		}  
		if (GUI.Button (optionRect2, optionText2)) {
			selectOption2();
		}
		if (GUI.Button (optionRect3, optionText3)) {
			selectOption3();
		}
	}
}
