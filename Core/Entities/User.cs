namespace Core.Entities
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Lastname { get; set;}
		public string Email { get; set; }
		public string Password { get; set; }
		public int UserTypeID { get; set; }
		public UserStatus Status { get; set; }
	}

	public enum UserStatus
    {
        Active = 1,
        Inactive = 2,
    }
}
