using Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class AutorizacionController : Controller
    {
        Sistema miSistema = Sistema.Instancia;
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string contrasenia)
        {
            Usuario usuario = miSistema.Login(email, contrasenia);

            if (usuario == null)
            {
                ViewBag.msg = "Email o contraseña incorrectos";
                return View();
            }

            HttpContext.Session.SetString("LogueadoEmail", usuario.Email);
            HttpContext.Session.SetString("LogueadoNombre", usuario.Nombre);
            HttpContext.Session.SetString("LogueadoRol", usuario.Rol());

            return RedirectToAction("Index", "Home");
        }



        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
