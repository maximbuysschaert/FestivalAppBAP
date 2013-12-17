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
    public class DALStages
    {
        public static ObservableCollection<Stage> GetStages()
        {
            ObservableCollection<Stage> lijst = new ObservableCollection<Stage>();

            string SQL = "SELECT * FROM Stage";
            Database.GetData(SQL);
            DbDataReader reader = Database.GetData(SQL);
            while (reader.Read())
            {
                Stage Nieuw = new Stage();
                Nieuw.id = Int32.Parse(reader["ID"].ToString());
                Nieuw.name = reader["Name"].ToString();

                lijst.Add(Nieuw);
            }

            return lijst;
        }

        public static void InsertStage(Stage SelectedStage)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = new SqlParameter("param1", SelectedStage.name);

            string SQL = "INSERT INTO Stage (Name) VALUES (@param1);";
            Database.ModifyData(SQL, parameters);
        }

        public static void UpdataStage(Stage SelectedStage)
        {
            DbParameter[] parameters = new DbParameter[2];
            parameters[0] = new SqlParameter("param1", SelectedStage.name);
            parameters[1] = new SqlParameter("param2", SelectedStage.id);

            string SQL = "UPDATE Stage SET Name=@param1 WHERE ID=@param2;";
            Database.ModifyData(SQL, parameters);
        }

        public static void DeleteStage(Stage SelectedStage)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = new SqlParameter("param1", SelectedStage.id);

            string SQL = "DELETE FROM Stage WHERE ID = @param1;";
            Database.ModifyData(SQL, parameters);
        }
    }
}
