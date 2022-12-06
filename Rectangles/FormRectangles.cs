using System;
using System.IO;
using System.Windows.Forms;

namespace Rectangles
{
    public partial class FormRectangles : Form
    {
        public FormRectangles()
        {
            InitializeComponent();
        }
   
        int[] coordinates = new int[8];

        private void BtnCheckPositions_Click(object sender, EventArgs e)
        {
            string line = "";
            string[] points = new string[8];
            

            string path = Application.StartupPath + @"\rectangles.txt";
            StreamReader streamReader = new StreamReader(path);

            bool finished = false;

            while (!finished)
            {
                line = streamReader.ReadLine();

                if (line == null)
                {
                    finished = true;
                }
                else
                {
                    points = line.Split(' ');

                    for (int i = 0; i < points.Length; i++)
                    {
                        coordinates[i] = Convert.ToInt32(points[i]);
                    }

                    CheckPositions(coordinates);
                }
            }
        }

        private void CheckPositions(int[] coordinates)
        {
            int ax1 = coordinates[0];
            int ax2 = coordinates[2];
            int bx1 = coordinates[4];
            int bx2 = coordinates[6];

            int ay1 = coordinates[1];
            int ay2 = coordinates[3];
            int by1 = coordinates[5];
            int by2 = coordinates[7];

            //If further left point of 'a' is < further right point of 'b', then 'a' is entirely on the left side of 'b' or they share a side if =.
            //We apply the same logic if it's on the left side.
            //If the bottom point of 'a' is > than the top of 'b', then 'a' is above 'b', or they share side of =.
            //We apply the same logic if it's below.
            if (ax2 <= bx1 || ax1 >= bx2 || ay1 >= by2 || ay2 <= by1)
            {
                TxtResult.Text += "no overlap" + Environment.NewLine;
            }
            else
            {
                //We check if one point of 'a' lies between one of the points of 'b' and the other point of 'a' doesn't. Then we have overlap.
                //We apply this logic if it's on the left side or, if it's on the right.
                if (ax1 < bx1 && ax2 > bx1 && ax2 < bx2 || ax1 > bx1 && ax1 < bx2 && ax2 > bx2)
                {
                    TxtResult.Text += "overlap" + Environment.NewLine;
                }
                //We check of both points of 'b' are between both points of 'a', then 'a' surrounds 'b'.
                else if (bx1 > ax1 && bx1 < ax2 && bx2 > ax1 && bx2 < ax2 && by1 < ay2 && by1 > ay1)
                {
                    TxtResult.Text += "first surrounds the second" + Environment.NewLine;
                }
                //We check of both points of 'a' are between both points of 'b', then 'b' surrounds 'a'.
                else if (ax1 > bx1 && ax1 < bx2 && ax2 > bx1 && ax2 < bx2 && ay1 < by2 && ay1 > by1)
                {
                    TxtResult.Text += "second overlaps the first" + Environment.NewLine;
                }
            }
        }
    }
}
