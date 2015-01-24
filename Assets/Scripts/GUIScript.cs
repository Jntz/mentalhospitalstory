using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

	private GameScript gameScript;
	public Texture currentTexture = new Texture(); //texture which use in background
	public Rect optionRect1, optionRect2, optionRect3, descriptionRect, bgRect, menuRect, menuRestartRect, menuExitToMainMenuRect, menuCloseMenuRect;
	public string optionText1, optionText2, optionText3, description, menuText = "Menu", menuRestart = "Restart Game",
					menuExitToMainMenu = "Exit to Main Menu";

	public GUIStyle descriptionGUIStyle, buttonGUIStyle;
	private bool menuOpen = false;

	// Use this for initialization
	void Start () {
		gameScript = gameObject.GetComponent<GameScript> ();

		initializeRectsPos ();
	}
	
	void initializeRectsPos() {
		float width = Screen.width * 0.6f, height = 30f, buttonStartingPos = Screen.height * .6f, 
			menuXOffset = 5f, x = 50f, y = buttonStartingPos, descriptionWidth = Screen.width - 40f, descriptionHeight = Screen.height * 0.7f, 
			centerWidth = Screen.width / 2f, centerHeight = Screen.height / 2f;

		optionRect1 = new Rect (x, y, width - x, height);
		y += height + 15f;	//update to next button y-position

		optionRect2 = new Rect (x, y, width - x, height);
		y += height + 15f; //update to next button y-position

		optionRect3 = new Rect (x, y, width - x, height);

		bgRect = new Rect (width + 20f, 
		                   Screen.height * .6f,
		                   (Screen.width * 0.4f)-20f, 
		                   Screen.height * .4f);
		descriptionRect = new Rect (centerWidth - (descriptionWidth / 2f), 30f, descriptionWidth, descriptionHeight);

		menuRect = new Rect (menuXOffset, 5f, 50f, 20f);

		menuRestartRect = new Rect (menuRect.x + menuXOffset + menuRect.width, 5f, 120f, 20f);
		menuExitToMainMenuRect = new Rect (menuRestartRect.x + menuXOffset + menuRestartRect.width, 5f, 120f, 20f);

	}

	public void updateGUIdata(Texture bgImage, string option1, string option2, string option3, string desc) {
		currentTexture = bgImage;
		optionText1 = option1;
		optionText2 = option2;
		optionText3 = option3;
		description = desc;
	}

	void OnGUI() {
		GUI.DrawTexture (bgRect, currentTexture, ScaleMode.StretchToFill); //texture fill camera area
		GUI.TextField (descriptionRect, description, descriptionGUIStyle);

		if (menuOpen) {
			drawMenu ();
		}

		if (GUI.Button (optionRect1, optionText1, buttonGUIStyle)) {
			gameScript.selectOption(1);
		}  
		if (GUI.Button (optionRect2, optionText2, buttonGUIStyle)) {
			gameScript.selectOption(2);
		}
		if (GUI.Button (optionRect3, optionText3, buttonGUIStyle)) {
			gameScript.selectOption(3);
		}
		if (GUI.Button (menuRect, menuText)) {
			menuOpen = !menuOpen;	//toggle menuOpen value true/false
		}
	}
	void drawMenu() {
		if (GUI.Button (menuRestartRect, menuRestart)) {
			Application.LoadLevel(Application.loadedLevel);
		}
		if (GUI.Button (menuExitToMainMenuRect, menuExitToMainMenu)) {
			Application.LoadLevel (0);
		}
	}
}
