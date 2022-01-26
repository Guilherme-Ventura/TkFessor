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
        public DadosInvocador BuscarRequicicao(string nickname)
        {
            var client = new RestClient("https://br1.api.riotgames.com/lol/summoner/v4/summoners/by-name/" + nickname);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-Riot-Token", "RGAPI-022d267e-3754-47ba-ab6b-3228226bf467");
            IRestResponse response = client.Execute(request);

            DadosInvocador saida = JsonConvert.DeserializeObject<DadosInvocador>(response.Content);

            return saida;
        }

        public List<DadosPerfil> BuscarPerfil(string id)
        {
            var client = new RestClient("https://br1.api.riotgames.com/lol/league/v4/entries/by-summoner/" + id);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-Riot-Token", "RGAPI-022d267e-3754-47ba-ab6b-3228226bf467");
            IRestResponse response = client.Execute(request);

            List<DadosPerfil> saida = JsonConvert.DeserializeObject<List<DadosPerfil>>(response.Content);

            return saida;
        }
    }
}
