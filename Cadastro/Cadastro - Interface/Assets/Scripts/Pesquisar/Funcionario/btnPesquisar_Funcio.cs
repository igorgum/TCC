using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class btnPesquisar_Funcio : MonoBehaviour {

	public GameObject controllerOPC; //pra pegar o endereço da URL
	public Dropdown dropdownPor;
	public Transform Content; //esse é o CONTENT do VIEWPORT
	//listar todos os possiveis inputfields
	public InputField inputfieldNome;
	public InputField inputfieldEmail;
	public InputField inputfieldRFID;

	public Dropdown dropdownFuncao; /*somente usado por funcionario*/
	public UnityEngine.Object prefab; //um prefab de botão, pra usar como molde pros resultados
	public UnityEngine.Object naoEncontreiNada; //um prefab de texto escrito q ñ encontrou nada



	public GameObject Loading;






	//Deve ser chamado pelo OnValueChange do dropdownPor
	public void AtualizaInteractable() {
		/* se não houver um segundo dropdown como no caso de Funcao
		if (dropdownPor.value != 0) {
			gameObject.GetComponent<Button>().interactable = true;
		} else {
			gameObject.GetComponent<Button>().interactable = false;
		}
		*/

		/*se houver segundo dropdown*/
		if (dropdownPor.value != 0) {
			if (dropdownPor.value == 3) { //Se DROPDOWN estiver em FUNÇÃO
				if (dropdownFuncao.value != 0) { //Se DROPDOWN FUNÇÃO não estiver zerado 
					gameObject.GetComponent<Button> ().interactable = true;
				} else {
					gameObject.GetComponent<Button> ().interactable = false;
				}
			} else {//Se DROPDOWN NÃO estiver em FUNÇÃO
				gameObject.GetComponent<Button> ().interactable = true;
			}

		} else {
			gameObject.GetComponent<Button>().interactable = false;
		}
	}




	//deve ser chamado pelo próprio botão Pesquisar
	public void Pesquisar(){
		//esse switch pega o valor do dropdown "POR" e 
		//associa o inputfield correto pra puxar no banco
		switch (dropdownPor.value) {
		case 1:
			LimpaContent ();
			StartCoroutine(ConsultaPorNome());
			break;
		case 2: 
			LimpaContent ();
			StartCoroutine(ConsultaPorEmail());
			break;
		case 3:
			LimpaContent ();
			//StartCoroutine(ConsultaPorFuncao());
			break;
		case 4:
			LimpaContent ();
			StartCoroutine(ConsultaPorRFID());
			break;
		default:
			break;
		}
	}





	//Deve ser chamado pelo botão listartodos
	public void PesquisarTodos(){
		LimpaContent ();
		StartCoroutine ("PesquisarTudo");
	}
	public IEnumerator PesquisarTudo(){
		Loading.SetActive (false);
		WWW txtConsulta = new WWW (controllerOPC.GetComponent<OPC_Controller>().endereco
			+ "/tcc/consultas/funcionario/porNome.php"
			+ "?nome=");
		yield return txtConsulta;

		//print ("Resultado da consulta: " + txtConsulta.text);
		//////////////////////////////////////////////////////////
		/////////////////////////////////////////arrumando o vetor
		/**//**//**//**/int qnts=0;
		/**//**//**//**/
		/**//**//**//**/String[] listaDeSubstrings = txtConsulta.text.Split('|');
		/**//**//**//**/Array.Resize(ref listaDeSubstrings, listaDeSubstrings.Length - 1); //Tirando duplicata gerada pelo splitter
		/**//**//**//**/////////////////////////////////contando numero de pessoas
		/**//**//**//**/int cont = 0;
		/**//**//**//**/foreach (var substring1 in listaDeSubstrings) {
		/**//**//**//**/	cont++;
		/**//**//**//**/}
		/**//**//**//**/if (cont == 0) {
		/**//**//**//**/	Instantiate (naoEncontreiNada, Content); //nenhum resultado
		/**//**//**//**/} else {
		/**//**//**//**/	qnts = cont / 5;
		/**//**//**//**/	print ("retornei " + qnts + " pessoas");
		/**//**//**//**/}
		//////////////////////////////////////Instanciando botões, CUSTOMIZAVEL
		int numeroDeCamposRetornados=4; //MUDE ISSO, são qnts campos o SELECT retorna
		numeroDeCamposRetornados++;
		for(int i = 0; i<qnts; i++){
			//print (listaDeSubstrings [i*numeroDeCamposRetornados+0]); //o ZERO é o campo que vc quer
			GameObject instancia = (GameObject)Instantiate (prefab, Content);
			instancia.SetActive (true);
			instancia.name="novobotao";
			instancia.GetComponentInChildren<Text>().text= listaDeSubstrings [i*numeroDeCamposRetornados+2];
			instancia.GetComponent<btnFuncio_Script> ().codigoFuncionario = listaDeSubstrings [i*numeroDeCamposRetornados+0];
			instancia.GetComponent<btnFuncio_Script> ().agoraPegaFoto = true;
		}
		Loading.SetActive (false);
	}
		
	//Além de ser chamado internamente,
	//tambem deve ser chamado pelo DropdownPor OnValueChange
	public void LimpaContent(){
		foreach (Transform filho in Content) {
			//print("nome do atual =" + filho.name);
			if (filho.name == "novobotao" || filho.name == "btnNaoEncontreiNada(Clone)") {
				//print("apaguei o de cima");
				Destroy (filho.gameObject);
			}
		}
	}

	//////////////////////////////////////////////////Consultas


	IEnumerator ConsultaPorNome(){
		Loading.SetActive (true);
		string nome = inputfieldNome.text;
		if (nome == "") {
			Instantiate (naoEncontreiNada, Content); //nenhum resultado
			Loading.SetActive (false);
			yield break; 
		}

		WWW txtConsulta = new WWW (controllerOPC.GetComponent<OPC_Controller>().endereco
			+ "/tcc/consultas/funcionario/porNome.php"
			+ "?nome=" + nome);
		yield return txtConsulta;

		//print ("Resultado da consulta: " + txtConsulta.text);
		//////////////////////////////////////////////////////////
		/////////////////////////////////////////arrumando o vetor
		/**//**//**//**/int qnts=0;
		/**//**//**//**/
		/**//**//**//**/String[] listaDeSubstrings = txtConsulta.text.Split('|');
		/**//**//**//**/Array.Resize(ref listaDeSubstrings, listaDeSubstrings.Length - 1); //Tirando duplicata gerada pelo splitter
		/**//**//**//**/////////////////////////////////contando numero de pessoas
		/**//**//**//**/int cont = 0;
		/**//**//**//**/foreach (var substring1 in listaDeSubstrings) {
		/**//**//**//**/	cont++;
		/**//**//**//**/}
		/**//**//**//**/if (cont == 0) {
		/**//**//**//**/	Instantiate (naoEncontreiNada, Content); //nenhum resultado
		/**//**//**//**/} else {
		/**//**//**//**/	qnts = cont / 5;
		/**//**//**//**/	print ("retornei " + qnts + " pessoas");
		/**//**//**//**/}
		//////////////////////////////////////Instanciando botões, CUSTOMIZAVEL
		int numeroDeCamposRetornados=4; //MUDE ISSO, são qnts campos o SELECT retorna
		numeroDeCamposRetornados++;
		for(int i = 0; i<qnts; i++){
			//print (listaDeSubstrings [i*numeroDeCamposRetornados+0]); //o ZERO é o campo que vc quer
			GameObject instancia = (GameObject)Instantiate (prefab, Content);
			instancia.SetActive (true);
			instancia.name="novobotao";
			instancia.GetComponentInChildren<Text>().text= listaDeSubstrings [i*numeroDeCamposRetornados+2];
			instancia.GetComponent<btnFuncio_Script> ().codigoFuncionario = listaDeSubstrings [i*numeroDeCamposRetornados+0];
			instancia.GetComponent<btnFuncio_Script> ().agoraPegaFoto = true;
		}
		Loading.SetActive (false);
	}


	IEnumerator ConsultaPorEmail(){
		Loading.SetActive (true);
		string email = inputfieldEmail.text;
		if (email == "") { 
			Instantiate (naoEncontreiNada, Content); //nenhum resultado
			Loading.SetActive (false);
			yield break; 
		}
		WWW txtConsulta = new WWW (controllerOPC.GetComponent<OPC_Controller>().endereco
			+ "/tcc/consultas/funcionario/porEmail.php"
			+ "?email=" + email);
		yield return txtConsulta;

		//print ("Resultado da consulta: " + txtConsulta.text);
		//////////////////////////////////////////////////////////
		/////////////////////////////////////////arrumando o vetor
		/**//**//**//**/int qnts=0;
		/**//**//**//**/
		/**//**//**//**/String[] listaDeSubstrings = txtConsulta.text.Split('|');
		/**//**//**//**/Array.Resize(ref listaDeSubstrings, listaDeSubstrings.Length - 1); //Tirando duplicata gerada pelo splitter
		/**//**//**//**/////////////////////////////////contando numero de pessoas
		/**//**//**//**/int cont = 0;
		/**//**//**//**/foreach (var substring1 in listaDeSubstrings) {
		/**//**//**//**/	cont++;
		/**//**//**//**/}
		/**//**//**//**/if (cont == 0) {
		/**//**//**//**/	Instantiate (naoEncontreiNada, Content); //nenhum resultado
		/**//**//**//**/} else {
		/**//**//**//**/	qnts = cont / 5;
		/**//**//**//**/	print ("retornei " + qnts + " pessoas");
		/**//**//**//**/}
		//////////////////////////////////////Instanciando botões, CUSTOMIZAVEL
		int numeroDeCamposRetornados=4; //MUDE ISSO, são qnts campos o SELECT retorna
		numeroDeCamposRetornados++;
		for(int i = 0; i<qnts; i++){
			//print (listaDeSubstrings [i*numeroDeCamposRetornados+0]); //o ZERO é o campo que vc quer
			GameObject instancia = (GameObject)Instantiate (prefab, Content);
			instancia.SetActive (true);
			instancia.name="novobotao";
			instancia.GetComponentInChildren<Text>().text= listaDeSubstrings [i*numeroDeCamposRetornados+2];
			instancia.GetComponent<btnFuncio_Script> ().codigoFuncionario = listaDeSubstrings [i*numeroDeCamposRetornados+0];
			instancia.GetComponent<btnFuncio_Script> ().agoraPegaFoto = true;
		}
		Loading.SetActive (false);
	}



	/*
	IEnumerator ConsultaPorFuncao(){
		//fazer!
	}
	*/



	IEnumerator ConsultaPorRFID(){
		Loading.SetActive (true);
		string rfid = inputfieldRFID.text;
		if (rfid == "") { 
			Instantiate (naoEncontreiNada, Content); //nenhum resultado
			Loading.SetActive (false);
			yield break; 
		}
		WWW txtConsulta = new WWW (controllerOPC.GetComponent<OPC_Controller>().endereco
			+ "/tcc/consultas/funcionario/porCodigo2.php"
			+ "?codigo=" + rfid);
		yield return txtConsulta;

		//print ("Resultado da consulta: " + txtConsulta.text);
		//////////////////////////////////////////////////////////
		/////////////////////////////////////////arrumando o vetor
		/**//**//**//**/int qnts=0;
	/**//**//**//**/
	/**//**//**//**/String[] listaDeSubstrings = txtConsulta.text.Split('|');
	/**//**//**//**/Array.Resize(ref listaDeSubstrings, listaDeSubstrings.Length - 1); //Tirando duplicata gerada pelo splitter
	/**//**//**//**/////////////////////////////////contando numero de pessoas
	/**//**//**//**/int cont = 0;
	/**//**//**//**/foreach (var substring1 in listaDeSubstrings) {
	/**//**//**//**/	cont++;
	/**//**//**//**/}
	/**//**//**//**/if (cont == 0) {
	/**//**//**//**/	Instantiate (naoEncontreiNada, Content); //nenhum resultado
	/**//**//**//**/} else {
	/**//**//**//**/	qnts = cont / 5;
	/**//**//**//**/	print ("retornei " + qnts + " pessoas");
	/**//**//**//**/}
	//////////////////////////////////////Instanciando botões, CUSTOMIZAVEL
	int numeroDeCamposRetornados=4; //MUDE ISSO, são qnts campos o SELECT retorna
	numeroDeCamposRetornados++;
	for(int i = 0; i<qnts; i++){
		//print (listaDeSubstrings [i*numeroDeCamposRetornados+0]); //o ZERO é o campo que vc quer
		GameObject instancia = (GameObject)Instantiate (prefab, Content);
		instancia.SetActive (true);
		instancia.name="novobotao";
		instancia.GetComponentInChildren<Text>().text= listaDeSubstrings [i*numeroDeCamposRetornados+2];
		instancia.GetComponent<btnFuncio_Script> ().codigoFuncionario = listaDeSubstrings [i*numeroDeCamposRetornados+0];
		instancia.GetComponent<btnFuncio_Script> ().agoraPegaFoto = true;
	}
	Loading.SetActive (false);
	}
}
