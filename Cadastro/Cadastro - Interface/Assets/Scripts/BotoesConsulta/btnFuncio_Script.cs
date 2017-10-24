using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnFuncio_Script : MonoBehaviour {

	public GameObject funcioController;
	public string codigoFuncionario;
	public GameObject objAvatar;
	public GameObject controllerOPC;
	public Texture texturaPadrao;
	public bool agoraPegaFoto = false;

	private bool tinhafoto;
	private bool isPossoPassar;

	public void PassaDadosProController() {
		if (isPossoPassar) {
			funcioController.GetComponent<DadosFuncio> ().ResgatarDados (codigoFuncionario);

			if (tinhafoto) {//se tinha foto, manda dar fetch
				funcioController.GetComponent<DadosFuncio> ().StartCoroutine("carregarImagem");
			}
		}
	}

	//Assim que o botão for instanciado, tenta pegar a foto do funcionario
	void FixedUpdate(){
		if(agoraPegaFoto){
			StartCoroutine ("PegaFoto");
			agoraPegaFoto = false;
		}
	}
	IEnumerator PegaFoto(){
		WWW wwwimg = new WWW (controllerOPC.GetComponent<OPC_Controller>().endereco
							  	+ "/tcc/uploads/" + codigoFuncionario + ".png");
		yield return wwwimg;

		//se conseguiu pegar a imagem
		if (wwwimg.error == null) {
			//objAvatar.GetComponent<RawImage> ().texture = wwwimg.texture;
			gameObject.GetComponentInChildren<RawImage>().texture = wwwimg.texture;
			tinhafoto = true;
		} else {
		//se não conseguiu
			//objAvatar.GetComponent<RawImage> ().texture = texturaPadrao;
			gameObject.GetComponentInChildren<RawImage>().texture = texturaPadrao;
			tinhafoto = false;
		}
		isPossoPassar = true;
	}
}
