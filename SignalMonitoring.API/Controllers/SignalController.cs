﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalMonitoring.API.Hubs;
using SignalMonitoring.API.Models;
using SignalMonitoring.API.Services;

namespace SignalMonitoring.API.Controllers
{
    [Route("api/v1/signals")]
    [ApiController]
    public class SignalController : ControllerBase
    {
        private readonly ISignalService _signalService;
        private readonly IHubContext<SignalHub> _hubContext;
        public SignalController(ISignalService signalService, IHubContext<SignalHub> hubContext)
        {
            _signalService = signalService;
            _hubContext = hubContext;
        }

        [HttpPost]
        [Route("deliverypoint")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<IActionResult> SignalArrived(SignalInputModel inputModel) {

            //you can validate input here
            //then if the inputModel is valid then you can save the signal
            var saveResult = await _signalService.SaveSignalAsync(inputModel);
            if(saveResult)
            {

                SignalViewModel signalViewModel = new SignalViewModel {
                    Description = inputModel.Description,
                    CustomerName = inputModel.CustomerName,
                    Area = inputModel.Area,
                    Zone = inputModel.Zone,
                    SignalStamp = Guid.NewGuid().ToString(),
                };

                //notify all clients by using SignalHub
                await _hubContext.Clients.All.SendAsync("SignalMessageReceived", signalViewModel);
            }

            return StatusCode(200, saveResult);
        }
    }
}