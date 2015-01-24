using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class GameScript : MonoBehaviour {
	
	UIScript uiScript;
	private TextAsset gameData;
	public Scene[] scenes; //list of scenes 

	//static definitions
	private int currentSceneIndex = 0;
	
	public List<int> sceneIndexLog = new List<int> (); //logging all user selections

	// Use this for initialization
	void Start () {
		uiScript = gameObject.GetComponent<UIScript> ();
		loadGameData ();
		setCurrentScene (currentSceneIndex); //start to first index
	}

	public void setCurrentScene(int newSceneIndex) {
		currentSceneIndex = newSceneIndex;
		Scene scene = scenes [currentSceneIndex];
		sceneIndexLog.Add (currentSceneIndex); //add to scene index log

		uiScript.updateUIData (scene.bgImage, scene.option1.text, scene.option2.text, scene.option3.text, scene.description);

		//guiScript.updateGUIdata (scene.bgImage, scene.option1.text, scene.option2.text, scene.option3.text, scene.description);
	}

	void loadGameData() {
		int i = 0;
		string description, bgImageName, id, optionID;
		Sprite bgImage;
		SceneOption option1, option2, option3;
		XmlNode childNode;
		
		//Load game data in xml file
		gameData = Resources.Load("gameData") as TextAsset;
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.PreserveWhitespace = false;
		xmldoc.LoadXml ( gameData.text );
		
		scenes = new Scene[xmldoc.DocumentElement.ChildNodes.Count];
		
		foreach (XmlNode node in xmldoc.DocumentElement.ChildNodes) { //loop all scene and initialize scene instance
			//If cannot parse int value in string => continue
			id = node.Attributes["id"].Value;
			
			bgImageName = (string) node.Attributes["bgImage"].Value;
			bgImage = Resources.Load<Sprite>("Sprites/" + bgImageName);
			
			//If cannot parse int value in string => continue
			childNode = node.ChildNodes[0];
			optionID = childNode.ChildNodes[0].Attributes["id"].Value;
			option1 = new SceneOption(optionID, childNode.ChildNodes[0].InnerXml.Trim());

			optionID = childNode.ChildNodes[1].Attributes["id"].Value;
			option2 = new SceneOption(optionID, childNode.ChildNodes[1].InnerXml.Trim());

			optionID = childNode.ChildNodes[2].Attributes["id"].Value;
			option3 = new SceneOption(optionID, childNode.ChildNodes[2].InnerXml.Trim());
			
			childNode = node.ChildNodes[1];
			description = childNode.InnerText. Trim();
			
			scenes[i++] = new Scene(id, description, bgImage, option1, option2, option3);
		}
		
	}

	public void selectOption(int selectedOption) {
		//check which button pressed, find id in scenelist and update current scene
		string findThisId;
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
			 
			if(string.Equals(scenes[i].id, findThisId)) {
				setCurrentScene(i); //update current scene texture, description and buttons
				return;
			}
		}
	}
	
	[System.Serializable]
	public class SceneOption {
		public string id, text;
		
		public SceneOption(string id, string text) {
			this.id = id;
			this.text = text;
		}
	}
	[System.Serializable]
	public class Scene {
		public string id, description;
		public Sprite bgImage;
		public SceneOption option1, option2, option3;
		
		public Scene(string id, string description, Sprite bgImage, SceneOption option1, SceneOption option2, SceneOption option3) {
			this.id = id;
			this.description = description;
			this.bgImage = bgImage;
			this.option1 = option1;
			this.option2 = option2;
			this.option3 = option3;
		}
	}
}
