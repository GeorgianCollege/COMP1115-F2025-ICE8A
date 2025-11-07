using System;

namespace ICE8A
{
    public partial class SelectionForm : Form
    {
        Random random = new Random();

        public SelectionForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method generates random values for the character attributes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Generate_Click(object sender, EventArgs e)
        {
            TextBox_AGL.Text = Roll6d10DropLowest().ToString();
            TextBox_STR.Text = Roll6d10DropLowest().ToString();
            TextBox_VGR.Text = Roll6d10DropLowest().ToString();
            TextBox_PER.Text = Roll6d10DropLowest().ToString();
            TextBox_INT.Text = Roll6d10DropLowest().ToString();
            TextBox_WIL.Text = Roll6d10DropLowest().ToString();

            ComputeSecondaryAttributes();
        }

        private void ComputeSecondaryAttributes()
        {
            TextBox_AWA.Text = (Convert.ToInt32(TextBox_AGL.Text) + Convert.ToInt32(TextBox_PER.Text)).ToString();
            TextBox_TOU.Text = (Convert.ToInt32(TextBox_STR.Text) + Convert.ToInt32(TextBox_VGR.Text)).ToString();
            TextBox_RES.Text = (Convert.ToInt32(TextBox_INT.Text) + Convert.ToInt32(TextBox_WIL.Text)).ToString();
        }


        /// <summary>
        /// Deprecated: This method rolls 5d10 and returns the total
        /// </summary>
        /// <returns>An integer value between 5 and 50</returns>
        int Roll5d10()
        {
            int total = 0;
            for (int die = 0; die < 5; die++)
            {
                total += random.Next(1, 11);
            }
            return total;
        }

        /// <summary>
        /// This method rolls 6d10, drops the lowest die, and returns the total of the remaining dice
        /// </summary>
        /// <returns></returns>
        int Roll6d10DropLowest()
        {
            int[] rolls = new int[6];
            for (int die = 0; die < 6; die++)
            {
                rolls[die] = random.Next(1, 11);
            }

            Array.Sort(rolls);

            int total = 0;
            for (int die = 1; die < 6; die++)
            {
                total += rolls[die];
            }
            return total;
        }

    }
}
