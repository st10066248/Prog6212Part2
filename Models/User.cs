namespace ClaimsManagamentSystem.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; } // Lecturer, Program Coordinator, Academic Manager
    }

}
