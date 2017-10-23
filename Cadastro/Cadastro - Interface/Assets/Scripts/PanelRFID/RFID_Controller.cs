using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;

public class RFID_Controller : MonoBehaviour {

	//InputFields que serão utilizados
	public InputField[] listaInputFields;
	//Paineis
	public GameObject panelRFID;
	public GameObject escolherOpcao;
	public GameObject sucesso;
	public GameObject erro;

	//porta para ser usado em Conversa()
	private SerialPort porta;
	//msg quem vem do arduino
	public string msg = null;
	//porta que será usada
	public GameObject com;
	/*public string com = "COM4";*/


	//Inicializa os painéis da cena
	void Start(){
		Fecha ();
	}

	//Funcao chamada pelos botões OK
	public void Fecha(){
		panelRFID.SetActive (false);
		escolherOpcao.SetActive (false);
		sucesso.SetActive (false);
		erro.SetActive (false);
	}




	//Recebe como parâmetro o indice da lista de inputfields para jogar o resultado lido
	public void Consulta(int indice){
		panelRFID.SetActive (true);
		Comunicacao ();
		if (msg != null) {
			//jogar no indice a msg
			listaInputFields[indice].text=msg;
			//tornar msg null
			msg=null;
			//mostrar oque aconteceu
			escolherOpcao.SetActive (false);
			sucesso.SetActive (true);
		} else {
			escolherOpcao.SetActive (false);
			erro.SetActive (true);
		}
	}



	//funcao chamada pelo botão APAGAR
	public void Apagar(int indice){
		if (listaInputFields.Length > indice) {
			listaInputFields [indice].text = null;
		}
		Fecha ();
	}





	//Abre o menu para formulários
	public void Menu(int indiceRecebido){
		panelRFID.SetActive (true);
		escolherOpcao.SetActive (true);
		escolherOpcao.GetComponent<Indice_Holder> ().setIndice (indiceRecebido);
	}







	//Faz a comunicação com o Arduino
	//Retorna uma string com o código
	//Se não conseguir, returna uma string NULL
	private string Comunicacao(){
		//Definiçao da Porta COM que sera utilizada
		porta = new SerialPort(com.GetComponent<OPC_Controller>().porta, 9600);
		msg = null;
		StartCoroutine (conversa());
		print ("saí");
		return msg;

	}

	IEnumerator conversa(){
		porta.Open ();
		porta.ReadTimeout = 10000; //no maximo 10 segundos pra ler
		porta.WriteTimeout = 10000; //no maximo 10 segundos pra escrever
		porta.NewLine = ";"; //define char de fim de linha como ";"

		if (porta.IsOpen) {
			try
			{
				porta.WriteLine ("Ativa"); //writeline manda o parâmetro + o char de fim de linha
				porta.BaseStream.Flush (); //limpa caca

				msg = porta.ReadLine(); //le todo o buffer até o fim de linha
				porta.Close();
			} catch (System.Exception){
				porta.Close();
				print ("cai no exception");
			} 
			yield return null; //sai do conversa() dps de x segundos
		}
	}
}
