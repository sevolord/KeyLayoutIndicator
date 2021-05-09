#include <Adafruit_NeoPixel.h>
#define PIN        6
#define NUMPIXELS 7
Adafruit_NeoPixel pixels(NUMPIXELS, PIN, NEO_GRB + NEO_KHZ800);
bool connection = false;
int val;
void setup()
{
  Serial.begin(9600);     // вклчюаем общение по UART
  Serial.setTimeout(50);  // задаем время опроса быстрее, чем одна секунда по умолчанию
  pixels.begin();
  pixels.setBrightness(250);
}

void loop() {

  if (Serial.available() > 0)  // если пришли данные в порт
  {
    connection = true;  // включаем флаг, что данные приходили.
    val = Serial.parseInt(); // кладем в переменную прищедшее значение
    //Serial.println(val);
    if (val == 10) // если пришедшее значение 10 - выключаем светодиод
    {
      pixels.clear(); // меняем свойство цвета

      connection = false; // флаг выключаем, показываеем специэффекты
    }
    if (val == 1) // если пришедшее значение 1 - включаем зеленый
    {
      myShowAllColor(0, 250, 0);
    }
    if (val == 2) // если пришедшее значение 2 - включаем красный
    {
      myShowAllColor(250, 0, 0);
    }

    if (val == 3) // если пришедшее значение 3 - включаем синий
    {
      myShowAllColor(0, 0, 250);
    }
  }

  if (!connection)  // если нет входящих данных, показываем спецэффекты
  {
    rainbow(10);
  }



}

void myShowAllColor(int R, int G, int B)
{
  for (int i = 0; i < NUMPIXELS; i++)
  {
    pixels.setPixelColor(i, pixels.Color(R, G, B));
    pixels.show();
  }
}

void rainbow(int wait) {
  // Hue of first pixel runs 5 complete loops through the color wheel.
  // Color wheel has a range of 65536 but it's OK if we roll over, so
  // just count from 0 to 5*65536. Adding 256 to firstPixelHue each time
  // means we'll make 5*65536/256 = 1280 passes through this outer loop:
  for (long firstPixelHue = 0; firstPixelHue < 5 * 65536; firstPixelHue += 256) {
    for (int i = 0; i < pixels.numPixels(); i++) { // For each pixel in strip...
      // Offset pixel hue by an amount to make one full revolution of the
      // color wheel (range of 65536) along the length of the strip
      // (strip.numPixels() steps):
      int pixelHue = firstPixelHue + (i * 65536L / pixels.numPixels());
      // strip.ColorHSV() can take 1 or 3 arguments: a hue (0 to 65535) or
      // optionally add saturation and value (brightness) (each 0 to 255).
      // Here we're using just the single-argument hue variant. The result
      // is passed through strip.gamma32() to provide 'truer' colors
      // before assigning to each pixel:
      pixels.setPixelColor(i, pixels.gamma32(pixels.ColorHSV(pixelHue)));
    }
    pixels.show(); // Update strip with new contents
    delay(wait);  // Pause for a moment
  }
}
