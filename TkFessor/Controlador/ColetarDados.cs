using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using TkFessor.Entidades;
using Newtonsoft.Json;

namespace TkFessor.Controlador
{
    public class ColetarDados
    {
        string apiKey = "RGAPI-eb35390e-e049-47d4-97ad-1c7bf2063eb6";
        public DadosInvocador BuscarRequicicao(string nickname)
        {
            var client = new RestClient("https://br1.api.riotgames.com/lol/summoner/v4/summoners/by-name/" + nickname);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-Riot-Token", apiKey);
            IRestResponse response = client.Execute(request);

            DadosInvocador saida = JsonConvert.DeserializeObject<DadosInvocador>(response.Content);

            return saida;
        }

        public List<DadosPerfil> BuscarPerfil(string id)
        {
            var client = new RestClient("https://br1.api.riotgames.com/lol/league/v4/entries/by-summoner/" + id);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-Riot-Token", apiKey);
            IRestResponse response = client.Execute(request);

            List<DadosPerfil> saida = JsonConvert.DeserializeObject<List<DadosPerfil>>(response.Content);

            return saida;
        }

        public List<DadosMaestria> BuscarMaestria(string id)
        {
            var client = new RestClient("https://br1.api.riotgames.com/lol/champion-mastery/v4/champion-masteries/by-summoner/" + id);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-Riot-Token", apiKey);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

            List<DadosMaestria> saida = JsonConvert.DeserializeObject<List<DadosMaestria>>(response.Content);

            return saida;
        }

        public List<string> BuscarChaveHistorico(string puuid)
        {
            var client = new RestClient("https://americas.api.riotgames.com/lol/match/v5/matches/by-puuid/" + puuid + "/ids?start=0&count=10");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-Riot-Token", apiKey);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

            List<string> saida = JsonConvert.DeserializeObject<List<string>>(response.Content);

            return saida;
        }

        public DadosHistorico BuscarInfoPartida(string chaveHistorico)
        {
            var client = new RestClient("https://americas.api.riotgames.com/lol/match/v5/matches/" + chaveHistorico);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-Riot-Token", apiKey);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

            DadosHistorico saida = JsonConvert.DeserializeObject<DadosHistorico>(response.Content);

            return saida;
        }
    }
}
