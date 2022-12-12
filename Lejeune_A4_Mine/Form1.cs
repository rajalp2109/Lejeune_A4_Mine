using System;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form1 : Form
    {
        const string PROGRAMMER = "Gregory Lejeune";
        int counter = 1;
        public Form1()
        {
            InitializeComponent();
        }

        private int GetRandom(int min, int max)
        /* Name: GetRandom
        * Send: int min, int max
        * Return: int coolNumber
        * Description: Generates a number for login purposes */

        {
            Random rand = new Random();
            int coolNumber = rand.Next(min, max);
            return coolNumber;
        }

        private void Form1_Load(object sender, EventArgs e)
        //Loads form, gets a random generated code, and displays only the login group box
        {
            int min = 100000;
            int max = 200000;
            this.Text += " " + PROGRAMMER;
            grpChoose.Visible = false;
            grpStats.Visible = false;
            grpText.Visible = false;
            txtCode.Focus();

            int generatedCode = GetRandom(min, max);
            lblCode.Text = generatedCode.ToString();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        //When clicking the login button, it checks your entry against random generated code and tracks attempts made until locked out
        {

            if (txtCode.Text == lblCode.Text)
            {
                grpChoose.Visible = true;
                grpLogin.Enabled = false;


            }
            else if (counter < 3)
            {
                MessageBox.Show(counter + " incorrect code(s) entered\nTry again - only 3 attempts allowed");
                counter++;


            }
            else
            {
                MessageBox.Show("3 attempts to login\nAccount locked - Closing program", PROGRAMMER);
                this.Close();
            }
        }
        private void ResetTextGrp()
        /* Name: ResetTextGrp
        * Send: none
        * Return: none
        * Description: Clears out text in the Text Groupbox */
        {
            txtString1.Clear();
            txtString2.Clear();
            chkSwap.Checked = false;
            lblResults.Text = "";
            this.AcceptButton = btnJoin;
            this.CancelButton = btnReset;
        }
        private void ResetStatsGrp()
        /* Name: ResetStatsGrp
        * Send: none
        * Return: none
        * Description: Clears out text in the Stat Groupbox */
        {
            numTotal.Value = 10;
            lblSumOutput.Text = "";
            lblMeanOutput.Text = "";
            lblOddOutput.Text = "";
            this.AcceptButton = btnGenerate;
            this.CancelButton = btnClear;
            lstNumbers.Items.Clear();
        }
        private void SetupOption()
        /* Name: ResetStatsGrp
        * Send: none
        * Return: none
        * Description: Checks which radio button out of Stat or Text is checked, and makes visable the respective selection */
        {

            if (radStats.Checked == true)
            {
                grpStats.Visible = true;
                grpText.Visible = false;
                ResetStatsGrp();
            }
            else
            {
                grpText.Visible = true;
                grpStats.Visible = false;
                ResetTextGrp();
            }
        }

        private void radText_CheckedChanged(object sender, EventArgs e)
        //Calls SetupOption - more details in function
        {
            SetupOption();
        }

        private void radStats_CheckedChanged(object sender, EventArgs e)
        //Calls SetupOption - more details in function
        {
            SetupOption();

        }

        private void btnReset_Click(object sender, EventArgs e)
        //Calls ResetTextGrp - more details in function
        {
            ResetTextGrp();
        }

        private void btnClear_Click(object sender, EventArgs e)
        //Calls ResetStatsGrp - more details in function
        {
            ResetStatsGrp();
        }

        private void Swap(ref string AString, ref string BString)
        /* Name: ResetStatsGrp
        * Send: ref string Astring, ref string BString
        * Return: none
        * Description: Swaps strings that were input in text section */
        {
            string swappingStrings;

            swappingStrings = AString;
            AString = BString;
            BString = swappingStrings;
        }

        private bool CheckInput()
        /* Name: CheckInput
        * Send: None
        * Return: bool dataCheck
        * Description: Validates that strings in text section have data */
        {
            bool dataCheck;

            if (txtString1.Text == "" || txtString2.Text == "")
            {
                dataCheck = false;
            }
            else
            {
                dataCheck = true;
            }
            return dataCheck;
        }

        private void chkSwap_CheckedChanged(object sender, EventArgs e)
        //When user selects the swap checkbox, it will check if user inputted data and swaps the strings accordingly.
        //Also puts text in the results label letting the user know the strings have been swapped
        {
            bool testData = CheckInput();

            if (testData)
            {
                string firstString = txtString1.Text;
                string secondString = txtString2.Text;
                Swap(ref firstString, ref secondString);
                txtString1.Text = firstString;
                txtString2.Text = secondString;
                lblResults.Text = "";
                lblResults.Text += "Strings have been swapped!\n";
            }
        }

        private void btnJoin_Click(object sender, EventArgs e)
        //Checks to see if there is data in strings - then displays the strings and shows what they look like joined in results label
        {
            bool testData = CheckInput();

            if (testData)
            {
                lblResults.Text = "";
                lblResults.Text = "First string = " + txtString1.Text + "\nSecond string = " + txtString2.Text + "\n" + "Joined = " + txtString1.Text + "-->" + txtString2.Text;
            }
            else
            {
                lblResults.Text = "";
            }
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        //Checks if there is data in the text boxes, then provides user data on how many characters are in each string
        {
            bool testData = CheckInput();

            if (testData)
            {
                lblResults.Text = "";
                lblResults.Text = "First string = " + txtString1.Text + "\n Characters = " + txtString1.TextLength + "\nSecond string = " + txtString2.Text + "\n Characters = " + txtString2.TextLength;
            }

            else
            {
                lblResults.Text = "";
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        //Generates a list of numbers based on numeric selector amount. Provides a Sum, an Odd count, and the mean for the list values generated.
        {
            Random rand = new Random(733);
            lstNumbers.Items.Clear();
            for (int i = 0; i < numTotal.Value; i++)
            {
                lstNumbers.Items.Add(rand.Next(1000, 5000 + 1));
            }

            int addSum = AddList();
            lblSumOutput.Text = addSum.ToString("N0");

            int oddCount = CountOdd();
            lblOddOutput.Text = oddCount.ToString();

            int mean = addSum / lstNumbers.Items.Count;
            lblMeanOutput.Text = mean.ToString("N2");
        }

        private int AddList()
        /* Name: AddList
        * Send: None
        * Return: int totalSum
        * Description: Runs through a while loop to add all numbers in list */
        {
            int counter, totalIndex, totalSum;
            totalIndex = lstNumbers.Items.Count;
            totalSum = 0;
            counter = 0;



            while (counter < totalIndex)
            {

                int nextNumber = Convert.ToInt32(lstNumbers.Items[counter]);
                totalSum += nextNumber;
                counter++;
            }
            return totalSum;
        }
        private int CountOdd()
        /* Name: CountOdd
        * Send: None
        * Return: int totalOdds
        * Description: Runs through a do while loop to validate if number is odd, then count those numbers */
        {
            int counter, totalIndex, totalOdds, nextNumber;
            totalIndex = lstNumbers.Items.Count;
            totalOdds = 0;
            counter = 0;

            do
            {
                nextNumber = Convert.ToInt32(lstNumbers.Items[counter]);
                if (nextNumber % 2 == 1)
                    totalOdds++;
                counter++;

            }
            while (counter < totalIndex);
            return totalOdds;

        }
    }
}