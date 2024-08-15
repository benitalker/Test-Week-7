using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Test_Week_7.Models;

namespace Test_Week_7.Service
{
    public class TodoService(IHttpClientFactory clientFactory) : ITodoService
    {
        public async Task<TodosList?> GetAllTodosAsync()
        {
            var httpClient = clientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://dummyjson.com/todos");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync(); 
            TodosList? todos = JsonSerializer.Deserialize<TodosList>(
            content,new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return todos;
        }

        public async Task<TodoModel?> CreateTodoAsync(TodoModel newTodo)
        {
            var httpClient = clientFactory.CreateClient();
            var httpContent = new StringContent(JsonSerializer.Serialize(newTodo), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://dummyjson.com/todos", httpContent);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TodoModel>(responseContent);
        }

        public async Task<TodoModel?> GetTodoByIdAsync(int id)
        {
            var httpClient = clientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://dummyjson.com/todos/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TodoModel>(content);
        }

        public async Task<TodoModel?> UpdateTodoAsync(TodoModel updatedTodo)
        {
            var httpClient = clientFactory.CreateClient();

            var httpContent = new StringContent(JsonSerializer.Serialize(updatedTodo, new JsonSerializerOptions()
            { PropertyNameCaseInsensitive = true }), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"https://dummyjson.com/todos/{updatedTodo.Id}", httpContent);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TodoModel>(responseContent);
        }

        public async Task DeleteTodoAsync(int id)
        {
            var httpClient = clientFactory.CreateClient();
            var response = await httpClient.DeleteAsync($"https://dummyjson.com/todos/{id}");
            response.EnsureSuccessStatusCode();
        }
    }

}
