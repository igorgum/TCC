using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeletarOque_Script : MonoBehaviour {

	public GameObject nomeMenu;

	public GameObject[] listaDeControllerDeDados;

	public void DeletarFormulario(){
		switch (nomeMenu.GetComponent<Text>().text) {
		case "Funcionário":
			listaDeControllerDeDados [0].GetComponent<DadosFuncio> ().Deletar();
			break;
		default:
			Debug.Log ("Indice do SalvarOque caiu no default, revise o código");
			break;
		}
	}
}
