using System;
using System.Collections.Generic;
using System.Text;

namespace Bionessori.Models {
    /// <summary>
    /// Модель описывает роли пользователя.
    /// </summary>
    public class UserRole {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Role { get; set; } 
    }
}
