using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Twilio;
using System.Diagnostics;

namespace MVCSocialAuth.Models
{
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Twilio Begin
            var Twilio = new TwilioRestClient(
              System.Configuration.ConfigurationManager.AppSettings["SMSAccountIdentification"],
              System.Configuration.ConfigurationManager.AppSettings["SMSAccountPassword"]);
            var result = Twilio.SendMessage(
              System.Configuration.ConfigurationManager.AppSettings["SMSAccountFrom"],
              message.Destination, message.Body
            );
            // Status is one of Queued, Sending, Sent, Failed or null if the number is not valid
             Trace.TraceInformation(result.Status);
            // Twilio doesn't currently have an async API, so return success.
             return Task.FromResult(0);
            // Twilio End
        }
    }
}