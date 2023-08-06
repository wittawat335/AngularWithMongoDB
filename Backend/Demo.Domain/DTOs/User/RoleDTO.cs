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
        public string RoleCode { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string CreateBy { get; set; }
    }
}
