﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernStore.Domain.Services;

namespace ModernStore.Infra.Services
{
    public class EmailService : IEmailService
    {
        public void Send(string name, string toEmail, string subject, string body)
        {
            //Implementar o System.Net.Mail
        }
    }
}
