namespace CleanTemplate.Core.Entities
{
    public class User
    {
        public User(string id, string firstName, string lastName, string userId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            UserId = userId;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
    }
}
