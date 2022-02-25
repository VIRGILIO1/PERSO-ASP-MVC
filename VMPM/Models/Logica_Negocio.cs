using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace VMPM.Models
{
    public class Logica_Negocio
    {
        public List<Personas> Lista_Clientes()
        {
            List<Personas> P = new List<Personas>();
            VIRGILIOEntities BD = new VIRGILIOEntities();

            return BD.Personas.ToList();

            

        }


        public void Guardar_Personas(Personas people)
        {
            VIRGILIOEntities BD = new VIRGILIOEntities();
            Personas P = new Personas();
            P.NOMBRE = people.NOMBRE;
            P.APELLIDO = people.APELLIDO;
            P.DIRECCION = people.DIRECCION;
            P.CEDULA = people.CEDULA;
            BD.Personas.Add(P);
            BD.SaveChanges();

        }


        public bool Verificar_Nombre_Usuario(string Cedula)
        {
            VIRGILIOEntities BD = new VIRGILIOEntities();
          
            // int Resultado = (from x in BD.Personas where x.NOMBRE.Contains(Cedula) select x.NOMBRE).Count(); 
           int   Resultado = (from x in BD.Personas where x.CEDULA == (Cedula) select x.CEDULA).Count();
            if (Resultado !=0) { return true; }

            return false;
        }

        public List<Personas> Filtro_Personas(int OffsetValue, int PagingSize)
        {
            VIRGILIOEntities BD = new VIRGILIOEntities();
            //   var V = BD.Filtro_Usuario(OffsetValue, PagingSize).ToList();

            List<Personas> P = new List<Personas>();
            P = BD.Database.SqlQuery<Personas>("EXEC Filtro_Usuario {0}, {1}", OffsetValue, PagingSize).ToList();
            return P;
        }


        public int Cantidad_Personas()
        {
            VIRGILIOEntities BD = new VIRGILIOEntities();
            int Cantidad = BD.Personas.Count();
            return Cantidad;
        }

        public int Verificar_Quórum_Personas(int OffsetValue)
        {
            VIRGILIOEntities BD = new VIRGILIOEntities();
            int PagingSize = 5;
            int Resultado = BD.Database.SqlQuery<Personas>("EXEC Filtro_Usuario {0}, {1}", OffsetValue, PagingSize).Count();

            return Resultado;
        }

        public void Actualizar_Personas(Personas P)
        {
            VIRGILIOEntities BD = new VIRGILIOEntities();
          //  Personas P = new Personas();
            BD.UPDATE_CLINETES(P.NOMBRE,P.APELLIDO,P.CEDULA,P.DIRECCION,P.ID);

        }



    }
}