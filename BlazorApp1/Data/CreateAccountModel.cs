namespace BlazorApp1.Data
{
    public class CreateAccountModel
    {
        public string userName { get; set; }
        public string password { get; set; }

        public string retypedPassword { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public AccountTypes accountType { get; set; }


        public enum AccountTypes
        {
            Student,
            Society
        }
    }
}
