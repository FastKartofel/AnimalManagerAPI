using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PatientCareAPI.Models;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PatientCareAPI.DAL
{
    public class AnimalRepository : IAnimalRepository
    {

        //get the animal by some of his attributes
        public IEnumerable<Animal> GetAnimals(string orderBy)
        {
            string orderByColumn = GetOrderByColumn(orderBy);

            using (SqlConnection con = new SqlConnection("Data Source=localhost\\SQLEXPRESS01;Initial Catalog=ANIMALL;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = $"SELECT * FROM ANIMALL ORDER BY {orderByColumn}";

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                var list = new List<Animal>();
                while (reader.Read())
                {
                    var s = new Animal();
                    s._idAnimal = (int)reader["idAnimal"];
                    s.Name = reader["Name"].ToString();
                    s.Description = reader["Description"].ToString();
                    s.Category = reader["Category"].ToString();
                    s.Area = reader["Area"].ToString();
                    list.Add(s);
                };
                return list;
            }
        }


        //method for selecting the way we are ordering the data in the response
        private string GetOrderByColumn(string orderBy)
        {
            var validColumns = new HashSet<string> { "name", "description", "category", "area" };

            if (string.IsNullOrEmpty(orderBy) || !validColumns.Contains(orderBy.ToLower()))
            {
                return "Name";
            }

            return orderBy;
        }


        //create and post a new animal to the database
        public Animal AddAnimal(Animal _animal)
        {

            using (SqlConnection con = new SqlConnection("Data Source=localhost\\SQLEXPRESS01;Initial Catalog=ANIMALL;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO ANIMALL (Name, Description, Category, Area) VALUES (@Name, @Description, @Category, @Area); SELECT SCOPE_IDENTITY();", con);

                //cmd.Parameters.AddWithValue("@idNumber", _animal._idAnimal);
                cmd.Parameters.AddWithValue("@Name", _animal.Name);
                cmd.Parameters.AddWithValue("@Description", _animal.Description);
                cmd.Parameters.AddWithValue("@Category", _animal.Category);
                cmd.Parameters.AddWithValue("@Area", _animal.Area);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


                return _animal;
            }
        }

        //delete animlas fro mthe database
        public void DeleteAnimal(int idAnimal)
        {
            using (var connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS01;Initial Catalog=ANIMALL;Integrated Security=True"))
            {
                var command = new SqlCommand("DELETE FROM ANIMALL WHERE idAnimal = @idAnimal");

                command.Connection = connection;
                command.Parameters.AddWithValue("idAnimal", idAnimal);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                connection.Close();

            }
        }

        //update animals in the database, by inputiong his id !(something still wrong with this method)
        public void ChangeAnimal(int idAnimal, Animal animal)
        {
            using (var connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS01;Initial Catalog=ANIMALL;Integrated Security=True"))
            {

                var command = new SqlCommand("UPDATE Animal SET Name = @Name, Description = @Description, Category = @Category, Area = @Area WHERE idAnimal = @IdAnimal", connection);

                command.Parameters.AddWithValue("@Name", animal.Name);
                command.Parameters.AddWithValue("@Description", animal.Description);
                command.Parameters.AddWithValue("@Category", animal.Category);
                command.Parameters.AddWithValue("@Area", animal.Area);
                command.Parameters.AddWithValue("@IdAnimal", idAnimal);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
