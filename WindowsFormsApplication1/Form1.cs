using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void browseButton_Click(object sender, System.EventArgs e)
        {
            // Se crea el OpenFileDialog
            OpenFileDialog dialog = new OpenFileDialog();
            // Se muestra al usuario esperando una acción
            DialogResult result = dialog.ShowDialog();

            // Si seleccionó un archivo (asumiendo que es una imagen lo que seleccionó)
            // la mostramos en el PictureBox de la inferfaz
            if (result == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(dialog.FileName);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    // Objetos de conexión y comando
                    System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(@"Data Source=.;Initial Catalog=Store;Integrated Security=True;");
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                    // Estableciento propiedades
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO Products VALUES (@id, @name, @quantity, @price, @image)";

                    // Creando los parámetros necesarios
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.Add("@quantity", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@price", System.Data.SqlDbType.SmallMoney);
                    cmd.Parameters.Add("@image", System.Data.SqlDbType.Image);

                    // Asignando los valores a los atributos
                    cmd.Parameters["@id"].Value = int.Parse(idBox.Text);
                    cmd.Parameters["@name"].Value = nameBox.Text;
                    cmd.Parameters["@quantity"].Value = int.Parse(quantityBox.Text);
                    cmd.Parameters["@price"].Value = float.Parse(priceBox.Text);

                    // Asignando el valor de la imagen

                    // Stream usado como buffer
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    // Se guarda la imagen en el buffer
                    pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    // Se extraen los bytes del buffer para asignarlos como valor para el 
                    // parámetro.
                    cmd.Parameters["@image"].Value = ms.GetBuffer();

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
