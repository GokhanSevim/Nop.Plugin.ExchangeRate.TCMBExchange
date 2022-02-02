using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Nop.Plugin.ExchangeRate.TCMBExchange.Models
{
    public class ConfigurationModel : BaseNopModel
    {

        [NopResourceDisplayName("Plugins.ExchangeRate.TCMBExchange.Fields.ApiKey")]
        public string TCMBExchangeApi { get; set; }


        private decimal _AdditionalFee;
        [NopResourceDisplayName("Plugins.ExchangeRate.TCMBExchange.Fields.AdditionalFee")]
        public decimal AdditionalFee
        {
            get { return _AdditionalFee; }
            set { _AdditionalFee = value < 0 ? 0 : value; }
        }

        [NopResourceDisplayName("Plugins.ExchangeRate.TCMBExchange.Fields.USD")]
        public bool IsUSD { get; set; }

        [NopResourceDisplayName("Plugins.ExchangeRate.TCMBExchange.Fields.AUD")]
        public bool IsAUD { get; set; }

        [NopResourceDisplayName("Plugins.ExchangeRate.TCMBExchange.Fields.DKK")]
        public bool IsDKK { get; set; }

        [NopResourceDisplayName("Plugins.ExchangeRate.TCMBExchange.Fields.EUR")]
        public bool IsEUR { get; set; }

        [NopResourceDisplayName("Plugins.ExchangeRate.TCMBExchange.Fields.GBP")]
        public bool IsGBP { get; set; }

        [NopResourceDisplayName("Plugins.ExchangeRate.TCMBExchange.Fields.CHF")]
        public bool IsCHF { get; set; }

        [NopResourceDisplayName("Plugins.ExchangeRate.TCMBExchange.Fields.SEK")]
        public bool IsSEK { get; set; }

        [NopResourceDisplayName("Plugins.ExchangeRate.TCMBExchange.Fields.CAD")]
        public bool IsCAD { get; set; }

        [NopResourceDisplayName("Plugins.ExchangeRate.TCMBExchange.Fields.KWD")]
        public bool IsKWD { get; set; }

        [NopResourceDisplayName("Plugins.ExchangeRate.TCMBExchange.Fields.NOK")]
        public bool IsNOK { get; set; }

        [NopResourceDisplayName("Plugins.ExchangeRate.TCMBExchange.Fields.SAR")]
        public bool IsSAR { get; set; }

        [NopResourceDisplayName("Plugins.ExchangeRate.TCMBExchange.Fields.JPY")]
        public bool IsJPY { get; set; }

        [NopResourceDisplayName("Plugins.ExchangeRate.TCMBExchange.Fields.BGN")]
        public bool IsBGN { get; set; }

        [NopResourceDisplayName("Plugins.ExchangeRate.TCMBExchange.Fields.RON")]
        public bool IsRON { get; set; }

        [NopResourceDisplayName("Plugins.ExchangeRate.TCMBExchange.Fields.RUB")]
        public bool IsRUB { get; set; }

        [NopResourceDisplayName("Plugins.ExchangeRate.TCMBExchange.Fields.IRR")]
        public bool IsIRR { get; set; }

        [NopResourceDisplayName("Plugins.ExchangeRate.TCMBExchange.Fields.CNY")]
        public bool IsCNY { get; set; }

        [NopResourceDisplayName("Plugins.ExchangeRate.TCMBExchange.Fields.PKR")]
        public bool IsPKR { get; set; }

        [NopResourceDisplayName("Plugins.ExchangeRate.TCMBExchange.Fields.QAR")]
        public bool IsQAR { get; set; }

        [NopResourceDisplayName("Plugins.ExchangeRate.TCMBExchange.Fields.KRW")]
        public bool IsKRW { get; set; }

        [NopResourceDisplayName("Plugins.ExchangeRate.TCMBExchange.Fields.AZN")]
        public bool IsAZN { get; set; }

        [NopResourceDisplayName("Plugins.ExchangeRate.TCMBExchange.Fields.AED")]
        public bool IsAED { get; set; }
    }
}