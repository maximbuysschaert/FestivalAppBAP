using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;     //nieuw
using System.Data.Common;
using System.Data;     //nieuw

namespace Oefening1.Model
{
    class Database
    {
        //vooraf: instellingen ophalen uit config-bestand
        private static ConnectionStringSettings ConnectionString 
        {
            get 
            {
                //met onderstaande lijn haalt hij alle informatie uit het configuratiebestand op 
                //dat te maken heeft met de connectionstring met naam "ConnectionString"
                return ConfigurationManager.ConnectionStrings["ConnectionString"];
            }
        }

        //stap 1: connectie opvragen
        private static DbConnection GetConnection()
        {
            DbConnection con = DbProviderFactories.GetFactory(ConnectionString.ProviderName).CreateConnection();
            con.ConnectionString = ConnectionString.ConnectionString;

            con.Open();

            return con;
        }

        //stap 2: connectie vrijgeven
        public static void ReleaseConnection(DbConnection con)
        {
            if (con != null)
            {
                con.Close();
                con = null;
            }
        }

        //stap 3: Command gaan opstellen: sql-string & parameters doorgeven
        //opmerking: keyword params laat toe deze methode op te roepen met slechts één parameter, nl. de sql-string
        private static DbCommand BuildCommand(String sql, params DbParameter[] parameters)
        {
            //intern in deze methode gaan we connectie leggen met de database
            DbCommand command = GetConnection().CreateCommand();

            //command -> boodschappenlijstje
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = sql;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            return command;
        }

        //stap 3bis: hulpmethode om parameters te maken 
        //deze methode maakt een parameter aan (die dan later kan doorgegeven worden via de methode BuildCommand)
        public static DbParameter AddParameter(String naam, object value)
        {
            //parameters zijn provider-afhankelijk. Ik ben dus verplicht terug te gaan naar de factory, specifiek voor mijn provider
            DbParameter par = DbProviderFactories.GetFactory(ConnectionString.ProviderName).CreateParameter();
            par.ParameterName = naam;
            par.Value = value;

            return par;
        }

        //stap 4A: data ophalen (select-statements)
        public static DbDataReader GetData(string sql, params DbParameter[] parameters)
        {
            //zie ovirge methode(s)
            DbCommand command = BuildCommand(sql, parameters);
            DbDataReader reader = null;

            try
            {
                command = BuildCommand(sql, parameters);

                //op onderstaande lijn wordt naar database gegaan, en wordt met een dataReader teruggekeerd.
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                //teruggeven
                return reader;
            }
            catch(Exception ex)
            {
                //even afprinten wat verkeerd is
                Console.WriteLine(ex.Message);
                if (reader != null) reader.Close();
                if (command != null) ReleaseConnection(command.Connection);

                //fout doorgeven aan de aanroeper
                throw ex;
            }
        }

        //stap 4B: database gaan wijzigen (insert-delete-update)
        public static int ModifyData(String sql, params DbParameter[] parameters)
        {
            DbCommand command = null;
            try
            {
                command = BuildCommand(sql, parameters);
                int aantalRijenGewijzigd = command.ExecuteNonQuery();

                //connectie vrijgeven
                ReleaseConnection(command.Connection);

                //aantal verwijderde/toegevoegde/aangepaste rijen wordt terug gegeven zo heeft de gebruiker controle of het gelukt is of niet
                return aantalRijenGewijzigd;
            }
            catch (Exception ex)
            {
                if (command != null) ReleaseConnection(command.Connection);

                //fout doorgeven aan de aanroeper
                throw ex;
            }
        }

        //EXTRA: werken met transacties
        //vooraf: transactie aanmaken (waarin alle commando's ofwel lukken, ofwel niet lukken)
        public static DbTransaction BeginTransaction()
        {
            DbConnection con = null;

            try
            {
                con = GetConnection();
                //transactie aanmaken teruggeven
                return con.BeginTransaction();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (con != null) ReleaseConnection(con);
                throw ex;
            }
        }

        //stap 3 extra: command ifv transactie
        private static DbCommand BuildCommand(DbTransaction trans, String sql, params DbParameter[] parameters)
        {
            //intern in deze methode gaan we connectie leggen met de database
            DbCommand command = trans.Connection.CreateCommand();

            //command -> boodschappenlijstje
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = sql;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            return command;
        }

        //stap 4 extra A: Data ophalen binnen in een transactie
        public static DbDataReader GetData(DbTransaction trans, string sql, params DbParameter[] parameters)
        {
            //zie ovirge methode(s)
            DbCommand command = BuildCommand(sql, parameters);
            DbDataReader reader = null;

            try
            {
                command = BuildCommand(trans, sql, parameters);

                //op onderstaande lijn wordt naar database gegaan, en wordt met een dataReader teruggekeerd.
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                //teruggeven
                return reader;
            }
            catch (Exception ex)
            {
                //even afprinten wat verkeerd is
                Console.WriteLine(ex.Message);
                if (reader != null) reader.Close();
                if (command != null) ReleaseConnection(command.Connection);

                //fout doorgeven aan de aanroeper
                throw ex;
            }
        }

        //stap 4 extra B: Data wijzigen binnen in een transactie
        public static int ModifyData(DbTransaction trans, String sql, params DbParameter[] parameters)
        {
            DbCommand command = null;
            try
            {
                command = BuildCommand(trans, sql, parameters);
                int aantalRijenGewijzigd = command.ExecuteNonQuery();

                //connectie vrijgeven
                ReleaseConnection(command.Connection);

                //aantal verwijderde/toegevoegde/aangepaste rijen wordt terug gegeven zo heeft de gebruiker controle of het gelukt is of niet
                return aantalRijenGewijzigd;
            }
            catch (Exception ex)
            {
                if (command != null) ReleaseConnection(command.Connection);

                //fout doorgeven aan de aanroeper
                throw ex;
            }
        }
    }
}
