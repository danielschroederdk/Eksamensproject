using System.Collections.Generic;
using EksamensProject.Core.Entity;

namespace EksamensProject.Core.DomainService
{
    public interface ICompositionRepository
    {
        Composition Create(Composition composition);
        Composition ReadById(int id);
        IEnumerable<Composition> ReadAll();
        Composition Update(Composition compositionUpdate);
        Composition Delete(int id);
    }
    
}