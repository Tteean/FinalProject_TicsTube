﻿using Microsoft.Extensions.Configuration;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;

namespace FinalProject_Service.Extentions
{
    public class PayPalClient
    {
        private readonly IConfiguration _config;

        public PayPalClient(IConfiguration config)
        {
            _config = config;
        }

        public PayPalEnvironment Environment => new SandboxEnvironment(
            _config["PayPal:ClientId"],
            _config["PayPal:Secret"]
        );

        public PayPalHttpClient Client => new PayPalHttpClient(Environment);
    }
}
