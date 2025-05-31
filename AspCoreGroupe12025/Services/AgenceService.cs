using System.Collections.Generic;
using System.Linq;
using AspCoreGroupe12025.Entities;
using AspCoreGroupe12025.Models;

namespace AspCoreGroupe12025.Services
{
    public interface IAgenceService
    {
        IEnumerable<Agence> GetAll();
        Agence GetById(int id);
        void Create(CreateAgenceRequest model);
        void Update(int id, UpdateAgenceRequest model);
        void Delete(int id);
    }

    public class AgenceService : IAgenceService
    {
        private List<Agence> _agences = new(); // Pour l'exemple, simule une base

        public IEnumerable<Agence> GetAll() => _agences;

        public Agence GetById(int id) => _agences.FirstOrDefault(a => a.IdAgence == id);

        public void Create(CreateAgenceRequest model)
        {
            var agence = new Agence
            {
                IdAgence = _agences.Count + 1,
                NineaAgence = model.NineaAgence,
                AdresseAgence = model.AdresseAgence,
                Longitude = model.Longitude,
                Latitude = model.Latitude,
                RccmAgence = model.RccmAgence,
                IdGestionnaire = model.IdGestionnaire
            };
            _agences.Add(agence);
        }

        public void Update(int id, UpdateAgenceRequest model)
        {
            var agence = GetById(id);
            if (agence != null)
            {
                agence.NineaAgence = model.NineaAgence;
                agence.AdresseAgence = model.AdresseAgence;
                agence.Longitude = model.Longitude;
                agence.Latitude = model.Latitude;
                agence.RccmAgence = model.RccmAgence;
                agence.IdGestionnaire = model.IdGestionnaire;
            }
        }

        public void Delete(int id)
        {
            var agence = GetById(id);
            if (agence != null)
                _agences.Remove(agence);
        }
    }
}
