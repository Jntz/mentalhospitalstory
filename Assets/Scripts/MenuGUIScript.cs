using UnityEngine;
using System.Collections;

public class MenuGUIScript : MonoBehaviour {

	public Rect startGameRect, creditsRect;
	private string startGame = "Start the Game", 
		credits = "CREDITS:\n\n" +
			"Nimi1: Mitä teki\n" + 
			"Nimi2: Mitä teki\n" + 
			"Nimi3: Mitä teki\n" + 
			"Nimi4: Mitä teki\n\n" + 
			"FGJ/GGJ Rovaniemi 2015";
	public GUIStyle creditsGUIStyle;


	// Use this for initialization
	void Start () {
		float btnWidth = 300f, btnHeight = 30f, screenCenterWidth = Screen.width / 2f, screenCenterHeight = Screen.height / 2f, yOffset = 10f;

		startGameRect = new Rect (screenCenterWidth - (btnWidth/2f), screenCenterHeight - (btnHeight/2f), btnWidth, btnHeight);
		creditsRect = new Rect (startGameRect.x, startGameRect.y + startGameRect.height + yOffset, 300f, 300f);
	}

	void OnGUI() {
		GUI.TextField (creditsRect, credits, creditsGUIStyle);
		
		if (GUI.Button (startGameRect, startGame)) {
			Application.LoadLevel (1);
		}  
	}
}
