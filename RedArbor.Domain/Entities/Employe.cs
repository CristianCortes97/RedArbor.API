using System;
using System.Collections.Generic;

namespace RedArbor.Domain.Entities;

public partial class Employe
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? DeleteOn { get; set; }

    public string Email { get; set; } = null!;

    public string? Fax { get; set; }

    public string? Name { get; set; }

    public DateTime? Lastlogin { get; set; }

    public string Password { get; set; } = null!;

    public int PortalId { get; set; }

    public int RoleId { get; set; }

    public int StatusId { get; set; }

    public string? Telephone { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public DateTime? DeletedOn { get; set; }

    public string Username { get; set; } = null!;
}
