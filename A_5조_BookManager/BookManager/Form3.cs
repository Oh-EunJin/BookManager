using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookManager
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            Text = "사용자 관리";
            // 데이터 그리드 설정
            DataGridView1.DataSource = DataManager.Users;
            DataGridView1.CurrentCellChanged += DataGridView1_CurrentCellChanged;
            // 버튼 설정
            button1.Click += (sender, e) =>
            {
                // 추가 버튼
                try
                {
                    if (DataManager.Users.Exists((x) => x.Id == int.Parse(textBox1.Text)))
                    {
                        MessageBox.Show("사용자 ID가 겹칩니다");
                    }
                    else
                    {
                        User user = new User()
                        {
                            Id = int.Parse(textBox1.Text),
                            Name = textBox2.Text
                        };
                        DataManager.Users.Add(user);
                        DataGridView1.DataSource = null;
                        DataGridView1.DataSource = DataManager.Users;
                        DataManager.Save();
                    }
                }
                catch (Exception exception)
                {
                }
                clear();
            };
            button2.Click += (sender, e) =>
            {
                // 수정 버튼
                try
                {
                    User user = DataManager.Users.Single((x) => x.Id == int.Parse(textBox1.Text));
                    user.Name = textBox2.Text;
                    DataGridView1.DataSource = null;
                    DataGridView1.DataSource = DataManager.Users;
                }
                catch (Exception exception)
                {
                    MessageBox.Show("존재하지 않는 사용자입니다");
                }
                clear();
            };
            button3.Click += (sender, e) =>
            {
                // 수정 버튼
                try
                {
                    User user = DataManager.Users.Single((x) => x.Id == int.Parse(textBox1.Text));
                    DataManager.Users.Remove(user);
                    DataGridView1.DataSource = null;
                    DataGridView1.DataSource = DataManager.Users;
                    DataManager.Save();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("존재하지 않는 사용자입니다");
                }
                clear();
            };
        }

        private void DataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                // 그리드의 셀이 선택되면 텍스트박스에 글자 지정
                User user = DataGridView1.CurrentRow.DataBoundItem as User;
                textBox1.Text = user.Id.ToString();
                textBox2.Text = user.Name;
            }
            catch (Exception exception)
            {
            }
        }
        private void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            new Form1().ShowDialog();
            this.Close();
        }
    }
}
