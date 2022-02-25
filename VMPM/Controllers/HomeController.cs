using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMPM.Models;
namespace VMPM.Controllers
{
    public class HomeController : Controller
    {
        int OffsetValue = 0;
      
        public ActionResult Index()
        {
            ///   Logica_Negocio logi = new Logica_Negocio();
            //   var Datos = logi.Lista_Clientes();

            int PagingSize = 5;
           

           Logica_Negocio Logi = new Logica_Negocio();
          List<Personas> Resultado = Logi.Filtro_Personas(OffsetValue, PagingSize);




            return View(Resultado);
        }

        public ActionResult Agregar_Personas()
        {
            // ViewBag.Message = "Your application description page.";
            ViewBag.Error = false;
            return View();
        }

        public ActionResult Error()
        {
        //    ViewBag.Message = "Your contact page.";

            return View();
        }
        
        [HttpGet]
        public ActionResult Update_Personas(Personas people)
        {
            ViewBag.HOLA = 100;
            return View(people);
        
        }

        public ActionResult VisualisarEpersonas(Personas people) 
        {
            return View(people);
        }


        [HttpPost]
        public ActionResult Guardar_Personas(Personas people)
        {
            
            if (!ModelState.IsValid) {
                ViewBag.Error = false;
              return View("Error");
            
             //   return View("Agregar_Personas");
            }
            Logica_Negocio Logi = new Logica_Negocio();
            bool Resultado = Logi.Verificar_Nombre_Usuario(people.CEDULA);
            if (Resultado) {ViewBag.Error = true; }
            if (!Resultado) {ViewBag.Error = false;}

            Logi.Guardar_Personas(people);
            return View("Agregar_Personas");
        }


        public ActionResult Filtro_Personas_Controller(int OffsetValue2)
        {
            int PagingSize = 5;
            int Resultado2;
            Logica_Negocio Logi = new Logica_Negocio();
            List<Personas> Resultado = new List<Personas>();
            int CantidadPersonas = Logi.Cantidad_Personas();
        
            if(OffsetValue2 == 30)
            {
                int comodin = 0;
                  if(comodin == 0) { comodin = 30; }

                if (CantidadPersonas > comodin)
                {
                    Resultado = Logi.Filtro_Personas(comodin, PagingSize);
                    OffsetValue = OffsetValue2;
                    // OffsetValue2 = 0;
                    comodin = comodin +5;
                    return View("Index", Resultado);
                }
            }



            if (CantidadPersonas > OffsetValue2)
            {
               Resultado = Logi.Filtro_Personas(OffsetValue2, PagingSize);
                OffsetValue = OffsetValue2;
                // OffsetValue2 = 0;
                return View("Index", Resultado);
            }

            Resultado2 = Logi.Verificar_Quórum_Personas(OffsetValue2);

            if (Resultado2 == 0) { View("Index", Resultado); }

            if (OffsetValue2 <= CantidadPersonas)

            {
                //  OffsetValue2 = CantidadPersonas + 5;
                Resultado = Logi.Filtro_Personas(OffsetValue2, PagingSize);
                OffsetValue = OffsetValue2;
                OffsetValue2 = 0;
                OffsetValue2++;
                return View("Index", Resultado);
            }

            return View("Index",Resultado);
        }

        [HttpPost]
        public ActionResult Actualizar_Personas(Personas people)
        {
            Logica_Negocio LG = new Logica_Negocio();
            people.ID =  int.Parse(TempData["ID"].ToString());
            LG.Actualizar_Personas(people);
            // return View("Index");
            return  RedirectToAction("Index");
        }



    }
}