using A1CRCS_SOF_202231_.Models;

namespace A1CRCS_SOF_202231_.Data
{
    public interface IItemRepository
    {
        void Create(Item item);
        void Delete(string id);
        IEnumerable<Item> Read();
        IEnumerable<Item> OwnItems(string id);
        Item? ReadFromId(string id);
        void Update(Item item);
        void Reserve(string id, string userId);
        void UnReserve(string id);
        IEnumerable<Item> Search(ItemCategory category, int price, bool res);
        IEnumerable<Comment> GetComments(string id);
    }
}
