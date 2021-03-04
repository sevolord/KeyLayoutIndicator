// права на библиотеки принадлежат их правообладателям.
// просто меняем цвет светодиода в зависимости от пришедшего значения
// в простое - переливаемся цветами
#include "FastLED.h"

#define NUM_LEDS 1
#define DATA_PIN 6

CRGB leds[NUM_LEDS];
int val = 0;
bool connection = false; 

void setup()
{
  Serial.begin(9600);
  Serial.setTimeout(50);
  FastLED.addLeds<NEOPIXEL, DATA_PIN>(leds, NUM_LEDS);
}

void loop() {
  
  if (Serial.available() > 0)
  {
    connection = true;
    val = Serial.parseInt();
    //Serial.println(val);
    if (val == 10)
    {
      leds[0] = CRGB::Black;
      FastLED.show();
      connection = false;
    }
    if (val == 1)
    {
      leds[0] = CRGB::Green;
      FastLED.show();
    }
    if (val == 2)
    {
      leds[0] = CRGB::Red;
      FastLED.show();
    }

    if (val == 3)
    {
      leds[0] = CRGB::Blue;
      FastLED.show();
    }
  }
   
  if (!connection)
  {
    cylon();
  }

}
void cylon() //функция "переливания" цвета из стандартной библиотеки fastled
{
  static uint8_t hue = 0;
  for (int i; i < 256; i++)
    static uint8_t hue = 0;

  for (int i = 0; i < NUM_LEDS; i++)
  {
    leds[i] = CHSV(hue++, 255, 255);
    FastLED.show();
    fadeall();
    delay(10);
  }
  for (int i = (NUM_LEDS) - 1; i >= 0; i--) {
    leds[i] = CHSV(hue++, 255, 255);
    FastLED.show();
    fadeall();
    delay(10);
  }
}

void fadeall() {
  for (int i = 0; i < NUM_LEDS; i++) {
    leds[i].nscale8(250);
  }
}
