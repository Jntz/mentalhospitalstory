using UnityEngine;
using System.Xml;
using System.IO;
using System.Collections;

public class GUIScript : MonoBehaviour {

	public Texture[] textures;
	private Texture currentTexture = new Texture(); //texture which use in background
	public Rect optionRect1, optionRect2, optionRect3;
	public string optionText1, optionText2, optionText3;
	public GUIStyle buttonStyle;
	private TextAsset gameData;

	public Scene[] scenes;

	//Static definitions
	private float width = 100f, height = 30f, buttonStartingPos = 0f;
	private int index = 0, jumpingValue;//jumping value = where is next texture in array, example 0 => first, 1 = empty (or different start), 2 = empty (or different start) 
										//3 => first and select first option (start => press option 1), 4 => first and select second option, 5 => first and select third option
	private int currentSceneIndex = 0;

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
		SceneOption option1, option2, option3;

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
		initializeOptions ();
		loadAllBackground ();
	}
	void loadGameData() {
		gameData = Resources.Load("gameData") as TextAsset;
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.LoadXml ( gameData.text );

		int id, i = 0, optionID;
		string description, bgImageName;
		Texture bgImage;
		SceneOption option1, option2, option3;

		scenes = new Scene[xmldoc.DocumentElement.ChildNodes.Count];

		foreach (XmlNode node in xmldoc.DocumentElement.ChildNodes) {

			if(!int.TryParse(node.Attributes["id"].Value, out id)) {
				continue;
			}
			//id = (int) int. node.Attributes["id"].Value;
			bgImageName = (string) node.Attributes["bgImage"].Value;
			bgImage = Resources.Load("Backgrounds/" + bgImageName) as Texture;

			if(!int.TryParse(node.ChildNodes[0].Attributes["id"].Value, out optionID)) {
				continue;
			}
			option1 = new SceneOption(optionID, node.ChildNodes[0].Value);

			if(!int.TryParse(node.ChildNodes[1].Attributes["id"].Value, out optionID)) {
				continue;
			}
			option2 = new SceneOption(optionID, node.ChildNodes[1].Value);

			if(!int.TryParse(node.ChildNodes[2].Attributes["id"].Value, out optionID)) {
				continue;
			}
			option3 = new SceneOption(optionID, node.ChildNodes[2].Value);

			description = node.ChildNodes[3].InnerText.Trim();

			scenes[i++] = new Scene(id, description, bgImage, option1, option2, option3);
		}

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
		currentTexture = textures [index];
	}
	
	void selectOption1() {
		Debug.Log ("Press button one");

		//currentTexture = textures[]
	}
	void selectOption2() {
		Debug.Log ("Press button two");
	}
	void selectOption3() {
		Debug.Log ("Press button three");
	}

	void OnGUI() {
		GUI.DrawTexture (new Rect (0f, 0f, Screen.width, Screen.height), currentTexture, ScaleMode.ScaleToFit); //texture fill camera area
					
		if (GUI.Button (optionRect1, optionText1, buttonStyle)) {
			selectOption1();
		}  
		if (GUI.Button (optionRect2, optionText2, buttonStyle)) {
			selectOption2();
		}
		if (GUI.Button (optionRect3, optionText3, buttonStyle)) {
			selectOption3();
		}
	}
}
