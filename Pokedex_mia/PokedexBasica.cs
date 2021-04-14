using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PruebaBBDD
{
    public partial class PokedexBasica : Form
    {
        Conexion miConexion = new Conexion();
        int idActual = 1;
        public PokedexBasica()
        {
            InitializeComponent();
            asignaPokemon();
        }

        //boton derecha
        private void button2_Click(object sender, EventArgs e)
        {
            idActual++;
            if (idActual > 151) {
                idActual = 1;
            }
            asignaPokemon();
        }


        //boton izquierda
        private void button1_Click(object sender, EventArgs e)
        {
            idActual--;
            if (idActual < 1) {
                idActual = 151;
            }
            asignaPokemon();
        }

        private Image convierteBlodAImagen(byte[] img)
        {
            MemoryStream ms = new System.IO.MemoryStream(img);
            return (Image.FromStream(ms));

        }

        private void asignaPokemon() {
            DataTable pokemonElegido = miConexion.getPokemonPorId(idActual);
            nombrePokemon.Text = pokemonElegido.Rows[0]["nombre"].ToString();
            pictureBox1.Image = convierteBlodAImagen((byte[])pokemonElegido.Rows[0]["imagen"]);
        }
    }
}
