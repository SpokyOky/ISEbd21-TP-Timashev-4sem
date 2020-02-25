using SecurityBusinessLogic.ViewModels;
using SecurityBusinessLogic.Interfaces;
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
using SecurityBusinessLogic.BindingModels;

namespace SecuritySystemView
{
    public partial class FormEquipment : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }
        private readonly IEquipmentLogic logic;
        private int? id;
        private Dictionary<int, (string, int)> equipmentRaws;

        public FormEquipment(IEquipmentLogic service)
        {
            InitializeComponent();
            this.logic = service;
        }

        private void FormEquipment_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    EquipmentViewModel view = logic.Read(new EquipmentBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.EquipmentName;
                        textBoxCost.Text = view.Cost.ToString();
                        equipmentRaws = view.EquipmentRaws;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                equipmentRaws = new Dictionary<int, (string, int)>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (equipmentRaws != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in equipmentRaws)
                    {
                        dataGridView.Rows.Add(new object[] { pc.Key, pc.Value.Item1,
pc.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormEquipmentRaw>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (equipmentRaws.ContainsKey(form.Id))
                {
                    equipmentRaws[form.Id] = (form.RawName, form.Count);
                }
                else
                {
                    equipmentRaws.Add(form.Id, (form.RawName, form.Count));
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormEquipmentRaw>();
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                form.Id = id;
                form.Count = equipmentRaws[id].Item2;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    equipmentRaws[form.Id] = (form.RawName, form.Count);
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                        equipmentRaws.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxCost.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (equipmentRaws == null || equipmentRaws.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new EquipmentBindingModel
                {
                    Id = id,
                    EquipmentName = textBoxName.Text,
                    Cost = Convert.ToDecimal(textBoxCost.Text),
                    EquipmentRaws = equipmentRaws
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
