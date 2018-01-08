/* Catraca digital usando lasers
  Codigo Inicial
  - Melhoria de implementação, agora tem funções.
  - 

*/

const int pResistor1 = A0; // Photoresistor analog pin A0
const int pResistor2 = A1; // Photoresistor analog pin A1

int value1;
int value2;
bool eh1, eh2, passo1, passo2 = false;

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
bool break1() {
  value1 = analogRead(pResistor1);
  if (value1 < 800)
  {
    return true;
  } else {
    return false;
  }
}

bool break2() {
  value2 = analogRead(pResistor2);
  if (value2 < 800) {
    return true;
  } else {
    return false;
  }
}

void verifica() {

  if (break1() == true) {
    // Serial.print("Lazer 1 quebrou");
    eh1 = true;
  }
  if (break2() == true)
  {
    // Serial.print("Lazer 2 quebrou");
    eh2 = true;
  }
  if (eh1 == true && eh2 == true) {
    eh1 = false;
    eh2 = false;
  }

  if (eh1 == true && eh2 == false) {
    passo1 = true;
  } if (passo1 == true && eh2 == true) {
    Serial.print("Entrou");
    eh1 = false;
    eh2 = false;
    passo1 = false;
  }

  if (eh2 == true && eh1 == false) {
    passo2 = true;
  } if (passo2 == true && eh1 == true) {
    Serial.print("Saiu");
    eh1 = false;
    eh2 = false;
    passo2 = false;
  }
}
