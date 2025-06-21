namespace OrderManagementApp.Exceptions
{
    public class IdFormatException : Exception
    {
        public IdFormatException(string ErrorMessage) : base(ErrorMessage) { }
    }
}
