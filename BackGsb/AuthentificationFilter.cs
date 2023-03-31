using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Net.Http;


namespace BackGsb
{
    public class ProcedureAuthentification : ActionFilterAttribute
    {
        //définition d'une variable qui stocke le token utilisé pour stocker les api
        private string ApiKeyToCheck = "azerty";

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            bool validkey = false;

            IEnumerable<string> requestHeader;

            var checkApiExist = actionContext.Request.Headers.TryGetValues("token", out requestHeader);

            // c'est bon, le token récupéré depuis le header est authentique
            if (checkApiExist == true)
            {
                if (requestHeader.FirstOrDefault().Equals(ApiKeyToCheck))
                {
                    validkey = true;
                }
            }

            //si c'est pas bon, le token n'est pas authentique et on affiche un message accès non autorisé
            if (!validkey)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }
    }
}