namespace Gss.Application.Services.Authentication;

public interface IAuthenticatioService {
    AuthenticationResult Register (string FirstName,string LastName,string Email,string Password );
    AuthenticationResult Login (string Email,string Password);

}