using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using System.IO;

namespace JSON_Trial
{
    public partial class Form1 : Form
    {
        Subject Datasub = new Subject();
        string path = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\database.json";
        public Form1()
        {
            InitializeComponent();
            Readjson();
            ReadRowJSON(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clearjson();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Readjson();
        }
        private void Readjson()
        {
            string jsonString = File.ReadAllText(path);

            //Singular Mode
            //Subject databaseread = JsonConvert.DeserializeObject<Subject>(jsonString);
            //dataGridView1.Rows.Clear();
            //dataGridView1.Rows.Add();
            //dataGridView1.Rows[0].Cells[0].Value = databaseread.EmployeeName;
            //dataGridView1.Rows[0].Cells[1].Value = databaseread.EmployeeId;
            //dataGridView1.Rows[0].Cells[2].Value = databaseread.EmployeeCity;
            //dataGridView1.Rows[0].Cells[3].Value = databaseread.EmployeeStatus;

            //Array Mode
            var jsobject = JObject.Parse(jsonString);
            JArray item = (JArray)jsobject["EmployeeName"];
            dataGridView1.Rows.Clear();
            
            for(int i = 0; i < item.Count; i++)
            {
                dataGridView1.Rows.Add(jsobject["EmployeeName"][i], jsobject["EmployeeId"][i], jsobject["EmployeeCity"][i], jsobject["EmployeeStatus"][i]);    
            }
        }
        private void ReadRowJSON(int i)
        {
            string jsonString = File.ReadAllText(path);
            var jsobject = JObject.Parse(jsonString);
            textBox1.Text = jsobject["EmployeeName"][i].ToString();
            textBox2.Text = jsobject["EmployeeId"][i].ToString();
            textBox3.Text = jsobject["EmployeeCity"][i].ToString();
            textBox4.Text = jsobject["EmployeeStatus"][i].ToString();
            label9.Text = i.ToString();
        }
        private void WriteRowJSON()
        {
            int i = Convert.ToInt32(label9.Text);
            string jsonString = File.ReadAllText(path);
            Subject databaseread = JsonConvert.DeserializeObject<Subject>(jsonString);
            databaseread.EmployeeName[i] = textBox1.Text;
            databaseread.EmployeeId[i] = Convert.ToInt32(textBox2.Text);
            databaseread.EmployeeCity[i] = textBox3.Text;
            databaseread.EmployeeStatus[i] = textBox4.Text;

            string result = JsonConvert.SerializeObject(databaseread);
            if (File.Exists(path))
            {
                File.Delete(path);
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(result.ToString());
                    tw.Close();
                }
            }
            else if (!File.Exists(path))
            {
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(result.ToString());
                    tw.Close();
                }
            }
            Readjson();
        }
        private void Clearjson()
        {
            string jsonString = File.ReadAllText(path);
            var jsobject = JObject.Parse(jsonString);
            JArray item = (JArray)jsobject["EmployeeName"];

            ////Singular mode
            //Datasub.EmployeeName = "";
            //Datasub.EmployeeId = 0;
            //Datasub.EmployeeCity = "";
            //Datasub.EmployeeStatus = "";
            //string result = JsonConvert.SerializeObject(Datasub);

            //Array Mode
            List<string> stringlist = new List<string>();
            List<int> intlist = new List<int>();
            for (int j = 0; j < item.Count; j++)
            {
                intlist.Add(0);
                stringlist.Add("");
            }
            Datasub.EmployeeName = stringlist.ToArray();
            Datasub.EmployeeId = intlist.ToArray();
            Datasub.EmployeeCity = stringlist.ToArray();
            Datasub.EmployeeStatus = stringlist.ToArray();
            string result = JsonConvert.SerializeObject(Datasub);

            if (File.Exists(path))
            {
                File.Delete(path);
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(result.ToString());
                    tw.Close();
                }
            }
            else if (!File.Exists(path))
            {
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(result.ToString());
                    tw.Close();
                }
            }
            Readjson();
        }
        private void Updatejson()
        {
            List<string> name = new List<string>();
            List<int> id = new List<int>();
            List<string> city = new List<string>();
            List<string> status = new List<string>();

            for(int i = 0; i<dataGridView1.Rows.Count-1; i++)
            {
                name.Add(dataGridView1.Rows[i].Cells[0].Value.ToString());
                id.Add(Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value));
                city.Add(dataGridView1.Rows[i].Cells[2].Value.ToString());
                status.Add(dataGridView1.Rows[i].Cells[3].Value.ToString());
            }
            Datasub.EmployeeName = name.ToArray();
            Datasub.EmployeeId =id.ToArray();
            Datasub.EmployeeCity = city.ToArray();
            Datasub.EmployeeStatus= status.ToArray();

            string result = JsonConvert.SerializeObject(Datasub);
            if (File.Exists(path))
            {
                File.Delete(path);
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(result.ToString());
                    tw.Close();
                }
            }
            else if (!File.Exists(path))
            {
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(result.ToString());
                    tw.Close();
                }
            }
            Readjson();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Updatejson();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (label9.Text !="0")
            {
                int i = Convert.ToInt32(label9.Text);
                ReadRowJSON(i-1);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string jsonString = File.ReadAllText(path);
            var jsobject = JObject.Parse(jsonString);
            JArray item = (JArray)jsobject["EmployeeName"];
            int i = Convert.ToInt32(label9.Text);
            if (i<item.Count-1)
            {
                ReadRowJSON(i+1);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            WriteRowJSON();
        }
    }
}
