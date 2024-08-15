using Microsoft.AspNetCore.Mvc;
using Test_Week_7.Models;
using Test_Week_7.Service;

namespace Test_Week_7.Controllers
{
    public class TodosController(ITodoService _todoService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var todos = await _todoService.GetAllTodosAsync();
            return View(todos.Todos ?? []);
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(TodoModel todo)
        {
            if (ModelState.IsValid)
            {
                await _todoService.CreateTodoAsync(todo);
                return RedirectToAction(nameof(Index));
            }
            return View("Index");
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var todo = await _todoService.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var todo = await _todoService.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TodoModel todo)
        {
            if (ModelState.IsValid)
            {
                await _todoService.UpdateTodoAsync(todo);
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var todo = await _todoService.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _todoService.DeleteTodoAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
