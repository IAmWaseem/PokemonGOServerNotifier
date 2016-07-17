using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using PokemonServerNotifier.Commands;

namespace PokemonServerNotifier
{
    public class PokemonGOServerChecker
    {
        public async Task<ServerStatus> CheckServerStatus()
        {
            using (var client = new HttpClient())
            {
                var data = await client.GetStringAsync("http://cmmcd.com/PokemonGo/");
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(data);
                var node = document.DocumentNode.SelectNodes("//table");
                var text = node[0].ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[1].InnerText;
                if (text.ToLower().Contains("offline"))
                    return ServerStatus.Offline;
                if (text.ToLower().Contains("online"))
                    return ServerStatus.Online;
                if (text.ToLower().Contains("unstable"))
                    return ServerStatus.Unstable;
            }
            return ServerStatus.Unknown;
        }
    }
}
