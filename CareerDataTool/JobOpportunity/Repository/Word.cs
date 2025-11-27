using Microsoft.Data.SqlClient;
using System.Data;


namespace JobOpportunity.Repository
{
    public class Word : DbEntity
    {
        public Word() : base()
        {
        }
        public List<KeyWordsAllInfo> GetAll()
        {
            List<KeyWordsAllInfo> words = new List<KeyWordsAllInfo>();
            try
            {
                using (SqlCommand command = new SqlCommand("SelectWordsGetAll", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            words.Add(new KeyWordsAllInfo
                            {
                                IdWord = int.Parse(reader["idWord"].ToString()),
                                KeyWord = reader["word"].ToString(),
                                Variations = new List<string>(reader["variations"].ToString().Split(';')),
                                Description = reader["description"].ToString(),
                                Category = reader["category"].ToString(),
                                FineCategory = reader["fineCategory"].ToString()
                            });
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                connection.Close();
            }
            return words;

        }
    }
}
