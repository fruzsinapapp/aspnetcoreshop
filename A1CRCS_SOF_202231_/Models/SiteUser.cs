using Microsoft.AspNetCore.Identity;

namespace A1CRCS_SOF_202231_.Models
{
    public class SiteUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string PictureContentType { get; set; }
        public byte[] PictureData { get; set; }
        
    }
}
