using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Senit.Abstractions.Messaging;
using Senit.Messaging.Messages;
using RawRabbit.Exceptions;

namespace Senit.Modules.User.Controllers
{
    [Route("")]
    public class RegistrationController : Controller
    {
        private readonly BusClientWrapper _busClient;

        public RegistrationController(BusClientWrapper busClient)
        {
            _busClient = busClient;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var data = await _busClient.Request<TestRequestMessage, TestResponseMessage>(new TestRequestMessage
                {
                    Input = "in"
                });

                return Ok(data);
            }
            catch (MessageHandlerException ex)
            {

                var failureReason = ex.Message;

                if (!string.IsNullOrEmpty(ex.InnerMessage))
                {
                    failureReason = ex.InnerMessage;
                }

                return BadRequest(new { error = $"RPC failed. Reason: {failureReason}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
