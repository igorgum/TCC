using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginInputField_script : MonoBehaviour {

	public InputField login;
	public InputField campo;

	// Use this for initialization
	public void LoginNotNull () {
		if (login.text != "") {
			campo.interactable = true;
		} else {
			campo.interactable = false;
		}
	}

}
