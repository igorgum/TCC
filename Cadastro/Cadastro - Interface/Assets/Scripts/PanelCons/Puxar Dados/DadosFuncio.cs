using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class DadosFuncio : MonoBehaviour {
	public GameObject Loading;
	//Favor usar um controller para armazenar esse script
	//Esse script deve ser chamado pelo button da lista de Consultas

	//Dados originais
	public string codigo;
	public string login;
	public string nome;
	public string email;
	public string caminho; //usado pelo FILEBROWSER, ou seja, é o caminho da NOVA imagem
	public int funcao;
	public bool imagemintacta = true;
	//GameObjects
	public InputField campoCodigo;
	public InputField campoLogin;
	public InputField campoNome;
	public InputField campoEmail;
	public Dropdown campoFuncao;
	public GameObject btnFileBrowser; //nao é o campo, é o botão q contem o script FileBrowser
	//imagem padrao de avatar E objAvatar
	public Texture avatarPadrao;
	public GameObject objAvatar;
	//Usado pra pegar o endereco
	public GameObject controllerOPC;
	public Dropdown funcaoHolder; //um dropdown q segura todas as funcoes do bd, somente necessario em funcionario












	//Pesquisa no banco
	//recebe o codigo via botao
	//o parâmetro código é o código de funcionario
	public void ResgatarDados(string codigo){
		this.codigo = codigo;
		StartCoroutine("Consulta");
	}
	IEnumerator Consulta(){
		Loading.SetActive (true);
		WWW txtConsulta = new WWW (controllerOPC.GetComponent<OPC_Controller>().endereco
									+ "/tcc/consultas/funcionario/porCodigo.php"
									+ "?codigo=" + codigo);
		yield return txtConsulta;

		String[] substrings = txtConsulta.text.Split('|');
		substrings[4] = substrings[4].Remove(substrings[4].Length - 1); //tira o ponto e virgula do final

		//Jogando a consulta nas variaveis agora:
		codigo = substrings[0];
		login = substrings[1];
		nome = substrings[2];
		email = substrings[3];

		/////////////////////////////////////////////////pegando o dropdown função no banco, pode apagar nos outros controller
		funcao = 0; //Reminder: tava retornando string e deveria ser int, cuida disso dps

		WWW txtConsulta2 = new WWW (controllerOPC.GetComponent<OPC_Controller>().endereco
			+ "/tcc/consultas/funcoes/retornarTodas.php");
		yield return txtConsulta2;

		String[] substrings2 = txtConsulta2.text.Split(';');
		Array.Resize(ref substrings2, substrings2.Length - 1); //Tirando duplicata gerada pelo splitter

		funcaoHolder.GetComponent<Dropdown> ().ClearOptions();
		List<string> novaLista2 = new List<string>();
		novaLista2.Add("Selecione uma funcao...");
		foreach (var substring in substrings2){
			novaLista2.Add(substring);
		}
		funcaoHolder.GetComponent<Dropdown> ().AddOptions(novaLista2);

		//Agora fazer uma segunda consulta pra saber qual o CD da primeira consulta
		WWW cdDaConsulta = new WWW (controllerOPC.GetComponent<OPC_Controller>().endereco
			+ "/tcc/consultas/funcoes/retornarCD.php?funcao="+substrings[4]);
		yield return cdDaConsulta;
		//Agora vamos comparar
		int codigoFuncaoRetornada = Int32.Parse(cdDaConsulta.text);
		if (substrings [4] == funcaoHolder.options[codigoFuncaoRetornada].text) {
			//Agora que sabemos q é compativel, funcao vira isso
			funcao = codigoFuncaoRetornada;
		}
		Loading.SetActive (false);
		DevolverDadosOriginais ();
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	}








	//Zera dados
	public void Zerar(){
		campoCodigo.text = null;
		campoLogin.text = null;
		campoNome.text = null;
		campoEmail.text = null;
		campoFuncao.value = 0;
		objAvatar.GetComponent<RawImage> ().texture = avatarPadrao;
		imagemintacta = true;
		caminho = null;

	}

	//Chamado pelo Cancelar Modificação
	//Faz com que os campos voltem ao estado padrão
	public void DevolverDadosOriginais(){
		campoCodigo.text = codigo;
		campoLogin.text = login;
		campoNome.text = nome;
		campoEmail.text = email;
		campoFuncao.value = funcao;
		StartCoroutine ("carregarImagem");
		imagemintacta = true;

	}
	IEnumerator carregarImagem(){
			WWW wwwimg = new WWW (controllerOPC.GetComponent<OPC_Controller> ().endereco
			             + "/tcc/uploads/" + codigo + ".png");
			yield return wwwimg;

			if (wwwimg.error == null) {
				objAvatar.GetComponent<RawImage> ().texture = wwwimg.texture;
			} else {
				objAvatar.GetComponent<RawImage> ().texture = avatarPadrao;
			}
	}

	//Pega os Dropdowns e Inputfields, armazena nas variaveis e manda p/ o php
	//Em seguida chama o zerar
	public void Atualizar(){
		Debug.Log ("fui chamado");
		//decidir se imagem foi alterada, e se for, chama o enviodepng
		if (!imagemintacta) {/*entao alterou, da upload nela*/
			StartCoroutine (EnvioDePNG());
		}
		StartCoroutine ("updatefuncio");
	}
	IEnumerator updatefuncio(){
		Loading.SetActive (true);

		WWWForm formulario = new WWWForm();
		formulario.AddField ("VARCd_Funcionario", campoCodigo.text);
		formulario.AddField ("VARLogin", campoLogin.text);
		formulario.AddField ("VARNm_Funcionario", campoNome.text);
		formulario.AddField ("VAREmail", campoEmail.text);
		formulario.AddField ("VARCdFuncao", campoFuncao.value);
		formulario.AddField ("X", codigo);

		Loading.SetActive (true);
		WWW update = new WWW( controllerOPC.GetComponent<OPC_Controller> ().endereco
			+ "/tcc/insercoes/updates/funcionario.php", formulario);
		yield return update;

		Loading.SetActive (false);
	}


	IEnumerator EnvioDePNG(){
		//string caminho = objAvatar.GetComponent<FileBrowserPNG> ().caminho;

		//instacia o formulario
		WWWForm formulario = new WWWForm();
		//novo nome da imagem (dps faz o algoritmo de selecionar primeironome)
		string novoNome=codigo+".png";
		//pega os bytes da imagem
		byte[] bytesDaImg = File.ReadAllBytes(caminho);
		//adiciona imagem ao formulario
		formulario.AddBinaryData("arquivo", bytesDaImg, novoNome,"image/png");
		//faz upload
		WWW w = new WWW("http://localhost/tcc/uploadAvatar.php", formulario);
		yield return w;
		Debug.Log("consegui trocar o avatar, mas falta o resto");
	}
}
