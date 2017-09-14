using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inicializar_script : MonoBehaviour {

	public GameObject painelPrincipal;
	public GameObject painelLogin;
	public Text texto;

	void Start () {
		painelLogin.SetActive (true);
		painelPrincipal.SetActive(false);


	}

	void Update(){
		if (Input.GetMouseButtonDown (0)){
			texto.text = "";
		}
	}

}
