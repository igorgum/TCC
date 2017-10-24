using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class FileBrowserPNG : MonoBehaviour {

	public string caminho;
	public bool debugar;
	public GameObject objController;

	public void AbrePainel(){
		caminho = EditorUtility.OpenFilePanel("Escolha o arquivo a enviar", "", "png");
		if (caminho != "") {
			objController.GetComponent<DadosFuncio> ().imagemintacta = false;
		}
	}

	/* DEBUG
	//Somente para debug, depois retire deste script e jogue no botão SALVAR
	void Update(){
		if (debugar) {
			debugar = false;
			StartCoroutine(DebugarEnvioDePNG ());
		}
	}
	IEnumerator DebugarEnvioDePNG(){
		//instacia o formulario
		WWWForm formulario = new WWWForm();
		//novo nome da imagem (dps faz o algoritmo de selecionar primeironome)
		string novoNome="goiabada.png";
		//pega os bytes da imagem
		byte[] bytesDaImg = File.ReadAllBytes(caminho);
		//adiciona imagem ao formulario
		formulario.AddBinaryData("arquivo", bytesDaImg, novoNome,"image/png");
		//faz upload
		WWW w = new WWW("http://localhost/tcc/uploadTeste.php", formulario);
		yield return w;
	}
	*/
}
