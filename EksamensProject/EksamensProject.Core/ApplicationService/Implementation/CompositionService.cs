using System;
using System.Collections.Generic;
using EksamensProject.Core.Entity;

namespace EksamensProject.Core.ApplicationService.Implementation
{
    public class CompositionService : ICompositionService
    {
        public Composition CreateNewComposition(string name, DateTime dateTime, double duration, Tempo tempo)
        {
            throw new NotImplementedException();
        }

        public Composition CreateComposition(Composition composition)
        {
            throw new NotImplementedException();
        }

        public Composition FindCompositionById(int id)
        {
            throw new NotImplementedException();
        }

        public Composition Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Composition UpdateComposition(Composition compositionUpdate)
        {
            throw new NotImplementedException();
        }

        public List<Composition> GetCompositions()
        {
            throw new NotImplementedException();
        }
    }
}