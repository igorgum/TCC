using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoginInputField_script : MonoBehaviour, IPointerDownHandler {

	public InputField login;
	public InputField campo;
	public GameObject avatarObjeto;

	void Start(){
		//campo.OnPointerClick.AddListener(() => PodeAtualizarAvatar());
		//gameObject.GetComponent<InputField>.OnPointerClick.AddListener(delegate{ PodeAtualizarAvatar();});

	}

	public void LoginNotNull () {
		if (login.text != "") {
			campo.interactable = true;
		} else {
			campo.interactable = false;
		}
	}

	//void PodeAtualizarAvatar(){
	public void OnPointerDown(PointerEventData data){
		if (avatarObjeto.activeInHierarchy) {
			Avatar_script avatarScript = avatarObjeto.GetComponent<Avatar_script> ();
			avatarScript.atualizar ();
		}
	}

}
