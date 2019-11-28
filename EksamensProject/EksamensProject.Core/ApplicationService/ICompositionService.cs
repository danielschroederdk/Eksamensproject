using System;
using System.Collections.Generic;
using EksamensProject.Core.Entity;

namespace EksamensProject.Core.ApplicationService
{
    public interface ICompositionService
    {
        Composition CreateNewComposition(String name, DateTime dateTime, double duration, Tempo tempo);
        Composition CreateComposition(Composition composition);
        Composition FindCompositionById(int id);
        Composition Delete(int id);
        Composition UpdateComposition(Composition compositionUpdate);
        List<Composition> GetCompositions();
    }
}