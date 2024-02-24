using A1CRCS_SOF_202231_.Models;

namespace A1CRCS_SOF_202231_.Data
{
    public class CommentRepository:ICommentRepository
    {
        ApplicationDbContext context;
        public CommentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(Comment comment)
        {
            context.Comments.Add(comment);
            context.SaveChanges();
        }
        public void Delete(string id)
        {
            var comment = ReadFromId(id);
            context.Comments.Remove(comment);
            context.SaveChanges();
        }
        public IEnumerable<Comment> Read()
        {
            return context.Comments;
        }
        public Comment? ReadFromId(string id)
        {
            return context.Comments.FirstOrDefault(i => i.Uid == id);
        }
    }
}
