using A1CRCS_SOF_202231_.Models;

namespace A1CRCS_SOF_202231_.Data
{
    public interface ICommentRepository
    {
        void Create(Comment comment);

        void Delete(string id);

        public IEnumerable<Comment> Read();

        public Comment? ReadFromId(string id);
    }
}
