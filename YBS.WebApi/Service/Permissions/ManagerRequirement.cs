using System;
using Microsoft.AspNetCore.Authorization;
using Model.Enums;

namespace Service.Permissions
{
    public class ManagerRequirement : IAuthorizationRequirement
    {
        public LevelEnum UserLevel { get; set; }

        public ManagerRequirement(LevelEnum userLevel)
        {
            UserLevel = userLevel;
        }
    }
}
