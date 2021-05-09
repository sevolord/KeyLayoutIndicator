#include <Adafruit_NeoPixel.h>
#define PIN       6
#define NUMPIXELS 7
Adafruit_NeoPixel pixels(NUMPIXELS, PIN, NEO_GRB + NEO_KHZ800);
bool connection = false;
int val;
void setup()
{
  Serial.begin(115200);     // вклчюаем общение по UART
  Serial.setTimeout(5);  // задаем время опроса быстрее, чем одна секунда по умолчанию
  pixels.begin();
  pixels.setBrightness(50);
}

void loop() {

  if (Serial.available())  // если пришли данные в порт
  {
    connection = true;  // включаем флаг, что данные приходили.
    char str[30];
    int amount = Serial.readBytesUntil(';', str, 30);
    str[amount] = NULL;


    // достаёт инты из строки и кладёт в int буфер
    int data[10];         // буфер интов
    int count = 0;        // счётчик интов
    char* offset = str;   // указатель для работы
    while (true) {
      data[count++] = atoi(offset); // пишем число в буфер
      offset = strchr(offset, ','); // поиск следующей запятой
      if (offset) offset++;         // если это не NULL - продолжаем
      else break;                   // иначе покидаем цикл
    }
    //выводим инт буфер
    for (int i = 0; i < count; i++) {
      Serial.print(i);
      Serial.print(": ");
      Serial.println(data[i]);
    }
    //вид принимаемой строки: 1,2,3,4,5,6,7;
    //где: 1 - тип параметра, 2 - первый светодиод обращения, 3 - последний светодиод обращения
    //     4 - red, 5 - green, 6 - blue, 7 - Brightness 
    switch (data[0])
    {
      case 1: // напр: 1,3,5,255,0,0,150; - красный цвет пикселов с 3 по 5, яркость 150 из 255
        for (int j = data[1]; j <= data[2]; j++)
          pixels.setPixelColor(j, pixels.Color(data[3], data[4], data[5]));
        pixels.setBrightness(data[6]);
    }
    pixels.show();
  }

  if (!connection)  // если нет входящих данных, показываем спецэффекты
  {
    rainbow(5);
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
    if (Serial.available()) for (int y = 0; y < pixels.numPixels(); y++) pixels.setPixelColor(y, pixels.Color(0, 0, 0)); // если что-то пришло, все гасим
    pixels.show(); // Update strip with new contents
    if (Serial.available()) break; // и прерываем
    delay(wait);  // Pause for a moment
  }
}
