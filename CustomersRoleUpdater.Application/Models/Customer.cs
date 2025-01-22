using System.Security.Principal;

namespace CustomersRoleUpdater.Application.Models;

public class Customer
{
    public Guid Id { get; set; }
    public Role Role { get; set; }
}
