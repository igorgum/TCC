using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimpaListas_Script : MonoBehaviour {

	public GameObject[] btnPesquisar;

	public void LimparListas(){
		if(btnPesquisar[0].activeInHierarchy){
			btnPesquisar [0].GetComponent<btnPesquisar_Funcio> ().LimpaContent ();
		}
		//programar outros de acordo com a lista
	}
}
