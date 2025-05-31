using AspCoreGroupe12025.Entities;
using AspCoreGroupe12025.Models;
using System.Collections.Generic;
using System.Linq;

namespace AspCoreGroupe12025.Services
{
    public class ClientService : IClientService
    {
        private readonly List<Client> _clients = new();

        public IEnumerable<Client> GetAll() => _clients;

        public Client GetById(int id) => _clients.FirstOrDefault(c => c.Id == id);

        public void Create(CreateClientRequest model)
        {
            var client = new Client
            {
                Id = _clients.Count + 1,
                Title = model.Title,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PasswordHash = model.Password, // à hasher en vrai
                Adresse = model.Adresse,
                Telephone = model.Telephone,
                CNIClient = model.CNIClient,
                Role = Role.User
            };
            _clients.Add(client);
        }

        public void Update(int id, UpdateClientRequest model)
        {
            var client = GetById(id);
            if (client != null)
            {
                client.Title = model.Title;
                client.FirstName = model.FirstName;
                client.LastName = model.LastName;
                client.Email = model.Email;
                client.Adresse = model.Adresse;
                client.Telephone = model.Telephone;
                client.CNIClient = model.CNIClient;
            }
        }

        public void Delete(int id)
        {
            var client = GetById(id);
            if (client != null)
                _clients.Remove(client);
        }
    }
}
