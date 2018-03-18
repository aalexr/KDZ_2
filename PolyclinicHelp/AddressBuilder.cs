using System;
using System.Windows.Forms;

namespace PolyclinicHelp
{
    public partial class AddressBuilder : Form
    {
        private readonly StartForm _paretForm;

        private AddressBuilder()
        {
            InitializeComponent();
        }

        public AddressBuilder(Form form) : this()
        {
            _paretForm = form as StartForm;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (double.TryParse(pointXtextBox.Text, out var x) && double.TryParse(pointYtextBox.Text, out var y))
            {
                ((Clinic) _paretForm.ClinicSource.Current).Address = new Address(admTextBox.Text, districtTextBox.Text, addressTextBox.Text, x, y);
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Введенные координаты не являются числом. Попробуйте еще раз!");
                pointXtextBox.Text = pointYtextBox.Text = string.Empty;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }
    }
}
