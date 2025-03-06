using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cirkla_DAL.Models.Enums
{
    public enum CircleJoinRequestType
    {
        UserRequestToJoin,
        UserRequestToBecomeAdmin,
        MemberInviteToUser,
        MembershipInviteFromAdmin,
        AdminInviteFromAdmin
    }
}