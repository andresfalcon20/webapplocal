using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PeopleController : Controller
    {
        private static List<Person> people = new List<Person>
        {
            new Person { IdPeople = 1, NamePeople = "Juan", Age = 30 },
            new Person { IdPeople = 2, NamePeople = "Ana", Age = 25 },
            new Person { IdPeople = 3, NamePeople = "Carlos", Age = 40 }
        };

        // GET: People
        public async Task<IActionResult> Index()
        {
            return View(people);
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var person = people.FirstOrDefault(p => p.IdPeople == id);
            if (person == null)
                return NotFound();

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPeople,NamePeople,Age")] Person person)
        {
            if (ModelState.IsValid)
            {
                // Agregar persona a la lista en memoria
                person.IdPeople = people.Max(p => p.IdPeople) + 1;
                people.Add(person);
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var person = people.FirstOrDefault(p => p.IdPeople == id);
            if (person == null)
                return NotFound();

            return View(person);
        }

        // POST: People/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPeople,NamePeople,Age")] Person person)
        {
            if (id != person.IdPeople)
                return NotFound();

            if (ModelState.IsValid)
            {
                var existingPerson = people.FirstOrDefault(p => p.IdPeople == id);
                if (existingPerson == null)
                    return NotFound();

                existingPerson.NamePeople = person.NamePeople;
                existingPerson.Age = person.Age;

                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var person = people.FirstOrDefault(p => p.IdPeople == id);
            if (person == null)
                return NotFound();

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = people.FirstOrDefault(p => p.IdPeople == id);
            if (person != null)
            {
                people.Remove(person);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return people.Any(e => e.IdPeople == id);
        }
    }
}
