using Nop.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.ExchangeRate.TCMBExchange
{
    public class TCMBExchangeSettings : ISettings
    {
        /// <summary>
        /// https://evds2.tcmb.gov.tr/ Gets or sets an API key
        /// </summary>
        public string ApiKey { get; set; }

        public decimal AdditionalFee { get; set; }

        public bool IsUSD { get; set; }

        public bool IsAUD { get; set; }

        public bool IsDKK { get; set; }

        public bool IsEUR { get; set; }

        public bool IsGBP { get; set; }

        public bool IsCHF { get; set; }

        public bool IsSEK { get; set; }

        public bool IsCAD { get; set; }

        public bool IsKWD { get; set; }

        public bool IsNOK { get; set; }

        public bool IsSAR { get; set; }

        public bool IsJPY { get; set; }

        public bool IsBGN { get; set; }

        public bool IsRON { get; set; }

        public bool IsRUB { get; set; }

        public bool IsIRR { get; set; }

        public bool IsCNY { get; set; }

        public bool IsPKR { get; set; }

        public bool IsQAR { get; set; }

        public bool IsKRW { get; set; }

        public bool IsAZN { get; set; }

        public bool IsAED { get; set; }
    }
}
