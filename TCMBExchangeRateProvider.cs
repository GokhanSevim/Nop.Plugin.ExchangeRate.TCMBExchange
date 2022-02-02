using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using Nop.Core;
using Nop.Core.Http;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Plugins;

namespace Nop.Plugin.ExchangeRate.TCMBExchange
{

    public class TCMBExchangeRateProvider : BasePlugin, IExchangeRateProvider
    {
        #region Fields

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalizationService _localizationService;
        private readonly IWebHelper _webHelper;
        private readonly ILogger _logger;
        private readonly TCMBExchangeSettings _exchangeSettings;

        #endregion

        #region Ctor

        public TCMBExchangeRateProvider(IHttpClientFactory httpClientFactory,
            ILocalizationService localizationService,
            IWebHelper webHelper,
            ILogger logger,
            TCMBExchangeSettings exchangeSettings)
        {
            _httpClientFactory = httpClientFactory;
            _localizationService = localizationService;
            _webHelper = webHelper;
            _logger = logger;
            _exchangeSettings = exchangeSettings;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets currency live rates
        /// </summary>
        /// <param name="exchangeRateCurrencyCode">Exchange rate currency code</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the exchange rates
        /// </returns>
        public IList<Core.Domain.Directory.ExchangeRate> GetCurrencyLiveRates(string exchangeRateCurrencyCode)
        {
            if (exchangeRateCurrencyCode == null)
                throw new ArgumentNullException(nameof(exchangeRateCurrencyCode));

            string CurrencyCode = "TRY";
            
            var ratesToTRY = new List<Core.Domain.Directory.ExchangeRate>()
            {
                new Core.Domain.Directory.ExchangeRate
                {
                    CurrencyCode = CurrencyCode,
                    Rate = 1,
                    UpdatedOn = DateTime.UtcNow
                }
            };

            try
            {
                if (string.IsNullOrEmpty(_exchangeSettings.ApiKey))
                {
                    throw new NopException(_localizationService.GetResource("Plugins.ExchangeRate.TCMBExchange.Info"));
                }

                int calcutaleDay = 0;

                if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                {
                    --calcutaleDay;
                }
                else if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                {
                    calcutaleDay -= 2;
                }

                string currentDate = DateTime.Now.AddDays(calcutaleDay).ToString("dd-MM-yyyy");

                List<string> Series = new List<string>();

                if (_exchangeSettings.IsUSD)
                {
                    Series.Add("TP.DK.USD.S");
                }

                if (_exchangeSettings.IsAUD)
                {
                    Series.Add("TP.DK.AUD.S");
                }

                if (_exchangeSettings.IsDKK)
                {
                    Series.Add("TP.DK.DKK.S");
                }

                if (_exchangeSettings.IsEUR)
                {
                    Series.Add("TP.DK.EUR.S");
                }

                if (_exchangeSettings.IsGBP)
                {
                    Series.Add("TP.DK.GBP.S");
                }

                if (_exchangeSettings.IsCHF)
                {
                    Series.Add("TP.DK.CHF.S");
                }

                if (_exchangeSettings.IsSEK)
                {
                    Series.Add("TP.DK.SEK.S");
                }

                if (_exchangeSettings.IsCAD)
                {
                    Series.Add("TP.DK.CAD.S");
                }

                if (_exchangeSettings.IsKWD)
                {
                    Series.Add("TP.DK.KWD.S");
                }

                if (_exchangeSettings.IsNOK)
                {
                    Series.Add("TP.DK.NOK.S");
                }

                if (_exchangeSettings.IsSAR)
                {
                    Series.Add("TP.DK.SAR.S");
                }

                if (_exchangeSettings.IsJPY)
                {
                    Series.Add("TP.DK.JPY.S");
                }

                if (_exchangeSettings.IsBGN)
                {
                    Series.Add("TP.DK.BGN.S");
                }

                if (_exchangeSettings.IsRON)
                {
                    Series.Add("TP.DK.RON.S");
                }

                if (_exchangeSettings.IsRUB)
                {
                    Series.Add("TP.DK.RUB.S");
                }

                if (_exchangeSettings.IsIRR)
                {
                    Series.Add("TP.DK.IRR.S");
                }

                if (_exchangeSettings.IsCNY)
                {
                    Series.Add("TP.DK.CNY.S");
                }

                if (_exchangeSettings.IsPKR)
                {
                    Series.Add("TP.DK.PKR.S");
                }

                if (_exchangeSettings.IsQAR)
                {
                    Series.Add("TP.DK.QAR.S");
                }

                if (_exchangeSettings.IsKRW)
                {
                    Series.Add("TP.DK.KRW.S");
                }

                if (_exchangeSettings.IsAZN)
                {
                    Series.Add("TP.DK.AZN.S");
                }

                if (_exchangeSettings.IsAED)
                {
                    Series.Add("TP.DK.AED.S");
                }

                string SeriesJoin = string.Join('-', Series);

                if (Series != null && Series.Count > 0)
                {

                    var httpClient = _httpClientFactory.CreateClient(NopHttpDefaults.DefaultHttpClient);
                    var jsonData = httpClient.GetStringAsync($"https://evds2.tcmb.gov.tr/service/evds/series={SeriesJoin}&startDate={currentDate}&endDate={currentDate}&type=json&key={_exchangeSettings.ApiKey}").Result;

                    var currencyDatas = Newtonsoft.Json.JsonConvert.DeserializeObject<TCMBExchangeResponse>(jsonData);

                    if (currencyDatas != null && currencyDatas.TotalCount > 0)
                    {
                        var currency = currencyDatas.Items.FirstOrDefault();

                        if (_exchangeSettings.IsUSD && decimal.TryParse(currency.USD, NumberStyles.Currency, CultureInfo.InvariantCulture, out var currencyRateUSD))
                        {
                            ratesToTRY.Add(new Core.Domain.Directory.ExchangeRate()
                            {
                                CurrencyCode = "USD",
                                Rate = currencyRateUSD,
                                UpdatedOn = DateTime.Now
                            });
                        }

                        if (_exchangeSettings.IsAUD && decimal.TryParse(currency.AUD, NumberStyles.Currency, CultureInfo.InvariantCulture, out var currencyRateAUD))
                        {
                            ratesToTRY.Add(new Core.Domain.Directory.ExchangeRate()
                            {
                                CurrencyCode = "AUD",
                                Rate = currencyRateAUD,
                                UpdatedOn = DateTime.Now
                            });
                        }

                        if (_exchangeSettings.IsDKK && decimal.TryParse(currency.DKK, NumberStyles.Currency, CultureInfo.InvariantCulture, out var currencyRateDKK))
                        {
                            ratesToTRY.Add(new Core.Domain.Directory.ExchangeRate()
                            {
                                CurrencyCode = "DKK",
                                Rate = currencyRateDKK,
                                UpdatedOn = DateTime.Now
                            });
                        }

                        if (_exchangeSettings.IsEUR && decimal.TryParse(currency.EUR, NumberStyles.Currency, CultureInfo.InvariantCulture, out var currencyRate))
                        {
                            ratesToTRY.Add(new Core.Domain.Directory.ExchangeRate()
                            {
                                CurrencyCode = "EUR",
                                Rate = currencyRate,
                                UpdatedOn = DateTime.Now
                            });
                        }

                        if (_exchangeSettings.IsGBP && decimal.TryParse(currency.GBP, NumberStyles.Currency, CultureInfo.InvariantCulture, out var currencyRateGBP))
                        {
                            ratesToTRY.Add(new Core.Domain.Directory.ExchangeRate()
                            {
                                CurrencyCode = "GBP",
                                Rate = currencyRateGBP,
                                UpdatedOn = DateTime.Now
                            });
                        }

                        if (_exchangeSettings.IsCHF && decimal.TryParse(currency.CHF, NumberStyles.Currency, CultureInfo.InvariantCulture, out var currencyRateCHF))
                        {
                            ratesToTRY.Add(new Core.Domain.Directory.ExchangeRate()
                            {
                                CurrencyCode = "CHF",
                                Rate = currencyRateCHF,
                                UpdatedOn = DateTime.Now
                            });
                        }

                        if (_exchangeSettings.IsSEK && decimal.TryParse(currency.SEK, NumberStyles.Currency, CultureInfo.InvariantCulture, out var currencyRateSEK))
                        {
                            ratesToTRY.Add(new Core.Domain.Directory.ExchangeRate()
                            {
                                CurrencyCode = "SEK",
                                Rate = currencyRateSEK,
                                UpdatedOn = DateTime.Now
                            });
                        }

                        if (decimal.TryParse(currency.CAD, NumberStyles.Currency, CultureInfo.InvariantCulture, out var currencyRateCAD))
                        {
                            ratesToTRY.Add(new Core.Domain.Directory.ExchangeRate()
                            {
                                CurrencyCode = "CAD",
                                Rate = currencyRateCAD,
                                UpdatedOn = DateTime.Now
                            });
                        }

                        if (_exchangeSettings.IsKWD && decimal.TryParse(currency.KWD, NumberStyles.Currency, CultureInfo.InvariantCulture, out var currencyRateKWD))
                        {
                            ratesToTRY.Add(new Core.Domain.Directory.ExchangeRate()
                            {
                                CurrencyCode = "KWD",
                                Rate = currencyRateKWD,
                                UpdatedOn = DateTime.Now
                            });
                        }

                        if (_exchangeSettings.IsNOK && decimal.TryParse(currency.NOK, NumberStyles.Currency, CultureInfo.InvariantCulture, out var currencyRateNOK))
                        {
                            ratesToTRY.Add(new Core.Domain.Directory.ExchangeRate()
                            {
                                CurrencyCode = "NOK",
                                Rate = currencyRateNOK,
                                UpdatedOn = DateTime.Now
                            });
                        }

                        if (_exchangeSettings.IsSAR && decimal.TryParse(currency.SAR, NumberStyles.Currency, CultureInfo.InvariantCulture, out var currencyRateSAR))
                        {
                            ratesToTRY.Add(new Core.Domain.Directory.ExchangeRate()
                            {
                                CurrencyCode = "SAR",
                                Rate = currencyRateSAR,
                                UpdatedOn = DateTime.Now
                            });
                        }

                        if (decimal.TryParse(currency.JPY, NumberStyles.Currency, CultureInfo.InvariantCulture, out var currencyRateJPY))
                        {
                            ratesToTRY.Add(new Core.Domain.Directory.ExchangeRate()
                            {
                                CurrencyCode = "JPY",
                                Rate = currencyRateJPY,
                                UpdatedOn = DateTime.Now
                            });
                        }

                        if (_exchangeSettings.IsBGN && decimal.TryParse(currency.BGN, NumberStyles.Currency, CultureInfo.InvariantCulture, out var currencyRateBGN))
                        {
                            ratesToTRY.Add(new Core.Domain.Directory.ExchangeRate()
                            {
                                CurrencyCode = "BGN",
                                Rate = currencyRateBGN,
                                UpdatedOn = DateTime.Now
                            });
                        }

                        if (_exchangeSettings.IsRON && decimal.TryParse(currency.RON, NumberStyles.Currency, CultureInfo.InvariantCulture, out var currencyRateRON))
                        {
                            ratesToTRY.Add(new Core.Domain.Directory.ExchangeRate()
                            {
                                CurrencyCode = "RON",
                                Rate = currencyRateRON,
                                UpdatedOn = DateTime.Now
                            });
                        }

                        if (_exchangeSettings.IsRUB && decimal.TryParse(currency.RUB, NumberStyles.Currency, CultureInfo.InvariantCulture, out var currencyRateRUB))
                        {
                            ratesToTRY.Add(new Core.Domain.Directory.ExchangeRate()
                            {
                                CurrencyCode = "RUB",
                                Rate = currencyRateRUB,
                                UpdatedOn = DateTime.Now
                            });
                        }

                        if (_exchangeSettings.IsIRR && decimal.TryParse(currency.IRR, NumberStyles.Currency, CultureInfo.InvariantCulture, out var currencyRateIRR))
                        {
                            ratesToTRY.Add(new Core.Domain.Directory.ExchangeRate()
                            {
                                CurrencyCode = "IRR",
                                Rate = currencyRateIRR,
                                UpdatedOn = DateTime.Now
                            });
                        }

                        if (decimal.TryParse(currency.CNY, NumberStyles.Currency, CultureInfo.InvariantCulture, out var currencyRateCNY))
                        {
                            ratesToTRY.Add(new Core.Domain.Directory.ExchangeRate()
                            {
                                CurrencyCode = "CNY",
                                Rate = currencyRateCNY,
                                UpdatedOn = DateTime.Now
                            });
                        }

                        if (_exchangeSettings.IsPKR && decimal.TryParse(currency.PKR, NumberStyles.Currency, CultureInfo.InvariantCulture, out var currencyRatePKR))
                        {
                            ratesToTRY.Add(new Core.Domain.Directory.ExchangeRate()
                            {
                                CurrencyCode = "PKR",
                                Rate = currencyRatePKR,
                                UpdatedOn = DateTime.Now
                            });
                        }

                        if (_exchangeSettings.IsQAR && decimal.TryParse(currency.QAR, NumberStyles.Currency, CultureInfo.InvariantCulture, out var currencyRateQAR))
                        {
                            ratesToTRY.Add(new Core.Domain.Directory.ExchangeRate()
                            {
                                CurrencyCode = "QAR",
                                Rate = currencyRateQAR,
                                UpdatedOn = DateTime.Now
                            });
                        }

                        if (_exchangeSettings.IsKRW && decimal.TryParse(currency.KRW, NumberStyles.Currency, CultureInfo.InvariantCulture, out var currencyRateKRW))
                        {
                            ratesToTRY.Add(new Core.Domain.Directory.ExchangeRate()
                            {
                                CurrencyCode = "KRW",
                                Rate = currencyRateKRW,
                                UpdatedOn = DateTime.Now
                            });
                        }

                        if (_exchangeSettings.IsAZN && decimal.TryParse(currency.AZN, NumberStyles.Currency, CultureInfo.InvariantCulture, out var currencyRateAZN))
                        {
                            ratesToTRY.Add(new Core.Domain.Directory.ExchangeRate()
                            {
                                CurrencyCode = "AZN",
                                Rate = currencyRateAZN,
                                UpdatedOn = DateTime.Now
                            });
                        }

                        if (_exchangeSettings.IsAED && decimal.TryParse(currency.AED, NumberStyles.Currency, CultureInfo.InvariantCulture, out var currencyRateAED))
                        {
                            ratesToTRY.Add(new Core.Domain.Directory.ExchangeRate()
                            {
                                CurrencyCode = "AED",
                                Rate = currencyRateAED,
                                UpdatedOn = DateTime.Now
                            });
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.Error("T.C.M.B Exchange Service : ", ex);
            }

            //return result for the euro
            if (exchangeRateCurrencyCode.Equals(CurrencyCode, StringComparison.InvariantCultureIgnoreCase) == false)
            {
                if (_exchangeSettings.AdditionalFee > 0)
                {
                    ratesToTRY.Where(x=> x.CurrencyCode != CurrencyCode).ToList().ForEach((item) =>
                    {
                        item.Rate = Math.Round(item.Rate + ((item.Rate * _exchangeSettings.AdditionalFee) / 100), 4);
                    });
                }

                return ratesToTRY;
            }


            //use only currencies that are supported by TRY
            var exchangeRateCurrency = ratesToTRY.FirstOrDefault(rate => rate.CurrencyCode.Equals(exchangeRateCurrencyCode, StringComparison.InvariantCultureIgnoreCase));

            if (exchangeRateCurrency == null)
                throw new NopException(_localizationService.GetResource("Plugins.ExchangeRate.TCMBExchange.Error"));

            if (_exchangeSettings.AdditionalFee > 0)
            {
                ratesToTRY.Where(x => x.CurrencyCode != CurrencyCode).ToList().ForEach((item) =>
                {
                    item.Rate = Math.Round(exchangeRateCurrency.Rate / item.Rate, 4);
                    item.Rate = Math.Round(item.Rate + ((item.Rate * _exchangeSettings.AdditionalFee) / 100), 4);
                });
            }
            else
            {
                ratesToTRY.Where(x => x.CurrencyCode != CurrencyCode).ToList().ForEach((item) =>
                {
                    item.Rate = Math.Round(exchangeRateCurrency.Rate / item.Rate, 4);
                });

            }

            return ratesToTRY;
        }

        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/TCMBExchange/Configure";
        }

        /// <summary>
        /// Install the plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override void Install()
        {
            //locales
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Fields.ApiKey", "Api Key");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Fields.AdditionalFee", "İlave % Oran (Min. %0)");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Fields.ActiveSelected", "Döviz Seçimi");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Error", "Varsayılan döviz kuru Türk Lirası olduğunda doğru olarak kullanılabilir.");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Info", "https://evds2.tcmb.gov.tr/ adresinden Üyelik oluşturup Profil sayfasında yer alan Api bilgilerinizi tanımlamalısınız.");


            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Fields.USD", "ABD DOLARI");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Fields.AUD", "AVUSTRALYA DOLARI");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Fields.DKK", "DANİMARKA KRONU");
             _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Fields.EUR", "EURO");
             _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Fields.GBP", "İNGİLİZ STERLİNİ");
             _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Fields.CHF", "İSVİÇRE FRANGI");
             _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Fields.SEK", "İSVEÇ KRONU");
             _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Fields.CAD", "KANADA DOLARI");
             _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Fields.KWD", "KUVEYT DİNARI");
             _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Fields.NOK", "NORVEÇ KRONU");
             _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Fields.SAR", "SUUDİ ARABİSTAN RİYALİ");
             _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Fields.JPY", "JAPON YENİ");
             _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Fields.BGN", "BULGAR LEVASI");
             _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Fields.RON", "RUMEN LEYİ");
             _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Fields.RUB", "RUS RUBLESİ");
             _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Fields.IRR", "İRAN RİYALİ");
             _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Fields.CNY", "ÇİN YUANI");
             _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Fields.PKR", "PAKİSTAN RUPİSİ");
             _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Fields.QAR", "KATAR RİYALİ");
             _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Fields.KRW", "GÜNEY KORE WONU");
             _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Fields.AZN", "AZERBAYCAN YENİ MANATI");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.ExchangeRate.TCMBExchange.Fields.AED", "BİRLEŞİK ARAP EMİRLİKLERİ DİRHEMİ");

            base.Install();
        }

        /// <summary>
        /// Uninstall the plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override void Uninstall()
        {
            //locales
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Fields.ApiKey");
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Fields.AdditionalFee");
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Fields.ActiveSelected");
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Error");
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Info");

             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Fields.USD");
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Fields.AUD");
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Fields.DKK");
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Fields.EUR");
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Fields.GBP");
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Fields.CHF");
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Fields.SEK");
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Fields.CAD");
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Fields.KWD");
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Fields.NOK");
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Fields.SAR");
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Fields.JPY");
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Fields.BGN");
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Fields.RON");
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Fields.RUB");
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Fields.IRR");
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Fields.CNY");
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Fields.PKR");
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Fields.QAR");
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Fields.KRW");
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Fields.AZN");
             _localizationService.DeletePluginLocaleResources("Plugins.ExchangeRate.TCMBExchange.Fields.AED");

            base.Uninstall();
        }

        #endregion

    }
}