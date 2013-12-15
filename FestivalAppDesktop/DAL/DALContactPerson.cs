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
    public class DALContactPerson
    {
        public static ObservableCollection<ContactPerson> GetContactPersons()
        {
            ObservableCollection<ContactPerson> lijst = new ObservableCollection<ContactPerson>();

            string SQL = "SELECT * FROM ContactPerson";
            Database.GetData(SQL);
            DbDataReader reader = Database.GetData(SQL);
            while (reader.Read())
            {
                ContactPerson Nieuw = new ContactPerson();
                Nieuw.ID = Int32.Parse(reader["ID"].ToString());
                Nieuw.FirstName = reader["FirstName"].ToString();
                Nieuw.LastName = reader["LastName"].ToString();
                Nieuw.Company = reader["Company"].ToString();


                int JobRole = Int32.Parse(reader["JobRole"].ToString());
                ObservableCollection<ContactPersonType> types = DALContactPersonTypes.GetContactPersonTypes();

                foreach (ContactPersonType type in types)
                {
                    if (type.id == JobRole)
                    {
                        Nieuw.ContactPersonType = type;
                    }
                }


                Nieuw.Address = reader["Address"].ToString();
                Nieuw.City = reader["City"].ToString();
                Nieuw.Email = reader["Email"].ToString();
                Nieuw.Phone = reader["Phone"].ToString();
                Nieuw.CellPhone = reader["Cellphone"].ToString();

                lijst.Add(Nieuw);
            }

            return lijst;
        }

        public static void UpdataContactPerson(ContactPerson SelectedContactPerson)
        {
            DbParameter[] parameters = new DbParameter[10];
            parameters[0] = new SqlParameter("param1", SelectedContactPerson.FirstName);
            parameters[1] = new SqlParameter("param2", SelectedContactPerson.LastName);
            parameters[2] = new SqlParameter("param3", SelectedContactPerson.Company);
            parameters[3] = new SqlParameter("param4", SelectedContactPerson.ContactPersonType.id);
            parameters[4] = new SqlParameter("param5", SelectedContactPerson.Address);
            parameters[5] = new SqlParameter("param6", SelectedContactPerson.City);
            parameters[6] = new SqlParameter("param7", SelectedContactPerson.Email);
            parameters[7] = new SqlParameter("param8", SelectedContactPerson.Phone);
            parameters[8] = new SqlParameter("param9", SelectedContactPerson.CellPhone);
            parameters[9] = new SqlParameter("param10", SelectedContactPerson.ID);

            string SQL = "UPDATE ContactPerson SET FirstName=@param1, LastName=@param2, Company=@param3, JobRole=@param4, Address=@param5, City=@param6, Email=@param7, Phone=@param8, Cellphone=@param9  WHERE ID=@param10;";
            Database.ModifyData(SQL, parameters);
        }

        public static void InsertContactPerson(ContactPerson SelectedContactPerson)
        {
            //Console.WriteLine(SelectedContactPerson.FirstName);
            DbParameter[] parameters = new DbParameter[9];
            parameters[0] = new SqlParameter("param1", SelectedContactPerson.FirstName);
            parameters[1] = new SqlParameter("param2", SelectedContactPerson.LastName);
            parameters[2] = new SqlParameter("param3", SelectedContactPerson.Company);
            parameters[3] = new SqlParameter("param4", SelectedContactPerson.ContactPersonType.id);
            parameters[4] = new SqlParameter("param5", SelectedContactPerson.Address);
            parameters[5] = new SqlParameter("param6", SelectedContactPerson.City);
            parameters[6] = new SqlParameter("param7", SelectedContactPerson.Email);
            parameters[7] = new SqlParameter("param8", SelectedContactPerson.Phone);
            parameters[8] = new SqlParameter("param9", SelectedContactPerson.CellPhone);

            string SQL = "INSERT INTO ContactPerson (FirstName, LastName, Company, JobRole, Address, City, Email, Phone, Cellphone) VALUES (@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9);";
            Database.ModifyData(SQL, parameters);
        }

        public static void DeleteContactPerson(ContactPerson SelectedContactPerson)
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = new SqlParameter("param1", SelectedContactPerson.ID);

            string SQL = "DELETE FROM ContactPerson WHERE ID = @param1;";
            Database.ModifyData(SQL, parameters);
        }
    }
}
