using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microservice.Infrastructure.Entities.DB
{
    public class UserCode
    {
        public Guid Id { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public string Code { get; set; }

        public virtual User User { get; set; }
    }
}
