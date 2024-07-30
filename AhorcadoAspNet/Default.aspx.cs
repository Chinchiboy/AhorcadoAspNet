using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AhorcadoAspNet
{
    public partial class Default : Page
    {
        private static readonly string[] Words = { "javascript", "programacion", "ahorcado", "desarrollo", "html", "css" };

        private string SelectedWord
        { get { return (string)ViewState["SelectedWord"]; } set { ViewState["SelectedWord"] = value; } }

        private List<char> CorrectLetters
        { get { return (List<char>)ViewState["CorrectLetters"] ?? new List<char>(); } set { ViewState["CorrectLetters"] = value; }}

        private List<char> WrongLetters
        {get { return (List<char>)ViewState["WrongLetters"] ?? new List<char>(); }set { ViewState["WrongLetters"] = value; }}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializeGame();
                BindAlphabetRepeater();
            }
            DisplayWord();
            DisplayWrongLetters();
            UpdateHangmanImage();
        }

        private void InitializeGame()
        {
            Random random = new Random();
            SelectedWord = Words[random.Next(Words.Length)];
            CorrectLetters = new List<char>();
            WrongLetters = new List<char>();
            messageLabel.Text = string.Empty;
            restartButton.Visible = false;
            EnableAlphabetButtons(true);
            UpdateHangmanImage();
        }

        private void BindAlphabetRepeater()
        {
            var alphabet = "abcdefghijklmnopqrstuvwxyz".Select(c => new { Letter = c }).ToList();
            AlphabetRepeater.DataSource = alphabet;
            AlphabetRepeater.DataBind();
        }

        protected void LetterButton_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            char guessedLetter = b.Text[0];
            if (!CorrectLetters.Contains(guessedLetter) && !WrongLetters.Contains(guessedLetter))
            {
                if (SelectedWord.Contains(guessedLetter.ToString()))
                {
                    CorrectLetters.Add(guessedLetter);
                    messageLabel.Text = "¡Correcto!";
                }
                else
                {
                    WrongLetters.Add(guessedLetter);
                    messageLabel.Text = "¡Incorrecto!";
                }
                DisplayWord();
                DisplayWrongLetters();
                CheckGameStatus();
                b.Enabled = false;
                UpdateHangmanImage();
            }
        }

        private void DisplayWord()
        {
            wordContainer.Text = string.Join(" ", SelectedWord.ToCharArray().Select(letter => CorrectLetters.Contains(letter) ? letter.ToString() : "_"));
        }

        private void DisplayWrongLetters()
        {
            wrongLettersContainer.Text = $"Letras incorrectas: {string.Join(", ", WrongLetters)}";
        }

        private void CheckGameStatus()
        {
            if (WrongLetters.Count >= 6)
            {
                messageLabel.Text = $"¡Perdiste! La palabra era \"{SelectedWord}\".";
                EnableAlphabetButtons(false);
                restartButton.Visible = true;
            }
            else if (SelectedWord.All(letter => CorrectLetters.Contains(letter)))
            {
                messageLabel.Text = "¡Ganaste!";
                EnableAlphabetButtons(false);
                restartButton.Visible = true;
            }
        }

        private void UpdateHangmanImage()
        {
            hangmanImage.ImageUrl = $"~/img/ahorcado{WrongLetters.Count}.jpg";
        }

        protected void RestartButton_Click(object sender, EventArgs e)
        {
            InitializeGame();
            BindAlphabetRepeater();
        }

        private void EnableAlphabetButtons(bool enable)
        {
            foreach (RepeaterItem item in AlphabetRepeater.Items)
            {
                Button b = (Button)item.FindControl("btnLetter");
                if (b != null)  b.Enabled = enable;
            }
        }
    }
}
