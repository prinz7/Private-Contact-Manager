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

        public static string GetPhoneNumberTypeString(PhoneNumberType type)
        {
            switch (type)
            {
                case PhoneNumberType.PRIVATE:
                    return Resources.PhonePrivate;
                case PhoneNumberType.PRIVATE_MOBILE:
                    return Resources.PhonePrivateMobile;
                case PhoneNumberType.BUSINESS:
                    return Resources.PhoneBusiness;
                case PhoneNumberType.BUSINESS_MOBILE:
                    return Resources.PhoneBusinessMobile;
                default:
                    return "";
            }
        }
    }
}
