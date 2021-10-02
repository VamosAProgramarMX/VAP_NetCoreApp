using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAP_NetCoreApp.Models.ViewModels;

namespace VAP_NetCoreApp.Models.Queries
{
    public class Users : BaseQuery
    {
        public Users() : base() { }
        
        /// <summary>
        /// Mostrar todos los usuarios en la tabla
        /// </summary>
        /// <returns>Lista de usuarios</returns>
        public List<UserVM> GetAll()
        {
            using (var db = GetConnection())
            {
                return db.Query<UserVM>(@"SELECT * FROM [UserVM]").ToList();
            }
        }

        /// <summary>
        /// Traer a un usuario en especifico por su ID
        /// </summary>
        /// <returns>El usuario que tenga el ID que le proporcionamos</returns>
        public User GetByID(int ID)
        {
            using (var db = GetConnection())
            {
                return db.QueryFirstOrDefault<User>(@"SELECT * FROM [User] WHERE ID = @ID", new { ID });
            }
        }

        /// <summary>
        /// Crea un nuevo usuario en la tabla User, usando un INSERT directo en C#
        /// </summary>
        /// <param name="user">Objeto usuario con las propiedades a guardar</param>
        /// <returns>BaseResult indicando si se agrego el registro o si hubo un error, muestra el mensaje en Message</returns>
        public BaseResult CreateUsingSQLCode(User user)
        {
            var rowsAffected = 0;
            using (var db = GetConnection())
            {
                rowsAffected = db.Execute(@"INSERT INTO [User] (Email, Balance, Age, RoleID) 
                                                 VALUES (@Email, @Balance, @Age, @RoleID)", user);
            }

            return new BaseResult
            {
                Success = rowsAffected > 0,
                Message = rowsAffected > 0 ? string.Empty : "Por favor contactanos para revisar el problema con este usuario"
            };
        }


        /// <summary>
        /// Crea un nuevo usuario en la tabla User, usando un procedimiento almacenado
        /// </summary>
        /// <param name="user">Objeto usuario con las propiedades a guardar</param>
        /// <returns>BaseResult indicando si se agrego el registro o si hubo un error, muestra el mensaje en Message</returns>
        public BaseResult CreateUsingStoredProcedure(User user)
        {
            using (var db = GetConnection())
            {
                return db.QueryFirstOrDefault<BaseResult>(@"User_AddUser", new { 
                    user.Email, user.RoleID, user.Balance, user.Age
                }, commandType:  System.Data.CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Edita las propiedades del usuario en la tabla User, usando un UPDATE directo en C#
        /// </summary>
        /// <param name="user">Objeto usuario con las propiedades a actualizar</param>
        /// <returns>BaseResult indicando si se actualizo el registro o si hubo un error, muestra el mensaje en Message</returns>
        public BaseResult Update(User user)
        {
            var rowsAffected = 0;
            using (var db = GetConnection())
            {
                rowsAffected = db.Execute(@"UPDATE [User]
                                               SET Email = @Email
                                                 , RoleID = @RoleID
                                                 , IsActive = @IsActive
                                                 , Age = @Age
                                                 , Balance = @Balance
                                             WHERE ID = @ID", user);
            }

            return new BaseResult
            {
                Success = rowsAffected > 0,
                Message = rowsAffected > 0 ? string.Empty : "Por favor contactanos para revisar el problema con este usuario"
            };
        }

        /// <summary>
        /// Elimina un usuario de la table User
        /// </summary>
        /// <param name="ID">ID del usuario</param>
        /// <returns>BaseResult indicando si se elimino el registro o si hubo un error, muestra el mensaje en Message</returns>
        public BaseResult Delete(int ID)
        {
            try
            {
                var rowsAffected = 0;
                using (var db = GetConnection())
                {
                    rowsAffected = db.Execute(@"DELETE [User] WHERE ID = @ID", new { ID });
                }

                return new BaseResult
                {
                    Success = rowsAffected > 0,
                    Message = rowsAffected > 0 ? string.Empty : "Por favor contactanos para revisar el problema con este usuario"
                };
            }
            catch (Exception ex)
            {
                return new BaseResult { Message = ex.Message, Success = false };
            }
        }
    }
}
