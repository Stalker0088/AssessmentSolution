using System;
using System.Drawing;
using System.Windows.Forms;

namespace HomeAffairsDigitalIdentityProcessor
{
    public partial class Form1 : Form
    {
        private Label lblTitle;
        private Label lblName;
        private Label lblIDNumber;
        private Label lblCitizenship;

        private TextBox txtName;
        private TextBox txtIDNumber;
        private ComboBox cmbCitizenship;

        private Button btnValidate;
        private Button btnGenerateProfile;

        private TextBox txtResults;

        private CitizenProfile currentProfile;

        public Form1()
        {
            InitializeComponent();
            BuildForm();
        }

        private void BuildForm()
        {
            this.Text = "Home Affairs Digital Identity Processor";
            this.Size = new Size(760, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            lblTitle = new Label();
            lblTitle.Text = "Home Affairs Digital Identity Processor";
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(150, 30);
            this.Controls.Add(lblTitle);

            lblName = new Label();
            lblName.Text = "Full Name:";
            lblName.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblName.AutoSize = true;
            lblName.Location = new Point(60, 100);
            this.Controls.Add(lblName);

            txtName = new TextBox();
            txtName.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            txtName.Size = new Size(280, 30);
            txtName.Location = new Point(220, 96);
            this.Controls.Add(txtName);

            lblIDNumber = new Label();
            lblIDNumber.Text = "ID Number:";
            lblIDNumber.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblIDNumber.AutoSize = true;
            lblIDNumber.Location = new Point(60, 150);
            this.Controls.Add(lblIDNumber);

            txtIDNumber = new TextBox();
            txtIDNumber.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            txtIDNumber.Size = new Size(280, 30);
            txtIDNumber.Location = new Point(220, 146);
            this.Controls.Add(txtIDNumber);

            lblCitizenship = new Label();
            lblCitizenship.Text = "Citizenship Status:";
            lblCitizenship.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblCitizenship.AutoSize = true;
            lblCitizenship.Location = new Point(60, 200);
            this.Controls.Add(lblCitizenship);

            cmbCitizenship = new ComboBox();
            cmbCitizenship.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            cmbCitizenship.Size = new Size(280, 30);
            cmbCitizenship.Location = new Point(220, 196);
            cmbCitizenship.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCitizenship.Items.Add("Citizen");
            cmbCitizenship.Items.Add("Permanent Resident");
            cmbCitizenship.Items.Add("Visitor");
            cmbCitizenship.SelectedIndex = 0;
            this.Controls.Add(cmbCitizenship);

            btnValidate = new Button();
            btnValidate.Text = "Validate ID";
            btnValidate.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            btnValidate.Size = new Size(130, 40);
            btnValidate.Location = new Point(220, 250);
            btnValidate.Click += btnValidate_Click;
            this.Controls.Add(btnValidate);

            btnGenerateProfile = new Button();
            btnGenerateProfile.Text = "Generate Profile";
            btnGenerateProfile.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            btnGenerateProfile.Size = new Size(160, 40);
            btnGenerateProfile.Location = new Point(370, 250);
            btnGenerateProfile.Click += btnGenerateProfile_Click;
            this.Controls.Add(btnGenerateProfile);

            txtResults = new TextBox();
            txtResults.Font = new Font("Consolas", 10, FontStyle.Regular);
            txtResults.Multiline = true;
            txtResults.ReadOnly = true;
            txtResults.ScrollBars = ScrollBars.Vertical;
            txtResults.Size = new Size(640, 230);
            txtResults.Location = new Point(60, 320);
            this.Controls.Add(txtResults);
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
            {
                return;
            }

            currentProfile = new CitizenProfile(
                txtName.Text.Trim(),
                txtIDNumber.Text.Trim(),
                cmbCitizenship.SelectedItem.ToString()
            );

            txtResults.Text = currentProfile.ValidateID();
        }

        private void btnGenerateProfile_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
            {
                return;
            }

            currentProfile = new CitizenProfile(
                txtName.Text.Trim(),
                txtIDNumber.Text.Trim(),
                cmbCitizenship.SelectedItem.ToString()
            );

            txtResults.Text = currentProfile.GenerateProfileSummary();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter the full name.",
                    "Missing Name",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtIDNumber.Text))
            {
                MessageBox.Show("Please enter the ID number.",
                    "Missing ID Number",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtIDNumber.Focus();
                return false;
            }

            if (cmbCitizenship.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a citizenship status.",
                    "Missing Citizenship",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbCitizenship.Focus();
                return false;
            }

            return true;
        }
    }
}