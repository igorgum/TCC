using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicializar_script : MonoBehaviour {

	public GameObject painelPrincipal;
	public GameObject painelLogin;

	void Start () {
		painelLogin.SetActive (true);
		painelPrincipal.SetActive(false);
	}

}
