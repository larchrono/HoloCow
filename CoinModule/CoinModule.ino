int coinnum = 0 ;
const int ledPin = 13;                   // LED
const int buttonPin = 2;                 // 按鈕(pushbutton)

void setup() {
  Serial.begin (9600);             // Serial Port begin
  pinMode(ledPin, OUTPUT); 
  pinMode(buttonPin, INPUT);
  
  digitalWrite(ledPin, LOW);
  
  attachInterrupt(0, addcoin, RISING ); 
}

void loop() { 

}

void addcoin() {
 coinnum ++ ;
 Serial.println(coinnum);
 
 digitalWrite(ledPin, HIGH);
 delay(500);
 digitalWrite(ledPin, LOW);
}
