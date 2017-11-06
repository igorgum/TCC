using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;

public class OPC_Controller : MonoBehaviour {

	public Dropdown dropdownPorta;
	public InputField inputfieldEndereco;
	public GameObject simCOM;
	public GameObject naoCOM;
	public GameObject simCONEXAO;
	public GameObject naoCONEXAO;
	public GameObject txtDropdownPorta;


	public string porta;
	public string endereco;
	public bool conectadoPodeLogar = false;


	public GameObject Loading;
	public GameObject panelLoginIsActive;

	void Start () {
		dropdownPorta.value = 0;
		inputfieldEndereco.text= Network.player.ipAddress + ":80";

		AtualizaPorta();
		AtualizaEndereco ();
	}

	void FixedUpdate(){
		if (panelLoginIsActive.activeSelf) {
			inputfieldEndereco.interactable = true;
		} else {
			inputfieldEndereco.interactable = false;
		}
	}





	public void AtualizaEndereco(){
		endereco = inputfieldEndereco.text;
		StartCoroutine (SiteVivo());
	}
	public IEnumerator SiteVivo(){
		Loading.SetActive (true);

		WWW w = new WWW (endereco+"/tcc/kkeaemen.php"+"?pergunta=kk");
		simCONEXAO.SetActive(false);
		naoCONEXAO.SetActive(false);
		yield return w;
		if (w.text == "eaemen") {
			simCONEXAO.SetActive(true);
			naoCONEXAO.SetActive(false);
			conectadoPodeLogar = true;
		} else {
			simCONEXAO.SetActive(false);
			naoCONEXAO.SetActive(true);
			conectadoPodeLogar = false;
		}

		Loading.SetActive (false);
	}











	public void AtualizaPorta(){
		porta = txtDropdownPorta.GetComponent<Text> ().text;
		StartCoroutine (Pergunta ());
	}
	IEnumerator Pergunta(){
		SerialPort portaArduino = new SerialPort (porta, 9600);
		portaArduino.ReadTimeout = 10000; //no maximo 10 segundos pra ler
		portaArduino.WriteTimeout = 10000; //no maximo 10 segundos pra escrever
		portaArduino.NewLine = ";"; //define char de fim de linha como ";"
		string resposta = null;
		try{
			portaArduino.Open ();
			if (portaArduino.IsOpen) {
				//SE PORTA ESTIVER ABERTA
				try {
					portaArduino.WriteLine ("kk"); //writeline manda o parâmetro + o char de fim de linha
					portaArduino.BaseStream.Flush (); //limpa caca

					resposta = portaArduino.ReadLine (); //le todo o buffer até o fim de linha
					portaArduino.Close ();
				} catch (System.Exception) {
				} 
				//VE PERGUNTA
				if (resposta == "eaemen") {
					simCOM.SetActive (true);
					naoCOM.SetActive (false);
				} else {
					//SE RESPOSTA NAO FOR EAEMEN
					simCOM.SetActive (false);
					naoCOM.SetActive (true);
				}
			} else {
				//SE PORTA NAO ESTIVER ABERTA
				simCOM.SetActive(false);
				naoCOM.SetActive(true);
			}
			portaArduino.Close ();
		}
		catch{
			simCOM.SetActive(false);
			naoCOM.SetActive(true);
		}
		yield return new WaitForSeconds (20);
	}



}
