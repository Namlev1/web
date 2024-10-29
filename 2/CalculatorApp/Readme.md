# PAMIW Lab 2
## 1. Stworzenie kalkulatora w technologii web MVC
Utworzyliśmy nowy projekt *ASP DotNet* o nazwie *CalculatorApp*.
`CalculatorModel.cs`
```cs
namespace CalculatorApp.Models
{
    public class CalculatorModel
    {
        public double Number1 { get; set; }
        public double Number2 { get; set; }
        public double Result { get; set; }
    }
}
```
`CalculatorController.cs`
```cs
using Microsoft.AspNetCore.Mvc;
using CalculatorApp.Models;

namespace CalculatorApp.Controllers
{
    public class CalculatorController : Controller
    {
        // GET: /Calculator/
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Calculator/
        [HttpPost]
        public IActionResult Index(CalculatorModel model)
        {
            if (ModelState.IsValid)
            {
                // Dodaj liczby i ustaw wynik
                model.Result = model.Number1 + model.Number2;
            }

            // Przekaż model z wynikiem do widoku
            return View(model);
        }
    }
}
```
`Views/Calculator/Index.cshtml`
```html
@model CalculatorApp.Models.CalculatorModel

<h2>Simple Calculator</h2>

<form asp-action="Index" method="post">
    <div>
        <label for="Number1">First Number:</label>
        <input type="number" name="Number1" value="@Model.Number1" step="any" />
    </div>
    <div>
        <label for="Number2">Second Number:</label>
        <input type="number" name="Number2" value="@Model.Number2" step="any" />
    </div>
    <div>
        <button type="submit">Calculate</button>
    </div>
</form>

@if (Model != null)
{
    <div>
        <h3>Result: @Model.Result</h3>
    </div>
}
```
`Program.cs`
```cs
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Calculator}/{action=Index}/{id?}");
```
## 2. Stworzenie aplikacji typu "TODO list" z wykorzystaniem sesji
Poniższy kod dodaje model **Todo**, kontroler, oraz widok. Możliwe jest dodanie nowego **Todo**, ustawianie zadania jako *Completed*, oraz usuwanie zadania.
### Model
`TodoModel.cs`
```cs
namespace CalculatorApp.Models
{
    public class TodoModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}

```
### Kontroler
`TodoController.cs`
```cs
using Microsoft.AspNetCore.Mvc;
using CalculatorApp.Models;
using System.Collections.Generic;

namespace CalculatorApp.Controllers
{
    public class TodoController : Controller
    {
        private static List<TodoModel> todos = new List<TodoModel>();
        private static int nextId = 1;

        public IActionResult Index()
        {
            return View(todos);
        }

        [HttpPost]
        public IActionResult Index(TodoModel model)
        {
            if (ModelState.IsValid)
            {
                model.Id = nextId++;
                todos.Add(model);
            }

            return View(todos);
        }

        // POST: /Todo/Complete
        [HttpPost]
        public IActionResult Complete(int id)
        {
            var todo = todos.Find(t => t.Id == id);
            if (todo != null)
            {
                todo.IsCompleted = true;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var todo = todos.Find(t => t.Id == id);
            if (todo != null)
            {
                todos.Remove(todo);
            }
            return RedirectToAction("Index");
        }
    }
}
```
- **Metoda GET `Index()`**: Wyświetla listę zadań.
- **Metoda POST `Index(TodoModel model)`**: Dodaje nowe zadanie do listy, jeśli model jest poprawny.
- **Metoda POST `Complete(int id)`**: Ustawia zadanie o danym `id` jako ukończone.
- **Metoda POST `Delete(int id)`**: Usuwa zadanie o danym `id` z listy.
### Widok
`Views/Todo/Index.cshtml`
```html
@model IEnumerable<CalculatorApp.Models.TodoModel>

<h2>Todo List</h2>

<form asp-action="Index" method="post">
    <div>
        <label for="Title">Todo Item:</label>
        <input type="text" name="Title" />
    </div>
    <div>
        <button type="submit">Add Todo</button>
    </div>
</form>

@if (Model != null && Model.Any())
{
    <h3>Your Todos:</h3>
    <ul>
        @foreach (var todo in Model)
        {
            <li>
                @todo.Title 
                @if (!todo.IsCompleted)
                {
                    <form asp-action="Complete" method="post" style="display:inline;">
                        <input type="hidden" name="id" value="@todo.Id" />
                        <button type="submit">Complete</button>
                    </form>
                }
                else
                {
                    <span>(Completed)</span>
                }
                <form asp-action="Delete" method="post" style="display:inline;">
                    <input type="hidden" name="id" value="@todo.Id" />
                    <button type="submit">Delete</button>
                </form>
            </li>
        }
    </ul>
}
else
{
    <p>No todos available. Add a new one!</p>
}
```
- **Formularz dodawania zadania**: Składa się z pola tekstowego `Title` do wpisania treści zadania i przycisku "Add Todo" do jego dodania.
- **Lista zadań**: Wyświetla wszystkie zadania z modelu.
    - Każde zadanie może być oznaczone jako "Complete" (jeśli jest nieukończone) lub "Delete" w celu usunięcia.
- **Komunikat**: Jeśli lista zadań jest pusta, wyświetla komunikat "No todos available. Add a new one!"
## 3. Stworzenie aplikacji używającej Local Storage
Poniższy kod dodaje kontroler **Quote** oraz widok, w którym przechowywane są cytaty z wykorzystaniem *LocalStorageAPI*.
### Kontroler
`QuoteController.cs`
```cs
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorApp.Controllers
{
    public class QuoteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
```
**Metoda `Index()`**: Wyświetla widok główny (Index) dla sekcji cytatów.
### Widok
`Views/Quote/Index.cshtml`
```html
@{
    ViewData["Title"] = "My Quotes";
}

<h2>@ViewData["Title"]</h2>

<div>
    <input type="text" id="new-quote" placeholder="Add your favorite quote" />
    <button id="add-quote-btn">Add Quote</button>
</div>

<ul id="quotes-list">
    <!-- Lista cytatów zostanie wygenerowana przez JavaScript -->
</ul>

<script>
    document.addEventListener('DOMContentLoaded', function () {
        // Funkcja do pobierania cytatów z Local Storage
        function getQuotes() {
            const quotes = localStorage.getItem('quotes');
            return quotes ? JSON.parse(quotes) : [];
        }

        // Funkcja do zapisywania cytatów w Local Storage
        function saveQuotes(quotes) {
            localStorage.setItem('quotes', JSON.stringify(quotes));
        }

        // Funkcja do renderowania listy cytatów
        function renderQuotes() {
            const quotesList = document.getElementById('quotes-list');
            quotesList.innerHTML = ''; // Wyczyść aktualną listę

            const quotes = getQuotes();
            quotes.forEach((quote, index) => {
                const li = document.createElement('li');
                const text = document.createElement('span');
                text.textContent = quote;

                // Dodaj przyciski do edytowania i usuwania
                const editBtn = document.createElement('button');
                editBtn.textContent = 'Edit';
                editBtn.onclick = function () {
                    const newQuote = prompt("Edit quote:", quote);
                    if (newQuote) {
                        quotes[index] = newQuote;
                        saveQuotes(quotes);
                        renderQuotes();
                    }
                };

                const deleteBtn = document.createElement('button');
                deleteBtn.textContent = 'Delete';
                deleteBtn.onclick = function () {
                    quotes.splice(index, 1); // Usuń cytat
                    saveQuotes(quotes);
                    renderQuotes();
                };

                li.appendChild(text);
                li.appendChild(editBtn);
                li.appendChild(deleteBtn);
                quotesList.appendChild(li);
            });
        }

        // Obsługa dodawania nowego cytatu
        document.getElementById('add-quote-btn').onclick = function () {
            const newQuote = document.getElementById('new-quote').value;
            if (newQuote) {
                const quotes = getQuotes();
                quotes.push(newQuote);
                saveQuotes(quotes);
                renderQuotes();
                document.getElementById('new-quote').value = ''; // Wyczyść pole tekstowe
            }
        };

        // Renderuj cytaty przy pierwszym załadowaniu strony
        renderQuotes();
    });
</script>


```
- **Nagłówek**: Wyświetla tytuł "My Quotes".
- **Formularz dodawania cytatu**: Składa się z pola tekstowego do wprowadzenia cytatu oraz przycisku "Add Quote" do jego dodania.
- **Lista cytatów**: Generowana dynamicznie za pomocą JavaScript; zawiera przyciski do edytowania i usuwania cytatów.
- **Skrypt JavaScript**:
    - **Pobieranie i zapisywanie cytatów**: Używa `localStorage` do przechowywania cytatów w przeglądarce.
    - **Renderowanie cytatów**: Funkcja wyświetlająca aktualną listę cytatów.
    - **Obsługa przycisków**: Funkcje do dodawania, edytowania i usuwania cytatów.
## Nawigacja
W celu łatwego destępu do utworzonych stron, dodaliśmy następującą nawigację w domyślnym widoku:
```html
<li class="nav-item">
	<a class="nav-link text-dark" asp-area="" asp-controller="Todo" asp-action="">Todo</a>
</li>
<li class="nav-item">
	<a class="nav-link text-dark" asp-area="" asp-controller="Quote" asp-action="Index">Quotes</a>
</li>
```
