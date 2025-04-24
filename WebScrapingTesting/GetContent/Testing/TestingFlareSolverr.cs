using FlareSolverrSharp.Solvers;
using System.Net;
using System.Text.RegularExpressions;
using Cookie = System.Net.Cookie;

public class TestingFlareSolverr
{
    private static readonly Regex _CookieRegex = new Regex(@"([^\(\)<>@,;:\\""/\[\]\?=\{\}\s]+)=([^,;\\""\s]+)");


    public async Task GetFromHDFull()
    {
        var flareClient = new FlareSolverr("http://localhost:5665/");
        flareClient.MaxTimeout = (int)TimeSpan.FromMinutes(3).TotalMilliseconds;

        string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36";
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://nopecha.com/demo/cloudflare");
        requestMessage.Headers.Add("User-Agent", userAgent);

        // Resolver Cloudflare y obtener cookies
        var response = await flareClient.Solve(requestMessage);
        if (response.Status != "ok")
        {
            Console.WriteLine("Error obteniendo cookies.");
            return;
        }

        // Extraer cookies y convertirlas a formato "clave=valor"
        var cookies = response.Solution.Cookies;

        // Convertir las cookies a un formato adecuado
        var cookiesHeader = ConvertCookiesToHeaderFormat(cookies);

        // En lugar de acceder a HttpContext, manejamos las cookies directamente
        await HacerRequestConCookies(response.Solution.Url, cookiesHeader);
    }

    // Convertir cookies de FlareSolverr a formato "key=value" para usar en el header
    private static string ConvertCookiesToHeaderFormat(FlareSolverrSharp.Types.Cookie[] cookies)
    {
        var cookieList = new List<string>();
        foreach (var cookie in cookies)
        {
            cookieList.Add($"{cookie.Name}={cookie.Value}");
        }
        return string.Join("; ", cookieList);
    }

    public async Task<HttpResponseMessage> HacerRequestConCookies(string url, string cookiesHeader)
    {
        var cookies = new CookieContainer();

        // Verificar si hay cookies en el encabezado
        if (!string.IsNullOrWhiteSpace(cookiesHeader))
        {
            var requestUri = new Uri(url);
            var cookieUrl = new Uri(requestUri.Scheme + "://" + requestUri.Host);
            var cookieDictionary = CookieHeaderToDictionary(cookiesHeader);
            foreach (var kv in cookieDictionary)
            {
                cookies.Add(cookieUrl, new Cookie(kv.Key, kv.Value));
            }
        }

        var handler = new HttpClientHandler
        {
            CookieContainer = cookies,
            UseCookies = true
        };

        using (var httpClient = new HttpClient(handler))
        {
            var response = await httpClient.GetAsync(url); // O cualquier otro tipo de solicitud
            var data = response.Content.ReadAsStringAsync();
            return response;
        }
    }

    // Función para convertir el header de cookies (en formato "key=value") a un diccionario
    private static Dictionary<string, string> CookieHeaderToDictionary(string cookiesHeader)
    {
        var cookieDictionary = new Dictionary<string, string>();

        // Usamos la expresión regular para extraer las cookies
        var matches = _CookieRegex.Matches(cookiesHeader);
        foreach (Match match in matches)
        {
            var key = match.Groups[1].Value;
            var value = match.Groups[2].Value;
            cookieDictionary[key] = value;
        }

        return cookieDictionary;
    }
}
