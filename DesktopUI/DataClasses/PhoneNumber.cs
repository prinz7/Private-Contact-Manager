namespace DesktopUI.DataClasses
{
    public enum PhoneNumberType
    {
        PRIVATE         = 0,
        PRIVATE_MOBILE  = 1,
        BUSINESS        = 2,
        BUSINESS_MOBILE = 3
    }

    public class PhoneNumber
    {
        public string Number { get; set; }
        public PhoneNumberType NumberType { get; set; }
    }
}
