namespace BRS.Model
{
    public class UserDto
    {
        public Guid RoleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserEmail { get; set; }
        public string ContactNumber { get; set; }
    }
}
