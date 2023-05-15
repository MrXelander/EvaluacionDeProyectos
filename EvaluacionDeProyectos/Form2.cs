using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelNumberFormat;
using Microsoft.VisualBasic;
using static System.Windows.Forms.DataFormats;

namespace EvaluacionDeProyectos
{
    public partial class Form2 : Form
    {
        Doc doc;
        public Form2(Doc doc)
        {
            InitializeComponent();
            this.doc = doc;
            init();
        }

        public void init()
        {
            dataGridView1.Columns.Add("Concepto", "Concepto");
            int[] unidades = new int[doc.Anos];
            double[] pu = new double[doc.Anos];
            double[] ingresos = new double[doc.Anos];
            double[] cv = new double[doc.Anos];
            double[] uai = new double[doc.Anos];
            double[] isr = new double[doc.Anos];
            double[] inetos = new double[doc.Anos];
            double[] cf = new double[doc.Anos];
            unidades[0] = doc.Unidades;
            pu[0] = doc.Precio;
            double deprec = (doc.Infra * (doc.D_infra/100)) + (doc.Lineas * (doc.D_lineas/100));
            double sumcf = 0;
            for (int i = 0; i < doc.Anos; i++)
            {
                dataGridView1.Columns.Add("Anio" + (i + 1), "Año " + (i + 1));
                if (i != 0)
                {
                    unidades[i] = (int)Math.Round(unidades[i - 1] + (unidades[i-1] * (doc.I_unidades/100)));
                    pu[i] = Math.Round(pu[i - 1] + (pu[i - 1] * (doc.Iprecio / 100)), 2);
                }
                ingresos[i] = Math.Round(unidades[i] * pu[i], 2);
                cv[i] = Math.Round(doc.Costosv * unidades[i], 2);
                uai[i] = Math.Round(ingresos[i] - (doc.Cf + cv[i] + deprec), 2);
                isr[i] = Math.Round(uai[i] * (doc.Isr/100), 2);
                inetos[i] = Math.Round(uai[i] - isr[i], 2);
                cf[i] = Math.Round(inetos[i] + deprec, 2);
                sumcf += cf[i];
            }
            //agregando filas
            dataGridView1.Rows.Add();
            dataGridView1.Rows[0].Cells[0].Value = "Ingresos";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[1].Cells[0].Value = "Costos fijos";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[2].Cells[0].Value = "Costos variables";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[3].Cells[0].Value = "Depreciación";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[4].Cells[0].Value = "Utilidad antes de impuestos";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[5].Cells[0].Value = "Impuestos (" + doc.Isr + "%)";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[6].Cells[0].Value = "Ingresos netos";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[7].Cells[0].Value = "Flujo de efectivo";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[8].Cells[0].Value = "Suma Flujo de efectivo";
            //llenando las celdas
            for (int j = 1; j < doc.Anos + 1; j++)
            {
                dataGridView1.Rows[0].Cells[j].Value = ingresos[j-1].ToString("C");
                dataGridView1.Rows[1].Cells[j].Value = doc.Cf.ToString("C");
                dataGridView1.Rows[2].Cells[j].Value = cv[j-1].ToString("C");
                dataGridView1.Rows[3].Cells[j].Value = deprec.ToString("C");
                dataGridView1.Rows[4].Cells[j].Value = uai[j-1].ToString("C");
                dataGridView1.Rows[5].Cells[j].Value = isr[j - 1].ToString("C");
                dataGridView1.Rows[6].Cells[j].Value = inetos[j - 1].ToString("C");
                dataGridView1.Rows[7].Cells[j].Value = cf[j - 1].ToString("C");
            }
            dataGridView1.Rows[8].Cells[doc.Anos].Value = sumcf.ToString("C");
            //Grid 2
            double inversiones = (doc.Infra + doc.Lineas + doc.Ctrabajo) *-1;
            double[] pr = new double[doc.Anos + 1];
            pr[0] = inversiones;
            dataGridView2.Columns.Add("Concepto", "Concepto");
            for (int i = 0; i < doc.Anos + 1; i++)
            {
                dataGridView2.Columns.Add("Anio" + (i), "Año " + (i));
                if (i != 0)
                {
                    pr[i] = pr[i-1] + cf[i - 1];
                }
            }
            //agregando filas
            dataGridView2.Rows.Add();
            dataGridView2.Rows[0].Cells[0].Value = "Flujo de efectivo";
            dataGridView2.Rows[0].Cells[1].Value = inversiones.ToString("C");
            dataGridView2.Rows.Add();
            dataGridView2.Rows[1].Cells[0].Value = "Periodo de recuperación";
            //llenando las celdas
            for (int j = 1; j < dataGridView2.Columns.Count; j++)
            {
                if (j != dataGridView2.Columns.Count - 1)
                {
                    dataGridView2.Rows[0].Cells[j+1].Value = cf[j - 1].ToString("C");
                }
                dataGridView2.Rows[1].Cells[j].Value = pr[j - 1].ToString("C");
            }
            //grid 3
            dataGridView3.Columns.Add("concepto", "Concepto");
            dataGridView3.Columns.Add("cantidad", "Cantidad");
            dataGridView3.Rows.Add();
            dataGridView3.Rows[0].Cells[0].Value = "Promedio flujo de efectivo";
            double prom = sumcf / doc.Anos;
            dataGridView3.Rows[0].Cells[1].Value = prom.ToString("C");
            dataGridView3.Rows.Add();
            dataGridView3.Rows[1].Cells[0].Value = "Rendimiento";
            double rend = Math.Round((prom / inversiones)*-100, 2);
            dataGridView3.Rows[1].Cells[1].Value = rend + "%";
            dataGridView3.Rows.Add();
            dataGridView3.Rows[2].Cells[0].Value = "VPN";
            double vpn = Math.Round(CalcularVNA(doc.Trema, cf) + inversiones, 2);
            dataGridView3.Rows[2].Cells[1].Value = vpn.ToString("C");
            dataGridView3.Rows.Add();
            dataGridView3.Rows[3].Cells[0].Value = "TIR";
            double[] cf2 = new double[doc.Anos + 1];
            cf2[0] = inversiones;
            for (int i = 1; i<cf2.Length; i++)
            {
                cf2[i] = cf[i - 1];
            }
            double tir = Math.Round(Financial.IRR(ref cf2,0.1)*100, 2);
            dataGridView3.Rows[3].Cells[1].Value = tir + "%";
            //grid 4
            dataGridView4.Columns.Add("concepto", "Concepto");
            dataGridView4.Columns.Add("inversiones", "Inversiones");
            dataGridView4.Columns.Add("intereses", "Intereses");
            dataGridView4.Rows.Add();
            dataGridView4.Rows[0].Cells[0].Value = "Infraestructura";
            dataGridView4.Rows[0].Cells[1].Value = doc.Infra.ToString("C");
            dataGridView4.Rows[0].Cells[2].Value = (doc.Infra*(doc.D_infra/100)).ToString("C");
            dataGridView4.Rows.Add();
            dataGridView4.Rows[1].Cells[0].Value = "Lineas de producción";
            dataGridView4.Rows[1].Cells[1].Value = doc.Lineas.ToString("C");
            dataGridView4.Rows[1].Cells[2].Value = (doc.Lineas * (doc.D_lineas / 100)).ToString("C");
            dataGridView4.Rows.Add();
            dataGridView4.Rows[2].Cells[0].Value = "Capital de trabajo";
            dataGridView4.Rows[2].Cells[1].Value = doc.Ctrabajo.ToString("C");
            dataGridView4.Rows[2].Cells[2].Value = (0).ToString("C");
            dataGridView4.Rows.Add();
            dataGridView4.Rows[3].Cells[0].Value = "Total";
            dataGridView4.Rows[3].Cells[1].Value = (doc.Infra + doc.Lineas + doc.Ctrabajo).ToString("C");
            dataGridView4.Rows[3].Cells[2].Value = ((doc.Infra * (doc.D_infra / 100)) + (doc.Lineas * (doc.D_lineas / 100))).ToString("C");
            // Ajustar el ancho de las columnas al contenido de las celdas
            acomodar(dataGridView1);
            acomodar(dataGridView2);
            acomodar(dataGridView3);
            acomodar(dataGridView4);
        }

        public void acomodar(DataGridView dataGridView)
        {
            for(int i = 0; i < dataGridView.Columns.Count; i++)
            {
                dataGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        public double CalcularVNA(double tasaDescuento, double[] flujosEfectivo)
        {
            tasaDescuento = tasaDescuento / 100;
            double vna = 0.0;
            for (int i = 0; i < flujosEfectivo.Length; i++)
            {
                vna += flujosEfectivo[i] / Math.Pow(1 + tasaDescuento, i+1);
            }
            return vna;
        }
    }
}