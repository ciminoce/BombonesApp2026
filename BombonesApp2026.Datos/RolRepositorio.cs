using BombonesApp2026.Entidades;
using Microsoft.Data.SqlClient;

namespace BombonesApp2026.Datos
{
    public class RolRepositorio
    {
        private readonly string _cadenaConexion;
        public RolRepositorio()
        {
            _cadenaConexion = "Data Source=.; Initial Catalog=Bombones2026; Integrated Security=True;TrustServerCertificate=True";
        }
        public List<Rol> GetLista()
        {
            List<Rol> lista = new List<Rol>();
            using (SqlConnection conn=new SqlConnection(_cadenaConexion) )
            {
                conn.Open();
                string sql = "SELECT RolId, Nombre, Descripcion, Activo FROM Roles";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader=command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Rol rol = ConstruirRol(reader);
                            lista.Add(rol);
                        }
                    }
                }

            }
            return lista;
        }
        public List<Rol> FiltrarPorActivo(bool activo)
        {
            List<Rol> lista = new List<Rol>();
            using (SqlConnection conn = new SqlConnection(_cadenaConexion))
            {
                conn.Open();
                string sql = "SELECT RolId, Nombre, Descripcion, Activo FROM Roles WHERE Activo=@activo";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@activo", activo);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Rol rol = ConstruirRol(reader);
                            lista.Add(rol);
                        }
                    }
                }

            }
            return lista;
        }

        public void Agregar(Rol rol)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_cadenaConexion))
                {
                    conn.Open();
                    string sql = @"INSERT INTO Roles (Nombre, Descripcion, Activo)
                        VALUES(@nombre, @desc, @activo) SELECT SCOPE_IDENTITY()";
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@nombre", rol.Nombre);
                        if (!string.IsNullOrEmpty(rol.Descripcion))
                        {
                            command.Parameters.AddWithValue("@desc", rol.Descripcion);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@desc", DBNull.Value);
                        }
                        command.Parameters.AddWithValue("@activo", rol.Activo);
                        int id = Convert.ToInt32(command.ExecuteScalar());
                        rol.RolId = id;
                    }
                }

            }
            catch (SqlException ex)
            {
                if (ex.Number==2601){
                    throw new Exception("Registro duplicado", ex);
                }
                
                throw new Exception("Error al intentar agregar un registro",ex);
            } 
        }
        public bool ExisteRol(Rol rol)
        {
            using (SqlConnection conn=new SqlConnection(_cadenaConexion))
            {
                conn.Open();
                string sql = @"SELECT COUNT(*) FROM Roles 
                            WHERE Nombre=@nombre AND RolId<>@rolId";
                using (SqlCommand command=new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@nombre", rol.Nombre);
                    command.Parameters.AddWithValue("@rolId", rol.RolId);
                    object result=command.ExecuteScalar();
                    return Convert.ToInt32(result) > 0;
                }
            }
        }
        public void Borrar(int rolId)
        {
            using (SqlConnection conn=new SqlConnection(_cadenaConexion))
            {
                conn.Open();
                string sql = "DELETE FROM Roles WHERE RolId=@rolId";
                using (SqlCommand command=new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@rolId", rolId);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void Editar(Rol rol)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_cadenaConexion))
                {
                    conn.Open();
                    string sql = @"UPDATE Roles SET Nombre=@nombre,
                    Descripcion=@desc, Activo=@activo 
                    WHERE RolId=@rolId";
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@nombre", rol.Nombre);
                        if (!string.IsNullOrEmpty(rol.Descripcion))
                        {
                            command.Parameters.AddWithValue("@desc", rol.Descripcion);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@desc", DBNull.Value);
                        }
                        command.Parameters.AddWithValue("@activo", rol.Activo);
                        command.Parameters.AddWithValue("@rolId", rol.RolId);
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (SqlException ex)
            {

                throw new Exception("Error al intentar editar un registro",ex);
            } 
        }
        public Rol? GetById(int id)
        {
            using (SqlConnection conn =new SqlConnection(_cadenaConexion))
            {
                conn.Open();
                string sql = @"SELECT RolId, Nombre, Descripcion, Activo
                        FROM Roles WHERE RolId=@rolId";
                using (SqlCommand command=new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@rolId", id);
                    using (SqlDataReader reader=command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Rol rol = ConstruirRol(reader);
                            return rol;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
        public bool TieneRegistrosRelacionados(int rolId)
        {
            using (SqlConnection conn=new SqlConnection(_cadenaConexion))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM Usuarios WHERE RolId=@rolId";
                using (SqlCommand command=new SqlCommand(sql,conn))
                {
                    command.Parameters.AddWithValue("@rolId", rolId);
                    object result= command.ExecuteScalar();
                    return Convert.ToInt32(result) > 0;
                }
            }
        }
        private Rol ConstruirRol(SqlDataReader reader)
        {
            return new Rol
            {
                RolId = reader.GetInt32(0),
                Nombre = reader.GetString(1),
                Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                Activo=reader.GetBoolean(3)
            };
        }
    }
}
