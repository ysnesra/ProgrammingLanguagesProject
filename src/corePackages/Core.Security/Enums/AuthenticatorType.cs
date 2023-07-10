namespace Core.Security.Enums;

public enum AuthenticatorType
{
    None = 0,   //Email-Password ile
    Email = 1,  //Email platformu üzerinden
    Otp = 2     //Otp platformu üzerinden
}