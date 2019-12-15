using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EksamensProject.Core.DomainService;
using EksamensProject.Core.Entity;

namespace EksamensProject.Core.ApplicationService.Implementation
{
    public class CompositionService : ICompositionService
    {
        readonly ICompositionRepository _compositionRepository;

        public CompositionService(ICompositionRepository compositionRepository)
        {
            _compositionRepository = compositionRepository;
        }
        
        public Composition CreateNewComposition(string name, String year, double duration, Tempo tempo, Style style)
        {
            var newComposition = new Composition()
            {
                Name = name,
                Duration = duration,
                Year = year,
                Tempo = tempo,
                Style = style
            };
                
            _compositionRepository.Create(newComposition);
            return newComposition;
        }

        public Composition CreateComposition(Composition composition)
        {
            if (composition == null)
            {
                throw new InvalidDataException("Composition cannot be null");
            }
            return _compositionRepository.Create(composition);
        }

        public Composition FindCompositionById(int id)
        {
            return _compositionRepository.ReadById(id) == null
                ? throw new InvalidDataException("Composition not found")
                : _compositionRepository.ReadById(id);        
        }

        public Composition Delete(int id)
        {
            return FindCompositionById(id) == null
                ? throw new InvalidDataException("User not found or already deleted")
                : _compositionRepository.Delete(id);
        }

        public Composition UpdateComposition(Composition compositionUpdate)
        {
            return _compositionRepository.Update(compositionUpdate);
        }

        public List<Composition> GetCompositions()
        {
            return _compositionRepository.ReadAll().ToList();
        }
    }
}