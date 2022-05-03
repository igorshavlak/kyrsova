using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace gergergergerg.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(email,subject,htmlMessage);
        }
        public async Task Execute(string email, string subject, string body)
        {
            MailjetClient client = new MailjetClient("927886b5701371c305a3eed25d65cf63", "f1e9cc5f210f71965d13adbedb4cdce2")
            {
                Version = ApiVersion.V3_1,
            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
             .Property(Send.Messages, new JArray {
     new JObject {
      {
       "From",
       new JObject {
        {"Email", "dotnetuseremail@protonmail.com"},
        {"Name", "igor"}
       }    
      }, {
       "To",
       new JArray {
        new JObject {
         {
          "Email",
          email
         }, {
          "Name",
          "DotNetMastery"
         }
        }
       }
      }, {
       "Subject",
       subject
      }, {
       "HTMLPart",
       body
      }
     }
             });
           await client.PostAsync(request);
        }
    }
}
