using A1CRCS_SOF_202231_.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A1CRCS_SOF_202231_.Models
{
    [ModelBinder(BinderType = typeof(CommentModelBinder))]
    public class Comment
    {
        [Key]
        public string Uid { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string ItemId { get; set; }
        [NotMapped]
        public virtual Item Item { get; set; } //lazy loading

        public string OwnerId { get; set; }
        [NotMapped]
        public virtual SiteUser Owner { get; set; } //lazy loading
        public Comment()
        {
            Uid = Guid.NewGuid().ToString();
        }
    }
}
