using dols_compulsory_2.Server.Models;
using DOLS_Compulsory_2.Server.DAL;

namespace dols_compulsory_2.Server.Services
{
    public class NoteService
    {

        private readonly NoteDAL _noteDAL;

        public NoteService(NoteDAL noteDAL)
        {
            _noteDAL = noteDAL;
        }

        public List<NoteModel> GetAll()
        {
            return _noteDAL.GetAll();
        }

        public NoteModel GetById(int id)
        {
            return _noteDAL.GetById(id);
        }

        public NoteModel Create(NoteDTO dto)
        {
            return _noteDAL.Create(dto);
        }

        public List<NoteModel> Search(string keyword)
        {
            return _noteDAL.Search(keyword);
        }
    }
}