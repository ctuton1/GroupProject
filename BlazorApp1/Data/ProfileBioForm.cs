using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Data
{
    public class ProfileBioForm
    {
        [Required]
        [StringLength(255)]
        public string? profileBio { get; set; }
    }
}
