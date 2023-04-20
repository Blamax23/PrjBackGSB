using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModelGSB
{
    // requete du token, j'envoie username et password
    public class TokenRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}