using System;

namespace GraphMediator.Stubs
{
    public class OneCarePatient
    {
        internal readonly int _id;
        internal readonly DateTime _birthDate;
        internal readonly Gender _gender;

        public OneCarePatient(int Id, DateTime BirthDate, Gender Gender)
        {
            _id = Id;
            _birthDate = BirthDate;
            _gender = Gender;
        }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
