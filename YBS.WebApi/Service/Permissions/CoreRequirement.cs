using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Model.Enums;

namespace Service.Permissions
{
    public class CoreRequirement : IAuthorizationRequirement
    {
        public LevelEnum UserLevel { get; set; }

        public CoreRequirement(LevelEnum userLevel)
        {
            UserLevel = userLevel;
        }
    }
}
