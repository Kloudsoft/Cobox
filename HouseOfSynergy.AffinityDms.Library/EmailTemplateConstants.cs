using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.Library
{
    public static class EmailTemplateConstants
    {
        public const string InviteExternalUserByEmail = "<p>Hi <strong>[{RecieverEmail}]</strong>,</p><p>[{SenderUsername}] invited you to a [{SenderCompanyName}] shared&nbsp;[{EnitityType}] called \"[{sharedEntityName}]\". Please click below&nbsp;or copy-paste the link in your browser and login with the following credentials:</p><p><strong><a title='Click Invite Url' href='[{LinkForInvite}]' target='_blank'>[{LinkForInvite}]</a></strong></p><p><strong>Email:</strong></p><p>[{RecieverEmail}]</p><p><strong>Password:</strong></p><p>[{RecieverPassword}]</p>";
        public const string GreetingsMessage = "<strong>Hi [{RecieverName}],</strong> <br>You have successfully created <a href='[{URL}]'><strong>[{RecieverCompanyName}]</strong></a> Account, we are so glad you decided to try out our services.";
        public const string GreetingsSubject = "[{RecieverName}], welcome to your new [{RecieverCompanyName}] Account";

    }
}
