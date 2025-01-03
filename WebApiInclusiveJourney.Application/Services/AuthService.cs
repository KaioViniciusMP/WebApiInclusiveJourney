﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiInclusiveJourney.Application.DTO.Request;
using WebApiInclusiveJourney.Application.DTO.Response;
using WebApiInclusiveJourney.Application.IServices;
using WebApiInclusiveJourney.Repository;
using WebApiInclusiveJourney.Repository.Models;

namespace WebApiInclusiveJourney.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly WebApiInclusiveJourneyContext _ctx;

        public AuthService(WebApiInclusiveJourneyContext context)
        {
            _ctx = context;
        }

        public AuthResponse Authentication(AuthRequest request)
        {
            TabPerson usuario = _ctx.tabPerson.FirstOrDefault(x =>  x.Password == request.password);
            if (usuario != null)
            {
                string tokenString = GerarTokenJwt(usuario);
                return new AuthResponse
                {
                    //token = tokenString,
                    userCode = usuario.Codigo,
                    //Nome = usuario.nome,
                    //Usuario = usuario.usuario
                };
            }
            return null;
        }

        public string GerarTokenJwt(TabPerson usuario)
        {
            var issuer = "var"; //Quem esta emitindo o token
            var audience = "var"; //Pra quem vai liberar o acesso
            var key = "8fab334a-5e6f-4d7c-8d8c-c35241836bd6"; //é uma chave secreta (atraves do gerador de gui on-line)
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));//pegou a "key", transformou em um bite que é oq o SymetricSecurityKey espera , e colocou dentro da securityKey
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);//Pega o securityKey, e fala que é uma credencial de identificação, e diz que é do modelo de criptografia "HmacSha256"

            var claims = new[]
            {
                new System.Security.Claims.Claim("Codigo",usuario.Codigo.ToString()) // na hora de gerar o token, guarda aqui que quem gerou o token foi esse "usuarioId", e o id do usuario, foi o que buscou no banco (usuario.id.ToString())
                // Um Claim contém uma informação específica sobre o usuário, como o nome, email, permissões de acesso, dentre outras. Essas informações podem ser utilizadas para verificar se o usuário tem as permissões necessárias para acessar determinado recurso ou funcionalidade do sistema.
            };

            var token = new JwtSecurityToken(issuer: issuer, claims: claims, audience: audience, expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials); // Essa é a linha onde passamos parametros para passar o token
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }


        public bool ForgotPassword(ForgotPasswordRequest request)
        {
            try
            {
                TabPerson usuario = _ctx.tabPerson.FirstOrDefault(x => x.Email == request.email);

                if (usuario != null)
                {
                    usuario.Password = request.newPassword; 

                    _ctx.tabPerson.Update(usuario);
                    _ctx.SaveChanges();

                    return true;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
