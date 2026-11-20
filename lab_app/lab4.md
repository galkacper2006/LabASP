
# Lab 4 — Entity Framework Core i DI

## Część D — Serwis i Dependency Injection

Utworzono:
- IBookService
- BookService
- PagedResult<T>

BookService korzysta z AppDbContext.
Zarejestrowano go jako AddScoped.

### D1
AddSingleton jest nieprawidłowy w tym przypadku,
ponieważ BookService zależy od AppDbContext
o czasie życia Scoped.

### D2
Kontroler korzysta z IBookService, co oddziela
logikę kontrolera od bazy danych i pozwala
stosować mocki podczas testów jednostkowych.

## Część E — CRUD

Zaimplementowano:
- Index — wyszukiwanie i stronicowanie
- Details — szczegóły książki
- Create — dodawanie
- Edit — edycję
- Delete — usuwanie

Dodano walidację unikalności ISBN.

## Zadania dodatkowe

- Sortowanie po title, price, date
- Kierunek asc i desc
- Wyszukiwanie po tytule i ISBN
- Automatyczne migracje przy starcie aplikacji

## Testy

- Trwałość danych po restarcie: do sprawdzenia
- 12 książek, pageSize=5: do sprawdzenia
- /Book?page=99: do sprawdzenia
- LIMIT i OFFSET w logach SQL: do sprawdzenia
- AsNoTracking i ChangeTracker: do sprawdzenia
