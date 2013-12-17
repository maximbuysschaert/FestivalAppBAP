using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Model;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.SqlClient;

namespace DAL
{
    public class DALContactPersonTypes
    {
        public static ObservableCollection<ContactPersonType> GetContactPersonTypes()
        {
            ObservableCollection<ContactPersonType> lijst = new ObservableCollection<ContactPersonType>();

            string SQL = "SELECT * FROM ContactPersonType";
            Database.GetData(SQL);
            DbDataReader reader = Database.GetData(SQL);
            while (reader.Read())
            {
                ContactPersonType Nieuw = new ContactPersonType();
                Nieuw.id = Int32.Parse(reader["ID"].ToString());
                Nieuw.name = reader["Name"].ToString();

                lijst.Add(Nieuw);
            }

            return lijst;
        }

        public static void InsertContactPersonType(ContactPersonType SelectedContactPersonType)
        {
            //Console.WriteLine(SelectedContactPerson.FirstName);
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = new SqlParameter("param1", SelectedContactPersonType.name);

            string SQL = "INSERT INTO ContactPersonType (Name) VALUES (@param1);";
            Database.ModifyData(SQL, parameters);
        }

        public static void UpdataContactPerson(ContactPersonType SelectedContactPersonType)
        {
            DbParameter[] parameters = new DbParameter[2];
            parameters[0] = new SqlParameter("param1", SelectedContactPersonType.name);
            parameters[1] = new SqlParameter("param2", SelectedContactPersonType.id);

            string SQL = "UPDATE ContactPersonType SET Name=@param1 WHERE ID=@param2;";
            Database.ModifyData(SQL, parameters);
        }

        public static void DeleteContactPerson(ContactPersonType SelectedContactPersonType)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = new SqlParameter("param1", SelectedContactPersonType.id);

            string SQL = "DELETE FROM ContactPersonType WHERE ID = @param1;";
            Database.ModifyData(SQL, parameters);
        }
    }
}
