using ApiCorePet.Criptografar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCorePet.Controllers
{
    public class ValidarUsuario
    {

        private Model.veterinarioserviceContext dbContext;
        public ValidarUsuario(Model.veterinarioserviceContext context)
        {
            dbContext = context;
        }
        public bool? TestarSenha(string _senha, string _email)
        {
            try
            {
                // string senhaCriptografada;
                
                string senha = dbContext.Autenticacao.Find(_email).Senha;
                Senha password = new Senha();
                //senhaCriptografada = password.ComparePassword(_senha);

                return password.ComparePassword(senha, _senha);
            }
            catch (ArgumentNullException e)
            {
                throw;
            }
            catch (InvalidOperationException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                throw;
            }
            catch (System.NullReferenceException e)
            {
                throw;
            }
        }
    }
}
