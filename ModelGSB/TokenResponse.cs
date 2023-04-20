using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;

namespace ModelGSB
{
    public class TokenResponse
    {
        public TokenResponse() 
        {
            this.Token = string.Empty;
            this.responseMsg = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.Unauthorized };
        }


        public string Token { get; set; }
        public HttpResponseMessage responseMsg { get; set; }
    }
}