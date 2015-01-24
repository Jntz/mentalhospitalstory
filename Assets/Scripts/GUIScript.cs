using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

	private GameScript gameScript;
	public Texture currentTexture = new Texture(); //texture which use in background
	public Rect optionRect1, optionRect2, optionRect3, descriptionRect, bgRect;
	public string optionText1, optionText2, optionText3, description;

	public GUIStyle descriptionGUIStyle;

	//Static definitions
	private float width, height = 30f, buttonStartingPos = 0f;

	// Use this for initialization
	void Start () {
		gameScript = gameObject.GetComponent<GameScript> ();
	
		initializeRectsPos ();
	}
	
	void initializeRectsPos() {
		buttonStartingPos = Screen.height - (3 * (height + 15f));
		width = Screen.width - 40f;

		float x = 20f, y = buttonStartingPos, descriptionWidth = 300f, descriptionHeight = 300f, 
				centerWidth = Screen.width / 2f, centerHeight = Screen.height / 2f;

		optionRect1 = new Rect (x, y, width, height);
		y += height + 15f;	//update to next button y-position

		optionRect2 = new Rect (x, y, width, height);
		y += height + 15f; //update to next button y-position

		optionRect3 = new Rect (x, y, width, height);

		bgRect = new Rect (0f, 0f, Screen.width, Screen.height); //background texture is fullscreen
		descriptionRect = new Rect (centerWidth - (descriptionWidth / 2f), centerHeight - (descriptionWidth / 2f), descriptionWidth, descriptionHeight);
	}

	public void updateGUIdata(Texture bgImage, string option1, string option2, string option3, string desc) {
		currentTexture = bgImage;
		optionText1 = option1;
		optionText2 = option2;
		optionText3 = option3;
		description = desc;
	}

	void OnGUI() {
		GUI.DrawTexture (bgRect, currentTexture, ScaleMode.ScaleToFit); //texture fill camera area
		GUI.TextField (descriptionRect, description, descriptionGUIStyle);
					
		if (GUI.Button (optionRect1, optionText1)) {
			gameScript.selectOption(1);
		}  
		if (GUI.Button (optionRect2, optionText2)) {
			gameScript.selectOption(2);
		}
		if (GUI.Button (optionRect3, optionText3)) {
			gameScript.selectOption(3);
		}
	}
}
