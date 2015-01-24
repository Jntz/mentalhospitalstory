using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

	private GameScript gameScript;
	public Texture currentTexture = new Texture(); //texture which use in background
	public Rect optionRect1, optionRect2, optionRect3, descriptionRect, bgRect, menuRect, menuRestartRect, menuExitToMainMenuRect, menuCloseMenuRect;
	public string optionText1, optionText2, optionText3, description, menuText = "Menu", menuRestart = "Restart Game",
					menuExitToMainMenu = "Exit to Main Menu";

	public GUIStyle descriptionGUIStyle;
	private bool menuOpen = false;
	

	// Use this for initialization
	void Start () {
		gameScript = gameObject.GetComponent<GameScript> ();
	
		initializeRectsPos ();
	}
	
	void initializeRectsPos() {
		float width, height = 30f, buttonStartingPos = 0f, menuYOffset = 5f;

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

		menuRect = new Rect (5f, menuYOffset, 50f, 20f);

		menuRestartRect = new Rect (5f, menuRect.y + menuYOffset + menuRect.height, 120f, 20f);
		menuExitToMainMenuRect = new Rect (5f, menuRestartRect.y + menuYOffset + menuRestartRect.height, 120f, 20f);

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
		if (GUI.Button (menuRect, menuText)) {
			menuOpen = !menuOpen;	//toggle menuOpen value true/false
		}

		if (menuOpen) {
			drawMenu ();
		}
	}
	void drawMenu() {
		if (GUI.Button (menuRestartRect, menuRestart)) {
			Application.LoadLevel(Application.loadedLevel);
		}
		if (GUI.Button (menuExitToMainMenuRect, menuExitToMainMenu)) {

		}
	}
}
