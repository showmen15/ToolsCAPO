using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.ComponentModel;

namespace SQLLibrary
{
    public static partial class SQL
    {
        #region ::    Global Static internals       ::

        private static bool isDisposing;

        private static SqlCommand cmd;
        private static SqlConnection conn;

        public delegate void ConnectionBrokenHandler(SqlException ex, out bool reconnectSuccessful);
        public static event ConnectionBrokenHandler ConnectionBroken;

        static SQL()
        {
            isDisposing = false;

            cmd = new SqlCommand();
            conn = new SqlConnection();
            conn.StateChange += new StateChangeEventHandler(conn_StateChange);
        }

        private static void conn_StateChange(object sender, StateChangeEventArgs e)
        {
            if (!isDisposing && e.CurrentState == ConnectionState.Closed)
                checkConnection();
        }

        private static void checkConnection()
        {
            if (cmd == null)
                cmd = new SqlCommand();

            if (cmd.Connection == null)
                conn = new SqlConnection(ConnectionString);

            if (conn.State != ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch (SqlException ex)
                {
                    bool reconnectSuccess = false;

                    if (ConnectionBroken != null)
                        ConnectionBroken(ex, out reconnectSuccess);

                    try
                    {
                        if (reconnectSuccess)
                            conn.Open();
                    }
                    catch (Exception)
                    {
                        // still some others problems...
                    }
                }
            }
            else
            {
                try
                {
                    checkingQuery();
                }
                catch (SqlException) { }
            }

            cmd.Connection = conn;
            cmd.CommandTimeout = 120;
            cmd.CommandText = string.Empty;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Clear();
        }

        private static void checkingQuery()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT GETDATE()";
            cmd.Parameters.Clear();

            cmd.ExecuteNonQuery();
        }

        public static string ConnectionString
        {
            get
            {
                return conn.ConnectionString;
            }
            set
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.ConnectionString = value + ";Asynchronous Processing=true";

                cmd.Connection = conn;
                conn.Open();
            }
        }

        public static bool IsConnected
        {
            get
            {
                return conn != null && conn.State == ConnectionState.Open;
            }
        }

        /// <summary>
        /// Can be executed only when application is terminated.
        /// </summary>
        public static void DisposeOnAppExit()
        {
            isDisposing = true;

            if (conn != null && conn.State == ConnectionState.Open)
                conn.Close();

            if (conn != null)
                conn.Dispose();

            if (cmd != null)
                cmd.Dispose();
        }

        public static void FlushParams()
        {
            if (cmd != null)
                cmd.Parameters.Clear();
        }

        public static object GetValueOrDBNull(this object a)
        {
            return (a == null) ? (object)DBNull.Value : a;
        }

        public static T GetValue<T>(this object a)
        {
            return (a is DBNull || a == null) ? default(T) : (T)a;
        }

        public static T GetValue<T>(this object a, T defaultValue)
        {
            //return (a is DBNull || a == null) ? defaultValue : (T)a;
            return (!(a is T)) ? defaultValue : (T)a;
        }

        public static bool IsNullOrDBNull(this object a)
        {
            return (a == null || a is DBNull);
        }

        public static bool IsNotNullOrDBNullAndIs<T>(this object a)
        {
            return (a != null && !(a is DBNull) && a is T);
        }

        public static string GetDescriptionAttr<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }

        public static string GetEnumDescr(this Enum source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }

        /// <summary>
        /// Checks if the TypeCode of numerical types are between SByte (5) and Decimal (15).
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNumeric(this Type type)
        {
            if (type == null) return false;

            TypeCode typeCode = Type.GetTypeCode(type);
            if ((int)typeCode >= 5 && (int)typeCode <= 15)
                return true;

            if (typeCode == TypeCode.Object &&
                type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return IsNumeric(Nullable.GetUnderlyingType(type));

            return false;
        }

        public static string ObjectToCurrencyString(object val)
        {
            CultureInfo ci = new CultureInfo("de-DE");

            return (val != null && val is decimal) ?
                Convert.ToDecimal(val).ToString("C", ci.NumberFormat) :
                decimal.Zero.ToString("C", ci.NumberFormat);
        }

        public static int GetFNV1aHashCode(this string str)
        {
            if (str == null)
                return 0;
            var length = str.Length;
            int hash = length;
            for (int i = 0; i != length; ++i)
                hash = (hash ^ str[i]) * 16777619;
            return hash;
        }

        public static int GetFNV1aHashCodeTrimmed(this string str)
        {
            if (str == null)
                return 0;

            str = str.Trim();
            var length = str.Length;
            int hash = length;
            for (int i = 0; i != length; ++i)
                hash = (hash ^ str[i]) * 16777619;
            return hash;
        }

        public static SqlDataReader ExecuteCustomQuery(string query)
        {
            checkConnection();
            cmd.CommandText = query;

            return cmd.ExecuteReader();
        }
        #endregion
    }
}
