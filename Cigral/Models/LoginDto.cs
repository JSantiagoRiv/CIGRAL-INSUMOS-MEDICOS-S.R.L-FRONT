using System;
using System.Collections.Generic;
using System.Text;

namespace Cigral.Models
{
    internal class LoginDto
    {
    }

    //Solicitud
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    //Respuesta
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public string NombreCompleto { get; set; }
        public bool EsAdmin { get; set; }
        public DateTime Expiracion {  get; set; }
    }


}
