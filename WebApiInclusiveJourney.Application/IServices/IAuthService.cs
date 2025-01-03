﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiInclusiveJourney.Application.DTO.Request;
using WebApiInclusiveJourney.Application.DTO.Response;
using WebApiInclusiveJourney.Repository.Models;

namespace WebApiInclusiveJourney.Application.IServices
{
    public interface IAuthService
    {
        AuthResponse Authentication(AuthRequest request);
        string GerarTokenJwt(TabPerson usuario);
        bool ForgotPassword(ForgotPasswordRequest request);
    }
}
