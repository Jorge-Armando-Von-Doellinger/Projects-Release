namespace HMS.ContractsMicroService.Core.Extensions
{
    public static class EnumExtension
    {
        public static string EnumFromString(this Enum value)
        {
            return value.ToString();
        }

        //public
    }
}
