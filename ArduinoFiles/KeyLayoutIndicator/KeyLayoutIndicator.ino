// права на библиотеки принадлежат их правообладателям.
// просто меняем цвет светодиода в зависимости от пришедшего значения
#include "FastLED.h"

#define NUM_LEDS 1
#define DATA_PIN 6

CRGB leds[NUM_LEDS];
int val = 0;


void setup() 
{
  Serial.begin(9600);
  Serial.setTimeout(50);
  FastLED.addLeds<NEOPIXEL, DATA_PIN>(leds, NUM_LEDS);
}

void loop() {
  if (Serial.available() > 0)
  {
    val = Serial.parseInt();
    //Serial.println(val);
  }
    if (val == 10)
  {
    leds[0] = CRGB::Black;
    FastLED.show();
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
