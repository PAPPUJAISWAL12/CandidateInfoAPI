using System;
using System.Collections.Generic;

namespace CandidateInfoAPI.Models;

public partial class CandidateInformation
{
    public long CandidateId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string PreferredTime { get; set; } = null!;

    public string? LinkedInProfileUrl { get; set; }

    public string? GitHubProfileUrl { get; set; }

    public string Comments { get; set; } = null!;
}
