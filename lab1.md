## Część A1 – Struktura projektu

- Program.cs – konfiguracja usług i obsługi żądań HTTP.
- Controllers – kontrolery obsługujące żądania.
- Models – klasy reprezentujące dane aplikacji.
- Views – widoki Razor odpowiedzialne za wygląd stron.
- wwwroot – pliki statyczne, np. CSS i JavaScript.
- appsettings.json – konfiguracja aplikacji.
- launchSettings.json – ustawienia uruchamiania, porty i środowisko.

## Część B – Podgląd HTTP

Tabela została zapisana jako tabela-B.png
w folderze screenshots.

## Część C1 – Kody statusu

- 404 – Not Found.
- 418 – I'm a teapot.
- 500 – Internal Server Error.

StatusCode(404) zwraca sam kod odpowiedzi HTTP,
a nie gotową stronę błędu HTML. Do wyświetlenia
własnej strony błędu potrzebna jest dodatkowa
konfiguracja aplikacji.

## Część D – Testy HTTP

POST /Book/Import z danymi JSON zwrócił 200 OK.

POST /Book/Import z Content-Type: text/plain
zwrócił 415 Unsupported Media Type.

GET /Book/Import powinien zwrócić 405 Method Not Allowed,
ponieważ akcja obsługuje tylko metodę POST.

## Zadania samodzielne

### Sum

Dla a=2 i b=3 wynik wynosi 5.

Dla a=x konwersja parametru na int powoduje
błąd model binding. W naszej implementacji
sprawdzenie ModelState zwraca 400 Bad Request.

### UserAgent

Nagłówek User-Agent zawiera informacje o kliencie.
Przeglądarka Opera przesyła informacje o przeglądarce
i systemie Windows, a Postman własny User-Agent.

### Health

Akcja zwraca JSON ze statusem "ok"
oraz aktualną datą i godziną.

Content-Type: application/json; charset=utf-8.
# Lab 1 – Pytania kontrolne

### 1. Czym różnią się metody GET i POST z punktu widzenia idempotentności i miejsca przekazywania danych?

GET służy do pobierania danych, jest idempotentny, a parametry najczęściej przekazywane są w adresie URL.

POST służy do przesyłania danych, zwykle w ciele żądania. Nie musi być idempotentny.

### 2. Co oznaczają kody 200, 302, 400, 401, 403, 404, 405, 415, 500?

- 200 – OK, żądanie zakończone sukcesem.
- 302 – Found, tymczasowe przekierowanie.
- 400 – Bad Request, niepoprawne żądanie.
- 401 – Unauthorized, wymagane uwierzytelnienie.
- 403 – Forbidden, brak uprawnień.
- 404 – Not Found, nie znaleziono zasobu.
- 405 – Method Not Allowed, niedozwolona metoda HTTP.
- 415 – Unsupported Media Type, nieobsługiwany typ danych.
- 500 – Internal Server Error, wewnętrzny błąd serwera.

### 3. Skąd model binding wie, czy id pochodzi ze ścieżki, czy z query string?

ASP.NET Core odczytuje parametry z różnych źródeł, m.in. ścieżki URL i query string. Korzysta z nazw parametrów i konfiguracji routingu. Można też wskazać źródło za pomocą [FromRoute] lub [FromQuery].

### 4. Dlaczego pliki z wwwroot są dostępne pod adresem, a pliki z Controllers/ nie?

Folder wwwroot zawiera pliki statyczne udostępniane przez serwer, np. CSS, JavaScript i obrazy.

Folder Controllers zawiera kod aplikacji wykonywany na serwerze. Dostęp do akcji kontrolerów odbywa się przez routing, a nie przez bezpośrednie udostępnianie plików.