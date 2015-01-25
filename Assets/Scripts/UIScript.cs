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
		this.description.text = description.Replace("  ", string.Empty);
		this.option1.text = option1;

		if (string.Equals (this.option1.text, string.Empty)) {
			this.option1.gameObject.SetActive (false);
		} 
		else {
			this.option1.gameObject.SetActive (true);	
		}

		this.option2.text = option2;
		if (string.Equals (this.option2.text, string.Empty)) {
			this.option2.gameObject.SetActive (false);
		} 
		else {
			this.option2.gameObject.SetActive (true);
		}

		this.option3.text = option3;
		if (string.Equals (this.option3.text, string.Empty)) {
			this.option3.gameObject.SetActive (false);
		} 
		else {
			this.option3.gameObject.SetActive (true);
		}

		this.image.sprite = image;
		if (this.image.sprite == null) {
			this.image.gameObject.SetActive(false);
		}
		else {
			this.image.gameObject.SetActive(true);
		}
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
