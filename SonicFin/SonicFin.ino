// HCSR04Ultrasonic/examples/UltrasonicDemo/UltrasonicDemo.pde
#include <Ultrasonic.h>

#define TRIGGER_PIN_A  11
#define ECHO_PIN_A     10

#define TRIGGER_PIN_B  9
#define ECHO_PIN_B     8

#define TRIGGER_PIN_C  7
#define ECHO_PIN_C     6

#define TRIGGER_PIN_D  5
#define ECHO_PIN_D     4

Ultrasonic sonics[4] = {Ultrasonic(TRIGGER_PIN_D, ECHO_PIN_D),Ultrasonic(TRIGGER_PIN_C, ECHO_PIN_C),Ultrasonic(TRIGGER_PIN_B, ECHO_PIN_B),Ultrasonic(TRIGGER_PIN_A, ECHO_PIN_A)};

void setup()
{
  Serial.begin(9600);
}

void loop()
{
  float minDistance = 3000;
  for(int i=0; i< 4 ; i++){
    float cmMsec, inMsec;
    long microsec = sonics[i].timing();
    
    cmMsec = sonics[i].convert(microsec, Ultrasonic::CM); // 計算距離，單位: 公分
    inMsec = sonics[i].convert(microsec, Ultrasonic::IN); // 計算距離，單位: 英吋

    if(cmMsec < minDistance){
      minDistance = cmMsec;
    }

    //Serial.print(cmMsec);
    //Serial.print(" ,");
  }
  //Serial.println();
  if(minDistance < 300){
      Serial.println(minDistance);
  }
  delay(500);
}
