using SecurityBusinessLogic.Interfaces;
using SecurityBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace SecuritySystemView
{
    public partial class FormEquipmentRaw : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id
        {
            get { return Convert.ToInt32(comboBoxRaw.SelectedValue); }
            set { comboBoxRaw.SelectedValue = value; }
        }        public string RawName { get { return comboBoxRaw.Text; } }        public int Count
        {
            get { return Convert.ToInt32(textBoxCount.Text); }
            set
            {
                textBoxCount.Text = value.ToString();
            }
        }

        public FormEquipmentRaw(IRawLogic logic)
        {
            InitializeComponent();
            List<RawViewModel> list = logic.Read(null);
            if (list != null)
            {
                comboBoxRaw.DisplayMember = "RawName";
                comboBoxRaw.ValueMember = "Id";
                comboBoxRaw.DataSource = list;
                comboBoxRaw.SelectedItem = null;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxRaw.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
