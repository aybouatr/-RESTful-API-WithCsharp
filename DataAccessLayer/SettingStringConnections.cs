using System;
using System.Collections.Generic;
using System.Text;

namespace SudentsDataAccessLayer
{
    internal class SettingStringConnections
    {
        public static string ConnectionString { get; } = "Server=.;Database=StudentsDB;User Id=sa;Password=123456;TrustServerCertificate=True;";
    }
}
