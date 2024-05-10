using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repositories.CommonInterfaces;

namespace Entities.CommonEntities
{
    public class User:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EMail { get; set; }
        public string? Password { get; set; }

    }
}
