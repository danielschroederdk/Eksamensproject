using System.Collections.Generic;
using EksamensProject.Core.Entity;

namespace EksamensProject.Core.DomainService
{
    public interface IStylesRepository
    {
        Style ReadById(int id);

        IEnumerable<Style> ReadAll();
    }
}