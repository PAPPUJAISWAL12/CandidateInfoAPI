using CandidateInfoAPI.Controllers;
using CandidateInfoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTest.Controllers
{
    public class CandidateInforamationsTestController
    {
        [Fact]
        public async void CandidateController_AddOrUpdate()
        {
            //Arrange
            var candidates = new List<CandidateInformation>().AsQueryable();
            var moqSet = new Mock<DbSet<CandidateInformation>>();
            moqSet.As<IQueryable<CandidateInformation>>().Setup(m => m.Provider).Returns(candidates.Provider);
            moqSet.As<IQueryable<CandidateInformation>>().Setup(m => m.Expression).Returns(candidates.Expression);
            moqSet.As<IQueryable<CandidateInformation>>().Setup(m => m.ElementType).Returns(candidates.ElementType);
            moqSet.As<IQueryable<CandidateInformation>>().Setup(m => m.GetEnumerator()).Returns(candidates.GetEnumerator());

            var mockContext = new Mock<CandidateDataContext>();
            mockContext.Setup(x => x.CandidateInformations).Returns(moqSet.Object);
            CandidateInformationsController controller = new CandidateInformationsController(mockContext.Object);

           
            var newCandidate = new CandidateInformation
            {
                CandidateId = 2,
                FirstName = "pappu",
                LastName = "jaiswal",
                EmailAddress = "pappujaiswal186@gmail.com",
                PhoneNumber = "+9779815310274",
                Comments = "Experienced in .NET",
                GitHubProfileUrl = "https://github.com/PAPPUJAISWAL12",
                LinkedInProfileUrl = "https://www.linkedin.com/in/pappu-jaiswal-7b4789201/"
            };
            //Act 
            var result = await controller.PostCandidateInformation(newCandidate);

            //Assert
            Assert.Equal("Candidate information added successfully", (result.Result as OkObjectResult)?.Value);

            // Verify that Add and SaveChanges were called once
            moqSet.Verify(m => m.Add(It.IsAny<CandidateInformation>()), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }
    }
}
