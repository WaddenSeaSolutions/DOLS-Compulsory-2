using Dapper;
using dols_compulsory_2.Server.Models;
using MySqlConnector;

namespace DOLS_Compulsory_2.Server.DAL
{
    public class NoteDAL
    {
        private readonly MySqlConnection _connection;

        public NoteDAL(MySqlConnection connection)
        {
            _connection = connection;
        }
        public List<NoteModel> GetAll()
        {
            var sql = "SELECT Id, Title, Content, CreatedAt FROM Notes";
            return _connection.Query<NoteModel>(sql).ToList();
        }

        public NoteModel GetById(int id)
        {
            var sql = "SELECT Id, Title, Content, CreatedAt FROM Notes WHERE Id = @Id";
            return _connection.QueryFirstOrDefault<NoteModel>(sql, new { Id = id });
        }

        public NoteModel Create(NoteDTO dto)
        {
            var sql = @"INSERT INTO Notes (Title, Content) VALUES (@Title, @Content);
                        SELECT LAST_INSERT_ID();";
            var noteId = _connection.ExecuteScalar<int>(sql, dto);
            return GetById(noteId);
        }

        public List<NoteModel> Search(string keyword)
        {
            var sql = "SELECT Id, Title, Content, CreatedAt FROM Notes WHERE Title LIKE @Keyword OR Content LIKE @Keyword";
            return _connection.Query<NoteModel>(sql, new { Keyword = $"%{keyword}%" }).ToList();
        }

        public async Task CreateTableIfNotExistsAsync()
        {
            var sql = @"
            CREATE TABLE IF NOT EXISTS Notes (
                Id INT PRIMARY KEY AUTO_INCREMENT,
                Title VARCHAR(255) NOT NULL,
                Content TEXT NOT NULL,
                CreatedAt DATETIME NOT NULL DEFAULT UTC_TIMESTAMP()
            )";

            try
            {
                await _connection.ExecuteAsync(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the Notes table.", ex);
            }
        }

    }
}
