using System;
using System.Collections.Generic;
using EksamensProject.Core.DomainService;
using EksamensProject.Core.Entity;
using FluentValidation.Results;

namespace EksamensProject.Core.ApplicationService.Implementation
{
    public class CompositionService : ICompositionService
    {
        private readonly ICompositionRepository _compositionRepository;

        public CompositionService(ICompositionRepository compositionRepository)
        {
            _compositionRepository = compositionRepository;
        }
        
        public Composition CreateNewComposition(string name, DateTime dateTime, double duration, Tempo tempo)
        {
            var newComposition = new Composition()
            {
                Name = name,
                Duration = duration,
                Year = dateTime,
                Tempo = tempo
            };
            _compositionRepository.Create(newComposition);
            return newComposition;
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