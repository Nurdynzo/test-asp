using Abp.Dependency;
using Plateaumed.EHR.Facilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.TwiML.Voice;

namespace Plateaumed.EHR.Facilities.Abstractions
{
    public interface IActivateOrDeactivateRoomCommandHandler : ITransientDependency
    {
        Task<Rooms> Handle(ActivateOrDeactivateRoom request);
    }
}
