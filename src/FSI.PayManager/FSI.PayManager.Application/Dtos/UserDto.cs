using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSI.PayManager.Application.Dtos
{
    public sealed class UserDto : IHasId
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}