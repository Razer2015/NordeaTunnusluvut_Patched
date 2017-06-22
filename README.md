# NordeaTunnusluvut_Patched

<h1> New Instructions at WIKI</h1>

[Tunnusluvut Patch Ohjeet](https://github.com/Razer2015/NordeaTunnusluvut_Patched/wiki)

<h2>Tarvittavat työkalut</h2>
Java 7 (JRE 1.7) tai Java 8 (JRE 1.8)<br>
apktool<br>
signapk<br>
Notepad tai Notepad++ (tai joku muu ohjelma millä voit muokata tekstitiedostoja)<br>
<h3>Huom!</h3> <p>Lisätty työkalu, jolla voi tehdä kohdat 2. ja 3. automaattisesti tiputtamalla kansion työkalun päälle.</p>

<h2>Ohjeet</h2>
<h3>1. Puretaan APK</h3>

```txt
apktool.bat d -r com.nordea.mobiletoken-1.apk -o nordea_130_nores
```

![alt tag](https://i.gyazo.com/22f41842b09ce08c5330d3cab6a3db03.png)

<h3>2. Paikannetaan muokattava SMALI -tiedosto</h3>
Polku:

```txt
nordea_130_nores\smali\o\ˊ.smali (19 kt)
(Suosittelen järjestelemään koon mukaan, jotta löydät tiedoston helposti)
```
![alt tag](https://i.gyazo.com/71e9abd81bf7f4f84dea978bcafe01e6.png)

<h3>3. Muokataan SMALI -tiedostoa</h3>
Notepad++ (Ctrl + H):

```txt
Etsittävä merkkijono: const/4 v0, 0x1
```
```txt
Korvaa merkkijonolla: const/4 v0, 0x0
```
![alt tag](https://i.gyazo.com/02579d57056af0a19515d7b1c3351a40.png)

<h3>4. Rakennetaan APK uudelleen</h3>

```txt
apktool.bat b nordea_130_nores -o Nordea_Tunnusluvut_1.3.0_patched.apk
```
![alt tag](https://i.gyazo.com/e700229b33f827a7d2a82d39531ca86d.png)

<h3>4. Allekirjoitetaan APK uudelleen, jotta sen voi asentaa</h3>

```txt
Avaa sign_improved.bat ja valitse oikea APK numerolla (2)
```
![alt tag](https://i.gyazo.com/6f5b76a9eb6ed15731a205c38795f890.png)

<h1 style="background-color:red">
HUOM!
</h1>
Koska APK -tiedoston allekirjoitus on nyt vaihtunut, et voi asentaa sitä suoraan alkuperäisen päälle ja et voi asentaa tulevia päivityksiä suoraan tämän APK:n päälle. Sinun täytyy poistaa vanha ensin. Muistaakseni tässä menee aktivointi, joten voit kokeilla varmuuskopioida sitä ottamalla kopion kansiosta "data/com.nordea.mobiletoken" ja palauttamalla sen uudelleenasennuksen jälkeen (en tosin tiedä toimiiko vaan kysyykö uudelleenaktivointia).

