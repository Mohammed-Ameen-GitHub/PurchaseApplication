using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using PurchaseApplication.Models;
using System.Web.Http;

namespace PurchaseApplication.Provider
{
    [Route("token")]
    public class OauthProvider : OAuthAuthorizationServerProvider
    {
       
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated(); 
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
          
            using (var db = new PurchaseEntities())
            {

                if (db != null)
                {
                    var user = db.Users.Where(o => o.UserName == context.UserName && o.Password == context.Password).FirstOrDefault();
                    if (user != null)
                    {
                         
                        identity.AddClaim(new Claim("UserName", context.UserName));
                        identity.AddClaim(new Claim("LoggedOn", DateTime.Now.ToString()));
                        context.Validated(identity);
                    }
                    else
                    {
                        context.SetError("Wrong Crendtials", "Provided username and password is incorrect");
                        context.Rejected();

                    }
                }
                else
                {
                    context.SetError("Wrong Crendtials", "Provided username and password is incorrect");
                    context.Rejected();
                }
                return;

            }
        }
    }
}