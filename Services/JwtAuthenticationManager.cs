using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListProject
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        //Création de user test
        private readonly IDictionary<string, string> users = new Dictionary<string, string>
        { {"test1", "password1"}, {"test2", "password2" } };
        private readonly string key;

        public JwtAuthenticationManager(string key)
        {
            this.key = key;
        }
        //Fonction pour connecter l'utilisateur
        public string Authenticate(string username, string password)
        {
            if(!users.Any(u => u.Key == username && u.Value == password))
            {
                return null;
            }

            //Création du token handler via l'extense JwT
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            //Création du token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                //1h de vie pour le token
                Expires = DateTime.UtcNow.AddHours(1),
                //Signature du token
                SigningCredentials = new SigningCredentials(
                                         new SymmetricSecurityKey(tokenKey),
                                         SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
