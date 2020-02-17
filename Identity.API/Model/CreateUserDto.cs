namespace Identity.API.Model
{
    public class CreateUserDto
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
    }
}