using Framework.Domain.Core;

namespace UserManagement.Core.Domains;

public class Clinic:Entity<int>
{
    protected Clinic()
    {
        
    }
    public Clinic(string name,IEnumerable<ClinicAddress> clinicAddresses)
    {
        Name = name;
        _clinicAddresses.Update(clinicAddresses.ToList());
    }

    public void Update(string name, IEnumerable<ClinicAddress> clinicAddresses)
    {
        Name = name;
        _clinicAddresses.Update(clinicAddresses.ToList());
    }

    public string Name { get; private set; }
    private readonly List<ClinicAddress> _clinicAddresses = new ();
    public IReadOnlyCollection<ClinicAddress> ClinicAddresses => _clinicAddresses;
}