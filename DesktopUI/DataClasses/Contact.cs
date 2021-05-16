using System.Collections.Generic;
using System.Linq;

namespace DesktopUI.DataClasses
{
    public class Contact
    {
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Organization { get; set; }
        public int BirthdayDay { get; set; }
        public int BirthdayMonth { get; set; }
        public int BirthdayYear { get; set; }
        public bool IsFirstNameBasis { get; set; }
        public List<Mail> MailAddesses { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; }
        public string Comment { get; set; }


        public string FirstCharacter { get { return Forename.FirstOrDefault().ToString().ToUpper(); } }
        public string FullName { get { return $"{Forename} {Surname}"; } }
    }
}
