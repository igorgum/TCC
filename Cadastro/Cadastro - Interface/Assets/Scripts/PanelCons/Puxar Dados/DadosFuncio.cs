using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class DadosFuncio : MonoBehaviour {
	//Favor usar um controller para armazenar esse script
	//Esse script deve ser chamado pelo button da lista de Consultas

	//Dados originais
	public string codigo;
	public string login;
	public string nome;
	public string email;
	public string avatar;
	public int funcao;
	public bool imagemintacta = true;
	//GameObjects
	public InputField campoCodigo;
	public InputField campoLogin;
	public InputField campoNome;
	public InputField campoEmail;
	public Dropdown campoFuncao;
	public GameObject btnFileBrowser; //nao é o campo, é o botão q contem o script FileBrowser
	public GameObject objAvatar;
	//imagem padrao de avatar
	public Texture avatarPadrao;
	//Usado pra pegar o endereco
	public GameObject controllerOPC;

	//Pesquisa no banco
	//recebe o codigo via botao
	//o parâmetro código é o código de funcionario
	public void ResgatarDados(string codigo){
		this.codigo = codigo;
		StartCoroutine("Consulta");
	}
	IEnumerator Consulta(){
		WWW txtConsulta = new WWW (controllerOPC.GetComponent<OPC_Controller>().endereco
									+ "/tcc/consultas/funcionario/porCodigo.php"
									+ "?codigo=" + codigo);
		yield return txtConsulta;

		String[] substrings = txtConsulta.text.Split('|');
		substrings[5] = substrings[5].Remove(substrings[5].Length - 1); //tira o ponto e virgula do final

		//Jogando a consulta nas variaveis agora:
		codigo = substrings[0];
		login = substrings[1];
		nome = substrings[2];
		email = substrings[3];
		avatar = substrings[4];
		funcao = 0; //substrings[5];   ta retornando string e deveria ser int, cuida disso dps
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
		WWW wwwimg = new WWW (avatar);
		yield return wwwimg;

		if (wwwimg != null) {
			objAvatar.GetComponent<RawImage> ().texture = wwwimg.texture;
		} else {
			objAvatar.GetComponent<RawImage> ().texture = avatarPadrao;
		}
	}

	//Pega os Dropdowns e Inputfields, armazena nas variaveis e manda p/ o php
	//Em seguida chama o zerar
	public void Atualizar(){
		//decidir se imagem foi alterada, e se for, chama o enviodepng
	}

	IEnumerator EnvioDePNG(){
		string caminho = objAvatar.GetComponent<FileBrowserPNG> ().caminho;

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
	}
}
