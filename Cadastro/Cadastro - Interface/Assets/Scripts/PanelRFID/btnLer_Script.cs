using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnLer_Script : MonoBehaviour {

	public GameObject objController;
	public GameObject objIndice;

	public void Consultar(){
		int indice = objIndice.GetComponent<Indice_Holder> ().getIndice ();
		objController.GetComponent<RFID_Controller> ().Consulta (indice);
	}
	public void Apagar(){
		int indice = objIndice.GetComponent<Indice_Holder> ().getIndice ();
		objController.GetComponent<RFID_Controller> ().Apagar (indice);
		objController.GetComponent<RFID_Controller> ().Fecha();
	}
}
