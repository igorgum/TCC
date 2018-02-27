/* Catraca digital usando lasers
  Codigo Inicial
  - Melhoria de implementação, agora tem funções.
  - Conta quantas pessoas tem na sala.
  - otimizações no codigo + cleanup.

*/

const int pResistor1 = A0; // Photoresistor analog pin A0
const int pResistor2 = A1; // Photoresistor analog pin A1

int value1;
int value2;
int pessoas;
String oi;
bool passo1, passo2, block1 = false;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode(pResistor1, INPUT);// Set pResistor1 - A0 pin as an input
  pinMode(pResistor2, INPUT);// Set pResistor2 - A1 pin as an input
  Serial.print("setup completo");
}

void loop() {
  verifica();
}




bool breack1() {
  value1 = analogRead(pResistor1);
  if (value1 < 800)
  {
    return true;
  } else {
    return false;
  }
}

bool breack2() {
  value2 = analogRead(pResistor2);
  if (value2 < 800) {
    return true;
  } else {
    return false;
  }
}

bool block() {
  if (breack1() == false && breack2() == false) {
    return false;
  } else {
    return true;
  }

}


void verifica() {



  if (breack1() == true && breack2() == true) {
      //Seriel/print("lazers bloqueados");
    
  }



  if (passo1 == true && passo2 == true) {
    passo1 = false;
    passo2 = false;
  }



  // começa a verificar o passo 1
  if (breack1() == true && breack2() == false && passo2 == false && block1 == false) {
    passo1 = true;
    passo2 = false;
    block1 = true;
  }




  // começa a verificar o passo 2
  if (breack2() == true && breack1() == false && passo1 == false && block1 == false) {
    passo2 = true;
    passo1 = false;
    block1 = true;
  }



  //termina de verificar o passo1
  if (breack2() == true && passo1 == true) {
    Serial.print("Entrou");
    pessoas++;
    // aqui faz a contagem, todos os tratamentos envolvendo a quantidade de pessoas na sala devem ser feitas aqui.
    passo1 = false;
    passo2 = false;
  }

  //termina de verificar o passo2
  if (breack1() == true && passo2 == true) {
    Serial.print("Saiu");
    pessoas--;
    // decrementa a quantidade de pessoas, supostamente deve evitar que tenha menos de 0 pessoas, mas por motivos de debug ainda não adicionei isso.
    passo1 = false;
    passo2 = false;
  }


 //verifica lazors
    if (breack1() == false && breack2() == false) {
    block1 = false;
  }else{
    block1 = true;
  }

  oi = Serial.readString();
  if(oi == "oi;"){
    Serial.print(pessoas,DEC);
    Serial.print(";");
    }
}

