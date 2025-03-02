using CapaEntidad;
using CapaNegocio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Alquiler.Controllers
{
    public class VehiculoController : Controller
    {
        // GET: Vehiculo
        public ActionResult Index()
        {
            List<VehiculoCLS> listaVehiculos = ListarVehiculos();
            return View(listaVehiculos);
        }

        // Este es el método similar al que mostraste como ejemplo
        public List<VehiculoCLS> ListarVehiculos()
        {
            VehiculoBL obj = new VehiculoBL();
            return obj.ListarVehiculos();
        }

        // GET: Vehiculo/Filtrar
        public ActionResult Filtrar(string marca, string modelo)
        {
            VehiculoBL obj = new VehiculoBL();
            List<VehiculoCLS> listaVehiculos = obj.FiltrarVehiculos(marca, modelo);
            return View("Index", listaVehiculos);
        }

        // GET: Vehiculo/Details/5
        public ActionResult Details(int id)
        {
            List<VehiculoCLS> listaVehiculos = ListarVehiculos();
            VehiculoCLS vehiculo = listaVehiculos.Find(v => v.Id == id);

            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }

        // GET: Vehiculo/Create
        public ActionResult Create()
        {
            return View(new VehiculoCLS());
        }

        // POST: Vehiculo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VehiculoCLS vehiculo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    VehiculoBL obj = new VehiculoBL();
                    int resultado = obj.GuardarDatosVehiculo(vehiculo);

                    if (resultado > 0)
                    {
                        TempData["Mensaje"] = "Vehículo guardado correctamente";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "No se pudo guardar el vehículo");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error al guardar: {ex.Message}");
                }
            }

            return View(vehiculo);
        }

        // GET: Vehiculo/Edit/5
        public ActionResult Edit(int id)
        {
            List<VehiculoCLS> listaVehiculos = ListarVehiculos();
            VehiculoCLS vehiculo = listaVehiculos.Find(v => v.Id == id);

            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }

        // POST: Vehiculo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VehiculoCLS vehiculo)
        {
            if (id != vehiculo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    VehiculoBL obj = new VehiculoBL();
                    int resultado = obj.GuardarDatosVehiculo(vehiculo);

                    if (resultado > 0)
                    {
                        TempData["Mensaje"] = "Vehículo actualizado correctamente";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "No se pudo actualizar el vehículo");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error al actualizar: {ex.Message}");
                }
            }

            return View(vehiculo);
        }

        // GET: Vehiculo/Delete/5
        public ActionResult Delete(int id)
        {
            List<VehiculoCLS> listaVehiculos = ListarVehiculos();
            VehiculoCLS vehiculo = listaVehiculos.Find(v => v.Id == id);

            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }

        // POST: Vehiculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                VehiculoBL obj = new VehiculoBL();
                int resultado = obj.EliminarVehiculo(id);

                if (resultado > 0)
                {
                    TempData["Mensaje"] = "Vehículo eliminado correctamente";
                }
                else
                {
                    TempData["Error"] = "No se pudo eliminar el vehículo";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al eliminar: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        // API para obtener datos como JSON
        [HttpGet]
        public JsonResult ObtenerVehiculos()
        {
            List<VehiculoCLS> lista = ListarVehiculos();
            return Json(new { data = lista });
        }

        // API para filtrar datos como JSON
        [HttpGet]
        public JsonResult FiltrarVehiculosJson(string marca, string modelo)
        {
            VehiculoBL obj = new VehiculoBL();
            List<VehiculoCLS> lista = obj.FiltrarVehiculos(marca, modelo);
            return Json(new { data = lista });
        }
    }
}