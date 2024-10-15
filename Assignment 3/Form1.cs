namespace Assignment_3
{
    public partial class Assignment3Form : Form
    {
        public Assignment3Form()
        {
            InitializeComponent();
            pictureBox1.Click += pictureBox1_Click;
            pictureBox2.Click += pictureBox2_Click;
            pictureBox3.Click += pictureBox3_Click;
            pictureBox4.Click += pictureBox4_Click;
            pictureBox5.Click += pictureBox5_Click;
        }
        private const int NO_CARD = -1;
        private List<int> deck = new List<int>();
        private int[] hand = new int[5] { NO_CARD, NO_CARD, NO_CARD, NO_CARD, NO_CARD };
        private void Assignment3Form_Load(object sender, EventArgs e)
        {
            DealHand();
        }
        private void DealHand()
        {
            if (!chkKeep1.Checked && !chkKeep2.Checked && !chkKeep3.Checked && !chkKeep4.Checked && !chkKeep5.Checked)
            {
                ResetDeck();
                ShuffleDeck();
            }

            if (!chkKeep1.Checked) DealCardToHand(0);
            if (!chkKeep2.Checked) DealCardToHand(1);
            if (!chkKeep3.Checked) DealCardToHand(2);
            if (!chkKeep4.Checked) DealCardToHand(3);
            if (!chkKeep5.Checked) DealCardToHand(4);

            UpdateHandDisplay();
        }

        private void DealCardToHand(int position)
        {
            if (deck.Count > 0)
            {
                hand[position] = deck[0];
                deck.RemoveAt(0);
            }
        }

        private void UpdateHandDisplay()
        {
            PictureBox[] pictureBoxes = { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5 };

            for (int i = 0; i < hand.Length; i++)
            {
                if (hand[i] >= 0 && hand[i] < imageList1.Images.Count)
                {
                    pictureBoxes[i].Image = imageList1.Images[hand[i]];
                }
                else
                {
                    pictureBoxes[i].Image = null;
                }
            }
        }

        private void btnSaveHand_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = @"C:\";
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.Filter = "Text files (*.txt)|*.txt";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        foreach (var card in hand)
                        {
                            writer.WriteLine(card);
                        }
                    }
                }
            }
        }

        private void btnLoadHand_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = @"C:\";
                openFileDialog.DefaultExt = "txt";
                openFileDialog.Filter = "Text files (*.txt)|*.txt";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                    {
                        for (int i = 0; i < hand.Length && !reader.EndOfStream; i++)
                        {
                            hand[i] = int.Parse(reader.ReadLine());
                        }
                    }
                    UpdateHandDisplay();
                }
            }
        }

        private void ShuffleDeck()
        {
            Random rng = new Random();
            int n = deck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                int value = deck[k];
                deck[k] = deck[n];
                deck[n] = value;
            }
        }

        private void ResetDeck()
        {
            deck.Clear();
            for (int i = 0; i < imageList1.Images.Count; i++)
            {
                deck.Add(i);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            chkKeep1.Checked = !chkKeep1.Checked;  
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            chkKeep2.Checked = !chkKeep2.Checked;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            chkKeep3.Checked = !chkKeep3.Checked;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            chkKeep4.Checked = !chkKeep4.Checked;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            chkKeep5.Checked = !chkKeep5.Checked;
        }


        private void btnDeal_Click(object sender, EventArgs e)
        {
            DealHand(); 
        }

    }
}
