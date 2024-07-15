namespace GraphMediator.Stubs;

public record OneCarePatient(int Id, DateTime BirthDate,Gender Gender);

public enum Gender
{
    Male,
    Female
}
