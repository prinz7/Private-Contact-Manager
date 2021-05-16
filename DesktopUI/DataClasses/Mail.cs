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
    }
}
