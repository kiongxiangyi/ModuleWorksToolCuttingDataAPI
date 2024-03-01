using System.Data.SqlClient;

namespace GetComponents;
public class Components(string connectionString)
{
    private readonly string _connectionString = connectionString; // Private field to store the database connection string

    public object GetComponents()
    {

        try
        {
       
            string query = "SELECT * FROM [GTMS_Test].[dbo].[tblBauteile]";
            var components = new List<object>(); 


            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open(); 

                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    int id = reader.GetInt32(0); 

                    string component = reader.GetString(reader.GetOrdinal("Bauteil"));

                    components.Add(new { ID = id, Component = component });
                }

                reader.Close();

            }
            return components;

        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return null;
        }
    }
}