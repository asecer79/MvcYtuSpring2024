using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.CommonInterfaces
{
    public interface IEntityBase
    {
        [Key]
        public int Id { get; set; }


       
    }
}
