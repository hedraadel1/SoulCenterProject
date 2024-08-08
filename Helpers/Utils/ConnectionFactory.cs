//-----------------------------------------------------------------------
// <copyright file="ConnectionFactory.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Data;
using MySql.Data.MySqlClient;

namespace SoulCenterProject.Helpers.Utils
{
    public static class ConnectionFactory
    {
        public static IDbConnection CreateConnection()
        {
            return new MySqlConnection(
                "Server=178.18.251.168;Database=newgyral_erpnew;Uid=newgyral_erpnew;Pwd=Mm102030@@@;");
        }
    }
}
