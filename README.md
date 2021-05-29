# Tworzenie Interfejsu Użytkownika

## System obsługi i zarządzania ZOO

### Prowadzący projekt

* dr hab. inż. Sławomir Golak, prof. PŚ

### Skład sekcji

* [MICHALSKI Łukasz](https://github.com/LukaszMichalsky)
* [STICH Grzegorz](https://github.com/grzesti868)
* [WYCIŚLIK Jakub](https://github.com/jakuwyc150)

### Specyfikacja projektu

#### Opis projektu

Projekt zakłada zbudowanie prostego systemu informatycznego do zarządzania ZOO. System ten jest utworzony w postaci aplikacji internetowej, w którego skład wchodzą zarówno część interfejsu graficznego użytkownika, jak i część silnikowa (server-side), która z kolei będzie publikowała stworzone funkcjonalności za pośrednictwem interfejsu API opartego na architekturze REST.

**Architektura danych - kolekcje**

* Animals - kolekcja przechowująca aktualnie posiadane zwierzęta
* Category - lista kategorii posiadanych zwierząt w ZOO
* Food - kolekcja z informacjami dotyczącymi pasz dla zwierząt w ZOO
* Zookeeper - lista zatrudnionych osób pracujących jako opiekun zwierząt

**Architektura interfejsu użytkownika - formatki**

* Formatki operacji na danych (CRUD)
  * Zarządzanie kategoriami zwierząt
  * Zarządzanie poszczególnymi zwierzętami
  * Zarządzanie listą dostępnych pasz
  * Zarządzanie pracownikami

#### Wykorzystane technologie

* Warstwa interfejsowa - Angular w wersji 11
* Warstwa serwerowa - ASP.NET Core WebAPI
* Baza danych - MongoDB