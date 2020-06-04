using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalMonitoring.API.Models;
using SignalMonitoring.API.Persistence;

namespace SignalMonitoring.API.Services
{
    public class SignalService : ISignalService
    {
        private readonly MainDbContext _mainDbContext;
        public SignalService(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<bool> SaveSignalAsync(SignalInputModel inputModel)
        {
            try
            {
                SignalDataModel signalModel = new SignalDataModel {
                    CustomerName = inputModel.CustomerName,
                    Description = inputModel.Description,
                    AccessCode = inputModel.AccessCode,
                    Area = inputModel.Area,
                    Zone = inputModel.Zone,
                    SingalDate = DateTime.Now
                };

                // execute some business rules according to your cases

                _mainDbContext.Signals.Add(signalModel);

                return await _mainDbContext.SaveChangesAsync() > 0;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
