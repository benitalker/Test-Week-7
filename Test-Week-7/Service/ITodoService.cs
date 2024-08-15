using System.Text;
using Test_Week_7.Models;

namespace Test_Week_7.Service
{
    public interface ITodoService
    {

        Task<TodosList?> GetAllTodosAsync();
        Task<TodoModel?> GetTodoByIdAsync(int id);
        Task<TodoModel?> CreateTodoAsync(TodoModel newTodo);
        Task<TodoModel?> UpdateTodoAsync(TodoModel updatedTodo);
        Task DeleteTodoAsync(int id);
    }

}
