using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProgrammingLanguageListApp
{
    public partial class Form1 : Form
    {
        private Label lblTitle;
        private Label lblLanguage;
        private TextBox txtLanguage;
        private Button btnAdd;
        private Button btnRemove;
        private ListBox lstLanguages;
        private Label lblDateTime;

        public Form1()
        {
            InitializeComponent();
            BuildForm();
        }

        private void BuildForm()
        {
            this.Text = "Programming Language List App";
            this.Size = new Size(600, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            lblTitle = new Label();
            lblTitle.Text = "Programming Language Manager";
            lblTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(130, 30);
            this.Controls.Add(lblTitle);

            lblLanguage = new Label();
            lblLanguage.Text = "Enter Programming Language:";
            lblLanguage.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblLanguage.AutoSize = true;
            lblLanguage.Location = new Point(50, 90);
            this.Controls.Add(lblLanguage);

            txtLanguage = new TextBox();
            txtLanguage.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            txtLanguage.Size = new Size(300, 30);
            txtLanguage.Location = new Point(50, 120);
            this.Controls.Add(txtLanguage);

            btnAdd = new Button();
            btnAdd.Text = "Add Language";
            btnAdd.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            btnAdd.Size = new Size(140, 35);
            btnAdd.Location = new Point(370, 118);
            btnAdd.Click += btnAdd_Click;
            this.Controls.Add(btnAdd);

            btnRemove = new Button();
            btnRemove.Text = "Remove Selected";
            btnRemove.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            btnRemove.Size = new Size(160, 35);
            btnRemove.Location = new Point(350, 320);
            btnRemove.Click += btnRemove_Click;
            this.Controls.Add(btnRemove);

            lstLanguages = new ListBox();
            lstLanguages.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lstLanguages.Size = new Size(460, 150);
            lstLanguages.Location = new Point(50, 165);
            this.Controls.Add(lstLanguages);

            lblDateTime = new Label();
            lblDateTime.Text = "No action yet.";
            lblDateTime.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            lblDateTime.AutoSize = true;
            lblDateTime.Location = new Point(50, 330);
            this.Controls.Add(lblDateTime);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string language = txtLanguage.Text.Trim();

            if (string.IsNullOrWhiteSpace(language))
            {
                MessageBox.Show("Please enter a programming language.",
                    "Empty Input",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtLanguage.Focus();
                return;
            }

            foreach (var item in lstLanguages.Items)
            {
                if (item.ToString().Equals(language, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("This programming language already exists in the list.",
                        "Duplicate Language",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtLanguage.Clear();
                    txtLanguage.Focus();
                    return;
                }
            }

            lstLanguages.Items.Add(language);

            lblDateTime.Text = $"Language added: {language} | {DateTime.Now}";

            txtLanguage.Clear();
            txtLanguage.Focus();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstLanguages.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a programming language to remove.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string selectedLanguage = lstLanguages.SelectedItem.ToString();

            DialogResult confirmResult = MessageBox.Show(
                $"Are you sure you want to remove {selectedLanguage}?",
                "Confirm Remove",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                lstLanguages.Items.Remove(selectedLanguage);

                lblDateTime.Text = $"Language removed: {selectedLanguage} | {DateTime.Now}";

                MessageBox.Show($"{selectedLanguage} was removed successfully.",
                    "Removed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }
}