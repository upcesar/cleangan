using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cleangap.api.Services.HttpClient
{
    interface IApiCommand
    {
        bool IsSucess { get; }
        Task<string> ExecutePost(string action, object param);
        Task<string> ExecuteGet(string action);
    }
}
