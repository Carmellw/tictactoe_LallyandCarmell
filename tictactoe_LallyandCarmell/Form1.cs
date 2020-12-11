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
        private bool isPlayer1 = true; // משנה המכיל איזה שחקן משחק
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

        private void pictureBox_Box_Click(object sender, EventArgs e) //פעולת הלחיצה של כל המקומות בהם אפשר לשחק
        {
            PictureBox pictureBox = sender as PictureBox;

            if (isPlayer1) //בדיקת איזה שחקן משחק
            {
                //שחקן 1- איקס
                if (pictureBox.Image != m_Images[1] && pictureBox.Image != m_Images[2])//אם המקום ריק אז נכניס איקס לאותו מקום והתור הבא הוא של שחקן 2
                {
                    pictureBox.Image = m_Images[1];
                    isPlayer1 = false;
                }

                else// המקום לא ריק- תשלח הודעה שמודיעה שהמקום לא ריק (התור נשאר של שחקן 1
                {
                    MessageBox.Show("choose an empty space");
                }
            }

            else
            //שחקן 2- עיגול
            {
                if (pictureBox.Image != m_Images[1] && pictureBox.Image != m_Images[2])//אם המקום ריק אז נכניס עיגול לאותו מקום והתור הבא הוא של שחקן 1 
                {
                    pictureBox.Image = m_Images[2];
                    isPlayer1 = true;
                }

                else// המקום לא ריק- תשלח הודעה שמודיעה שהמקום לא ריק (התור נשאר של שחקן 2
                {
                    MessageBox.Show("choose an empty space");
                }
            }

            if (IsWin(FormToMatrix()) > 0) // אם מישהו ניצח
            {
                //בדיקת מי ניצח ושליחת הודעה בהתאם
                if (IsWin(FormToMatrix()) == 1) 
                {
                    MessageBox.Show("player 1 won");
                }
                else
                {
                    MessageBox.Show("player 2 won");
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
                    m[row, col] = WhatIsPicture(curPictureBox);
                    string x = WhatIsPicture(curPictureBox) ;

                }
            return m;
        }

        public string WhatIsPicture(PictureBox pictureBox) //פעולה המגלה איזו תמונה נמצאת
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

        public int IsWin(string[,] m) //פעולה המגלה אם מישהו ניצח ואם כן מי ניצח
        {
            //לולאת פור שעוברת על כל הטורים ובודקת אם יש טור שהכל בו אותו הדבר
            for (int i = 0; i < m.GetLength(0); i++)
            {
                if (m[i, 0] == m[i, 1] && m[i, 1] == m[i, 2])
                { 
                    if (m[i, 0] == "X")//אם כל הטור איקסים אז תחזיר 1
                    {
                        return 1;
                    }
                    else if (m[i, 0] == "O")// אם כל הטור עיגולים אז תחזיר 2
                    {
                        return 2;
                    }
                }
            }

            //לולאת פור שעוברת על כל השורות ובודקת אם יש שורה שהכל בה אותו הדבר
            for (int i = 0; i < m.GetLength(1); i++)
            {
                if (m[0, i] == m[1, i] && m[1, i] == m[2, i])
                {
                    if (m[0, i] == "X")//אם כל השורה איקסים תחזיר 1
                    {
                        return 1;
                    }
                    else if (m[0, i] == "O")//אם כל השורה עיגולים תחזיר 2
                    {
                        return 2;
                    }
                }
            }

            //בדיקת האלכסון הראשון
            if (m[0, 0] == m[1, 1] && m[1, 1] == m[2, 2])
            {
                if (m[0, 0] == "X")//אם בכל האלכסון יש איקסים תחזיר 1
                {
                    return 1;
                }
                else if (m[0, 0] == "O")// אם בכל האלכסון יש עיגולים תחזיר 2
                {
                    return 2;
                }
            }

            //בדיקת האלכסון השני
            if (m[0, 2] == m[1, 1] && m[1, 1] == m[2, 0])
            {
                if (m[0, 2] == "X")//אם בכל האלכסון יש איקסים תחזיר 1
                {
                    return 1;
                }
                else if (m[0, 2] == "O")// אם בכל האלכסון יש עיגולים תחזיר 2
                {
                    return 2;
                }
            }

            return 0;//אם לא נמצא אף ניצחון תחזיר 0

        }
    }
}
