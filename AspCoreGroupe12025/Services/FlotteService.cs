using AspCoreGroupe12025.Entities;
using AspCoreGroupe12025.Helpers;
using AspCoreGroupe12025.Models;

using AutoMapper;

namespace AspCoreGroupe12025.Services
{
    public interface IFlotteService
    {
        IEnumerable<Flotte> GetAll();
        Flotte GetById(int id);
        Flotte Create(CreateRequestFlotte model);
        void Update(int id, UpdateRequestFlotte model);
        void Delete(int id);
    }

    public class FlotteService : IFlotteService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FlotteService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Flotte> GetAll()
        {
            return _context.Flottes;
        }

        public Flotte GetById(int id)
        {
            return getFlotte(id);
        }

        public Flotte Create(CreateRequestFlotte model)
        {
            var entity = _mapper.Map<Flotte>(model);
            _context.Flottes.Add(entity);
            _context.SaveChanges();
            return entity;
        }


        public void Update(int id, UpdateRequestFlotte model)
        {
            var flotte = getFlotte(id);
            _mapper.Map(model, flotte);
            _context.Flottes.Update(flotte);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var flotte = getFlotte(id);
            _context.Flottes.Remove(flotte);
            _context.SaveChanges();
        }

        private Flotte getFlotte(int id)
        {
            var flotte = _context.Flottes.Find(id);
            if (flotte == null)
                throw new KeyNotFoundException("Flotte not found");

            return flotte;
        }
    }
}

