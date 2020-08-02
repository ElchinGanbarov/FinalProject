using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository.Repositories.SignalRepository;

namespace Messenger.Controllers
{
    public class NotificationController : Controller
    {
        private Repository.Models.Account _user => RouteData.Values["User"] as Repository.Models.Account;
        private INotificationRepository _notificationRepository;

        public NotificationController(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        [HttpGet]
        public IActionResult GetUserNotifications(int userId)
        {
            var notification = _notificationRepository.GetUserNotifications(userId);

            //return Ok(notification.Count);
            return Ok(new { userNotifications = notification, count = notification.Count });
        }

        //public IActionResult ReadNotification(int notificationId)
        //{

        //    _notificationRepository.ReadNotification(notificationId, _userManager.GetUserId(HttpContext.User));

        //    return Ok();
        //}

        //testing

        [HttpGet]
        public IActionResult testing()
        {
            //Account account = _friendsRepository.GetFriendById(9025);
            //return Ok(account);

            //return Ok(_accountDetailRepository.GetDatasPublic(3024, 9025));

            return Ok(_notificationRepository.GetUserNotifications(9025));
        }
    }
}