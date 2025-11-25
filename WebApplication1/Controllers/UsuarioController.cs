using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class UsuarioController : Controller
    {
        Sistema miSistema = Sistema.Instancia;

        public IActionResult Index()
        {
            return View();
        }

        // ============================
        // VER PERFIL EMPLEADO
        // ============================
        [HttpGet]
        public IActionResult VerPerfil()
        {
            string email = HttpContext.Session.GetString("LogueadoEmail");

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Autorizacion");
            }

            // Buscar usuario SIN LINQ
            Usuario usuario = null;
            foreach (Usuario u in miSistema.Usuarios)
            {
                if (u.Email.ToLower() == email.ToLower())
                {
                    usuario = u;
                    break;
                }
            }

            // Saco sus pagos del mes
            List<Pago> pagosMes = miSistema.ObtenerPagosDelMesPorEmail(email);
            double totalMes = miSistema.TotalGastado(email);

            ViewBag.Usuario = usuario;
            ViewBag.Pagos = pagosMes;
            ViewBag.TotalMes = totalMes;

            return View("PerfilEmpleado");
        }

        // ============================
        // GET: CARGAR PAGO
        // ============================
        [HttpGet]
        public IActionResult CargarPago()
        {
            ViewBag.Gastos = miSistema.Gastos;
            ViewBag.Metodos = Enum.GetValues(typeof(Pago.MetodoPago));
            return View();
        }

        // ============================
        // POST: CARGAR PAGO
        // ============================
        [HttpPost]
        public IActionResult CargarPago(
            string tipoPago,
            string gastoNombre,
            string metodo,
            double monto,
            DateTime? fechaUnico,
            DateTime? fechaInicio,
            DateTime? fechaFin
        )
        {
            string email = HttpContext.Session.GetString("LogueadoEmail");

            // Buscar usuario SIN LINQ
            Usuario user = null;
            foreach (Usuario u in miSistema.Usuarios)
            {
                if (u.Email.ToLower() == email.ToLower())
                {
                    user = u;
                    break;
                }
            }

            // Buscar gasto SIN LINQ
            TipoGasto gasto = null;
            foreach (TipoGasto g in miSistema.Gastos)
            {
                if (g.Nombre.ToLower() == gastoNombre.ToLower())
                {
                    gasto = g;
                    break;
                }
            }

            // Validaciones básicas
            if (user == null || gasto == null || monto <= 0)
            {
                ViewBag.Error = "Datos inválidos. Revise los campos.";
                ViewBag.Gastos = miSistema.Gastos;
                ViewBag.Metodos = Enum.GetValues(typeof(Pago.MetodoPago));
                return View();
            }

            Pago.MetodoPago metodoEnum =
                (Pago.MetodoPago)Enum.Parse(typeof(Pago.MetodoPago), metodo);

            // === PAGO ÚNICO ===
            if (tipoPago == "U")
            {
                if (fechaUnico == null)
                {
                    ViewBag.Error = "Debe ingresar la fecha del pago único.";
                    ViewBag.Gastos = miSistema.Gastos;
                    ViewBag.Metodos = Enum.GetValues(typeof(Pago.MetodoPago));
                    return View();
                }

                Pago nuevo = new Unico(
                    metodoEnum,
                    gasto,
                    user,
                    "Pago único generado desde la web",
                    monto,
                    fechaUnico.Value,
                    miSistema.Pagos.Count + 1
                );

                miSistema.AgregarPago(nuevo);
            }
            // === PAGO RECURRENTE ===
            else if (tipoPago == "R")
            {
                if (fechaInicio == null)
                {
                    ViewBag.Error = "Debe ingresar la fecha de inicio.";
                    ViewBag.Gastos = miSistema.Gastos;
                    ViewBag.Metodos = Enum.GetValues(typeof(Pago.MetodoPago));
                    return View();
                }

                Pago nuevo;

                if (fechaFin == null)
                {
                    nuevo = new Recurrente(
                        metodoEnum,
                        gasto,
                        user,
                        "Pago recurrente generado desde la web",
                        monto,
                        fechaInicio.Value
                    );
                }
                else
                {
                    nuevo = new Recurrente(
                        metodoEnum,
                        gasto,
                        user,
                        "Pago recurrente generado desde la web",
                        monto,
                        fechaInicio.Value,
                        fechaFin.Value
                    );
                }

                miSistema.AgregarPago(nuevo);
            }
            else
            {
                ViewBag.Error = "Tipo de pago inválido.";
                return View();
            }

            return RedirectToAction("VerPerfil");
        }

        // ============================
        // VER PAGOS DEL EMPLEADO
        // ============================
        public IActionResult VerPagos()
        {
            string email = HttpContext.Session.GetString("LogueadoEmail");
            if (email == null)
            {
                return RedirectToAction("Login", "Autorizacion");
            }

            List<Pago> pagosMes = miSistema.ObtenerPagosDelMesPorEmail(email);
            ViewBag.Pagos = pagosMes;

            return View("VerPagos");
        }
    }
}
