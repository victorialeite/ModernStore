using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace ModernStore.Api.Security
{
    public class TokenOptions
    {
        public string Issuer { get; set; } // quem está pedindo o Token
        public string Subject { get; set; } // assunto, palavra-chave definida para o Token
        public string Audience { get; set; } // quem tá recebendo o Token
        public DateTime NotBefore { get; set; } = DateTime.UtcNow; // não pode pedir Token fora da validade
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow; // data que o Token foi pedido
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromDays(2); // até quando o Token é válido
        public DateTime Expiration => IssuedAt.Add(ValidFor);

        public Func<Task<string>> JtiGenerator =>
            () => Task.FromResult(Guid.NewGuid().ToString());

        public SigningCredentials SigningCredentials { get; set; }
    }
}
