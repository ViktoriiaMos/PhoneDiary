using System.Data;
using System.Data.SqlClient;

namespace PhoneDiary
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=PhoneDiary;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //при запуску програми курсор знаходиться на textBox2
            // this.ActiveControl = textBox2;
            // textBox2.Focus();
            Display();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Clear();
            textBox3.Text = "";
            textBox4.Clear();
            // для очищення comboBox1
            comboBox1.SelectedIndex = -1;
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //  SqlCommand - only for insert, update, delete (send data to the database)
            // SqlDataReader - select (get data from database)
            //SqlAdapter - for both, to send data to database and to get data from database (a bit slower then 2 above)
            //with SqlAdapter dont need open and close the connection
            con.Open();
            SqlCommand cmd = new SqlCommand(@"INSERT INTO Mobiles
             (First, Last, Mobile, Email, Catagory)
             VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + comboBox1.Text + "')", con);
            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("Successfully saved!");
            Display();
        }

        //show all data from the db in datagridview
        void Display()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from Mobiles", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item[0].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();
            }
        }

        //onClick show info in textboxes
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            //based on a primary key
            SqlCommand cmd = new SqlCommand(@"DELETE FROM Mobiles WHERE (Mobile = '" + textBox3.Text + "')", con);
            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("Delete Successfully!");
            Display();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            //based on a primary key
            SqlCommand cmd = new SqlCommand(@"UPDATE Mobiles SET First='" + textBox1.Text + "'," +
                "Last='" + textBox2.Text + "', Mobile='" + textBox3.Text + "', Email='" + textBox4.Text + "'," +
                " Catagory='" + comboBox1.Text + "' WHERE (Mobile = '" + textBox3.Text + "')", con);
            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("Update Successfully!");
            Display();
        }

        //search in dataGridView
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from Mobiles WHERE (First like '%" + textBox5.Text + "%') or (Last like '%" + textBox5.Text + "%')", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item[0].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();
            }
        }
    }
}