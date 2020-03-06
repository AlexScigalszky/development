using System.ComponentModel.DataAnnotations.Schema;

namespace Example.Model.Model
{
    public class User : BaseModel
    {
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

        public long TypeId { get; set; }

        [NotMapped]
        public string Fullname
        {
            get
            {
                return this.Firstname + " " + this.Lastname;
            }
            private set { }
        }
    }
}
