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
using Pokedex_mia;

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
            //evolucion();
        }

        private Image convierteBlodAImagen(byte[] img)
        {
            MemoryStream ms = new System.IO.MemoryStream(img);
            return (Image.FromStream(ms));

        }

        private void asignaPokemon() {

            DataTable pokemonElegido = miConexion.getPokemonPorId(idActual);
            DataTable PreEvo = miConexion.getPokemonPorId(idActual - 1);
            DataTable PreEvo2 = miConexion.getPokemonPorId(idActual - 2);
            DataTable Evo = miConexion.getPokemonPorId(idActual + 1);
            

            nombrePokemon.Text = pokemonElegido.Rows[0]["nombre"].ToString();
            pictureBox1.Image = convierteBlodAImagen((byte[])pokemonElegido.Rows[0]["imagen"]);
            label1.Text = pokemonElegido.Rows[0]["altura"].ToString();
            label2.Text = pokemonElegido.Rows[0]["peso"].ToString();
            habitat.Text = pokemonElegido.Rows[0]["habitat"].ToString();
            especie.Text = pokemonElegido.Rows[0]["especie"].ToString();

            if (pokemonElegido.Rows[0]["preEvolucion"] == DBNull.Value && pokemonElegido.Rows[0]["posEvolucion"] != DBNull.Value)
            {
                pictureBox2.Image = convierteBlodAImagen((byte[])pokemonElegido.Rows[0]["imagen"]);
                if (Evo.Rows[0]["posEvolucion"] != DBNull.Value)
                {
                    pictureBox2.Image = convierteBlodAImagen((byte[])Evo.Rows[0]["imagen"]);
                }
                else
                {
                    pictureBox2.Image = null;
                }
            }
            else if (pokemonElegido.Rows[0]["preEvolucion"] != DBNull.Value && pokemonElegido.Rows[0]["posEvolucion"] != DBNull.Value)
            {
                pictureBox2.Image = convierteBlodAImagen((byte[])Evo.Rows[0]["imagen"]);
                pictureBox3.Image = convierteBlodAImagen((byte[])PreEvo.Rows[0]["imagen"]);
            }

            else if (pokemonElegido.Rows[0]["preEvolucion"] != DBNull.Value && pokemonElegido.Rows[0]["posEvolucion"] == DBNull.Value)
            {
                pictureBox3.Image = convierteBlodAImagen((byte[])PreEvo.Rows[0]["imagen"]);
                if (PreEvo.Rows[0]["preEvolucion"] != DBNull.Value)
                {
                    pictureBox2.Image = convierteBlodAImagen((byte[])PreEvo2.Rows[0]["imagen"]);
                }
                else 
                {
                    pictureBox2.Image = null;
                }

            }
            else
            {
                pictureBox3.Image = null;
            }

        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            MasInfo ventana = new MasInfo();
            ventana.Show();
        }
        
    }
}
