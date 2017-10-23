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



	void Start () {
		dropdownPorta.value = 0;
		inputfieldEndereco.text="127.0.0.1";

		AtualizaPorta();
		AtualizaEndereco ();
	}




	public void AtualizaEndereco(){
		endereco = inputfieldEndereco.text;
		StartCoroutine (SiteVivo());
	}
	public IEnumerator SiteVivo(){
		WWW w = new WWW (endereco+"/tcc/kkeaemen.php"+"?pergunta=kk");
		simCONEXAO.SetActive(false);
		naoCONEXAO.SetActive(false);
		yield return w;
		if (w.text == "eaemen") {
			simCONEXAO.SetActive(true);
			naoCONEXAO.SetActive(false);
		} else {
			simCONEXAO.SetActive(false);
			naoCONEXAO.SetActive(true);
		}
	}











	public void AtualizaPorta(){
		porta = txtDropdownPorta.GetComponent<Text>().text;

		SerialPort portaArduino = new SerialPort(porta, 9600);
		portaArduino.ReadTimeout = 10000; //no maximo 10 segundos pra ler
		portaArduino.WriteTimeout = 10000; //no maximo 10 segundos pra escrever
		portaArduino.NewLine = ";"; //define char de fim de linha como ";"
		string resposta = null;

		try{
			portaArduino.Open ();
		}
		catch{
			print ("cai no exception1");
		}


		if (portaArduino.IsOpen) {
			print ("entrei no if");
			//SE PORTA ESTIVER ABERTA
			try {
				print ("entrei no try");
				portaArduino.WriteLine ("kk"); //writeline manda o parâmetro + o char de fim de linha
				print ("escrevi");
				portaArduino.BaseStream.Flush (); //limpa caca
				print ("dei flush");

				resposta = portaArduino.ReadLine (); //le todo o buffer até o fim de linha
				print ("li");
				portaArduino.Close ();
				print ("fechei");
			} catch (System.Exception) {
				print ("cai no exception2");
			} 
			//VE PERGUNTA
			print ("vamos ver a pergunta");
			if (resposta == "eaemen") {
				print ("um");
				simCOM.SetActive (true);
				naoCOM.SetActive (false);
			} else {
				//SE RESPOSTA NAO FOR EAEMEN
				print ("dois");
				simCOM.SetActive (false);
				naoCOM.SetActive (true);
			}
		} else {
			//SE PORTA NAO ESTIVER ABERTA
			print ("tres");
			simCOM.SetActive(false);
			naoCOM.SetActive(true);
		}
		portaArduino.Close ();
	}
}
