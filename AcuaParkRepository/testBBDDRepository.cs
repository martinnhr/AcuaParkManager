using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcuaParkShared;
using MySql.Data.MySqlClient;
using Dapper;

namespace AcuaParkRepository
{
    public class testBBDDRepository : ItestBBDDRepository 
    {

        private readonly IConnectionString _connectionString;

        public testBBDDRepository(IConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<string> GetName()
        {
            try
            {
                var connectionString = await _connectionString.GetConectionString();

                using (var conexion = new MySqlConnection(connectionString))
                {
                    var sql = @"SELECT Nombre FROM test WHERE id = 2";

                    var result = await conexion.QueryFirstOrDefaultAsync<string>(sql);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}