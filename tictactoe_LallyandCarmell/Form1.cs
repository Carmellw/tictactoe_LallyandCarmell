using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tictactoe_LallyandCarmell.Properties;


namespace tictactoe_LallyandCarmell
{
    public partial class Form1 : Form
    {
        private bool isPlayer1 = true;
        private Image[] m_Images = new Image[3]; //המערך של התמונות

        public Form1()
        {
            InitializeComponent();
            SetImagesArray();
        }

        public void SetImagesArray()
        {
            m_Images[0] = Resources.white;
            m_Images[1] = Resources.x;
            m_Images[2] = Resources.circle;
        }

        private void pictureBox_Box_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;

            if (isPlayer1)
            {
                if (pictureBox.Image != m_Images[1] && pictureBox.Image != m_Images[2])
                {
                    pictureBox.Image = m_Images[1];
                    isPlayer1 = false;
                }

                else
                {
                    MessageBox.Show("choose an empty space");
                }
            }

            else
            {
                if (pictureBox.Image != m_Images[1] && pictureBox.Image != m_Images[2])
                {
                    pictureBox.Image = m_Images[2];
                    isPlayer1 = true;
                }

                else
                {
                    MessageBox.Show("choose an empty space");
                }
            }

        }

        public string[,] FormToMatrix()
        {

            //המטריצה המוחזרת

            string[,] m = new string[3, 3];
            int row, col;
            PictureBox curPictureBox;

            //עוברים על כלל הפקדים בטופס - נעזרים בלולאת foreach

            foreach (Control ctrl in this.Controls)
                if (ctrl is PictureBox)
                {
                    curPictureBox = ctrl as PictureBox;

                    //מציאת מספר השורה והעמודה מתוך שם הפקד

                    row = int.Parse(curPictureBox.Name[curPictureBox.Name.Length - 2].ToString());
                    col = int.Parse(curPictureBox.Name[curPictureBox.Name.Length - 1].ToString());
                    m[row, col] = curPictureBox.Text;


                }
            return m;
        }

        public string WhatIsPicture(PictureBox pictureBox)
        {
            if (pictureBox.Image == m_Images[1])
            {
                return ("X");
            }

            if (pictureBox.Image == m_Images[2])
            {
                return ("O");
            }

            return ("white");
        }
    }
}
