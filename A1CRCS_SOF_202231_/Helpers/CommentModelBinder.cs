using A1CRCS_SOF_202231_.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace A1CRCS_SOF_202231_.Helpers
{
    public class CommentModelBinder:IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            Comment comment = new Comment();
            comment.Uid = bindingContext.ValueProvider.GetValue("Uid").FirstValue;
            comment.Content = bindingContext.ValueProvider.GetValue("commentString").FirstValue;
            comment.Date = DateTime.Now;

            bindingContext.Result = ModelBindingResult.Success(comment);
            return Task.CompletedTask;
        }
    }
}
