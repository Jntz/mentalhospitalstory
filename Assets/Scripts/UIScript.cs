using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScript : MonoBehaviour {
	
	public Text description, option1, option2, option3;
	public Image image;
	public Button restartMenu, exitToMainMenu;

	void Start() {
		restartMenu.gameObject.SetActive (false);
		exitToMainMenu.gameObject.SetActive (false);
	}

	public void updateUIData(Sprite image, string option1, string option2, string option3, string description) {
		this.description.text = description.ToString ();
		this.option1.text = option1;
		this.option2.text = option2;
		this.option3.text = option3;

		this.image.sprite = image;
	}
	public void ToggleMenu() {
		restartMenu.gameObject.SetActive (!restartMenu.IsActive ());
		exitToMainMenu.gameObject.SetActive (!exitToMainMenu.IsActive ());
	}

	public void MenuAction(int option) {
		if (option == 1) {
			Application.LoadLevel(Application.loadedLevel);
		} 
		else if (option == 2) {
			Application.LoadLevel (0);
		}
	}
}
