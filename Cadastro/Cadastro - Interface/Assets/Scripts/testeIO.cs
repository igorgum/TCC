using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class testeIO : MonoBehaviour {
	//Definiçao da variavel velocidade
	public string id;
	public bool fechaPorta;
	public bool conversar;
	//Definiçao da Porta COM que sera utilizada
	SerialPort porta = new SerialPort("COM4", 9600);

	// Use this for initialization
	void Start () {
		print ("startei");
	}

	// Update is called once per frame
	void Update () 
	{
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
		porta.ReadTimeout = 5000; //no maximo 5 segundos pra ler
		porta.WriteTimeout = 5000; //no maximo 5 segundos pra escrever
		porta.NewLine = ";"; //define char de fim de linha como ";"

		if (porta.IsOpen) {
			try
			{
				porta.WriteLine ("Ativa"); //writeline manda o parâmetro + o char de fim de linha
				porta.BaseStream.Flush (); //limpa caca

				id = porta.ReadLine(); //le todo o buffer até o fim de linha
			} catch (System.Exception){
			} 
			yield return new WaitForSeconds (10); //sai do conversa() dps de 10 segundos
		}
	}
}