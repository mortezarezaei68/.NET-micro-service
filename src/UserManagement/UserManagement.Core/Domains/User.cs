using Framework.Domain.Core;

namespace UserManagement.Core.Domains;

public class User : AggregateRoot<int>
{
    private readonly List<UserRole> _userRoles = new();
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles;

    private readonly List<Clinic> _clinics = new();
    public IReadOnlyCollection<Clinic> Clinics => _clinics;
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public UserType UserType { get; private set; }

    public UserGenderType GenderType { get; private set; }

    public static User Add(UserGenderType genderType, string lastName, string firstName, UserType userType)
    {
        return new User
        {
            GenderType = genderType,
            LastName = lastName,
            FirstName = firstName,
            UserType = userType
        };
    }

    public void UpdateUserRole(IEnumerable<int> roleId)
    {
        var roles = roleId.Select(a => new UserRole(a)).ToList();
        _userRoles.Update(roles);
    }

    public void AddClinic(string name, IEnumerable<ClinicAddressOption> clinicAddressOptions)
    {
        var clinicAddress = clinicAddressOptions.Select(a => new ClinicAddress(a.Address, a.Street));
        _clinics.Add(new Clinic(name, clinicAddress));
    }

    public void UpdateClinic(int id, string name, IEnumerable<ClinicAddressOption> clinicAddressOptions)
    {
        var clinic = _clinics.FirstOrDefault(a => a.Id == id);
        if (clinic is null)
            throw new Exception("clinic is not exist");


        var clinicAddress = clinicAddressOptions.Select(a => new ClinicAddress(a.Address, a.Street));
        clinic.Update(name, clinicAddress);
    }
}