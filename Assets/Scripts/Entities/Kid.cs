using System;

[Serializable]
public class Kid
{
    public string FirstName;
    public string SecondName;
    public string Age;
    public string Gender;
    public string PhoneNumber;
    public string Email;

    public Kid(Kid Kid)
    {
        FirstName = Kid.FirstName;
        SecondName = Kid.SecondName;
        Age = Kid.Age;
        Gender = Kid.Gender;
        PhoneNumber = Kid.PhoneNumber;
        Email = Kid.Email;
    }

    public Kid(string firstName, string secondName, string age, string gender, string phoneNumber, string email)
    {
        FirstName = firstName;
        SecondName = secondName;
        Age = age;
        Gender = gender;
        PhoneNumber = phoneNumber;
        Email = email;
    }

    override
    public string ToString() => Email + "\t" + PhoneNumber + "\t\t" + Gender + "\t\t" + Age + "\t\t\t" + SecondName + "\t\t" + FirstName;
}


