﻿using BigMarket.Services.AuthAPI.Models;

namespace BigMarket.Services.AuthAPI.Services.IServices
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}