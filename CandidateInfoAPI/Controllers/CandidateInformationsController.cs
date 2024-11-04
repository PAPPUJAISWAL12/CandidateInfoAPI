using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CandidateInfoAPI.Models;

namespace CandidateInfoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateInformationsController : ControllerBase
    {
        private readonly CandidateDataContext _context;


        public CandidateInformationsController(CandidateDataContext context)
        {
            _context = context;
        }

        // GET: api/CandidateInformations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidateInformation>>> GetCandidateInformations()
        {
            return await _context.CandidateInformations.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<CandidateInformation>> PostCandidateInformation(CandidateInformation candidateInformation)
        {
            var candidate = _context.CandidateInformations.Where(x => x.EmailAddress == candidateInformation.EmailAddress).FirstOrDefault();
            if(candidate == null)
            {
                long id = _context.CandidateInformations.Any() ? _context.CandidateInformations.Max(x=>x.CandidateId)+1 : 1;
                candidateInformation.CandidateId = id;
                _context.CandidateInformations.Add(candidateInformation);
                await _context.SaveChangesAsync();
                return Ok("Candidate information added successfully");

            }
            else
            {
                candidate.EmailAddress = candidateInformation.EmailAddress;
                candidate.FirstName = candidateInformation.FirstName;
                candidate.LastName = candidateInformation.LastName;
                candidate.PhoneNumber = candidateInformation.PhoneNumber;                
                candidate.Comments = candidateInformation.Comments;
                candidate.GitHubProfileUrl = candidateInformation.GitHubProfileUrl;
                candidate.LinkedInProfileUrl = candidateInformation.LinkedInProfileUrl;
                _context.CandidateInformations.Update(candidate);
                await _context.SaveChangesAsync();
                return Ok("Candidate information updated successfully.");
            }
            
           
        }       

    }
}
