using Backend.Models;
using Backend.Models.Backend;
using Fiss.Extensions;
using Fiss.Request;
using Microsoft.Net.Http.Headers;
using System.Net.Http;
using System.Security.Principal;

namespace Backend.Services
{
    public class SecurityService
    {
        private readonly HttpClient _httpClient;

        public SecurityService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://iss.moex.com/iss/");
        }

        public async Task<List<Security>> GetAllAsync(String date)
        {
            var securitys = new List<Security>();

            var secidName = await GetPairsSecIdName();

            var request = new IssRequest();
            var path = "engines/stock/markets/shares/boards/TQBR/securities";
            request.FullPath(path);
            request.AddQuery(new KeyValuePair<string, string>("marketdata.columns", "SECID, LAST, LASTTOPREVPRICE"));
            request.AddQuery(new KeyValuePair<string, string>("date", date));
            await request.Fetch();
            var respones = request.ToResponse();

            foreach (var data in respones["Marketdata"].Rows.ToList())
            {
                var secid = data.Values["Secid"].ToString() ?? "";
                securitys.Add(new Security(
                        Guid.NewGuid(),
                        secid,
                        secidName[secid] ?? "",
                        Convert.ToDouble(data.Values["Last"]),
                        Convert.ToDouble(data.Values["Lasttoprevprice"])
                    ));
            }

            return securitys;
        }

        private async Task<Dictionary<String, String?>> GetPairsSecIdName()
        {
            var secidName = new Dictionary<String, String?>();

            var request = new IssRequest();
            var path = "engines/stock/markets/shares/boards/TQBR/securities";
            request.FullPath(path);
            request.AddQuery(new KeyValuePair<string, string>("securities.columns", "SECID,SECNAME"));
            await request.Fetch();
            var respones = request.ToResponse();

            foreach(var secur in respones["Securities"].Rows.ToList())
            {
                secidName.Add(secur.Values["Secid"].ToString(), secur.Values["Secname"].ToString());
            }

            return secidName;
        }

    }
}



//определение тикет, актуальную цену и изменение за день
//https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.json?iss.meta=off&iss.only=marketdata&marketdata.columns=SECID,LAST,LASTTOPREVPRICE

//определение тикет и наименование
//https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.json?iss.meta=off&iss.only=securities&securities.columns=SECID,SECNAME

//[1] "SECNAME"   string
//        [0] "SECID" string


//[2] "Lasttoprevprice"   string
//[1] "Last"  string
//[0] "Secid" string
