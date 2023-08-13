using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.DTOs.User
{
    public class RoleDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string IsActive { get; set; }
        public string CreateBy { get; set; }
    }
}
