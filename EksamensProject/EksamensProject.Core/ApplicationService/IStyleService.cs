using System.Collections.Generic;
using EksamensProject.Core.Entity;

namespace EksamensProject.Core.ApplicationService
{
    public interface IStyleService
    {
        List<Style> GetStyles();
        Style FindStyleById(int id);

    }
}