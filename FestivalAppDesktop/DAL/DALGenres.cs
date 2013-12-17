using Models.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALGenres
    {
        public static ObservableCollection<Genre> GetGenres()
        {
            ObservableCollection<Genre> lijst = new ObservableCollection<Genre>();

            string SQL = "SELECT * FROM Genre";
            Database.GetData(SQL);
            DbDataReader reader = Database.GetData(SQL);
            while (reader.Read())
            {
                Genre Nieuw = new Genre();
                Nieuw.id = Int32.Parse(reader["ID"].ToString());
                Nieuw.name = reader["Name"].ToString();

                lijst.Add(Nieuw);
            }

            return lijst;
        }

        public static void InsertGenre(Genre SelectedGenre)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = new SqlParameter("param1", SelectedGenre.name);

            string SQL = "INSERT INTO Genre (Name) VALUES (@param1);";
            Database.ModifyData(SQL, parameters);
        }

        public static void UpdataGenre(Genre SelectedGenre)
        {
            DbParameter[] parameters = new DbParameter[2];
            parameters[0] = new SqlParameter("param1", SelectedGenre.name);
            parameters[1] = new SqlParameter("param2", SelectedGenre.id);

            string SQL = "UPDATE Genre SET Name=@param1 WHERE ID=@param2;";
            Database.ModifyData(SQL, parameters);
        }

        public static void DeleteGenre(Genre SelectedGenre)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = new SqlParameter("param1", SelectedGenre.id);

            string SQL = "DELETE FROM ContactPersonType WHERE ID = @param1;";
            Database.ModifyData(SQL, parameters);
        }
    }
}
