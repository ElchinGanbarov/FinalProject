using AutoMapper;
using Messenger.Models.AccountDetail;
using Messenger.Models.Email;
using Messenger.Models.General;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repositories.AuthRepositories;
using Repository.Services;

namespace Messenger.Controllers
{
    public class NotificationController : Controller
    {
        private Repository.Models.Account _user => RouteData.Values["User"] as Repository.Models.Account;
        private IAuthRepository _authRepository;
        //private INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        private ISendEmail _sendEmailRepository;

        public NotificationController(/*INotificationRepository notificationRepository,*/
                                      IMapper mapper,
                                      IAuthRepository authRepository,
                                      ISendEmail sendEmail)
        {
            _authRepository = authRepository;
            //_notificationRepository = notificationRepository;
            _mapper = mapper;
            _sendEmailRepository = sendEmail;
        }

        [HttpPost]
        public IActionResult SendInvitationEmail(InvitationEmailViewModel invitationEmailViewModel)
        {
            if (ModelState.IsValid)
            {
                string link = HttpContext.Request.Scheme + "://" + Request.Host;

                _sendEmailRepository.InvitationEmail(invitationEmailViewModel.ReceiverEmail, invitationEmailViewModel.Text, invitationEmailViewModel.SenderFullname, link);
                return View("Views/Pages/Chat1.cshtml", new GeneralViewModel
                {
                    InvitationEmailViewModel = invitationEmailViewModel,
                    AccountDetailViewModel = _mapper.Map<Account, AccountDetailViewModel>(_authRepository.CheckByToken(_user.Token))
                });
            }
            else
            {
                return View("Views/Pages/Chat1.cshtml", new GeneralViewModel
                {
                    InvitationEmailViewModel = invitationEmailViewModel,
                    AccountDetailViewModel = _mapper.Map<Account, AccountDetailViewModel>(_authRepository.CheckByToken(_user.Token))
                });
            }
        }

        [HttpGet]
        public IActionResult GetUserNotifications(int userId)
        {
            //var notification = _notificationRepository.GetUserNotifications(userId);

            ////return Ok(notification.Count);
            //return Ok(new { userNotifications = notification, count = notification.Count });
            return Ok();
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

            return Ok();

            //return Ok(_notificationRepository.GetUserNotifications(9025));
        }
    }
}