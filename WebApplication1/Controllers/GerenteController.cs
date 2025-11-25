using Dominio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

public class GerenteController : Controller
{
    private Sistema miSistema = Sistema.Instancia;

    // =============================
    // PERFIL GERENTE
    // =============================
    [HttpGet]
    public IActionResult PerfilGerente()
    {
        string email = HttpContext.Session.GetString("LogueadoEmail");
        string rol = HttpContext.Session.GetString("LogueadoRol");

        if (string.IsNullOrEmpty(email))
            return RedirectToAction("Login", "Autorizacion");

        if (rol != "Gerente")
            return RedirectToAction("Index", "Home");

        // Buscar gerente sin LINQ
        Gerente gerente = null;
        foreach (Usuario u in miSistema.Usuarios)
        {
            if (u is Gerente && u.Email.ToLower() == email.ToLower())
            {
                gerente = (Gerente)u;
                break;
            }
        }

        if (gerente == null)
            return RedirectToAction("Login", "Autorizacion");

        // Pagos del mes
        List<Pago> pagosMes = miSistema.ObtenerPagosDelMesPorEmail(email);
        double totalMes = miSistema.TotalGastado(email);

        // Lista de miembros del equipo SIN LINQ
        List<Usuario> miembros = new List<Usuario>();

        foreach (Usuario u in miSistema.Usuarios)
        {
            if (u.Equipo == gerente.Equipo && u.Email.ToLower() != email.ToLower())
            {
                miembros.Add(u);
            }
        }

        // Ordenar la lista por email ascendente (burbuja, cero LINQ)
        for (int i = 0; i < miembros.Count - 1; i++)
        {
            for (int j = 0; j < miembros.Count - i - 1; j++)
            {
                if (string.Compare(miembros[j].Email, miembros[j + 1].Email, true) > 0)
                {
                    Usuario aux = miembros[j];
                    miembros[j] = miembros[j + 1];
                    miembros[j + 1] = aux;
                }
            }
        }

        ViewBag.Gerente = gerente;
        ViewBag.TotalMes = totalMes;
        ViewBag.Miembros = miembros;

        return View();
    }

    // =============================
    // VER PAGOS DEL GERENTE
    // =============================
    [HttpGet]
    public IActionResult VerPagosGerente()
    {
        string email = HttpContext.Session.GetString("LogueadoEmail");
        string rol = HttpContext.Session.GetString("LogueadoRol");

        if (string.IsNullOrEmpty(email))
            return RedirectToAction("Login", "Autorizacion");

        if (rol != "Gerente")
            return RedirectToAction("Index", "Home");

        List<Pago> pagos = miSistema.ObtenerPagosDelMesPorEmail(email);
        double totalMes = miSistema.TotalGastado(email);

        ViewBag.Pagos = pagos;
        ViewBag.TotalMes = totalMes;

        return View();
    }

    // =============================
    // CARGAR TIPO GASTO
    // =============================
    [HttpGet]
    public IActionResult CargarTipoGasto()
    {
        string email = HttpContext.Session.GetString("LogueadoEmail");
        string rol = HttpContext.Session.GetString("LogueadoRol");

        if (string.IsNullOrEmpty(email))
            return RedirectToAction("Login", "Autorizacion");

        if (rol != "Gerente")
            return RedirectToAction("Index", "Home");

        return View();
    }

    [HttpPost]
    public IActionResult CargarTipoGasto(string nombre, string descripcion)
    {
        string email = HttpContext.Session.GetString("LogueadoEmail");
        string rol = HttpContext.Session.GetString("LogueadoRol");

        if (string.IsNullOrEmpty(email))
            return RedirectToAction("Login", "Autorizacion");

        if (rol != "Gerente")
            return RedirectToAction("Index", "Home");

        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(descripcion))
        {
            ViewBag.Error = "Nombre y descripción son obligatorios.";
            return View();
        }

        // Buscar duplicado sin LINQ
        bool existe = false;
        foreach (TipoGasto tg in miSistema.Gastos)
        {
            if (tg.Nombre.ToLower() == nombre.ToLower())
            {
                existe = true;
                break;
            }
        }

        if (existe)
        {
            ViewBag.Error = "Ya existe un tipo de gasto con ese nombre.";
            return View();
        }

        miSistema.AgregarTipoGasto(new TipoGasto(nombre, descripcion));
        ViewBag.Mensaje = "Tipo de gasto agregado correctamente.";

        return View();
    }

    // =============================
    // ELIMINAR TIPO GASTO
    // =============================
    [HttpGet]
    public IActionResult EliminarTipoGasto()
    {
        string email = HttpContext.Session.GetString("LogueadoEmail");
        string rol = HttpContext.Session.GetString("LogueadoRol");

        if (string.IsNullOrEmpty(email))
            return RedirectToAction("Login", "Autorizacion");

        if (rol != "Gerente")
            return RedirectToAction("Index", "Home");

        ViewBag.Gastos = miSistema.Gastos;
        return View();
    }

    [HttpPost]
    public IActionResult EliminarTipoGasto(string nombreGasto)
    {
        string email = HttpContext.Session.GetString("LogueadoEmail");
        string rol = HttpContext.Session.GetString("LogueadoRol");

        if (string.IsNullOrEmpty(email))
            return RedirectToAction("Login", "Autorizacion");

        if (rol != "Gerente")
            return RedirectToAction("Index", "Home");

        // Buscar gasto sin LINQ
        TipoGasto encontrado = null;
        foreach (TipoGasto tg in miSistema.Gastos)
        {
            if (tg.Nombre.ToLower() == nombreGasto.ToLower())
            {
                encontrado = tg;
                break;
            }
        }

        if (encontrado == null)
        {
            ViewBag.Error = "No existe un tipo de gasto con ese nombre.";
            ViewBag.Gastos = miSistema.Gastos;
            return View();
        }

        if (miSistema.TipoGastoEnUso(encontrado))
        {
            ViewBag.Error = "No se puede eliminar: está siendo utilizado.";
            ViewBag.Gastos = miSistema.Gastos;
            return View();
        }

        miSistema.EliminarTipoGasto(encontrado);
        ViewBag.Mensaje = "Tipo de gasto eliminado correctamente.";
        ViewBag.Gastos = miSistema.Gastos;

        return View();
    }

    // =============================
    // CARGAR PAGO
    // =============================
    [HttpGet]
    public IActionResult CargarPago()
    {
        string email = HttpContext.Session.GetString("LogueadoEmail");
        string rol = HttpContext.Session.GetString("LogueadoRol");

        if (string.IsNullOrEmpty(email))
            return RedirectToAction("Login", "Autorizacion");

        if (rol != "Gerente")
            return RedirectToAction("Index", "Home");

        ViewBag.Gastos = miSistema.Gastos;
        ViewBag.Metodos = Enum.GetValues(typeof(Pago.MetodoPago));

        return View();
    }

    [HttpPost]
    public IActionResult CargarPago(string tipoPago, string gastoNombre, string metodo,
                                    double monto, DateTime? fechaUnico,
                                    DateTime? fechaInicio, DateTime? fechaFin)
    {
        string email = HttpContext.Session.GetString("LogueadoEmail");
        string rol = HttpContext.Session.GetString("LogueadoRol");

        if (string.IsNullOrEmpty(email))
            return RedirectToAction("Login", "Autorizacion");

        if (rol != "Gerente")
            return RedirectToAction("Index", "Home");

        // Buscar usuario sin LINQ
        Usuario user = null;
        foreach (Usuario u in miSistema.Usuarios)
        {
            if (u.Email.ToLower() == email.ToLower())
            {
                user = u;
                break;
            }
        }

        // Buscar tipo de gasto sin LINQ
        TipoGasto gasto = null;
        foreach (TipoGasto tg in miSistema.Gastos)
        {
            if (tg.Nombre.ToLower() == gastoNombre.ToLower())
            {
                gasto = tg;
                break;
            }
        }

        // Validaciones
        if (user == null || gasto == null || monto <= 0)
        {
            ViewBag.Error = "Datos inválidos.";
            ViewBag.Gastos = miSistema.Gastos;
            ViewBag.Metodos = Enum.GetValues(typeof(Pago.MetodoPago));
            return View();
        }

        Pago.MetodoPago metodoEnum =
            (Pago.MetodoPago)Enum.Parse(typeof(Pago.MetodoPago), metodo);

        if (tipoPago == "U")
        {
            if (fechaUnico == null)
            {
                ViewBag.Error = "Debe elegir la fecha de pago.";
                return View();
            }

            int nuevoId = miSistema.Pagos.Count + 1;

            miSistema.AgregarPago(new Unico(
                metodoEnum, gasto, user,
                "Pago único registrado por gerente",
                monto, fechaUnico.Value, nuevoId
            ));
        }
        else if (tipoPago == "R")
        {
            if (fechaInicio == null)
            {
                ViewBag.Error = "Debe elegir fecha inicio.";
                return View();
            }

            Pago nuevo;

            if (fechaFin == null)
            {
                nuevo = new Recurrente(
                    metodoEnum, gasto, user,
                    "Pago recurrente registrado por gerente",
                    monto, fechaInicio.Value
                );
            }
            else
            {
                nuevo = new Recurrente(
                    metodoEnum, gasto, user,
                    "Pago recurrente registrado por gerente",
                    monto, fechaInicio.Value, fechaFin.Value
                );
            }

            miSistema.AgregarPago(nuevo);
        }

        return RedirectToAction("VerPagosGerente");
    }

    // =============================
    // VER PAGOS DEL EQUIPO
    // =============================
    [HttpGet]
    public IActionResult VerPagosEquipo(int? mes, int? anio)
    {
        string email = HttpContext.Session.GetString("LogueadoEmail");
        string rol = HttpContext.Session.GetString("LogueadoRol");

        if (string.IsNullOrEmpty(email))
            return RedirectToAction("Login", "Autorizacion");

        if (rol != "Gerente")
            return RedirectToAction("Index", "Home");

        // Buscar gerente sin LINQ
        Usuario gerente = null;
        foreach (Usuario u in miSistema.Usuarios)
        {
            if (u.Email.ToLower() == email.ToLower())
            {
                gerente = u;
                break;
            }
        }

        DateTime hoy = DateTime.Today;
        int mesBuscado = mes ?? hoy.Month;
        int anioBuscado = anio ?? hoy.Year;

        List<Pago> pagos = miSistema.ObtenerPagosEquipoPorMes(gerente, mesBuscado, anioBuscado);

        double totalBase = miSistema.TotalBasePagos(pagos);
        double totalConBeneficios = miSistema.TotalConBeneficios(pagos);

        ViewBag.Pagos = pagos;
        ViewBag.Mes = mesBuscado;
        ViewBag.Anio = anioBuscado;
        ViewBag.TotalBase = totalBase;
        ViewBag.TotalConBeneficios = totalConBeneficios;

        return View();
    }
}
