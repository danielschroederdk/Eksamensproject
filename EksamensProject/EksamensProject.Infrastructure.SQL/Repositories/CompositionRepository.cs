using System.Collections.Generic;
using System.Linq;
using EksamensProject.Core.DomainService;
using EksamensProject.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace EksamensProject.Infrastructure.SQL.Repositories
{
    public class CompositionRepository : ICompositionRepository
    {
        readonly EksamensProjectContext _ctx;

        public CompositionRepository(EksamensProjectContext ctx)
        {
            _ctx = ctx;
        }

        public Composition Create(Composition composition)
        {
            _ctx.Add(composition);
            _ctx.SaveChanges();
            return composition;        
        }

        public Composition ReadById(int id)
        {
            return _ctx.Compositions.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Composition> ReadAll()
        {
            return _ctx.Compositions.ToList();        
        }

        public Composition Update(Composition compositionUpdate)
        {
            _ctx.Entry(compositionUpdate).State = EntityState.Modified;
            _ctx.SaveChanges();
            return compositionUpdate;
            
        }

        public Composition Delete(int id)
        {
            var compositionToDelete = ReadById(id);
            _ctx.Compositions.Remove(compositionToDelete);
            _ctx.SaveChanges();
            return compositionToDelete;        
        }
    }
}