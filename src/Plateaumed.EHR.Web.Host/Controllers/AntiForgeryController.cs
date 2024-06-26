﻿using Microsoft.AspNetCore.Antiforgery;

namespace Plateaumed.EHR.Web.Controllers
{
    public class AntiForgeryController : EHRControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
