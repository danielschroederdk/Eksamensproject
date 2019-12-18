using System.Collections.Generic;
using System.Linq;
using EksamensProject.Core.DomainService;
using EksamensProject.Core.Entity;

namespace EksamensProject.Infrastructure.SQL.Repositories
{
    public class StylesRepository : IStylesRepository
    {
        readonly EksamensProjectContext _ctx;
        
        public StylesRepository(EksamensProjectContext ctx)
        {
            _ctx = ctx;
        }

        public Style ReadById(int id)
        {
            return _ctx.Styles.FirstOrDefault(s => s.Id == id);

        }

        public IEnumerable<Style> ReadAll()
        {
            return _ctx.Styles.ToList();
        }
    }
}