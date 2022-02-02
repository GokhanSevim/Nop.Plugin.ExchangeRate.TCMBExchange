using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.ExchangeRate.TCMBExchange.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.ExchangeRate.TCMBExchange.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    public class TCMBExchangeController : BasePluginController
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly TCMBExchangeSettings _tcmbExchagneSettings;
        private readonly IPermissionService _permissionService;
        private readonly INotificationService _notificationService;

        #endregion

        #region Ctor

        public TCMBExchangeController(ILocalizationService localizationService,
            ISettingService settingService,
            TCMBExchangeSettings tcmbExchangeSettings,
            IPermissionService permissionService,
            INotificationService notificationService)
        {
            _localizationService = localizationService;
            _settingService = settingService;
            _tcmbExchagneSettings = tcmbExchangeSettings;
            _permissionService = permissionService;
            _notificationService = notificationService;
        }

        #endregion

        #region Methods

        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCurrencies))
                return AccessDeniedView();

            var model = new ConfigurationModel
            {
                TCMBExchangeApi = _tcmbExchagneSettings.ApiKey,
                AdditionalFee = _tcmbExchagneSettings.AdditionalFee,
                IsUSD = _tcmbExchagneSettings.IsUSD,
                IsAUD = _tcmbExchagneSettings.IsAUD,
                IsDKK = _tcmbExchagneSettings.IsDKK,
                IsEUR = _tcmbExchagneSettings.IsEUR,
                IsGBP = _tcmbExchagneSettings.IsGBP,
                IsCHF = _tcmbExchagneSettings.IsCHF,
                IsSEK = _tcmbExchagneSettings.IsSEK,
                IsCAD = _tcmbExchagneSettings.IsCAD,
                IsKWD = _tcmbExchagneSettings.IsKWD,
                IsNOK = _tcmbExchagneSettings.IsNOK,
                IsSAR = _tcmbExchagneSettings.IsSAR,
                IsJPY = _tcmbExchagneSettings.IsJPY,
                IsBGN = _tcmbExchagneSettings.IsBGN,
                IsRON = _tcmbExchagneSettings.IsRON,
                IsRUB = _tcmbExchagneSettings.IsRUB,
                IsIRR = _tcmbExchagneSettings.IsIRR,
                IsCNY = _tcmbExchagneSettings.IsCNY,
                IsPKR = _tcmbExchagneSettings.IsPKR,
                IsQAR = _tcmbExchagneSettings.IsQAR,
                IsKRW = _tcmbExchagneSettings.IsKRW,
                IsAZN = _tcmbExchagneSettings.IsAZN,
                IsAED = _tcmbExchagneSettings.IsAED
            };

            return View("~/Plugins/ExchangeRate.TCMBExchange/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCurrencies))
                return AccessDeniedView();

            _tcmbExchagneSettings.ApiKey = model.TCMBExchangeApi;
            _tcmbExchagneSettings.AdditionalFee = model.AdditionalFee;
            _tcmbExchagneSettings.IsUSD = model.IsUSD;
            _tcmbExchagneSettings.IsAUD = model.IsAUD;
            _tcmbExchagneSettings.IsDKK = model.IsDKK;
            _tcmbExchagneSettings.IsEUR = model.IsEUR;
            _tcmbExchagneSettings.IsGBP = model.IsGBP;
            _tcmbExchagneSettings.IsCHF = model.IsCHF;
            _tcmbExchagneSettings.IsSEK = model.IsSEK;
            _tcmbExchagneSettings.IsCAD = model.IsCAD;
            _tcmbExchagneSettings.IsKWD = model.IsKWD;
            _tcmbExchagneSettings.IsNOK = model.IsNOK;
            _tcmbExchagneSettings.IsSAR = model.IsSAR;
            _tcmbExchagneSettings.IsJPY = model.IsJPY;
            _tcmbExchagneSettings.IsBGN = model.IsBGN;
            _tcmbExchagneSettings.IsRON = model.IsRON;
            _tcmbExchagneSettings.IsRUB = model.IsRUB;
            _tcmbExchagneSettings.IsIRR = model.IsIRR;
            _tcmbExchagneSettings.IsCNY = model.IsCNY;
            _tcmbExchagneSettings.IsPKR = model.IsPKR;
            _tcmbExchagneSettings.IsQAR = model.IsQAR;
            _tcmbExchagneSettings.IsKRW = model.IsKRW;
            _tcmbExchagneSettings.IsAZN = model.IsAZN;
            _tcmbExchagneSettings.IsAED = model.IsAED;

            _settingService.SaveSetting(_tcmbExchagneSettings);

            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return RedirectToAction("Configure");
        }

        #endregion
    }
}
