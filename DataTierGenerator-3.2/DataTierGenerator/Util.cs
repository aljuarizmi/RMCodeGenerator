using System;
using System.Collections.Generic;
using System.Text;

namespace DataTierGenerator
{
    public class Util
    {

        public enum Provider
        {
            Access = 0,
            SqlClient = 1,
            Oracle = 2,
            Db2 = 3,
            MySql = 4,
            PostgreSQL = 5,
            ODBC = 6,
            OleDB = 7
        };

        public static string strProp;

        public static void SetPropietario(string strP)
        {
            strProp = strP;
        }

        public static string GetPropietario()
        {
            return strProp;
        }
	

    }
}
