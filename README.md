# Nop.Plugin.ExchangeRate.TCMBExchange

T.C.M.B. (Türkiye Cumhuriyeti Merkez Bankası) döviz kurlarının nopCommerce mağazanıza otomatik veya manuel olarak çekilmesini sağlayan döviz kuru servisi eklentisi.

**SystemName:** `CurrencyExchange.TCMBExchange`
**Author:** Silver Bilgi Teknolojileri

## nopCommerce Mağaza (Marketplace)

Eklenti, resmi nopCommerce mağazasında yayınlanmaktadır:

- **Ürün sayfası:** https://www.nopcommerce.com/tr/tc-merkez-bankasi-doviz-kurlari-servisi

Ürünün resmi dağıtım/yayın kanalı burasıdır.

## Desteklenen nopCommerce sürümleri

Her branch o sürüme özel, kendi başına derlenebilir tam kaynak kodu içerir. Kullandığınız
nopCommerce sürümüne uygun branch'i indirin/klonlayın.

| nopCommerce sürümü | Hedef .NET | Gerekli SDK | Branch |
|---|---|---|---|
| 4.30 | .NET Core 3.1 | 3.1.x | `v4.30` |
| 4.40 | .NET 5 | 5.0.x | `v4.40` |
| 4.50 | .NET 6 | 6.0.x | `v4.50` |
| 4.60 | .NET 7 | 7.0.x | `v4.60` |
| 4.70 | .NET 8 | 8.0.x | `v4.70` |
| 4.80 | .NET 9 | 9.0.x | `v4.80` |
| 4.90 | .NET 9 | 9.0.x | `v4.90` |

## Kurulum (derlenmiş eklenti)

1. İlgili branch'i indirin ve derleyin (bkz. "Kaynaktan derleme")
2. Derleme çıktısını nopCommerce kurulumunuzdaki `\Plugins` dizinine kopyalayın
3. https://evds2.tcmb.gov.tr/ adresinden üyelik oluşturup Profil sayfasındaki API bilgilerinizi
   eklenti ayarlarına tanımlayın
4. Yönetim panelinden eklentiyi bulup kurun/etkinleştirin

## Kaynaktan derleme

Eklenti bağımsız bir `.sln` içermez; **tam bir nopCommerce kaynak ağacı içinde** derlenir
(proje `..\..\Libraries\Nop.Services` ve `..\..\Presentation\Nop.Web.Framework` referanslarını
kullanır).

1. Kullandığınız sürüme uygun nopCommerce kaynağını klonlayın (branch adı `<X.YY>-bug-fixes`):

   ```bash
   git clone --depth 1 --branch 4.90-bug-fixes https://github.com/nopSolutions/nopCommerce.git
   ```

2. Bu deponun eşleşen branch'ini (ör. `v4.90`) klonlayın ve içeriğini nopCommerce kaynağındaki
   şu dizine kopyalayın:

   ```
   src/Plugins/Nop.Plugin.ExchangeRate.TCMBExchange/
   ```

3. Derleyin:

   ```bash
   dotnet build src/Plugins/Nop.Plugin.ExchangeRate.TCMBExchange/Nop.Plugin.ExchangeRate.TCMBExchange.csproj -c Release
   ```

   nopCommerce 4.80 ve 4.90'da csproj `$(SolutionDir)` kullandığından, tek projeyi derlerken bu
   değeri belirtin:

   ```bash
   dotnet build .../Nop.Plugin.ExchangeRate.TCMBExchange.csproj -c Release -p:SolutionDir=<nopCommerce>/src/
   ```

4. Çıktı otomatik olarak `src/Presentation/Nop.Web/Plugins/ExchangeRate.TCMBExchange/` altına
   yazılır; bu klasörü mağazanızın `\Plugins` dizinine kopyalayın.

## .NET SDK notu

Her branch, yukarıdaki tabloda belirtilen .NET sürümünü hedefler ve derleme için ilgili **.NET SDK**
gerekir. .NET Core 3.1 / .NET 5 / .NET 6 artık Microsoft tarafından desteklenmiyor (EOL); makinenizde
kurulu değilse yan-yana (side-by-side) kurabilirsiniz:

- İndirme sayfası: https://dotnet.microsoft.com/download/dotnet
- veya `dotnet-install` betiği ile kullanıcı-yerel kurulum (yönetici/admin gerektirmez):

  ```powershell
  # Windows PowerShell
  Invoke-WebRequest https://dot.net/v1/dotnet-install.ps1 -OutFile dotnet-install.ps1
  ./dotnet-install.ps1 -Channel 3.1 -InstallDir "$env:USERPROFILE\.dotnet"   # 5.0 ve 6.0 için tekrarlayın
  ```

  Ardından o SDK ile derlemek için `%USERPROFILE%\.dotnet\dotnet.exe` çağırın veya bu dizini PATH'e
  ekleyin.

> **Not (EOL sürümler):** nopCommerce'in derleme-sonrası `ClearPluginAssemblies` yardımcı aracı,
> hedeflenen .NET **çalışma-zamanının** PATH'teki `dotnet` tarafından bulunabilmesini gerektirir.
> Yan-yana kurulan bir SDK ile derliyorsanız `%USERPROFILE%\.dotnet` dizinini PATH'in başına ekleyin;
> aksi halde eklenti derlense bile bu son adım `hostfxr 0x80008006` hatası verebilir.

> **Not (Windows uzun yol):** Kaynağı çok derin bir dizinde derlerken Windows `MAX_PATH` (260 karakter)
> sınırına takılırsanız (`MSB3030` kopyalama hatası), nopCommerce kaynağını kısa bir yola (ör. `C:\nb`)
> klonlayıp oradan derleyin.
