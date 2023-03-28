namespace BlazorApp1.Data
{
    public class ProfileUserNameForm
    {
        public string? UserName { get; set; }

        //get pass from database
        private string savedPass = "";
        public string? Password { get; set; }
    }
}
