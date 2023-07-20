using Gss.Application.Common.Interfaces.Authentication;

namespace Gss.Application.Services.Authentication;

public class AuthenticationService : IAuthenticatioService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator ;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        //check if user already exists 

        // Create user (generate token)


        //Create JWT token 
        Guid userId = Guid.NewGuid();
        var Token = _jwtTokenGenerator.GenerateToken(userId, firstName, lastName);
       
        return new AuthenticationResult(
        userId,
        firstName,
        lastName,
        email,
        Token);


    }

    public AuthenticationResult Login(string email, string password)
    {

        return new AuthenticationResult(
            Guid.NewGuid(),
            "firstName",
            "lastName",
             email,
            "sabbat" //TODO: Generate token here

           );
    }

   
}
