using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SFB;

public class FileBrowserPNG : MonoBehaviour {

	public string caminho;
	public bool debugar;
	public GameObject objController;
	public int queDados=0;

	public void AbrePainel(){
		var extensions = new [] {
			new ExtensionFilter("Imagem PNG", "png" )
		};
		string[] caminhos=(StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, false));
		if (caminhos.Length > 0 && caminhos.Length < 2) {
			caminho = caminhos [0];
		} else {
			caminho = "";
		}

		if (caminho != "") {
			switch (queDados) {
			case 0: 
				objController.GetComponent<DadosFuncio> ().imagemintacta = false;
				objController.GetComponent<DadosFuncio> ().caminho = caminho;
				break;
			case 1:
				objController.GetComponent<DadosFuncioCAD> ().imagemintacta = false;
				objController.GetComponent<DadosFuncioCAD> ().caminho = caminho;
				break;
			}

			StartCoroutine("PegaImagemDisk");

		}
	}

	IEnumerator PegaImagemDisk(){
		WWW wwwimg = new WWW("file://"+caminho);
		yield return wwwimg;
		gameObject.GetComponentInParent<RawImage> ().texture = wwwimg.texture;
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
