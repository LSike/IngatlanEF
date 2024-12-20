﻿using IngatlanEF.IngatlanokWindows;
using IngatlanEF.Models;
using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IngatlanEF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string JELSZO = "Asgragh";
        public static bool isLogged = false;
        public static string logName = "";
        public static string[] tipusok = {"családi ház","lakás","építési telek","raktárépület","nyaraló","ba"};
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LogInOut(object sender, RoutedEventArgs e)
        {
            if (isLogged)
            {
                lblBejelentkezve.Content = "Bejelentkezve:";
                isLogged = false;
                menuBelepes.Header = "Belépés";
                menuIngatlanok.IsEnabled = false;
                menuUgyintezok.IsEnabled = false;
                menuExport.IsEnabled = false;
            }
            else
            {
                //megnyitok egy új ablakot, ott bekérem a jelszót és a felhasználónevet
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
                if (isLogged)
                {
                    //ha az helyes, akkor 
                    //aktiválom a másik két menüpontot
                    menuIngatlanok.IsEnabled = true;
                    menuUgyintezok.IsEnabled = true;
                    menuExport.IsEnabled = true;
                    //kiíratom a felhasználónevet
                    lblBejelentkezve.Content = $"Bejelentkezve: {logName}";
                    menuBelepes.Header = "Kilépés";
                }
            }
        }

        private void IngatlanokListaja(object sender, RoutedEventArgs e)
        {
            IngatlanListaWindow ingatlanListaWindow = new IngatlanListaWindow();
            ingatlanListaWindow.ShowDialog();
        }

        private void IngatlanokFelvitele(object sender, RoutedEventArgs e)
        {
            IngatlanokFelviteleWindow ingFelWin = new IngatlanokFelviteleWindow();
            ingFelWin.ShowDialog();
        }

        private void IngatlanokModositasa(object sender, RoutedEventArgs e)
        {
            IngatlanokModositasaWindow ingModWin = new IngatlanokModositasaWindow();
            ingModWin.ShowDialog();
        }

        private void Export(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog() { 
                FileName = "export.txt",
                DefaultExt = "*.txt",
                Filter = "txt|*.txt"
            };
            if (sfd.ShowDialog() == true)
            {
                if (File.Exists(sfd.FileName))
                {
                    MessageBoxResult result = MessageBox.Show("A fájl már létezik, felülírja?","Figyelmeztetés",MessageBoxButton.YesNo,MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        SaveIngatlansAs(sfd);

                    }
                }
                else
                {
                    SaveIngatlansAs(sfd);
                }
            }
            else
            {
                MessageBox.Show("Nincs kiválasztott állomány!");
            }
        }

        private static void SaveIngatlansAs(SaveFileDialog sfd)
        {
            using (var context = new MiskolcingatlanContext())
            {
                try
                {
                    List<Ingatlan> ingatlanok = context.Ingatlans.ToList();
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        foreach (Ingatlan ingatlan in ingatlanok)
                        {
                            sw.WriteLine($"{ingatlan.Település}, {ingatlan.Cim}\n" +
                                $"{ingatlan.Ár}");
                        }
                        sw.Close();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}