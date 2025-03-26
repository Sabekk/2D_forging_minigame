# 2D_forging_minigame

Unity version 2022.3.22f1

Minigra 2D kuźni w której której gracz może tworzyć różne przedmioty oraz otrzymywać misje.

Zastosowano (obecnie):
 - ScritableObjects
 - Object pooling
 - Sirenix

Do zastosowania pozostało:
 - New input system

Features:
 - modifiable values
 - quest system
 - effects system
 - crafting
 - unlocks system
 - character system
 - generyczne bazy danych z edytorami (Tools/)
 - uniwersalny UI

Gra nie posiada wielu scen wiec uruchomienie jej spowoduje start rozgrywki

# Przedmioty
Przedmioty dzielą się na 2 kategorie:
 - możliwe do produkcji
 - niemożliwe do produkcji - do zdobycia

Kluczowe cenychy przedmiotów:
 - mogą posiadać efekty - Obecnie zostały dodane jedynie efekty gdy gracz posiada przedmiot w ekwipunku. Efekty realnie wpływają na statystyki postaci
 - posiadają wartość - przyszły feature sklepu
 - oznaczone sa jako możliwe do sprzedania oraz kupna

# Przepisy
Przedmioty można tworzyć w odpowiedniej maszynie według przepisów.
Określają one:
 - w jakim rodzaju maszyny można produkowac dany przedmiot
 - zapotrzebowanie do utworzenia przedmiotu
 - szansę na powodzenie
 - czas trwania tworzenia
 - id finalnego przedmiotu

# Zasoby
Gra na poczatku dostarcza losową ilośc zasobów. Umożliwiają one produkcję przedmiotów.
Zaimplementowany został system, który automatycznie dostarcza zasoby co okreslony czas, aby gracz nie zablokował swojego postepu.

Do zaimplementowania pozostał sklep który umożliwi sprzedaż oraz zakup przedmiotów

# Misje
Gra oferuje system misji w których gracz musi spełni okreslone warunki. Misje mogą być czasowe.
W nagrode nakładane są odpowiednio ustaswione efekty pozytywne (w przypadku powodzenia) i negatywne (misja nieudana)

# Efekty
Zaimplementowany system efektów oferuje nakładanie oraz zdejmowanie efektów które w określony sposób wpływają na swój cel lub globalnie.
Obecnie są 2 radzaje efektów:
 - globalny - wpływa na managery
 - targetowalny

Sposób nakładania efektu:
 - permament - efek nakłądany jest raz bez możliwości jego zdjęcia
 - constant - efekt bierny. Może być zdejmowany poprzez manager
 - time_limited - (niezaimplementowany) Efekt który po okreslony czasie samoistnie zostanie zdjęty

# HUD
HUD gry dzieli się na 3 osobne elementy
![image](https://github.com/user-attachments/assets/e683e386-abfe-4253-8552-76dde341ff7a)

1. Left bar
Posiada aktualnie przycisk otwierający ekwipunek postaci (do implementacji)

2. Top bar
Z lewej strony widoczna jest liczba aktywnych misji oraz przycisk do otworzenia okna z dokładniejszymi o nich informacjami (do implementacji)
Z prawej strony widoczne są aktywne efekty

3. Right bar
Widoczne są wszytkie maszyny kuźni które dodatkowo informują gracza o aktualnym procesie produkcyjnym. Dodatowo otwierają okno maszyny. 
Do implementacji rozpoczynanie produkcji

![image](https://github.com/user-attachments/assets/dd6a59ce-64af-47fb-9a05-3372ff144bd0)

# Wyjaśnienia
**Character**

Mimo, że gra jest 2D i opiera się na UI postanowiłem utworzyć postać (w managerze).
_Dlaczego?:_
 - Postac posiada własne modyfikowalne wartości, które moga być zmieniane przez efekty. 
 - Posiada również swój ekwipunek z przedmiotami.
 - Postać posiada również swoją startową wartość data która w łatwy i czytelny sposób określa bazowe wartości czy przedmioty itd
 - Takie podejście pozwala na dalszy rozwój gry poprzez na przykład dodawanie kolejnych postaci NPC czy innych klas różniących się w początkowych ustawieniach


**Resource**

Resource mimo podobieństwa do modifiable values są oddzielną wartością. Są proste i nie wymagają nakładania na nich modyfikatorów.
Takie podojście pozwala zachować resource jako proste wartości oraz uniknąć potencjalnych problemów z np efektami czy modyfikatorami.

Mimo wszystko w przyszłości mogą zostać ujednolicone aby miały wspólną bazę


**Resource w managerze**

Postać posiada przedmioty, lecz manager odpowiada za resource.
_Dlaczego?:_
- kuźnia stanowi jedność. Niezależnie od posiadanych przedmiotów czy cech, resource zawsze będą wspólne dla każdego
- jedno miejsce kontroli nad resourcami pozwala na uniknięcie błędów oraz jest czytelniejsze


**Obsługa gry poprzez managery**

Gra w dużej mierze opiera się na obsłudze przez managery. Jest to prosta gra która nie wymaga indywidualnego podejścia do każdego obiektu czy elementu osobno.
Podejście te pozwala na zachowanie porzadku oraz kontroli nad rozgrywką. Pozwala również na łatwe rowzijanie oraz naprawę potencjalnych problemów


**Generyczne bazy danych**

Postanowiłem zastosować generyczne bazy nadych ze względu na małą ilość przechowywanych przez nie danych.
Podejście te pozwala na bardzo łatwe i czytelne dodawanie kolenych baz danych.
W szczególnych przypadkach oraz chęci posiadania obszerniejszych baz nic nie stoi na przeszkodzie aby bazę utworzyć w standardowy sposób
