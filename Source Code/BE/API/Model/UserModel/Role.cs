namespace API.Model.UserModel
{
    public enum  RoleEnum
    {
        Admin = 1,
        Manager = 2,
        DesignStaff = 3,
        ProductStaff = 4,
        Sale = 5,
    }

    public static class RoleConst
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string DesignStaff = "DesignStaff";
        public const string ProductStaff = "ProductStaff";
        public const string Sale = "Sale";
        public const string Customer = "Customer";
    }
}
