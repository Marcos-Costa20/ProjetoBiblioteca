using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult ListaDeUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            return View(new UsuarioService().Listar());
        }

        public IActionResult editarUsuario(int id)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            return View(new UsuarioService().buscarPorId(id));
        }

        [HttpPost]
        public IActionResult editarUsuario(Usuario userEditado)
        {

            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            new UsuarioService().editarUsuario(userEditado);
            return RedirectToAction("ListaDeUsuarios");
        }

        public IActionResult RegistrarUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarUsuarios(Usuario novoUser)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            novoUser.senha = Criptografo.TextoCriptografado(novoUser.senha);
            new UsuarioService().incluirUsuario(novoUser);


            return RedirectToAction("cadastroRealizado");

        }

        public IActionResult ExcluirUsuario(int id)
        {

            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            new UsuarioService().excluirUsuario(id);
             return RedirectToAction("ListaDeUsuarios");
        }

        [HttpPost]
        public IActionResult ExcluirUsuario(string decisao,int id)
        {
            if(decisao=="Excluir")
            {
                ViewData["Mensagem"] = "Exclusão do usuario "+new UsuarioService().buscarPorId(id).Nome+"realizada com sucesso";
                new UsuarioService().excluirUsuario(id);
                return View("ListaDeUsuarios",new UsuarioService().Listar());
            }
            else
            {
                ViewData["Mensagem"] = "Exclusão do cancelada";
                return View("ListaDeUsuarios",new UsuarioService().Listar());
            }
            
        }

        public IActionResult cadastroRealizado()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            return View();
        }

        public IActionResult NeedAdmin()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }


    }
}