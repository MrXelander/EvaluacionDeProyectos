using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionDeProyectos
{
    public class Doc
    {
        private int anos;
        private int unidades;
        private double i_unidades;
        private double precio;
        private double i_precio;
        private double costosv;
        private double infra;
        private double d_infra;
        private double lineas;
        private double d_lineas;
        private double isr;
        private double ctrabajo;
        private double trema;
        private double cf;

        public int Anos
        {
            get { return anos; }
            set { anos = value; }
        }
        public int Unidades
        {
            get { return unidades; }
            set { unidades = value; }
        }
        public double I_unidades
        {
            get { return i_unidades; }
            set { i_unidades = value; }
        }
        public double Precio 
        { 
            get { return precio; }
            set { precio = value; }
        }
        public double Iprecio
        {
            get { return i_precio; }
            set { i_precio = value; }
        }
        public double Costosv
        {
            get { return costosv; }
            set { costosv = value; }
        }
        public double Infra
        {
            get { return infra; }
            set { infra = value; }
        }
        public double D_infra
        {
            get { return d_infra; }
            set { d_infra = value; }
        }
        public double Lineas
        {
            get { return lineas; }
            set { lineas = value; }
        }
        public double D_lineas
        {
            get { return d_lineas; }
            set { d_lineas = value; }
        }
        public double Isr
        {
            get { return isr; }
            set { isr = value; }
        }
        public double Ctrabajo
        {
            get { return ctrabajo; }
            set { ctrabajo = value; }
        }
        public double Trema
        {
            get { return trema; }
            set { trema = value; }
        }
        public double Cf
        {
            get { return cf; }
            set { cf = value; }
        }
    }
}
