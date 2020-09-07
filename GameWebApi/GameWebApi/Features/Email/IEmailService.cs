using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Features.Email
{
    public interface IEmailService
    {
        bool SendEmailToUser(string userEmail, string message);
    }
}
