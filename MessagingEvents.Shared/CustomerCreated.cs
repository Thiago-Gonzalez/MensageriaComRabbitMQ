namespace MessagingEvents.Shared
{
    public class CustomerCreated
    {
        public CustomerCreated() { }

        public CustomerCreated(string fullName, string email, string phoneNumber, DateTime birthDate)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
        }

        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
