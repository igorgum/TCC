﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelarOque_Script : MonoBehaviour {

	public GameObject nomeMenu;

	public GameObject[] listaDeControllerDeDados;

	public void DevolverDados(){
		switch (nomeMenu.GetComponent<Text>().text) {
		case "Funcionário":
			listaDeControllerDeDados [0].GetComponent<DadosFuncio> ().DevolverDadosOriginais ();
			break;
		default:
			Debug.Log ("Indice do SalvarOque caiu no default, revise o código");
			break;
		}
	}
}
