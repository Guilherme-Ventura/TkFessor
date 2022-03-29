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
            request.AddHeader("X-Riot-Token", "RGAPI-0f5423c1-8b6f-4b43-b172-da9a3bf8b762");
            IRestResponse response = client.Execute(request);

            DadosInvocador saida = JsonConvert.DeserializeObject<DadosInvocador>(response.Content);

            return saida;
        }

        public List<DadosPerfil> BuscarPerfil(string id)
        {
            var client = new RestClient("https://br1.api.riotgames.com/lol/league/v4/entries/by-summoner/" + id);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-Riot-Token", "RGAPI-0f5423c1-8b6f-4b43-b172-da9a3bf8b762");
            IRestResponse response = client.Execute(request);

            List<DadosPerfil> saida = JsonConvert.DeserializeObject<List<DadosPerfil>>(response.Content);



            return saida;
        }
    }
}
