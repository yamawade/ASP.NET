using AspCoreGroupe12025.Entities;
using AspCoreGroupe12025.Models;
using System.Collections.Generic;

namespace AspCoreGroupe12025.Services
{
    public interface IClientService
    {
        IEnumerable<Client> GetAll();
        Client GetById(int id);
        void Create(CreateClientRequest model);
        void Update(int id, UpdateClientRequest model);
        void Delete(int id);
    }
}
