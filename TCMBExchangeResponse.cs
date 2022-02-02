using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.ExchangeRate.TCMBExchange
{
    public class TCMBExchangeResponse
    {
        [JsonProperty(PropertyName = "totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty(PropertyName = "items")]
        public List<TCMBExchangeDatas> Items { get; set; }
    }

    public class TCMBExchangeDatas
    {
        public string Tarih { get; set; }

        [JsonProperty(PropertyName = "TP_DK_USD_S")]
        public string USD { get; set; }

        [JsonProperty(PropertyName = "TP_DK_AUD_S")]
        public string AUD { get; set; }

        [JsonProperty(PropertyName = "TP_DK_DKK_S")]
        public string DKK { get; set; }

        [JsonProperty(PropertyName = "TP_DK_EUR_S")]
        public string EUR { get; set; }

        [JsonProperty(PropertyName = "TP_DK_GBP_S")]
        public string GBP { get; set; }

        [JsonProperty(PropertyName = "TP_DK_CHF_S")]
        public string CHF { get; set; }

        [JsonProperty(PropertyName = "TP_DK_SEK_S")]
        public string SEK { get; set; }

        [JsonProperty(PropertyName = "TP_DK_CAD_S")]
        public string CAD { get; set; }

        [JsonProperty(PropertyName = "TP_DK_KWD_S")]
        public string KWD { get; set; }

        [JsonProperty(PropertyName = "TP_DK_NOK_S")]
        public string NOK { get; set; }

        [JsonProperty(PropertyName = "TP_DK_SAR_S")]
        public string SAR { get; set; }

        [JsonProperty(PropertyName = "TP_DK_JPY_S")]
        public string JPY { get; set; }

        [JsonProperty(PropertyName = "TP_DK_BGN_S")]
        public string BGN { get; set; }

        [JsonProperty(PropertyName = "TP_DK_RON_S")]
        public string RON { get; set; }

        [JsonProperty(PropertyName = "TP_DK_RUB_S")]
        public string RUB { get; set; }

        [JsonProperty(PropertyName = "TP_DK_IRR_S")]
        public string IRR { get; set; }

        [JsonProperty(PropertyName = "TP_DK_CNY_S")]
        public string CNY { get; set; }

        [JsonProperty(PropertyName = "TP_DK_PKR_S")]
        public string PKR { get; set; }

        [JsonProperty(PropertyName = "TP_DK_QAR_S")]
        public string QAR { get; set; }

        [JsonProperty(PropertyName = "TP_DK_KRW_S")]
        public string KRW { get; set; }

        [JsonProperty(PropertyName = "TP_DK_AZN_S")]
        public string AZN { get; set; }

        [JsonProperty(PropertyName = "TP_DK_AED_S")]
        public string AED { get; set; }
    }
}
