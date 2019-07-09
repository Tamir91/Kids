using System;

[Serializable]
public class Admin
{

    public string UserName;
    public string UserPassword;

    public Admin(string userName, string userPassword)
    {
        UserName = userName;
        UserPassword = userPassword;
    }

    override
    public string ToString() => UserName + " " + UserPassword;
}
