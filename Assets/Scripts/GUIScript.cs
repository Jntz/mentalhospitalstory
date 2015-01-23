using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

	public Texture[] textures;
	public Rect optionRect1, optionRect2, optionRect3;

	// Use this for initialization
	void Start () {
		initializeOptionRects ();
		loadAllBackground ();
	}

	void initializeOptionRects() {
		float width = 100f, height = 30f;
		float x = 20f, y = Screen.height - (4* (height + 15f));

		optionRect1 = new Rect (x, y, width, height);
		y += height + 15f;
		optionRect2 = new Rect (x, y, width, height);
		y += height + 15f;
		optionRect3 = new Rect (x, y, width, height);
	}


	void loadAllBackground() {
		Object[] obj_textures = Resources.LoadAll("Backgrounds", typeof(Texture));
		
		textures = new Texture[obj_textures.Length];
		for (int i = 0; i < obj_textures.Length; i++) {
			textures[i] = obj_textures[i] as Texture;
		}
	}

	
	// Update is called once per frame
	void Update () {
		
	}
	void OnGUI() {
		//Scene: 1 background kuva, 3-4 vaihtoehtoa josta vaihtuu seuraavaan skeneen ja pitää tallentaa pelaajan eteneminen muistiin
		GUI.DrawTexture (new Rect (0f, 0f, Screen.width, Screen.height), textures[0], ScaleMode.ScaleToFit);

		//kolme vaihtoehtoa 1 2 3 
		if (GUI.Button (optionRect1, "test")) {
			Debug.Log ("Painoit ykköstä");		
		}  
		if (GUI.Button (optionRect2, "test2")) {
			Debug.Log ("Painoit kakkosta");
		}
		if (GUI.Button (optionRect3, "test3")) {
			Debug.Log ("Painoit kolmosta");
		}

	}
}
