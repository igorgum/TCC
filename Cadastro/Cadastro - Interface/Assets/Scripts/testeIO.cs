using UnityEngine;
using System.Collections;
using System.IO.Ports;
using UnityEngine.UI;

public class testeIO : MonoBehaviour {
	//Definiçao da variavel velocidade
	public string id;
	public bool fechaPorta;
	public bool conversar;

	public Text texto; //texto do display2

	//Definiçao da Porta COM que sera utilizada
	SerialPort porta = new SerialPort("COM3", 9600);

	// Use this for initialization
	void Start () {
		//ligando segundo display
		//O monitor 0 é o primário, o 1 é secundário
		if (Display.displays.Length > 1) {
			if (!Display.displays [1].active) {
				Display.displays [1].Activate ();
			}
		}

	}

	// Update is called once per frame
	void Update () 
	{
		texto.text = id;

		if (!fechaPorta && conversar) 
		{
			StartCoroutine (conversa());
			conversar=false;
		}
		if (fechaPorta)
		{
			porta.Close ();
			fechaPorta = !fechaPorta;
		}
	}

	IEnumerator conversa() 
	{
		porta.Open ();
		porta.ReadTimeout = 10000; //no maximo 10 segundos pra ler
		porta.WriteTimeout = 10000; //no maximo 10 segundos pra escrever
		porta.NewLine = ";"; //define char de fim de linha como ";"

		if (porta.IsOpen) {
			try
			{
				porta.WriteLine ("Ativa"); //writeline manda o parâmetro + o char de fim de linha
				porta.BaseStream.Flush (); //limpa caca

				id = porta.ReadLine(); //le todo o buffer até o fim de linha
				porta.Close();
			} catch (System.Exception){
				print ("cai no exception");
			} 
			yield return new WaitForSeconds (20); //sai do conversa() dps de 10 segundos
		}
	}
}