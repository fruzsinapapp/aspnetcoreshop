using A1CRCS_SOF_202231_.Models;

namespace A1CRCS_SOF_202231_.Data
{
    public class ItemRepository : IItemRepository
    {
        ApplicationDbContext context;
        public ItemRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(Item item)
        {
            context.Items.Add(item);
            context.SaveChanges();
        }

        public void Delete(string id)
        {
            var item = ReadFromId(id);
            context.Items.Remove(item);
            context.SaveChanges();
        }
        public void UnReserve(string id)
        {
            var item = ReadFromId(id);
            item.Reserved = false;
            item.ReservedBy = "";
            context.SaveChanges();
        }
        public void Reserve(string id, string userId)
        {
            var item = ReadFromId(id);
            item.Reserved = true;
            item.ReservedBy = userId;
            context.SaveChanges();
        }
        public IEnumerable<Item> Read()
        {
            return context.Items;
        }

        public Item? ReadFromId(string id)
        {
            return context.Items.FirstOrDefault(i => i.Uid == id);
        }

        public IEnumerable<Item> OwnItems(string id)
        {
            return context.Items.Where(i => i.OwnerId == id);
        }

        public void Update(Item item)
        {
            var old = ReadFromId(item.Uid);
            old.Title = item.Title;
            old.Price = item.Price;
            old.Reserved = item.Reserved;
            old.Owner = item.Owner;
            old.Category = item.Category;
            old.Date = item.Date;
            old.Description = item.Description;
            old.SequenceNum= item.SequenceNum;

            context.SaveChanges(true);
        }

        public IEnumerable<Item> Search(ItemCategory category, int price, bool res)
        {
            var valami = context.Items.Where(i => i.Category == category && i.Price < price && i.Reserved == res);
            return valami;
        }

        public IEnumerable<Comment> GetComments(string itemId)
        {
            var valami = context.Comments.Where(i => i.ItemId == itemId);
            return valami;
        }
    }
}
