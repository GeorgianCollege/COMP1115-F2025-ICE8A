using System;

namespace ICE8A
{
    enum Career
    {
        Army,
        Psion,
        Rogue,
        Telepath
    }

    public partial class SelectionForm : Form
    {
        // Class Variables (member variables)

        Random random = new Random();

        string[] Careers = Enum.GetNames<Career>();

        int[][] CareerStats =
        [
            [35, 35, 30, 30, 25, 25], // Army
            [30, 35, 30, 25, 35, 25], // Psion
            [35, 30, 30, 35, 25, 25], // Rogue
            [25, 30, 30, 35, 25, 35]  // Telepath
        ];

        // Declaring the PrimaryStatTextBoxes array
        TextBox[] PrimaryStatTextBoxes;

        // Declaring the SecondaryStatTextBoxes array
        TextBox[] SecondaryStatTextBoxes;

        /// <summary>
        /// Constructor function - this triggers when the Form is created (Instantiated)
        /// </summary>
        public SelectionForm()
        {
            InitializeComponent();

            // Populate the ComboBox with career options
            ComboBox_Career.Items.Clear();
            ComboBox_Career.Items.AddRange(Careers);

            // Initialize the PrimaryStatTextBoxes array
            PrimaryStatTextBoxes =
            [
                TextBox_AGL,
                TextBox_STR,
                TextBox_VGR,
                TextBox_PER,
                TextBox_INT,
                TextBox_WIL
            ];

            // Initialize the SecondaryStatTextBoxes array
            SecondaryStatTextBoxes =
            [
                TextBox_AWA,
                TextBox_TOU,
                TextBox_RES
            ];

        }

        /// <summary>
        /// This method generates random values for the character attributes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Random_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Random Generation is Destructive. Are you sure?", "Confirm Random Generation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                foreach (var stat in PrimaryStatTextBoxes)
                {
                    stat.Text = Roll6d10DropLowest().ToString();
                }

                ComputeSecondaryAttributes();
            }
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


        private void ComboBox_Career_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox_Career.SelectedIndex < 0) { return; }

            for (int attribute = 0; attribute < PrimaryStatTextBoxes.Length; attribute++)
            {
                PrimaryStatTextBoxes[attribute].Text = CareerStats[ComboBox_Career.SelectedIndex][attribute].ToString();
            }

            ComputeSecondaryAttributes();

        }
    }
}
