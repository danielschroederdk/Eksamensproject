using System.Collections.Generic;
using System.IO;
using System.Linq;
using EksamensProject.Core.ApplicationService;
using EksamensProject.Core.DomainService;
using EksamensProject.Core.Entity;

namespace EksamensProject.Core.ApplicationService.Implementation
{
    public class StyleService : IStyleService
    {
        private readonly IStylesRepository _stylesRepository;

        public StyleService(IStylesRepository stylesRepository)
        {
            _stylesRepository = stylesRepository;
        }

        public List<Style> GetStyles()
        {
            return _stylesRepository.ReadAll().ToList();
        }

        public Style FindStyleById(int id)
        {
            return _stylesRepository.ReadById(id) == null
                ? throw new InvalidDataException("Request not found")
                : _stylesRepository.ReadById(id);
        }
    }
}