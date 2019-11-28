using System.Collections.Generic;
using EksamensProject.Core.Entity;

namespace EksamensProject.Core.DomainService
{
    public interface ICompositionRepository
    {
        Composition Create(Composition composition);
        Composition ReadByID(int ID);
        IEnumerable<Composition> ReadAll();
        Composition Update(Composition compositionUpdate);
        Composition Delete(int ID);
    }
    
}