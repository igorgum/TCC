/* Catraca digital usando lasers 
Codigo Inicial

*/

const int pResistor1 = A0; // Photoresistor analog pin A0
const int pResistor2 = A1; // Photoresistor analog pin A1

int value1;          
int value2;          

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600); 
pinMode(pResistor1, INPUT);// Set pResistor1 - A0 pin as an input
pinMode(pResistor2, INPUT);// Set pResistor2 - A1 pin as an input
}

void loop() {
  // put your main code here, to run repeatedly:
  value1 = analogRead(pResistor1);
  value2 = analogRead(pResistor2);
   if(value2<800)
   {
   Serial.print("Resistor2 Break value2:");
   Serial.println(value2);
   }
   if(value1<800)
   {
   Serial.print("Resistor1 Break value1:");
   Serial.println(value1);
   }
  
}
