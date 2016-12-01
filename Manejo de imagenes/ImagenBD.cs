using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Manejo_de_imagenes
{
    class ImagenBD
    {
        private string cadena = "Data Source=.;Initial Catalog=tutoriales;Integrated Security=True;";
        public SqlConnection conn;
        public void conectar() { conn = new SqlConnection(cadena); }
        public ImagenBD() { conectar(); }

        SqlCommand cmd = new SqlCommand();

        public bool InsertarImagen(string nombre,PictureBox imagen)
        {
            cmd.Connection = conn;
            cmd.CommandText = "insert into Imagen(Nombre,Image)value(@Nombre.@Imagen)";
            cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Imagen",SqlDbType.Image);
            cmd.Parameters["@Nombre"].Value = nombre;

            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            imagen.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            cmd.Parameters["@Imagen"].Value = ms.GetBuffer();

            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();

            if(i >0)
            {
                return true;

                            }
            else { return false; }
        }

        //Archivos JPG|*.jpg|*.*

        SqlDataAdapter da;
        
    }
}
