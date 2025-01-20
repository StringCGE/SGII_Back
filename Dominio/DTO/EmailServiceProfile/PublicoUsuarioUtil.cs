using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGII_Back.Dominio
{

    public class RecoverPasswordRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }
    public class EmailConfirmationRequest
    {
        public string Email { get; set; }
    }

    public class EmailConfirmationVerificationRequest
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
