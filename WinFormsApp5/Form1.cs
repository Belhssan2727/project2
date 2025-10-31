using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace WinFormsApp5
{
    public partial class Form1 : Form
    {
        List<Website> Sites = new List<Website>();

        List<string> html = new List<string>();

        HttpClient c = new HttpClient();

        Logique l = new Logique();





        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = null;

        }





        // ajout d'un site dans le datagrid
        private void button1_Click(object sender, EventArgs e)
        {
            l.AjoutSite(Sites, dataGridView1, textBox1.Text, textBox2.Text);

            textBox1.Clear();
            textBox2.Clear();

        }





        //telecharger les site selectionnés dans la datagrid
        public static List<string> notifications = new List<string>(); //liste de msg de notifs
        private async void button2_Click(object sender, EventArgs e)
        {
            await l.TelechargerSite(dataGridView1, notifications, html);
        }









        //supprimer un site du datagrid
        private void button3_Click(object sender, EventArgs e)
        {
            l.supprimerSite(dataGridView1, Sites);

            textBox1.Clear();
            textBox2.Clear();

        }









    }
}
