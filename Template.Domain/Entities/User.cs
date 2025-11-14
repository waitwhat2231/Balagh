using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Domain.Entities;

public class User : IdentityUser
{
    public int GovernmentalEntityId { get; set; }
    public GovernmentalEntity GovernmentalEntity { get; set; } = default!;
    public List<Complaint> Complaints { get; set; } = [];
    public List<Device> Devices { get; set; } = [];
    public List<Note> Notes { get; set; } = [];
    public List<History> Histories { get; set; } = [];
    public bool IsDeleted { get; set; }
}
