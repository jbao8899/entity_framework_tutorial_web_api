namespace entity_framework_tutorial_web_api.Models
{
    public class Contact
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Nickname { get; set; }

        public string Place { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false; // for soft delete
    }
}
