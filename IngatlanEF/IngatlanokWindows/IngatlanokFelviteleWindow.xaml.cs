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
    /// Interaction logic for IngatlanokFelviteleWindow.xaml
    /// </summary>
    public partial class IngatlanokFelviteleWindow : Window
    {
        List<Ugyintezo> ugyintezok = UgyintezoService.GetAllUgyintezo();
        public IngatlanokFelviteleWindow()
        {
            InitializeComponent();
            cbxTipus.ItemsSource = MainWindow.tipusok;
            foreach (Ugyintezo ugyintezo in ugyintezok)
            {
                cbxUgyintezo.Items.Add($"{ugyintezo.Id}: {ugyintezo.Nev}");
            }
        }

        private void btnMentes_Click(object sender, RoutedEventArgs e)
        {
            Ingatlan ujIngatlan = new()
            {
                Id = 0,
                Település = tbxTelepules.Text,
                Cim = tbxCim.Text,
                Ár = int.Parse(tbxAr.Text),
                Tipus = cbxTipus.Text,
                KepNev = tbxKepnev.Text,
                UgyintezoId = int.Parse(cbxUgyintezo.Text.Split(":")[0])
            };
            IngatlanService.IngatlanInsert(ujIngatlan);
            MessageBox.Show("Sikeres rögzítés.");
        }

        private void btnMegsem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
