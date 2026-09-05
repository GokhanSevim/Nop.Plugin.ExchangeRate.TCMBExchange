# Nop.Plugin.ExchangeRate.TCMBExchange

T.C.M.B. (Türkiye Cumhuriyeti Merkez Bankası) döviz kurlarının nopCommerce mağazanıza otomatik veya manuel olarak çekilmesini sağlayan döviz kuru servisi eklentisi.

**SystemName:** `CurrencyExchange.TCMBExchange`
**Author:** Silver Bilgi Teknolojileri

## Desteklenen nopCommerce sürümleri

| nopCommerce sürümü | Branch |
|---|---|
| 4.30 | `v4.30` |
| 4.40 | `v4.40` |
| 4.50 | `v4.50` |
| 4.60 | `v4.60` |
| 4.70 | `v4.70` |
| 4.80 | `v4.80` |
| 4.90 | `v4.90` |

Kullandığınız nopCommerce sürümüne uygun branch'i indirin/klonlayın; her branch o sürüme özel, kendi başına derlenebilir tam kaynak kodu içerir.

## Kurulum

1. İlgili branch'i indirin
2. Projeyi derleyin (`dotnet build` veya Visual Studio)
3. Çıktıyı nopCommerce kurulumunuzdaki `\Plugins` dizinine kopyalayın
4. https://evds2.tcmb.gov.tr/ adresinden üyelik oluşturup Profil sayfasındaki API bilgilerinizi eklenti ayarlarına tanımlayın
5. Yönetim panelinden eklentiyi bulup kurun/etkinleştirin
