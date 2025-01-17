using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DbSQLServer2022
    {
        private readonly string _connectionString;

        public DbSQLServer2022()
        {
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SGII_BackDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
        
    }
}
