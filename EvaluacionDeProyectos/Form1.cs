namespace EvaluacionDeProyectos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Doc doc= new Doc();
            doc.Anos = Convert.ToInt32(tb_anos.Text);
            doc.Unidades = Convert.ToInt32(tb_unidades.Text);
            doc.I_unidades = Convert.ToDouble(tb_iunidades.Text);
            doc.Precio = Convert.ToDouble(tb_pu.Text);
            doc.Iprecio = Convert.ToDouble(tb_ipu.Text);
            doc.Costosv = Convert.ToDouble(tb_cv.Text);
            doc.Infra = Convert.ToDouble(tb_infra.Text);
            doc.D_infra = Convert.ToDouble(tb_dinfra.Text);
            doc.Lineas = Convert.ToDouble(tb_lineas.Text);
            doc.D_lineas = Convert.ToDouble(tb_dlineas.Text);
            doc.Isr = Convert.ToDouble(tb_isr.Text);
            doc.Ctrabajo = Convert.ToDouble(tb_ct.Text);
            doc.Trema = Convert.ToDouble(tb_trema.Text);
            doc.Cf = Convert.ToDouble(tb_cf.Text);
            Form2 form2 = new Form2(doc);
            form2.ShowDialog();
        }
    }
}