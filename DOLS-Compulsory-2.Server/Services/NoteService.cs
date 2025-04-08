using dols_compulsory_2.Server.Models;

namespace dols_compulsory_2.Server.Services
{
    public class NoteService
    {
        private readonly List<NoteModel> _notes = new();
        private int _nextId = 1;

        public List<NoteModel> GetAll()
        {
            return _notes;
        }

        public NoteModel? GetById(int id)
        {
            return _notes.FirstOrDefault(n => n.Id == id);
        }

        public NoteModel Create(NoteDTO dto)
        {
            var newNote = new NoteModel
            {
                Id = _nextId++,
                Title = dto.Title,
                Content = dto.Content
            };

            _notes.Add(newNote);
            return newNote;
        }

        public List<NoteModel> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return _notes;

            keyword = keyword.ToLower();

            return _notes
                .Where(n =>
                    n.Title.ToLower().Contains(keyword) ||
                    n.Content.ToLower().Contains(keyword))
                .ToList();
        }
    }
}