using System.Collections.Generic;
using System.Threading.Tasks;
using BackendNotas.Models;
using MongoDB.Driver;
using MongoDB.Bson;


namespace BackendNotas.Services
{
    public class NoteService
    {
        private readonly IMongoCollection<Note> _notes;

        public NoteService(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _notes = database.GetCollection<Note>("notes");
        }



        public async Task<List<Note>> GetNotesAsync()
        {
            var projection = Builders<Note>.Projection
                .Include(n => n.id)
                .Include(n => n.title)
                .Include(n => n.content)
                .Include(n => n.createdAt);

            return await _notes.Find(note => true)
                                .Project<Note>(projection)
                                .ToListAsync();
        }

        public async Task CreateNoteAsync(Note note)
        {
            await _notes.InsertOneAsync(note);
        }

        public async Task DeleteNoteAsync(string id)
        {
            var objectId = ObjectId.Parse(id);
            await _notes.DeleteOneAsync(note => note.id.Equals(objectId));
        }



    }
}

namespace BackendNotas.Models
{
    public interface IMongoDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string NotesCollectionName { get; set; }
    }

    public class MongoDatabaseSettings : IMongoDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string NotesCollectionName { get; set; }
    }
}

