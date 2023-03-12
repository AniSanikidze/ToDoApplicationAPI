using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Application.CustomExceptions
{
    public class BadRequestWhenUpdatingWithPatch : Exception
    {
        public string DomainClassName { get; private set; }
        public BadRequestWhenUpdatingWithPatch(string? message, string domainClassName) : base(message)
        {
            DomainClassName = domainClassName;
        }
    }
}
