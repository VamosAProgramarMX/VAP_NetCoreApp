using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAP_NetCoreApp.Models.Queries
{
    public class Roles : BaseQuery
    {
        public Roles() : base() { }
        
        /// <summary>
        /// Mostrar todos los usuarios en la tabla
        /// </summary>
        /// <returns>Lista de usuarios</returns>
        public List<Role> GetAll()
        {
            using (var db = GetConnection())
            {
                return db.Query<Role>(@"SELECT * FROM [Role]").ToList();
            }
        }
    }
}
