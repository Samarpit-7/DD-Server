

using System.ComponentModel.DataAnnotations;



namespace DD_Server.Models
{
    public class AppUser
    {

        public AppUser()
        {
        }

        public AppUser(string user_name, string email, string password)
        {
            this.user_name = user_name;
            this.email = email;
            this.password = password;
        }

        [Key]
        public int Id { get; set; }
        public string user_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string role { get; set; } = "User";
        public ICollection<Request> Requests { get; } = new List<Request>();
        public ICollection<Audit> Audits { get; } = new List<Audit>();
        public ICollection<Dictionary> Dictionary { get; } = new List<Dictionary>();

    }
}