using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnviarEmail_Script : MonoBehaviour {

	public InputField campoEmail;
	public GameObject panelEnviado;
	public GameObject panelCarregando;
	public GameObject controllerOPC;
	public GameObject ModificarRegistro;

	public void MandarEmail(){
		StartCoroutine ("Mandar");
	}

	IEnumerator Mandar(){
		//ativar carregando
		panelCarregando.SetActive(true);

		//consulta
		WWW txtConsulta = new WWW (controllerOPC.GetComponent<OPC_Controller>().endereco
			+ "/tcc/gerador.php"
			+ "?email=" + campoEmail.text);

		//segura as pontas
		while (txtConsulta.isDone == false) {
			yield return new WaitForSeconds (1);
		}
	
		//split
		String[] listaDeSubstrings = txtConsulta.text.Split(';');

		if (listaDeSubstrings.Length > 2 && listaDeSubstrings [1] == "kkeaemen") {
			Transform[] listaCriancas = panelEnviado.GetComponentsInChildren<Transform>();
			foreach (Transform filho in listaCriancas) {
				if (filho.name == "txtRESULTADOEMAIL") {
					filho.GetComponent<UnityEngine.UI.Text>().text="Email enviado para\n" + "''"+ campoEmail.text + "'' !";
					panelEnviado.SetActive (true);
				}
			}
		} else {
			Transform[] listaCriancas = panelEnviado.GetComponentsInChildren<Transform>();
			foreach (Transform filho in listaCriancas) {
				if (filho.name == "txtRESULTADOEMAIL") {
					filho.GetComponent<UnityEngine.UI.Text>().text="Erro no envio para\n" + "''"+ campoEmail.text + "'' !";
					panelEnviado.SetActive (true);
				}
			}
		}

		//desativar carregando
		panelCarregando.SetActive(false);
	}











	void FixedUpdate(){
		if (ModificarRegistro.activeSelf) {
			gameObject.GetComponent<Button> ().interactable = true;
		} else {
			gameObject.GetComponent<Button> ().interactable = false;
		}
	}
}
