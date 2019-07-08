using System;

[Serializable]
public class Person
{
    public string FirstName;
    public string SecondName;
    public string Age;
    public string Gender;
    public string PhoneNumber;
    public string Email;
    private Person newPerson;

    public Person(Person newPerson)
    {
        FirstName = newPerson.FirstName;
        SecondName = newPerson.SecondName;
        Age = newPerson.Age;
        Gender = newPerson.Gender;
        PhoneNumber = newPerson.PhoneNumber;
        Email = newPerson.Email;
    }

    public Person(string firstName, string secondName, string age, string gender, string phoneNumber, string email)
    {
        FirstName = firstName;
        SecondName = secondName;
        Age = age;
        Gender = gender;
        PhoneNumber = phoneNumber;
        Email = email;
    }

    override
    public string ToString() => FirstName + " " + SecondName + " " + Age + " " + Gender  + " " + PhoneNumber + " " + Email;
}


