using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SalvarOque_script : MonoBehaviour {

	public GameObject nomeMenu;

	public GameObject[] listaDeControllerDeDados;

	public void SalvarFormulario(){
		switch (nomeMenu.GetComponent<Text>().text) {
		case "Funcionário":
			listaDeControllerDeDados [0].GetComponent<DadosFuncio> ().Atualizar ();
			break;
		default:
			Debug.Log ("Indice do SalvarOque caiu no default, revise o código");
			break;
		}
	}
}
