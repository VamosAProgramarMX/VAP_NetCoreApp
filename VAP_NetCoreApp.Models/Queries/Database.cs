using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAP_NetCoreApp.Models.Queries
{
    public class Database
    {
        /// <summary>
        /// Queries de Users
        /// </summary>
        public Users Users { get; set; }

        /// <summary>
        /// Queries de Roles
        /// </summary>
        public Roles Roles { get; set; }

        public Database()
        {
            Users = new Users();
            Roles = new Roles();
        }
    }
}
