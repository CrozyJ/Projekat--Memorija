using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication13
{
    public partial class Form1 : Form
    {
        // Game Configuration
        private const string ImageFolderName = "Images";
        private const string HighScoreFile = "highscore.txt";

        private List<string> imagePaths = new List<string>();
        private List<PictureBox> cardBoxes = new List<PictureBox>();

        // Game State Tracking
        private PictureBox firstClicked = null;
        private PictureBox secondClicked = null;
        private Timer flipTimer = new Timer(); // Delay for mismatches

        private int secondsElapsed = 0;
        private int totalMatchesFound = 0;
        private int highScore = int.MaxValue;

        // Animation State Lock
        private bool isAnimating = false;

        public Form1()
        {
            InitializeComponent();

            // Force hook-up for designer elements
            this.btnRestart.Click += new EventHandler(btnRestart_Click);
            this.gameTimer.Tick += new EventHandler(gameTimer_Tick);

            SetupFlipTimer();
            LoadHighScore();
            InitializeGame();
        }

        private void SetupFlipTimer()
        {
            flipTimer.Interval = 850; // Slightly longer to let the flip animation finish
            flipTimer.Tick += new EventHandler(FlipTimer_Tick);
        }

        private void InitializeGame()
        {
            secondsElapsed = 0;
            totalMatchesFound = 0;
            lblTime.Text = "Time: 0s";
            firstClicked = null;
            secondClicked = null;
            gameTimer.Stop();
            isAnimating = false;

            foreach (PictureBox box in cardBoxes)
            {
                this.Controls.Remove(box);
                box.Dispose();
            }
            cardBoxes.Clear();

            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string imagesDir = Path.Combine(exePath, ImageFolderName);

            if (!Directory.Exists(imagesDir) || Directory.GetFiles(imagesDir).Length < 8)
            {
                MessageBox.Show("Please ensure the '" + ImageFolderName + "' folder exists next to the .exe and contains at least 8 images.", "Missing Assets");
                return;
            }

            List<string> files = Directory.GetFiles(imagesDir).Take(8).ToList();
            imagePaths = files.Concat(files).ToList();

            Random rng = new Random();
            imagePaths = imagePaths.OrderBy(x => rng.Next()).ToList();

            int cardSize = 90;
            int padding = 10;
            int startX = 20;
            int startY = 60;

            for (int i = 0; i < 16; i++)
            {
                int row = i / 4;
                int col = i % 4;

                PictureBox pb = new PictureBox();
                pb.Width = cardSize;
                pb.Height = cardSize;
                pb.Left = startX + (col * (cardSize + padding));
                pb.Top = startY + (row * (cardSize + padding));
                pb.BorderStyle = BorderStyle.FixedSingle;
                pb.BackColor = Color.DarkSlateGray;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Tag = imagePaths[i];

                pb.Click += new EventHandler(Card_Click);

                cardBoxes.Add(pb);
                this.Controls.Add(pb);
            }
        }

        /// <summary>
        /// This method creates the visual "3D" flip illusion by adjusting width and placement.
        /// </summary>
        private void AnimateFlip(PictureBox pb, Image targetImage)
        {
            int originalWidth = 90; // Matches our card size
            int originalLeft = pb.Left;
            int step = 10; // How many pixels it shrinks/grows per frame

            // PHASE 1: Shrink the card horizontally to the center
            while (pb.Width > step)
            {
                pb.Width -= step;
                pb.Left += step / 2; // Move it right so it looks centered
                pb.Refresh();        // Force Windows Forms to redraw immediately
                System.Threading.Thread.Sleep(10); // A tiny delay (10ms) per frame
            }

            // SWAP: Swap out the face-down background for the real image (or vice versa)
            pb.Image = targetImage;

            // PHASE 2: Stretch the card back out from the center
            while (pb.Width < originalWidth)
            {
                pb.Width += step;
                pb.Left -= step / 2; // Move it back left to maintain position
                pb.Refresh();
                System.Threading.Thread.Sleep(10);
            }

            // SAFETY RESET: Ensure absolute perfect alignment at the end
            pb.Width = originalWidth;
            pb.Left = originalLeft;
        }

        private void Card_Click(object sender, EventArgs e)
        {
            PictureBox clickedCard = sender as PictureBox;

            // Ignore click if: mismatch timer is running, an animation is playing,
            // card is already revealed, or clicking same card twice
            if (flipTimer.Enabled || isAnimating || clickedCard.Image != null || clickedCard == firstClicked)
                return;

            if (secondsElapsed == 0 && !gameTimer.Enabled)
            {
                gameTimer.Start();
            }

            // Lock input and trigger the opening flip animation
            isAnimating = true;
            Image cardImage = Image.FromFile(clickedCard.Tag.ToString());
            AnimateFlip(clickedCard, cardImage);
            isAnimating = false;

            if (firstClicked == null)
            {
                firstClicked = clickedCard;
            }
            else
            {
                secondClicked = clickedCard;

                if (firstClicked.Tag.ToString() == secondClicked.Tag.ToString())
                {
                    firstClicked = null;
                    secondClicked = null;
                    totalMatchesFound++;

                    if (totalMatchesFound == 8)
                    {
                        EndGame();
                    }
                }
                else
                {
                    flipTimer.Start();
                }
            }
        }

        private void FlipTimer_Tick(object sender, EventArgs e)
        {
            flipTimer.Stop();

            // Lock input while both mismatched cards flip back face-down sequentially
            isAnimating = true;
            AnimateFlip(firstClicked, null);
            AnimateFlip(secondClicked, null);
            isAnimating = false;

            firstClicked = null;
            secondClicked = null;
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            secondsElapsed++;
            lblTime.Text = "Time: " + secondsElapsed + "s";
        }

        private void EndGame()
        {
            gameTimer.Stop();
            MessageBox.Show("Congratulations! You solved it in " + secondsElapsed + " seconds.", "Victory!");

            if (secondsElapsed < highScore)
            {
                highScore = secondsElapsed;
                lblHighScore.Text = "High Score: " + highScore + "s";
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, HighScoreFile), highScore.ToString());
                MessageBox.Show("New High Score!", "🏆 Winner");
            }
        }

        private void LoadHighScore()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, HighScoreFile);
            if (File.Exists(path))
            {
                int savedScore;
                if (int.TryParse(File.ReadAllText(path), out savedScore))
                {
                    highScore = savedScore;
                    lblHighScore.Text = "High Score: " + highScore + "s";
                    return;
                }
            }
            lblHighScore.Text = "High Score: --";
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            InitializeGame();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        bool button1Clicked = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1Clicked)
            {
                gameTimer.Start();
                button1.Text = "Pause";
                button1Clicked = false;
                return;
            }
            gameTimer.Stop();
            button1.Text = "Resume";
            button1Clicked = true;
        }
    }
}