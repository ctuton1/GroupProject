using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Data
{
    public class ProfilePasswordForm
    {
        //retrieve current pass from db
        private string currentPass = "";
        public string? newPassword { get; set; }
        public string? retypedPassword { get; set; }

        public string? oldPassword { get; set; }
    }
}
