﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class DadosFuncioCAD : MonoBehaviour {

	//GameObjects
	public InputField campoCodigo;
	public InputField campoLogin;
	public InputField campoNome;
	public InputField campoEmail;
	public Dropdown campoFuncao;
	public GameObject btnFileBrowser; //nao é o campo, é o botão q contem o script FileBrowser

	//Usado pra pegar o endereco
	public GameObject controllerOPC;

	public string caminho; //usado pelo FILEBROWSER, ou seja, é o caminho da NOVA imagem
	public bool imagemintacta = true;

	public GameObject panelEnviado; //email enviado
	public GameObject panel_msgCadastro;
	public Text txt_msgCadastro;
	public GameObject Loading;






	//Pega os Dropdowns e Inputfields, armazena nas variaveis e manda p/ o php
	//Em seguida chama o zerar
	public void Mandar(){
		Debug.Log ("fui chamado");
		//vamos ver primeiro se o codigo é duplicado no banco
		StartCoroutine("VerificaDuplicata");
	}







	IEnumerator VerificaDuplicata(){
		Loading.SetActive (true);

		WWW v = new WWW(controllerOPC.GetComponent<OPC_Controller>().endereco+"/tcc/consultas/funcionario/porCodigo.php?codigo="
						+ campoCodigo.text);
		yield return v;

		Loading.SetActive (false);

		//debug1
		Debug.Log("campoCodigo.text="+campoCodigo.text);
		Debug.Log("v.text="+v.text);
		//end debug

		if (v.text == "") {
			Loading.SetActive (true);

			WWW v1 = new WWW(controllerOPC.GetComponent<OPC_Controller>().endereco+"/tcc/consultas/funcionario/porLoginExato.php?login="
				+ campoLogin.text);
			yield return v1;

			Loading.SetActive (false);

			if (v1.text == "" || campoLogin.text=="") {
				//ver duplicata de email

				Loading.SetActive (true);
				WWW v2 = new WWW(controllerOPC.GetComponent<OPC_Controller>().endereco+"/tcc/consultas/funcionario/porEmailExato.php?email="
					+ campoEmail.text);
				yield return v2;

				Loading.SetActive (false);

				if (v2.text == "" || campoEmail.text=="") {
					////////como achou o email, faz o resto:
					//decidir se imagem foi alterada, e se for, chama o enviodepng
					if (!imagemintacta) {/*entao alterou, da upload nela*/
						StartCoroutine ("EnvioDePNG");
					}
					StartCoroutine ("insertfuncio");
				} else {
					print (" retornou ");
					txt_msgCadastro.text = "Erro no cadastro:\nEmail já cadastrado!";
					panel_msgCadastro.SetActive (true);
				}
			} else {
				print (" retornou ");
				txt_msgCadastro.text = "Erro no cadastro:\nLogin já cadastrado!";
				panel_msgCadastro.SetActive (true);
			}
		} else {
			print (" retornou ");
			txt_msgCadastro.text = "Erro no cadastro:\nRFID já cadastrado!";
			panel_msgCadastro.SetActive (true);
		}
	}







	IEnumerator insertfuncio(){
		Loading.SetActive (true);

		WWWForm formulario = new WWWForm();
		formulario.AddField ("VARCd_Funcionario", campoCodigo.text);
		Debug.Log (campoCodigo.text);
		formulario.AddField ("VARLogin", campoLogin.text);
		formulario.AddField ("VARNm_Funcionario", campoNome.text);
		formulario.AddField ("VAREmail", campoEmail.text);
		formulario.AddField ("VARCdFuncao", campoFuncao.value);

		WWW update = new WWW( controllerOPC.GetComponent<OPC_Controller> ().endereco
			+ "/tcc/insercoes/inserts/funcionario.php", formulario);
		yield return update;

		//ver se consegue mandar email
		if (campoEmail.text != "") {
			Debug.Log ("nao entrei no else");
			StartCoroutine ("MandarEmail");
		} else {
			Debug.Log ("entrei no else");
			txt_msgCadastro.text = "Cadastro realizado\ncom sucesso!";
			panel_msgCadastro.SetActive (true);
		}

		Loading.SetActive (false);
	}


	IEnumerator EnvioDePNG(){
		//string caminho = objAvatar.GetComponent<FileBrowserPNG> ().caminho;
		Loading.SetActive (true);
		//instacia o formulario
		WWWForm formulario = new WWWForm();
		//novo nome da imagem (dps faz o algoritmo de selecionar primeironome)
		string novoNome=campoCodigo.text+".png";
		//pega os bytes da imagem
		byte[] bytesDaImg = File.ReadAllBytes(caminho);
		//adiciona imagem ao formulario
		formulario.AddBinaryData("arquivo", bytesDaImg, novoNome,"image/png");
		//faz upload
		WWW w = new WWW(controllerOPC.GetComponent<OPC_Controller>().endereco +"/tcc/uploadAvatar.php", formulario);
		yield return w;
		Debug.Log("consegui mandar o avatar, mas falta o resto");
		Loading.SetActive (false);
	}

	IEnumerator MandarEmail(){
		//ativar carregando
		Loading.SetActive(true);

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

		if (listaDeSubstrings.Length > 2 && listaDeSubstrings [1] == "kkeaemen") {
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
		Loading.SetActive(false);

		/*****************************************************************
		if (!imagemintacta) {//entao alterou, da upload nela
			StartCoroutine ("EnvioDePNG");
		}
		StartCoroutine ("insertfuncio");
		*/
	}
}
