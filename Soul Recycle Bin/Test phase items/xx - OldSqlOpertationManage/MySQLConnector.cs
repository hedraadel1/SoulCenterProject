//-----------------------------------------------------------------------
// <copyright file="MySQLConnector.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using MySql.Data.MySqlClient;

namespace SoulCenterProject.Test_phase_items.xx___OldSqlOpertationManage
{
    public class MySQLConnector
    {
        //write mysql connectionstring server ip adress : 178.18.251.168 - Database: onoo_soul - User: onoo_bu - pass : Qq202020@@@

        private readonly MySqlConnection _connection;
        private readonly string _connectionString;
        private readonly MySqlTransaction _transaction;


        public MySQLConnector(string connectionString) { _connectionString = connectionString; }

        public MySqlConnection GetConnection()
        {
            MySqlConnection conn = new MySqlConnection(_connectionString);
            conn.Open();
            return conn;
        }
    }
}
