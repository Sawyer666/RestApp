using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using RestApp.Controllers;
using RestApp.Models;
using RestApp.Repositories.Implimintations;
using RestApp.Repositories.Interfaces;

namespace RestAppTests
{
    public class MainControllerTests
    {
        private Mock<IRegionRepository<Region>> _regionMockRepository;
        private MainController _mainController;

        [SetUp]
        public void Setup()
        {
            _regionMockRepository = new Mock<IRegionRepository<Region>>();
            _mainController = new MainController(_regionMockRepository.Object);
        }

        [Test]
        public async Task CreateRegion_ReturnsSuccess()
        {
            var expectedRegion = new Region()
            {
                Id = 1,
                Alias = "alias",
                FullName = "fullName"
            };

            _regionMockRepository.Setup(x => x.CreateRegion(It.IsAny<Region>())).ReturnsAsync(expectedRegion);

            var region = await Task.Run(() => _mainController.CreateRegion(expectedRegion));

            Assert.That(region.Result, Is.TypeOf<CreatedAtActionResult>());
            _regionMockRepository.Verify();
        }

        [Test]
        public async Task GetRegion_ReturnsNotNull()
        {
            var expectedRegion = CreateRegion();
            _regionMockRepository.Setup(x => x.GetRegion(It.IsAny<int>())).ReturnsAsync(expectedRegion);
            _regionMockRepository.Setup(x => x.DeleteRegion(It.IsAny<int>())).ReturnsAsync(expectedRegion);
            var region = await Task.Run(() => _mainController.GetRegion(expectedRegion.Id));
            Assert.AreEqual(expectedRegion, region.Value);
            _regionMockRepository.Verify();
        }

        [Test]
        public async Task RemoveRegion_ReturnsSuccess()
        {
            var expectedRegion = CreateRegion();
            _regionMockRepository.Setup(x => x.GetRegion(It.IsAny<int>())).ReturnsAsync(expectedRegion);
            _regionMockRepository.Setup(x => x.DeleteRegion(It.IsAny<int>())).ReturnsAsync(expectedRegion);
            var region = await Task.Run(() => _mainController.DeleteRegion(expectedRegion.Id));
            
            Assert.AreEqual(expectedRegion, region.Value);
            _regionMockRepository.Verify();
        }

        [Test]
        public async Task UpdateRegion_ReturnsSuccess()
        {
            var expectedRegion = CreateRegion();
            var updateRegion = new Region() {Id = expectedRegion.Id, Alias = "new alias", FullName = "new fullname"};
            _regionMockRepository.Setup(x => x.GetRegion(It.IsAny<int>())).ReturnsAsync(expectedRegion);
            _regionMockRepository.Setup(x => x.UpdateRegion(updateRegion)).ReturnsAsync(updateRegion);
            var region = await Task.Run(() => _mainController.UpdateRegion(updateRegion.Id, updateRegion));
            Assert.AreEqual(updateRegion, region.Value);
            _regionMockRepository.Verify();
        }

        private Region CreateRegion()
        {
            return new Region()
            {
                Id = 1,
                Alias = "alias",
                FullName = "fullName"
            };
        }
    }
}