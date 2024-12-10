using IngatlanEF.Models;
using IngatlanEF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IngatlanEF.IngatlanokWindows
{
    /// <summary>
    /// Interaction logic for IngatlanokModositasaWindow.xaml
    /// </summary>
    public partial class IngatlanokModositasaWindow : Window
    {
        List<Ugyintezo> ugyintezok = UgyintezoService.GetAllUgyintezo();
        List<Ingatlan> ingatlanok;
        public IngatlanokModositasaWindow()
        {
            InitializeComponent();
            CbxSelectFeltolt();
            cbxSelect.SelectedIndex = 0;
            cbxTipus.ItemsSource = MainWindow.tipusok;
            foreach (Ugyintezo ugyintezo in ugyintezok)
            {
                cbxUgyintezo.Items.Add($"{ugyintezo.Id}: {ugyintezo.Nev}");
            }
        }

        private void CbxSelectFeltolt()
        {
            ingatlanok = IngatlanService.GetAllIngatlan();
            cbxSelect.Items.Clear();
            foreach (Ingatlan ingatlan in ingatlanok)
            {
                cbxSelect.Items.Add($"{ingatlan.Id}: {ingatlan.Település}, {ingatlan.Cim}");
            }
        }

        private void btnMegsem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void cbxSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //kiválasztott ingatlan megkeresése
            if (cbxSelect.SelectedIndex >= 0)
            {
                Ingatlan kivalasztott = ingatlanok.FirstOrDefault(i => i.Id == int.Parse(cbxSelect.SelectedItem.ToString().Split(':')[0]));
                //adatok megjelenítése a vezérlőn
                if (kivalasztott != null)
                {
                    tbxTelepules.Text = kivalasztott.Település;
                    tbxCim.Text = kivalasztott.Cim;
                    tbxAr.Text = kivalasztott.Ár.ToString();
                    tbxKepnev.Text = kivalasztott.KepNev;
                    cbxTipus.SelectedItem = MainWindow.tipusok.Contains(kivalasztott.Tipus) ? kivalasztott.Tipus : "ba";
                    cbxUgyintezo.SelectedItem = $"{kivalasztott.UgyintezoId}: {ugyintezok.FirstOrDefault(u => u.Id == kivalasztott.UgyintezoId).Nev}";
                }
                else
                {
                    MessageBox.Show("Nincs kiválasztott elem!");
                }
            }
        }

        private void btnMentes_Click(object sender, RoutedEventArgs e)
        {
            int ujAr = 0;
            int ujUgyi = 0;
            if (int.TryParse(tbxAr.Text, out ujAr) && int.TryParse(cbxUgyintezo.Text.Split(":")[0], out ujUgyi))
            {
                Ingatlan ujIngatlan = new()
                {
                    Id = int.Parse(cbxSelect.SelectedItem.ToString().Split(':')[0]),
                    Település = tbxTelepules.Text,
                    Cim = tbxCim.Text,
                    Ár = ujAr,
                    Tipus = cbxTipus.Text,
                    KepNev = tbxKepnev.Text,
                    UgyintezoId = ujUgyi
                };
                IngatlanService.IngatlanUpdate(ujIngatlan);
                MessageBox.Show("Sikeres rögzítés.");
                CbxSelectFeltolt();
            }
        }
    }
}
