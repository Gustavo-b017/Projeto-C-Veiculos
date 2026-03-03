using LojaVeiculos.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;

namespace LojaVeiculos.Controllers
{
    public class AdminController : Controller
    {
        //Método get  que exibe a pagina do login para o usuario
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        //Método post - recebe as credenciais enviadas pelo formulario do login
        [HttpPost]
        public async Task<IActionResult> Login(string usuario, string senha)
        {
            if(usuario =="Admin" && senha =="123456")
            {
                //Cria uma lista de Claims(declarações) sobre o usuario logado
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,"Gerente")
                };
                //define a identidade do usuario com base nas claims e no esquema de autenticação por cookies
                var identidade = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //Realiza o SignIn gerando o cooke de sessão no navegador do usuario.
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identidade));

                //após logar com sucesso , redireciona o gerente para o dasboard
                return RedirectToAction("Dashboard");
            }
            ViewBag.Erro = "Usuario/ Senha Inválidos";
            return View();
        }

        //cria uma lista estática para armazenar os interesses de clientes enqaunto a página
        //está rodando
        private static List<Interesse> _interesses = new List<Interesse>();

        public IActionResult SalvarInteresse(string nome, string telefone, string veiculo,double parcela)
        {
            //instancia um novo objeto da classe interesse com os dados recebidos do form
            var novo = new Interesse
            {
                Nome = nome,
                Telefone = telefone,
                VeiculoInteresse = veiculo,
                ValorParcela = parcela,
            };
            //adiciona o novo interesse a tabela em memoria
            _interesses.Add(novo);
            return View();
        }

        //Método que exibe o Dashboard , o autoriza impede que pessoas que não estão logadas
        //acesse esta rota
        [Authorize]
        public IActionResult Dashboard()
        {
            //retorna a lista de interesses 
            return View(_interesses);
        }
        public async Task<IActionResult> Logout()
        {
            //Remove o cookie de autenticação do navegador do usuario
            await HttpContext.SignOutAsync();
            //redireciona o usuario para a tela de login
            return RedirectToAction("Login");
        }
        

    }
}
