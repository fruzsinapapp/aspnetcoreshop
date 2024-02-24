using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using A1CRCS_SOF_202231_.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;

namespace A1CRCS_SOF_202231_.Models
{
    public enum ItemCategory
    {
        electronics, clothes, housing, sport, book, game
    }
    [ModelBinder(BinderType = typeof(ItemModelBinder))]
    public class Item
    {
        static Random r = new Random();

        [Key]
        public string Uid { get; set; }
        [Required(ErrorMessage = "The name of the product can not be empty")]
        public string Title { get; set; }
        [Required(ErrorMessage = "The description can not be empty")]
        public string Description { get; set; }
        [Required]
        [Range(200, int.MaxValue, ErrorMessage = "Price must be at least 200")]
        public int Price { get; set; }
        public bool Reserved { get; set; }
        public string? ReservedBy { get; set; }
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "The category can not be empty")]
        public ItemCategory Category { get; set; }

        public string? OwnerId { get; set; }
        [NotMapped]
        public virtual SiteUser? Owner { get; set; }
        
        [StringLength(200)]
        public string? ImageFileName { get; set; }
        public string? PictureContentType { get; set; }
        public byte[]? PictureData { get; set; }

        [NotMapped]
        public IFormFile? UploadedFile { get; set; }

        [NotMapped]
        public virtual ICollection<Comment>? Comments { get; set; }
        
        public int? SequenceNum { get; set; }
        public Item()
        {
            Uid = Guid.NewGuid().ToString();
            SequenceNum = r.Next(2, 10000);
        }
    }
}
