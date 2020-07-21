using AutoMapper;
using Messenger.Filters;
using Messenger.Lib;
using Messenger.Models.AccountDetail;
using Messenger.Models.AccountPrivacySecurity;
using Messenger.Models.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repositories.AccountRepository;
using Repository.Repositories.AuthRepositories;
using Repository.Services;
using System.Collections.Generic;

namespace Messenger.Controllers
{
    [TypeFilter(typeof(Auth))]
    public class AccountDetailController : Controller
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        private readonly IAccountDetailRepository _accountDetailRepository;
        private readonly IFriendsRepository _friendsRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IFileManager _fileManager;

        private Repository.Models.Account _user => RouteData.Values["User"] as Repository.Models.Account;

        public AccountDetailController(IAuthRepository authRepository,
                                       IMapper mapper,
                                       IAccountDetailRepository accountDetailRepository,
                                       IFriendsRepository friendsRepository,
                                       ICloudinaryService cloudinaryService,
                                       IFileManager fileManager)
        {
            _authRepository = authRepository;
            _mapper = mapper;
            _accountDetailRepository = accountDetailRepository;
            _friendsRepository = friendsRepository;
            _cloudinaryService = cloudinaryService;
            _fileManager = fileManager;
        }
        [HttpPost]
        public IActionResult Update(AccountDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<AccountDetailViewModel, Account>(model);
                
                var updateuser = _authRepository.CheckByToken(_user.Token);
              
                    if (updateuser.Name != user.Name ||
                        updateuser.Surname != user.Surname ||
                        updateuser.Address != user.Address ||
                        updateuser.Birthday != user.Birthday ||
                        updateuser.Email != user.Email ||
                        updateuser.Website != user.Website ||
                        updateuser.Phone != user.Phone)
                    {
                        _authRepository.UpdateAccount(updateuser, user);
                        return Ok(user);
                    }
            }
            return View("Views/Pages/Chat1.cshtml", new GeneralViewModel
            {
                AccountDetailViewModel = model
            });
        }

        [HttpPost]
        public IActionResult UpdateSocialLink(AccountSocialLinkViewModel model)
        {
            var updateuser = _authRepository.CheckByToken(_user.Token);
            
                if (ModelState.IsValid)
                {
                 var socialLink = _mapper.Map<AccountSocialLinkViewModel, AccountSocialLink>(model);
                socialLink.AccountId = _user.Id;
                    var updatesociallink = _accountDetailRepository.GetByAccountId(_user.Id);

                
                    if (updatesociallink.Facebook != socialLink.Facebook ||
                     updatesociallink.Twitter != socialLink.Twitter||
                     updatesociallink.Instagram != socialLink.Instagram ||
                     updatesociallink.Linkedin != socialLink.Linkedin)
                    {
                        _accountDetailRepository.UpdateSocialLink(updatesociallink, socialLink);
                        return Ok(socialLink);
                    }
                }
         

            return View("Views/Pages/Chat1.cshtml", new GeneralViewModel
            {
                AccountSocialLinkViewModel = model,
                AccountDetailViewModel = _mapper.Map<Account, AccountDetailViewModel>(updateuser)
            });
        }

        [HttpPost]
        public IActionResult ProfileImgUpload(IFormFile file)
        {
        
            var filename = _fileManager.Upload(file);
            
            var publicId = _cloudinaryService.Store(filename);
            _fileManager.Delete(filename);
            if(_user.ProfileImg != null)
            {
                _cloudinaryService.Delete(_user.ProfileImg);

            }
            _accountDetailRepository.ProfileImgUpload(_user.Id, publicId);
        


            return Ok(new
            {
                filename = publicId,
                src = _cloudinaryService.BuildUrl(publicId)
            });
        }

        [HttpPost]
        public IActionResult RemovePhoto()
        {
        
            if (_user.ProfileImg != null)
            {
                _cloudinaryService.Delete(_user.ProfileImg);
                _accountDetailRepository.DeletePhoto(_user.ProfileImg);
              
            }
           

            return Ok(new { status = 200 });
        }
        [HttpPost]
        public IActionResult RemoveELcinPhoto()
        {

            if (_user.ProfileImg != null)
            {
                _cloudinaryService.Delete(_user.ProfileImg);
                _accountDetailRepository.DeletePhoto(_user.ProfileImg);

            }


            return Ok(new { status = 200 });
        }

        //Privacy & Security Start
        #region Privacy & Security

        [TypeFilter(typeof(Auth))]
        [HttpPost]
        public IActionResult UpdatePrivacy(AccountPrivacyViewModel model)
        {
            if (ModelState.IsValid)
            {
                Account account = _authRepository.CheckByToken(_user.Token);
                if (account != null)
                {
                    _accountDetailRepository.UpdatePrivacy(account.Id,
                                                           model.ProfileImg,
                                                           model.StatusText,
                                                           model.LastSeen,
                                                           model.Phone,
                                                           model.Birthday,
                                                           model.Address,
                                                           model.SocialLink,
                                                           model.AcceptAllMessages);
                    return Ok(new { status = true, message = "Account Privacy Successfully Updated!" });
                }
            }

            return Ok(new { status = false, message = "Unexpected Error Ocoured! Update account privacy Failed!" });
        }

        [TypeFilter(typeof(Auth))]
        [HttpPost]
        public IActionResult UpdateSecurity(AccountSecurityViewModel model)
        {
            if (ModelState.IsValid)
            {
                Account account = _authRepository.CheckByToken(_user.Token);
                if (account != null)
                {
                    _accountDetailRepository.UpdateSecurity(account.Id, model.TwoFactoryAuth, model.LoginAlerts);
                    return Ok(new { status = true, message = "Account Security Successfully Updated!" });
                }
            }

            return Ok(new { status = false, message = "Unexpected Error Ocoured! Update account security Failed!" });
        }
        #endregion
        //Privacy & Security End
    }
}