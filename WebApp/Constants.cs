namespace WebApp
{
    public static class RoleNames
    {
        public const string Admin = "Admin";
        public const string Employee = "Employee";
        public const string Customer = "Customer";

        public const string CreateHousing = "CreateHousing";
        public const string EditHousing = "EditHousing";
        public const string DeleteHousing = "DeleteHousing";

        public const string CreateCustomer = "CreateCustomer";
        public const string EditCustomer = "EditCustomer";
        public const string DeleteCustomer = "DeleteCustomer";

        public const string ManageUser = "ManageUser";

        public static string[] AllRoles = new[]
        {
            Admin, Employee, Customer, 
            CreateHousing, EditHousing, DeleteHousing,
            CreateHousing, EditCustomer, DeleteCustomer
        };
    }
}
