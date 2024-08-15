namespace Test_Week_7.Models
{
    public class TodoModel
    {
        public int Id { get; set; }
        public string Todo { get; set; } = "";
        public bool Completed { get; set; }
        public int UserId { get; set; }
    }
}
