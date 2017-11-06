using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cad_OnAwake_Script : MonoBehaviour {

	public InputField[] listaInputfields;
	public Dropdown[] listaDropdowns;

	public bool zerarCaminhoPNG;
	public Texture avatarpadrao;
	public GameObject objAvatar;

	void OnEnable() {
		for (int i = 0; i < listaInputfields.Length; i++) {
			listaInputfields [i].text = "";
		}
		for (int i = 0; i < listaDropdowns.Length; i++) {
			listaDropdowns [i].value = 0;
		}

		if (zerarCaminhoPNG) {
			objAvatar.GetComponentInChildren<FileBrowserPNG> ().caminho = "";
			objAvatar.GetComponent<RawImage> ().texture = avatarpadrao;
		}
	}




	public void Apagar(){
		for (int i = 0; i < listaInputfields.Length; i++) {
			listaInputfields [i].text = "";
		}
		for (int i = 0; i < listaDropdowns.Length; i++) {
			listaDropdowns [i].value = 0;
		}
		objAvatar.GetComponentInChildren<FileBrowserPNG> ().caminho = "";
		objAvatar.GetComponent<RawImage> ().texture = avatarpadrao;

	}
}
