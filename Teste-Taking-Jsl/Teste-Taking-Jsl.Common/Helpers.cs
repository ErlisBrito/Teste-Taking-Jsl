using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Teste_Taking_Jsl.Common
{
    public static class Helpers
    {
        const string _chave = "abcdefghijklmnopqrstBRB-VIAGENSuvwxyzABCDEFGHIJKLMNOPQKRSTUVWXYZ1234567890@#$%";
        const string expressRegularFone = @"^\(?[1-9]{2}\)? ?(?:[2-8]|9[1-9])[0-9]{3}\-?[0-9]{4}$";
        const string email = "brb-solucoes@outlook.com.br";
        const string password = "BrbSolucoesTr@de&2022";


        public static string CriarSenhaAleatoria(int caracter)
        {
            const string caracteres = _chave;
            var novaSenha = new StringBuilder();
            var rnd = new Random();
            for (var i = 0; i < caracter; i++)
            {
                var escolhido = rnd.Next(caracteres.Length - 1);
                novaSenha = novaSenha.Append(caracteres[escolhido]);
            }

            return novaSenha.ToString();
        }

        public static bool ValidarEmail(string email)
        {
            var emailAdress = new EmailAddressAttribute();
            return emailAdress.IsValid(email);
        }

        public static bool ValidarTelefone(string number)
        {
            return Regex.Match(number, expressRegularFone).Success;
        }

    }
}