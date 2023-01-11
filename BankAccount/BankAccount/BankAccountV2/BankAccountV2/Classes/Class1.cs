using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountV2.Classes
{
    internal class AccountCultureInfo : System.Globalization.CultureInfo
    {
        public AccountCultureInfo(int culture) : base(culture)
        {
        }

        public AccountCultureInfo(int culture, bool useUserOverride) : base(culture, useUserOverride)
        {
        }

        public AccountCultureInfo(string name) : base(name)
        {
        }

        public AccountCultureInfo(string name, bool useUserOverride) : base(name, useUserOverride)
        {
        }
    }
}
