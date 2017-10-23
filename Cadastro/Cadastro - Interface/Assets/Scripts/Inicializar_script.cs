﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inicializar_script : MonoBehaviour {

	public GameObject painelPrincipal;
	public GameObject painelLogin;
	public Text texto;
	public InputField inputfieldLogin;
	public GameObject objetoAvatar;
	public GameObject painelOpcao;

	void Start () {
		painelLogin.SetActive (true);
		painelPrincipal.SetActive(false);
		painelOpcao.SetActive (true);

	}

	void Update(){
		if (Input.GetMouseButtonDown (0)){
			texto.text = "";
		}
		if (inputfieldLogin.text == "") {
			objetoAvatar.GetComponent<Avatar_script> ().atualizar ();
		}
	}

}
