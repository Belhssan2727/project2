using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp5
{
    internal class Logique
    {
        HttpClient c = new HttpClient();







        //verification si le texte entré est un lien ou pas (controle de saisie)
        public bool UrlVerif(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }










        //ajout d'un site a telechargé dans la liste
        public void AjoutSite(List<Website> Sites, DataGridView d, string nom, string lien)
        {
            Website w = new Website();
            if (UrlVerif(lien))
                {
                w.Name = nom;
                w.Link = lien;
                Sites.Add(w);
                d.DataSource = null;
                d.DataSource = Sites;
                d.ClearSelection();

            }

            else
            {
                MessageBox.Show("Please enter a valid website");
            }


        }








        //telecharger les sites selectionnées du datagrid
        public async Task TelechargerSite(DataGridView d, List<string> notif, List<string> HtmlResultat)
        {
            if (d.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row");
                return;
            }

            Form2 f2 = new Form2();
            f2.Show();

            foreach (DataGridViewRow row in d.SelectedRows)
            {
                Website w = row.DataBoundItem as Website;

                UpdateStatus(notif, $"Downloading {w.Name}...", f2);

                try
                {
                    string result = await c.GetStringAsync(w.Link);
                    await Task.Delay(4000);
                    HtmlResultat.Add(result);

                    UpdateStatus(notif, $"{w.Name} downloaded successfully", f2);
                }
                catch (Exception ex)
                {
                    UpdateStatus(notif, $"{w.Name} failed to download", f2);
                }
            }

            UpdateStatus(notif, "Download finished", f2);
            notif.Clear();
        }
        private void UpdateStatus(List<string> notif, string message, Form2 form)
        {
            notif.Add(message);
            form.UpdateList(notif);
        }








        //supprimer un site du datagrid
        public void supprimerSite(DataGridView d, List<Website> Sites )
        {
            if (d.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in d.SelectedRows)
                {
                    Website w = row.DataBoundItem as Website;
                    Sites.Remove(w);
                    d.DataSource = null;
                    d.DataSource = Sites;
                }

            }
            else
            {
                MessageBox.Show("Please select a Website(s)");
            }
        }









    }
        

}





    

