using pv03_Ferdiansyah_1703924.Model;
using pv03_Ferdiansyah_1703924.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pv03_Ferdiansyah_1703924
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void submitNim_Click(object sender, EventArgs e)
        {
            TodoRepository todo = new TodoRepository();
            
            string nim = tbNim.Text;
            int row = todo.countRows(nim);  //jumlah rows

            if (row > 0)    //ada nim
            {
                dataGridView2.DataSource = todo.getByNim(nim);
                submitNim.Enabled = false;
                tbNim.Enabled = false;
            }
            else    //belum ada nim
            {
                MessageBox.Show("NIM "+ nim.ToString() +" tidak terdaftar");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Todo temp = new Todo();

            temp.NimMhs = tbNim.Text;
            temp.Nama = tbNama.Text;
            temp.Kategori = tbKategori.Text;

            TodoRepository todo = new TodoRepository();

            todo.addTodo(temp);

            string nim = tbNim.Text;

            dataGridView2.DataSource = todo.getByNim(nim);

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;

            Todo temp = new Todo();

            temp.Id = Convert.ToInt16(tbDeleteId.Text);

            TodoRepository todo = new TodoRepository();

            todo.deleteTodo(temp);

            string nim = tbNim.Text;

            dataGridView2.DataSource = todo.getByNim(nim);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            tbUpdateId.Enabled = false;
            btnUpdate.Enabled = false;

            Todo temp = new Todo();

            temp.Id = Convert.ToInt16(tbUpdateId.Text);
            temp.Nama = tbUpdateNama.Text;
            temp.Kategori = tbUpdateKegiatan.Text;

            TodoRepository todo = new TodoRepository();

            todo.updateTodo(temp);

            string nim = tbNim.Text;

            dataGridView2.DataSource = todo.getByNim(nim);
        }
    }
}
