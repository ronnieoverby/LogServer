using System;
using System.Configuration;
using System.Data.Common;
using JetBrains.Annotations;

namespace LogServer.Client
{
    class LogServerConnection
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }

        public LogServerConnection([NotNull] string connStringOrName)
        {
            if (connStringOrName == null) throw new ArgumentNullException("connStringOrName");

            var connString = connStringOrName;
            try
            {
                connString = ConfigurationManager.ConnectionStrings[connStringOrName].ConnectionString;
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch { }
            // ReSharper restore EmptyGeneralCatchClause

            var csb = new DbConnectionStringBuilder();
            try
            {
                csb.ConnectionString = connString;
            }
            catch (ArgumentException ex)
            {
                throw new FormatException(string.Format("The connection string '{0}' is not formatted correctly. " +
                                                        "Example connection string: " +
                                                        "url=http://logserver.company.com:123;username=johndoe;password=secret",
                                                        connString), ex);
            }

            object value;
            if (csb.TryGetValue("url", out value))
                Url = (string)value;
            else
                throw new FormatException("A url was not specified in the connection string.");

            if (csb.TryGetValue("username", out value))
                Username = (string)value;

            if (csb.TryGetValue("password", out value))
                Password = (string)value;
        }
    }
}