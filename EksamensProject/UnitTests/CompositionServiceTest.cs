using System;
using System.IO;
using EksamensProject.Core.ApplicationService.Implementation;
using EksamensProject.Core.DomainService;
using EksamensProject.Core.Entity;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace UnitTests
{
    public class CompositionServiceTest
    {
        
        private readonly CompositionValidator _validator = new CompositionValidator();

        
        [Fact]
        public void CreateNullCompositionThrowsException()
        {
            var compositionRepo = new Mock<ICompositionRepository>();

            var service = new CompositionService(compositionRepo.Object);
            
            Exception ex = Assert.Throws<InvalidDataException>(() => 
                service.CreateComposition(null));
            Assert.Equal("Composition cannot be null", ex.Message);
        }

        [Fact]
        public void CreateCompositionWithNameMissing()
        {
            var compositionRepo = new Mock<ICompositionRepository>();
            var service = new CompositionService(compositionRepo.Object);

            var composition = service.CreateNewComposition("", "1720", 2.2, new Tempo(), new Style());
            
            var result = _validator.TestValidate(composition);

            result.ShouldHaveValidationErrorFor(c => c.Name);
        }
        
        [Fact]
        public void CreateCompositionWithYearMissing()
        {
            var compositionRepo = new Mock<ICompositionRepository>();
            var service = new CompositionService(compositionRepo.Object);

            var composition = service.CreateNewComposition("Vivaldi", "", 2.2, new Tempo(), new Style());
            
            var result = _validator.TestValidate(composition);

            result.ShouldHaveValidationErrorFor(c => c.Year);
        }
        
        [Fact]
        public void CreateCompositionWithDurationMissing()
        {
            var compositionRepo = new Mock<ICompositionRepository>();
            var service = new CompositionService(compositionRepo.Object);
         
            var composition = new Composition()
            {
                Name = "Vivaldi",
                Tempo = new Tempo(),
                Year = "1720",
                Comment = "Yay",
                Style = new Style(),
                URL = "url",
                PictureURL = "url"
            };

            service.CreateComposition(composition);
                     
            var result = _validator.TestValidate(composition);
         
            result.ShouldHaveValidationErrorFor(c => c.Duration);
        }
        
        [Fact]
        public void CreateCompositionWithNullTempo()
        {
            var compositionRepo = new Mock<ICompositionRepository>();
            var service = new CompositionService(compositionRepo.Object);
         
            var composition = service.CreateNewComposition("Vivaldi", "1720", 2.2, null, new Style());
                     
            var result = _validator.TestValidate(composition);
         
            result.ShouldHaveValidationErrorFor(c => c.Tempo);
        }
        
        [Fact]
        public void CreateCompositionWithNullStyle()
        {
            var compositionRepo = new Mock<ICompositionRepository>();
            var service = new CompositionService(compositionRepo.Object);
         
            var composition = service.CreateNewComposition("Vivaldi", "1720", 2.2, new Tempo(), null);
                     
            var result = _validator.TestValidate(composition);
         
            result.ShouldHaveValidationErrorFor(c => c.Style);
        }
        
        [Fact]
        public void FindCompositionByIdCompositionFound()
        {
            var compositionRepo = new Mock<ICompositionRepository>();
            var service = new CompositionService(compositionRepo.Object);
            
            var composition = new Composition(){Id = 1};
            
            compositionRepo.Setup(x => x.ReadById(It.IsAny<int>()))
                .Returns(composition);

            var output = service.FindCompositionById(1);
            Assert.True(composition.Equals(output));
        }
        
        [Fact]
        public void FindCompositionByIdCompositionNotFound()
        {
            var compositionRepo = new Mock<ICompositionRepository>();
            var service = new CompositionService(compositionRepo.Object);

            Exception ex = Assert.Throws<InvalidDataException>(() =>
                service.FindCompositionById(1));
            
            Assert.Equal("Composition not found", ex.Message);
        }
        
        [Fact]
        public void DeleteRequestById()
        {
            var compositionRepo = new Mock<ICompositionRepository>();
            var service = new CompositionService(compositionRepo.Object);

            compositionRepo.Setup(x => x.ReadById(It.IsAny<int>()))
                .Returns(new Composition(){Id = 1});
            
            var composition = service.CreateNewComposition("Vivaldi", "", 2.2, new Tempo(), new Style());
               
            compositionRepo.Setup(x => x.ReadById(It.IsAny<int>()))
                .Returns(composition);
            
            compositionRepo.Setup(r => r.Delete(It.IsAny<int>()));
            
            service.Delete(composition.Id);
            
            compositionRepo.Verify(r => r.Delete(composition.Id));

        }
        
        
        

    }
}