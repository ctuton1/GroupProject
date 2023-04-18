using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Data
{
    public class ProfileBioForm
    {
        [StringLength(255)]
        public string? profileBio { get; set; }
    }
}
