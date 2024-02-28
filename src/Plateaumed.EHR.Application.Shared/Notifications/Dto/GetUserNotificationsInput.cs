using System;
using Abp.Notifications;
using Plateaumed.EHR.Dto;

namespace Plateaumed.EHR.Notifications.Dto
{
    public class GetUserNotificationsInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}