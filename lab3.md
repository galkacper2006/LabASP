# Laboratorium 3 – Routing, middleware i filtry

## Zadanie B1 – Tabela tras aplikacji
| Adres                                | Kontroler.Akcja | Metoda HTTP |
| ------------------------------------ | --------------- | ----------- |
| /Admin/Dashboard/Index/{id?}         | Dashboard.Index | \*          |
| /Book/ByIsbn/{id?}                   | Book.ByIsbn     | \*          |
| /Book/ByYear/{id?}                   | Book.ByYear     | \*          |
| /Book/Create/{id?}                   | Book.Create     | GET         |
| /Book/Create/{id?}                   | Book.Create     | POST        |
| /Book/Details/{id:int}               | Book.Details    | GET         |
| /Book/Echo/{id?}                     | Book.Echo       | \*          |
| /Book/Health/{id?}                   | Book.Health     | \*          |
| /Book/Hello/{id?}                    | Book.Hello      | \*          |
| /Book/Import/{id?}                   | Book.Import     | POST        |
| /Book/Index/{id?}                    | Book.Index      | \*          |
| /Book/Inspect/{id?}                  | Book.Inspect    | \*          |
| /Book/Square/{id?}                   | Book.Square     | \*          |
| /Book/Status/{id?}                   | Book.Status     | \*          |
| /Book/Sum/{id?}                      | Book.Sum        | \*          |
| /Book/UserAgent/{id?}                | Book.UserAgent  | \*          |
| /books/isbn/{isbn:regex(^\d{{13}}$)} | Book.ByIsbn     | \*          |
| /books/year/{year:int:min(1450)}     | Book.ByYear     | \*          |
| /docs/{\*path}                       | Home.Docs       | \*          |
| /Home/Docs/{id?}                     | Home.Docs       | \*          |
| /Home/Error/{id?}                    | Home.Error      | \*          |
| /Home/Index/{id?}                    | Home.Index      | \*          |
| /Home/Privacy/{id?}                  | Home.Privacy    | \*          |
| /Home/Routes/{id?}  
### Middleware
- X-Elapsed-Ms: 3
- X-Request-Count: 10
- /blokada – 403 Forbidden 
### Filtry
- X-Action-Ms: 0
- /Book/Hello – działa
- /Book/Boom – obsługa błędu 500 działa
### Obsługa błędów
- /nie-ma-takiej-strony – własna strona 404
- /Book/Status?code=418 – własna strona 418