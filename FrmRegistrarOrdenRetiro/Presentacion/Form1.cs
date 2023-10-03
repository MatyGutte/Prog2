using FrmRegistrarOrdenRetiro.Entidades;
using FrmRegistrarOrdenRetiro.Servicios.Implementacion;
using FrmRegistrarOrdenRetiro.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmRegistrarOrdenRetiro
{
    public partial class FrmRegistrarOrden : Form
    {
        IServicio servi;
        OrdenRetiro nuevo;
        public FrmRegistrarOrden()
        {
            InitializeComponent();
            servi = new Servicio();
            nuevo = new OrdenRetiro();
        }

        private void FrmRegistrarOrden_Load(object sender, EventArgs e)
        {
            CargarMateriales();
            limpiar();
        }

        private void CargarMateriales()
        {
            cmbMaterial.Items.Clear();
            cmbMaterial.DataSource = servi.TraerMateriales();
            cmbMaterial.ValueMember = "Codigo";
            cmbMaterial.DisplayMember = "Nombre";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cmbMaterial.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un material...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if(nudCantidad.Value <= 0)
            {
                MessageBox.Show("Debe ingresar una cantidad válida...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            foreach (DataGridViewRow row in dgvOrden.Rows)
            {
                if (row.Cells["ColMaterial"].Value.ToString().Equals(cmbMaterial.Text))
                {
                    MessageBox.Show("Este material ya está presupuestado...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            Materiales m = (Materiales)cmbMaterial.SelectedItem;

            int cant = (int)nudCantidad.Value;
            DetalleOrden detalle = new DetalleOrden(m,cant);


            nuevo.AgregarDetalle(detalle);
            dgvOrden.Rows.Add(new object[] {m.Codigo, m.Nombre, m.Stock, cant, "Quitar"});
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtResponsable.Text))
            {
                MessageBox.Show("Debe ingresar un cliente...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if(dgvOrden.Rows.Count == 0)
            {
                MessageBox.Show("Debe ingresar al menos un material...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            foreach (DataGridViewRow dr in dgvOrden.Rows)
            {
                if (int.Parse(dr.Cells["Colstock"].Value.ToString()) < int.Parse(dr.Cells["ColCantidad"].Value.ToString()))
                {
                    MessageBox.Show("El Stock es insuficiente...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }    
            }
            GrabarOrden();            
        }

        private void GrabarOrden()
        {
            nuevo.Responsable = txtResponsable.Text;
            nuevo.Fecha = dtmFecha.Value;
            int confirmacion = servi.CrearOrden(nuevo);
            if (confirmacion != 0)
            {
                MessageBox.Show("Se registró con éxito la orden...\nNumero de lineas afectadas: " + confirmacion, "Informe", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                limpiar();
            }
            else
            {
                MessageBox.Show("NO se pudo registrar la orden...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }    
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void limpiar()
        {
            dtmFecha.Value = DateTime.Now;
            txtResponsable.Text = "Consumidor Final";
            nudCantidad.Value = 0;
            dgvOrden.Rows.Clear();
            cmbMaterial.SelectedIndex = -1;
        }

        private void dgwOrden_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvOrden.CurrentCell.ColumnIndex == 4)
            {
                nuevo.QuitarDetalle(dgvOrden.CurrentRow.Index);
                dgvOrden.Rows.RemoveAt(dgvOrden.CurrentRow.Index);
            }
        }
    }
}
