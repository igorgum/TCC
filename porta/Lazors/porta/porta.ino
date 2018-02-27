/*
   --------------------------------------------------------------------------------------------------------------------
   Example sketch/program showing An Arduino Door Access Control featuring RFID, EEPROM, Relay
   --------------------------------------------------------------------------------------------------------------------
   This is a MFRC522 library example; for further details and other examples see: https://github.com/miguelbalboa/rfid

   This example showing a complete Door Access Control System

  Simple Work Flow (not limited to) :
                                     +---------+
  +----------------------------------->READ TAGS+^------------------------------------------+
  |                              +--------------------+                                     |
  |                              |                    |                                     |
  |                              |                    |                                     |
  |                         +----v-----+        +-----v----+                                |
  |                         |MASTER TAG|        |OTHER TAGS|                                |
  |                         +--+-------+        ++-------------+                            |
  |                            |                 |             |                            |
  |                            |                 |             |                            |
  |                      +-----v---+        +----v----+   +----v------+                     |
  |         +------------+READ TAGS+---+    |KNOWN TAG|   |UNKNOWN TAG|                     |
  |         |            +-+-------+   |    +-----------+ +------------------+              |
  |         |              |           |                |                    |              |
  |    +----v-----+   +----v----+   +--v--------+     +-v----------+  +------v----+         |
  |    |MASTER TAG|   |KNOWN TAG|   |UNKNOWN TAG|     |GRANT ACCESS|  |DENY ACCESS|         |
  |    +----------+   +---+-----+   +-----+-----+     +-----+------+  +-----+-----+         |
  |                       |               |                 |               |               |
  |       +----+     +----v------+     +--v---+             |               +--------------->
  +-------+EXIT|     |DELETE FROM|     |ADD TO|             |                               |
        +----+     |  EEPROM   |     |EEPROM|             |                               |
                   +-----------+     +------+             +-------------------------------+


   Use a Master Card which is act as Programmer then you can able to choose card holders who will granted access or not

 * **Easy User Interface**

   Just one RFID tag needed whether Delete or Add Tags. You can choose to use Leds for output or Serial LCD module to inform users.

 * **Stores Information on EEPROM**

   Information stored on non volatile Arduino's EEPROM memory to preserve Users' tag and Master Card. No Information lost
   if power lost. EEPROM has unlimited Read cycle but roughly 100,000 limited Write cycle.

 * **Security**
   To keep it simple we are going to use Tag's Unique IDs. It's simple and not hacker proof.

   @license Released into the public domain.

   Typical pin layout used:
   -----------------------------------------------------------------------------------------
               MFRC522      Arduino       Arduino   Arduino    Arduino          Arduino
               Reader/PCD   Uno/101       Mega      Nano v3    Leonardo/Micro   Pro Micro
   Signal      Pin          Pin           Pin       Pin        Pin              Pin
   -----------------------------------------------------------------------------------------
   RST/Reset   RST          9             5         D9         RESET/ICSP-5     RST
   SPI SS      SDA(SS)      10            53        D10        10               10
   SPI MOSI    MOSI         11 / ICSP-4   51        D11        ICSP-4           16
   SPI MISO    MISO         12 / ICSP-1   50        D12        ICSP-1           14
   SPI SCK     SCK          13 / ICSP-3   52        D13        ICSP-3           15
   LED0        ***          A0
   LED1        ***          A1
   
*/
#include "remap.h"      // WEMOS D1 Pin remap
#include <EEPROM.h>     // We are going to read and write PICC's UIDs from/to EEPROM
#include <SPI.h>        // RC522 Module uses SPI protocol
#include <MFRC522.h>  // Library for Mifare RC522 Devices

/*
  Instead of a Relay you may want to use a servo. Servos can lock and unlock door locks too
  Relay will be used by default
*/

// #include <Servo.h>

/*
  For visualizing whats going on hardware we need some leds and to control door lock a relay and a wipe button
  (or some other hardware) Used common anode led,digitalWriting HIGH turns OFF led Mind that if you are going
  to use common cathode led or just seperate leds, simply comment out #define COMMON_ANODE,
*/


// #define COMMON_ANODE


#ifdef COMMON_ANODE
#define LED_ON LOW
#define LED_OFF HIGH
#else
#define LED_ON HIGH
#define LED_OFF LOW
#endif

constexpr uint8_t redLed = D5;   // Set Led Pins
constexpr uint8_t greenLed = D4;
constexpr uint8_t blueLed = D3;

constexpr uint8_t relay = D0;     // Set Relay Pin


uint8_t successRead;    // Variable integer to keep if we have Successful Read from Reader


byte readCard[4];   // Stores scanned ID read from RFID Module


// Create MFRC522 instance.
constexpr uint8_t RST_PIN = D7;     // Configurable, see typical pin layout above
constexpr uint8_t SS_PIN = 15;     // Configurable, see typical pin layout above

// String
String Ativa;

// bool

boolean leu;

MFRC522 mfrc522(SS_PIN, RST_PIN);
//////////////////////////////////// Lazors stuff ////////////////////////////////////////////////


const int pResistor1 = A0; // Photoresistor analog pin A0
const int pResistor2 = A0; // Photoresistor analog pin A0    MULTIPLEXAR

int value1;
int value2;
int pessoas;
String oi;
bool passo1, passo2, block1 = false;


///////////////////////////////////////// Setup ///////////////////////////////////
void setup() {
  
  // more lazor stuff
  Serial.begin(9600);
  Serial.println("setup Iniciado");
  pinMode(pResistor1, INPUT);// Set pResistor1 - A0 pin as an input
  pinMode(pResistor2, INPUT);// Set pResistor2 - A1 pin as an input
  
  //Arduino Pin Configuration
  pinMode(redLed, OUTPUT);
  pinMode(greenLed, OUTPUT);
  pinMode(blueLed, OUTPUT);
  pinMode(relay, OUTPUT);
  //Be careful how relay circuit behave on while resetting or power-cycling your Arduino
  digitalWrite(relay, HIGH);    // Make sure door is locked
  digitalWrite(redLed, LED_OFF);  // Make sure led is off
  digitalWrite(greenLed, LED_OFF);  // Make sure led is off
  digitalWrite(blueLed, LED_OFF); // Make sure led is off

  //Protocol Configuration
  SPI.begin();           // MFRC522 Hardware uses SPI protocol
  mfrc522.PCD_Init();    // Initialize MFRC522 Hardware

  //If you set Antenna Gain to Max it will increase reading distance
  //mfrc522.PCD_SetAntennaGain(mfrc522.RxGain_max);

  cycleLeds();    // Everything ready lets give user some feedback by cycling leds
  Serial.println("setup completo");
}


///////////////////////////////////////// Main Loop ///////////////////////////////////
void loop () {

  // digitalRead(wipeB) == LOW; - BOTÃO WIPE
  // digitalWrite(blue/red/green/Led, LED_OFF/ON);
  // Serial.println(F("stringue"));
  //
  // getID()
  //
  //
  // verifica lazors
  //verifica();
  //
    if (leu == true) {
      leu = !leu;
    };
    Serial.flush();
    if(Serial.available() > 0){
    Ativa = Serial.readString();
    Serial.print(Ativa);
    if (Ativa == "Ativa;") {
      while (leu == false) {
        leu = getID();            // sets to true when we get read from reader otherwise false
        digitalWrite(blueLed, LED_ON);    
        delay(200);
        digitalWrite(blueLed, LED_OFF);
        delay(200);
        if (leu == true) {
          Ativa = " ";
        } else {
          Serial.print("cai no logging");
          //LOGGING
        }
      }
    }
    else
    {
      if (Ativa == "kk;") {
        Serial.print("eaemen;");
      }
      digitalWrite(blueLed, LED_OFF);   // Turn off blue LED
      digitalWrite(redLed, LED_ON);  // Turn on red LED
      digitalWrite(greenLed, LED_OFF);   // Turn off green LED
    }
    }
}






/////////////////////////////////////////  Access Granted    ///////////////////////////////////
void granted ( uint16_t setDelay) {
  digitalWrite(blueLed, LED_OFF);   // Turn off blue LED
  digitalWrite(redLed, LED_OFF);  // Turn off red LED
  digitalWrite(greenLed, LED_ON);   // Turn on green LED
  digitalWrite(relay, LOW);     // Unlock door!
  delay(setDelay);          // Hold door lock open for given seconds
  digitalWrite(relay, HIGH);    // Relock door
  delay(1000);            // Hold green LED on for a second
}

///////////////////////////////////////// Access Denied  ///////////////////////////////////
void denied() {
  digitalWrite(greenLed, LED_OFF);  // Make sure green LED is off
  digitalWrite(blueLed, LED_OFF);   // Make sure blue LED is off
  digitalWrite(redLed, LED_ON);   // Turn on red LED
  delay(1000);
}


///////////////////////////////////////// Get PICC's UID ///////////////////////////////////
uint8_t getID() {
  // Getting ready for Reading PICCs
  if ( ! mfrc522.PICC_IsNewCardPresent()) { //If a new PICC placed to RFID reader continue
    return false;
  }
  if ( ! mfrc522.PICC_ReadCardSerial()) {   //Since a PICC placed get Serial and continue
    return false;
  }
  // There are Mifare PICCs which have 4 byte or 7 byte UID care if you use 7 byte PICC
  // I think we should assume every PICC as they have 4 byte UID
  // Until we support 7 byte PICCs
  for ( uint8_t i = 0; i < 4; i++) {  //
    readCard[i] = mfrc522.uid.uidByte[i];
    Serial.print(readCard[i], HEX);
  }
  Serial.print(";");
  mfrc522.PICC_HaltA(); // Stop reading
  return true;
}


void ShowReaderDetails() {
  // Get the MFRC522 software version
  byte v = mfrc522.PCD_ReadRegister(mfrc522.VersionReg);
  Serial.print(F("MFRC522 Software Version: 0x"));
  Serial.print(v, HEX);
  if (v == 0x91)
    Serial.print(F(" = v1.0"));
  else if (v == 0x92)
    Serial.print(F(" = v2.0"));
  else
    Serial.print(F(" (unknown),probably a chinese clone?"));
  Serial.println("");
  // When 0x00 or 0xFF is returned, communication probably failed
  if ((v == 0x00) || (v == 0xFF)) {
    Serial.println(F("WARNING: Communication failure, is the MFRC522 properly connected?"));
    Serial.println(F("SYSTEM HALTED: Check connections."));
    // Visualize system is halted
    digitalWrite(greenLed, LED_OFF);  // Make sure green LED is off
    digitalWrite(blueLed, LED_OFF);   // Make sure blue LED is off
    digitalWrite(redLed, LED_ON);   // Turn on red LED
    while (true); // do not go further
  }
}

///////////////////////////////////////// Cycle Leds (Program Mode) ///////////////////////////////////
void cycleLeds() {
  digitalWrite(redLed, LED_OFF);  // Make sure red LED is off
  digitalWrite(greenLed, LED_ON);   // Make sure green LED is on
  digitalWrite(blueLed, LED_OFF);   // Make sure blue LED is off
  delay(200);
  digitalWrite(redLed, LED_OFF);  // Make sure red LED is off
  digitalWrite(greenLed, LED_OFF);  // Make sure green LED is off
  digitalWrite(blueLed, LED_ON);  // Make sure blue LED is on
  delay(200);
  digitalWrite(redLed, LED_ON);   // Make sure red LED is on
  digitalWrite(greenLed, LED_OFF);  // Make sure green LED is off
  digitalWrite(blueLed, LED_OFF);   // Make sure blue LED is off
  delay(200);
}

//////////////////////////////////////// Normal Mode Led  ///////////////////////////////////
void normalModeOn () {
  digitalWrite(blueLed, LED_ON);  // Blue LED ON and ready to read card
  digitalWrite(redLed, LED_OFF);  // Make sure Red LED is off
  digitalWrite(greenLed, LED_OFF);  // Make sure Green LED is off
  digitalWrite(relay, HIGH);    // Make sure Door is Locked
}





//////////////////////////////// Lazor stuff ///////////////////////////////////////////////
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
    if (pessoas > 0){    // decrementa a quantidade de pessoas, supostamente deve evitar que tenha menos de 0 pessoas.
    pessoas--;
    passo1 = false;
    passo2 = false;
    }else
    {
      Serial.print("Algo de errado n esta certo");     // caso tenha 0 ou menos pessoas na sala e alguem sai.
    }
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

