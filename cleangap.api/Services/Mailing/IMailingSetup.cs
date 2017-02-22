using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cleangap.api.Services.Mailing
{
    interface IMailingSetup
    {
        string Host { get; }
        int Port { get; }

        bool DefaultCredencials { get; }

        string DisplayName { get; }

        string UserName { get; }

        string Password { get; }
    }
}
