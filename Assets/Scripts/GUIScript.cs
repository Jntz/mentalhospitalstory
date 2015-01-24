using UnityEngine;
using System.Xml;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class GUIScript : MonoBehaviour {

	private Texture currentTexture = new Texture(); //texture which use in background
	public Rect optionRect1, optionRect2, optionRect3, descriptionRect, bgRect;
	public string optionText1, optionText2, optionText3, description;
	private TextAsset gameData;
	public Scene[] scenes; //list of scenes 
	public GUIStyle descriptionGUIStyle;
	public GUISkin guiSkin = new GUISkin ();

	//Static definitions
	private float width, height = 30f, buttonStartingPos = 0f;
	private int currentSceneIndex = 0;

	public List<int> sceneIndexLog = new List<int> (); //logging all user selections

	[System.Serializable]
	public class SceneOption {
		public int id;
		public string text;

		public SceneOption(int id, string text) {
			this.id = id;
			this.text = text;
		}
	}
	[System.Serializable]
	public class Scene {
		public int id;
		public string description;
		public Texture bgImage;
		public SceneOption option1, option2, option3;

		public Scene(int id, string description, Texture bgImage, SceneOption option1, SceneOption option2, SceneOption option3) {
			this.id = id;
			this.description = description;
			this.bgImage = bgImage;
			this.option1 = option1;
			this.option2 = option2;
			this.option3 = option3;
		}
	}
	
	// Use this for initialization
	void Start () {
		loadGameData ();

		buttonStartingPos = Screen.height - (3 * (height + 15f));
		width = Screen.width - 40f;
		initializeRectsPos ();

		setCurrentScene ();

	}
	void setCurrentScene() {
		sceneIndexLog.Add (currentSceneIndex); //add to scene index log
		currentTexture = scenes [currentSceneIndex].bgImage;

		optionText1 = scenes [currentSceneIndex].option1.text;
		optionText2 = scenes [currentSceneIndex].option2.text;
		optionText3 = scenes [currentSceneIndex].option3.text;
		description = scenes [currentSceneIndex].description;
	}
	void loadGameData() {
		int id, i = 0, optionID;
		string description, bgImageName;
		Texture bgImage;
		SceneOption option1, option2, option3;
		XmlNode childNode;

		//Load game data in xml file
		gameData = Resources.Load("gameData") as TextAsset;
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.LoadXml ( gameData.text );

		scenes = new Scene[xmldoc.DocumentElement.ChildNodes.Count];

		foreach (XmlNode node in xmldoc.DocumentElement.ChildNodes) { //loop all scene and initialize scene instance
			//If cannot parse int value in string => continue
			if(!int.TryParse(node.Attributes["id"].Value, out id)) {
				continue;
			}

			bgImageName = (string) node.Attributes["bgImage"].Value;
			bgImage = Resources.Load("Backgrounds/" + bgImageName) as Texture;

			//If cannot parse int value in string => continue
			childNode = node.ChildNodes[0];
			if(!int.TryParse(childNode.ChildNodes[0].Attributes["id"].Value, out optionID)) {
				continue;
			}
			option1 = new SceneOption(optionID, childNode.ChildNodes[0].InnerXml);

			//If cannot parse int value in string => continue
			if(!int.TryParse(childNode.ChildNodes[1].Attributes["id"].Value, out optionID)) {
				continue;
			}
			option2 = new SceneOption(optionID, childNode.ChildNodes[1].InnerXml);

			//If cannot parse int value in string => continue
			if(!int.TryParse(childNode.ChildNodes[2].Attributes["id"].Value, out optionID)) {
				continue;
			}
			option3 = new SceneOption(optionID, childNode.ChildNodes[2].InnerXml);

			childNode = node.ChildNodes[1];
			description = childNode.InnerText.Trim();

			scenes[i++] = new Scene(id, description, bgImage, option1, option2, option3);
		}

	}
	void initializeRectsPos() {
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

	void OnGUI() {
		GUI.DrawTexture (bgRect, currentTexture, ScaleMode.ScaleToFit); //texture fill camera area
		GUI.TextArea (descriptionRect, description, descriptionGUIStyle);
					
		if (GUI.Button (optionRect1, optionText1)) {
			selectOption(1);
		}  
		if (GUI.Button (optionRect2, optionText2)) {
			selectOption(2);
		}
		if (GUI.Button (optionRect3, optionText3)) {
			selectOption(3);
		}
	}
	void selectOption(int selectedOption) {
		//check which button pressed, find id in scenelist and update current scene
		int findThisId;
		Debug.Log ("Press button " + selectedOption);
		switch (selectedOption) {
		case 1:
			findThisId = scenes [currentSceneIndex].option1.id;
			break;
		case 2:
			findThisId = scenes [currentSceneIndex].option2.id;
			break;
		case 3: 
			findThisId = scenes [currentSceneIndex].option3.id;
			break;
		default:
			return; // < 0 and > 3 is not allowed!
		}

		for (int i = 0; i < scenes.Length; i++) { 
			if(scenes[i].id == findThisId) {
				currentSceneIndex = i;
				break;
			}
		}
		setCurrentScene (); //update current scene texture, description and buttons
	}
}
