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

	public Dropdown dropdownFuncao; /*somente usado por funcionario*/
	public UnityEngine.Object prefab;










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
			StartCoroutine(ConsultaPorEmail());
			break;
		case 3:
			//StartCoroutine(ConsultaPorFuncao());
			break;
		case 4:
			//StartCoroutine(ConsultaPorRFID());
			break;
		default:
			break;
		}
	}





	//Deve ser chamado pelo botão listartodos
	public void PesquisarTodos(){
		
	}
		
	//Além de ser chamado internamente,
	//tambem deve ser chamado pelo DropdownPor OnValueChange
	public void LimpaContent(){
		foreach (Transform filho in Content) {
			print("nome do atual =" + filho.name);
			if (filho.name == "novobotao") {
				print("apaguei o de cima");
				Destroy (filho.gameObject);
			}
		}
	}

	//////////////////////////////////////////////////Consultas
	IEnumerator ConsultaPorNome(){
		string nome = inputfieldNome.text;

		WWW txtConsulta = new WWW (controllerOPC.GetComponent<OPC_Controller>().endereco
			+ "/tcc/consultas/funcionario/porNome.php"
			+ "?nome=" + nome);
		yield return txtConsulta;

		Debug.Log ("retornei isso:"+txtConsulta.text); ///////////////////////DEBUG
		String[] substrings = txtConsulta.text.Split('|');
		/*código pra pegar todos os campos
		foreach (var substring in substrings){
			print(substring);
		}
		*/
		//substrings[0];
		String[] listaDeSubstrings = txtConsulta.text.Split(';');
		Array.Resize(ref listaDeSubstrings, listaDeSubstrings.Length - 1); //Tirando duplicata gerada pelo splitter
			

		foreach (var substring in listaDeSubstrings){
			GameObject instancia = (GameObject)Instantiate (prefab, Content);
			instancia.SetActive (true);
			instancia.name="novobotao";
			instancia.GetComponentInChildren<Text>().text= substrings[2];
			instancia.GetComponent<btnFuncio_Script> ().codigoFuncionario = substrings [0];
			instancia.GetComponent<btnFuncio_Script> ().agoraPegaFoto = true;
			print ("codigo ="+substrings[0]);
		}
	}

	IEnumerator ConsultaPorEmail(){
		string email = inputfieldEmail.text;
		print ("EU TENTEI PEGAAAAAA:" + email);
		WWW txtConsulta = new WWW (controllerOPC.GetComponent<OPC_Controller>().endereco
			+ "/tcc/consultas/funcionario/porEmail.php"
			+ "?email=" + email);
		yield return txtConsulta;

		Debug.Log ("retornei isso:"+txtConsulta.text); ///////////////////////DEBUG
		String[] substrings = txtConsulta.text.Split('|');

		String[] listaDeSubstrings = txtConsulta.text.Split(';');
		Array.Resize(ref listaDeSubstrings, listaDeSubstrings.Length - 1); //Tirando duplicata gerada pelo splitter


		foreach (var substring in listaDeSubstrings){
			GameObject instancia = (GameObject)Instantiate (prefab, Content);
			instancia.SetActive (true);
			instancia.name="novobotao";
			instancia.GetComponentInChildren<Text>().text= substrings[2];
			instancia.GetComponent<btnFuncio_Script> ().codigoFuncionario = substrings [0];
			instancia.GetComponent<btnFuncio_Script> ().agoraPegaFoto = true;
			print ("codigo ="+substrings[0]);
		}
	}
	/*
	IEnumerator ConsultaPorFuncao(){
		//fazer!
	}

	IEnumerator ConsultaPorRFID(){
		//fazer!
	}
	*/
}
