using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using RestSharp;

namespace AppAspGroupe12025.Models
{
    public class Message
    {
        private readonly string _apiKey = "acd31e69604bfb60a44bc91d9732f75a62f4b6cf134c46e11075b82709713a79c91da1974d5c9f17";
        private readonly RestClient _client;

        public Message()
        {
            _client = new RestClient("https://api.wassenger.com/v1/messages");
        }

        // Méthode pour envoyer un message WhatsApp de manière asynchrone
        public async Task<string> SendMessageAsync(string numero, string message)
        {
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Token", _apiKey);
            request.Method = Method.Post;

            // Corps de la requête JSON
            var body = new
            {
                phone = numero,
                message = message,
                device = "67b90cb535c5096996d8a3e8"
                //phone= +221776862795,
                //message= "Ceci est un teste"
            };

            // Ajout du corps JSON à la requête
            request.AddJsonBody(body);

            // Exécution de la requête et obtention de la réponse
            RestResponse response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return "Message envoyé avec succès !";
            }
            else
            {
                return $"Erreur : {response.Content}";
            }
        
        }
    }
}