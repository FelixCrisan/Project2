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

namespace Project2
{
    /// <summary>
    /// Interaction logic for angajatiWindow.xaml
    /// </summary>
    public partial class angajatiWindow : Window
    {
        public angajatiWindow()
        {
            InitializeComponent();

            myDBEntities1 db = new myDBEntities1();
            var docs = from d in db.Angajatis
                       select new
                       {
                           NumeAngajati = d.Name,
                           Speciality = d.Specialization
                       };
            foreach (var item in docs)
            {

                Console.WriteLine(item.NumeAngajati);
                Console.WriteLine(item.Speciality);
            }

            this.gridAngajati.ItemsSource = docs.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            myDBEntities1 db = new myDBEntities1();

            Angajati angjatiObject = new Angajati()
            {

                Name = txtName.Text,
                Qualification = txtQualification.Text,
                Specialization = txtSpecialization.Text

            };

            db.Angajatis.Add(angjatiObject);
            db.SaveChanges();

        }

        private void btnLoadAngajati_Click(object sender, RoutedEventArgs e)
        {
            myDBEntities1 db = new myDBEntities1();
      
            this.gridAngajati.ItemsSource = db.Angajatis.ToList();
        }
        private int updatingAngajatiID = 0;
        private void gridAngajati_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridAngajati.SelectedIndex >= 0)
            {
                if (this.gridAngajati.SelectedItems.Count >= 0)
                {

                    if (this.gridAngajati.SelectedItems[0].GetType() == typeof(Angajati))
                    {
                        Angajati d = (Angajati)this.gridAngajati.SelectedItems[0];
                        this.txtName2.Text = d.Name;
                        this.txtSpecialization2.Text = d.Specialization;
                        this.txtQualification2.Text = d.Qualification;
                        this.updatingAngajatiID = d.Id;
                    }
                }
            }

        }

        private void btnUpdateAngajati_Click(object sender, RoutedEventArgs e)
        {
            myDBEntities1 db = new myDBEntities1();

            var r = from d in db.Angajatis
                    where d.Id == this.updatingAngajatiID
                    select d;

            Angajati obj = r.SingleOrDefault();

            if(obj != null)
            {
                obj.Name = this.txtName2.Text;
                obj.Specialization = this.txtSpecialization2.Text;
                obj.Qualification = this.txtQualification2.Text;

                db.SaveChanges();
            }
        }

        private void btnRemoveAngajati_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult msgBoxresult = MessageBox.Show("Are you sure you want to Remove Angajat?",
                "Remove Angajat",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning,
                MessageBoxResult.No
                );

            if (msgBoxresult == MessageBoxResult.Yes)
            {

                myDBEntities1 db = new myDBEntities1();

                var r = from d in db.Angajatis
                        where d.Id == this.updatingAngajatiID
                        select d;

                Angajati obj = r.SingleOrDefault();

                if (obj != null)
                {
                    db.Angajatis.Remove(obj);
                    db.SaveChanges();
                }
            }
        }
    }
}
