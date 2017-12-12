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
	public bool standalone; //para usar sem o FixedUpdate

	public void MandarEmail(){
		StartCoroutine ("Mandar");
	}

	IEnumerator Mandar(){
		//ativar carregando
		panelCarregando.SetActive(true);

		//deixar campo.email localmente
		string emailLocal = campoEmail.text;

		//consulta
		WWW txtConsulta = new WWW (controllerOPC.GetComponent<OPC_Controller>().endereco
			+ "/tcc/gerador.php"
			+ "?email=" + emailLocal);

		//segura as pontas
		while (txtConsulta.isDone == false) {
			yield return new WaitForSeconds (1);
		}
	
		//split
		String[] listaDeSubstrings = txtConsulta.text.Split(';');

		if (listaDeSubstrings.Length > 2 && listaDeSubstrings [1] == "kkeaemen" && !txtConsulta.text.Contains ("<title>404 Not Found</title>")) {
			Transform[] listaCriancas = panelEnviado.GetComponentsInChildren<Transform>();
			foreach (Transform filho in listaCriancas) {
				if (filho.name == "txtRESULTADOEMAIL") {
					filho.GetComponent<UnityEngine.UI.Text>().text="Email enviado para\n" + "''"+ emailLocal + "'' !";
					panelEnviado.SetActive (true);
				}
			}
		} else {
			Transform[] listaCriancas = panelEnviado.GetComponentsInChildren<Transform>();
			foreach (Transform filho in listaCriancas) {
				if (filho.name == "txtRESULTADOEMAIL") {
					filho.GetComponent<UnityEngine.UI.Text>().text="Erro no envio para\n" + "''"+ emailLocal + "'' !";
					panelEnviado.SetActive (true);
				}
			}
		}

		//desativar carregando
		panelCarregando.SetActive(false);
	}











	void FixedUpdate(){
		if (!standalone) {
			if (ModificarRegistro.activeSelf) {
				gameObject.GetComponent<Button> ().interactable = true;
			} else {
				gameObject.GetComponent<Button> ().interactable = false;
			}
		}
	}
}
