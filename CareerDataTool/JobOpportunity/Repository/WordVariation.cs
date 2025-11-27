using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobOpportunity.Repository
{
    public class WordVariation : DbEntity
    {
        public WordVariation() : base()
        {
        }
        public List<JobOpportunity.WordVariation> GetAll()
        {
            List<JobOpportunity.WordVariation> words = new List<JobOpportunity.WordVariation>();
            try
            {
                using (SqlCommand command = new SqlCommand("SelectWordVariationsGetAll", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            words.Add(new JobOpportunity.WordVariation
                            {
                                IdVariation = int.Parse(reader["idVariation"].ToString()),
                                IdWord = int.Parse(reader["idWord"].ToString()),
                                Word = reader["word"].ToString()
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
        public int Insert(List<JobOpportunity.WordVariation> wordVariations)
        {
            int idWordVariation = 0;
            try
            {
                connection.Open();
                foreach (var wordVariation in wordVariations)
                {
                    using (SqlCommand command = new SqlCommand("InsertWordVariations", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@idWord", wordVariation.IdWord);
                        command.Parameters.AddWithValue("@word", wordVariation.Word);

                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int id))
                        {
                            idWordVariation = id;
                        }

                    }
                }
                connection.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao inserir job: " + ex.Message);
                connection.Close();
            }
            return idWordVariation;

        }
    }
}
