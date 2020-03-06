using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Example.Model.Model
{
    public class UserToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string user_token { get; set; }
        public DateTime expired { get; set; }

        public long UsersId { get; set; }
        public virtual User User { get; set; }
    }
}
