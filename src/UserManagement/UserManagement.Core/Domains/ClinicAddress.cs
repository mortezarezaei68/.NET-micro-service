using Framework.Domain.Core;

namespace UserManagement.Core.Domains;

public class ClinicAddress: ValueObject
{
    public ClinicAddress(string address, string street)
    {
        Address = address;
        Street = street;
    }

    public string Street { get; private set; }
    public string Address { get; private set; }
}