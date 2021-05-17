namespace DesktopUI.DataClasses
{
    public enum MailType
    {
        PRIVATE  = 0,
        BUSINESS = 1
    }

    public class Mail
    {
        public string MailAddress { get; set; }
        public MailType MailType { get; set; }

        public static string GetMailTypeString(MailType type)
        {
            switch (type)
            {
                case MailType.PRIVATE:
                    return Resources.MailPrivate;
                case MailType.BUSINESS:
                    return Resources.MailBusiness;
                default:
                    return "";
            }
        }
    }
}
