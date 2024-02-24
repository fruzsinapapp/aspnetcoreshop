using A1CRCS_SOF_202231_.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace A1CRCS_SOF_202231_.Helpers
{
    public class ItemModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            Item item = new Item();
            item.Uid = bindingContext.ValueProvider.GetValue("Uid").FirstValue;
            item.Title = bindingContext.ValueProvider.GetValue("Title").FirstValue;
            item.Description = bindingContext.ValueProvider.GetValue("Description").FirstValue;
            item.Price = int.Parse(bindingContext.ValueProvider.GetValue("Price").FirstValue);
            item.Category = (ItemCategory)int.Parse(bindingContext.ValueProvider.GetValue("category-select").FirstValue);
            item.Reserved = false;
            item.Date = DateTime.Now;

            if (bindingContext.HttpContext.Request.Form.Files.Count > 0)
            {
                var file = bindingContext.HttpContext.Request.Form.Files[0];
                using (var stream = file.OpenReadStream())
                {
                    byte[] buffer = new byte[file.Length];
                    stream.Read(buffer, 0, (int)file.Length);
                    string filename = item.Uid + "." + file.FileName.Split('.')[1];
                    item.ImageFileName = filename;

                    item.PictureData = buffer;
                    item.PictureContentType = file.ContentType;
                }
            }

            bindingContext.Result = ModelBindingResult.Success(item);
            return Task.CompletedTask;
        }
    }
}
