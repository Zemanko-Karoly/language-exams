using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace nyelvVizsga
{
    public partial class Form1 : Form
    {



        int osszesen = 0;//for counting overall statistics
        List<string> nyelv = new List<string>();//languages
        List<int> osszSiker = new List<int>();//all succes from all languages and all years
        List<int> osszSikertelen = new List<int>();//all fails from all languages and all years
        List<int> osszember = new List<int>();//all examinees from all years

        // successful and failed exams based on years, (lists whit lists in them)
        List<List<int>> ev09 = new List<List<int>>();
        List<List<int>> ev10 = new List<List<int>>();
        List<List<int>> ev11 = new List<List<int>>();
        List<List<int>> ev12 = new List<List<int>>();
        List<List<int>> ev13 = new List<List<int>>();
        List<List<int>> ev14 = new List<List<int>>();
        List<List<int>> ev15 = new List<List<int>>();
        List<List<int>> ev16 = new List<List<int>>();
        List<List<int>> ev17 = new List<List<int>>();
        List<List<int>> ev18 = new List<List<int>>();
        public Form1()
        {
            InitializeComponent();

            //disable buttons
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
        }

        // scanning (beolvas button).
        private void button1_Click(object sender, EventArgs e)
        {
            var sikerAdat = new StreamReader(File.OpenRead("sikeres.csv"), Encoding.Default);//open sikeres.csv file
            var sikertelenAdat = new StreamReader(File.OpenRead("sikertelen.csv"), Encoding.Default);//open sikertelen.csv file


            //read data and save into lists

            while (!sikerAdat.EndOfStream)
            {
                var sor1 = sikerAdat.ReadLine();//successful exams
                var sor2 = sikertelenAdat.ReadLine();//failed exams
                var ertek1 = sor1.Split(';');//split successful
                var ertek2 = sor2.Split(';');//split failed

                nyelv.Add(ertek1[0]);//save lanuages

                List<int> adat09 = new List<int>();//part data list
                adat09.Add(Convert.ToInt32(ertek1[1]));//load part data list with successful
                adat09.Add(Convert.ToInt32(ertek2[1]));//load part data list with failed
                ev09.Add(adat09);//load parta data list into listed list

                List<int> adat10 = new List<int>();
                adat10.Add(Convert.ToInt32(ertek1[2]));
                adat10.Add(Convert.ToInt32(ertek2[2]));
                ev10.Add(adat10);

                List<int> adat11 = new List<int>();
                adat11.Add(Convert.ToInt32(ertek1[3]));
                adat11.Add(Convert.ToInt32(ertek2[3]));
                ev11.Add(adat11);

                List<int> adat12 = new List<int>();
                adat12.Add(Convert.ToInt32(ertek1[4]));
                adat12.Add(Convert.ToInt32(ertek2[4]));
                ev12.Add(adat12);

                List<int> adat13 = new List<int>();
                adat13.Add(Convert.ToInt32(ertek1[5]));
                adat13.Add(Convert.ToInt32(ertek2[5]));
                ev13.Add(adat13);

                List<int> adat14 = new List<int>();
                adat14.Add(Convert.ToInt32(ertek1[6]));
                adat14.Add(Convert.ToInt32(ertek2[6]));
                ev14.Add(adat14);

                List<int> adat15 = new List<int>();
                adat15.Add(Convert.ToInt32(ertek1[7]));
                adat15.Add(Convert.ToInt32(ertek2[7]));
                ev15.Add(adat15);

                List<int> adat16 = new List<int>();
                adat16.Add(Convert.ToInt32(ertek1[8]));
                adat16.Add(Convert.ToInt32(ertek2[8]));
                ev16.Add(adat16);

                List<int> adat17 = new List<int>();
                adat17.Add(Convert.ToInt32(ertek1[9]));
                adat17.Add(Convert.ToInt32(ertek2[9]));
                ev17.Add(adat17);

                List<int> adat18 = new List<int>();
                adat18.Add(Convert.ToInt32(ertek1[10]));
                adat18.Add(Convert.ToInt32(ertek2[10]));
                ev18.Add(adat18);

            }

            
            //remove at index 0 (remove lanuage names and years)
            nyelv.RemoveAt(0);
            ev09.RemoveAt(0);
            ev10.RemoveAt(0);
            ev11.RemoveAt(0);
            ev12.RemoveAt(0);
            ev13.RemoveAt(0);
            ev14.RemoveAt(0);
            ev15.RemoveAt(0);
            ev16.RemoveAt(0);
            ev17.RemoveAt(0);
            ev18.RemoveAt(0);

            //close opened files
            sikerAdat.Close();
            sikertelenAdat.Close();

            //disable button beolvas
            button1.Enabled = false;

            //enable buttons
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;

            //load with data osszSiker and osszSikertelen list
            for (int i = 0; i < nyelv.Count; i++)
            {
                //first index: which list(language). Second index: in language successfull or failed(0:successful,,,1:failed)
                osszesen = ev09[i][0] + ev10[i][0] + ev11[i][0] + ev12[i][0] + ev13[i][0] + ev14[i][0] + ev15[i][0] + ev16[i][0] + ev17[i][0] + ev18[i][0];
                osszSiker.Add(osszesen);

                osszesen = 0;

                osszesen = ev09[i][1] + ev10[i][1] + ev11[i][1] + ev12[i][1] + ev13[i][1] + ev14[i][1] + ev15[i][1] + ev16[i][1] + ev17[i][1] + ev18[i][1];
                osszSikertelen.Add(osszesen);

                osszesen = 0;

            }
            //load osszember list with data
            for (int i = 0; i < osszSiker.Count; i++)
            {
                osszesen = osszSiker[i] + osszSikertelen[i];
                osszember.Add(osszesen);
            }
        }

        //Népszerű nyelvek button
        private void button2_Click(object sender, EventArgs e)
        {
            //összember in descending with linq
            List<int> csokken = new List<int>(osszember.OrderByDescending(i => i));
            listBox1.Items.Clear();

            //looking for popular based on first three
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < osszember.Count; j++)
                {
                    if (csokken[i]==osszember[j])
                    {
                        listBox1.Items.Add(nyelv[j]);
                    }
                }

            }


        }

        // év ellenőr button
        private void button3_Click(object sender, EventArgs e)
        {
            //ellenőrizzük az évszámot
            if (Convert.ToInt32(textBox1.Text)<=2018 && Convert.ToInt32(textBox1.Text)>=2009)
            {
                listBox1.Items.Clear();

                double arany = double.MinValue, temp = 0;//arány mentésére és ideiglenes mentésre
                int vizsgaszam = 0, index=0;//sikertelen és sikertelen vizsgák összegzésére az adott évben és index mentésre
                List<int> nincsVizsga = new List<int>();//ide mentjük ahol nem találtunk vizsgázót

                //switchelünk az évszám alapján.
                switch (Convert.ToInt32(textBox1.Text))
                {
                    case 2009:
                        for (int i = 0; i < nyelv.Count; i++)
                        {
                            //adott nyelv össz vizsgája az évben
                            vizsgaszam = ev09[i][0] + ev09[i][1];
                            //ha incs vizsgázó lementjük a helyet és továbbmegyünk a következő nyelvre
                            while (vizsgaszam==0)
                            {
                                nincsVizsga.Add(i);
                                i++;
                                vizsgaszam = ev09[i][0] + ev09[i][1];

                            }
                            temp = (Convert.ToDouble(ev09[i][1]) / vizsgaszam) * 100;//arány számítás
                            if (temp > arany)//ha nagyobb mint az eddigi akkor lementjük és az indexet is
                            {
                                arany = temp;
                                index = i;
                            }

                        }
                        listBox1.Items.Add($"{nyelv[index]} nyelvből a vizsgázók {Math.Round(arany, 2)}% -a bukott meg 2009 -ben.");

                        //ellenőrizzük hogy volt e nyelv amiből nem volt vizsgázó
                        if (nincsVizsga.Count!=0)
                        {
                            listBox1.Items.Add($"Továbbá ebben az écben nem volt vizsgázó a következő nyelv(ek) -ből: ");

                            //kiírjuk a nyelveket amiből nem volt vizsgázó
                            foreach (var item in nincsVizsga)
                            {
                                listBox1.Items.Add($"   {nyelv[item]}");

                            }
                        }
                        else
                        {
                            listBox1.Items.Add("Ebben az évben minden nyelvből volt vizsgázó.");
                        }

                        break;


                    case 2010:
                        for (int i = 0; i < nyelv.Count; i++)
                        {
                            vizsgaszam = ev10[i][0] + ev10[i][1];
                            while (vizsgaszam == 0)
                            {
                                nincsVizsga.Add(i);
                                i++;
                                vizsgaszam = ev10[i][0] + ev10[i][1];

                            }
                            temp = (Convert.ToDouble(ev10[i][1]) / vizsgaszam) * 100;
                            if (temp > arany)
                            {
                                arany = temp;
                                index = i;
                            }

                        }
                        listBox1.Items.Add($"{nyelv[index]} nyelvből a vizsgázók {Math.Round(arany, 2)}% -a bukott meg 2010 -ben.");
                        if (nincsVizsga.Count != 0)
                        {
                            listBox1.Items.Add($"Továbbá ebben az écben nem volt vizsgázó a következő nyelv(ek) -ből: ");
                            foreach (var item in nincsVizsga)
                            {
                                listBox1.Items.Add($"   {nyelv[item]}");

                            }
                        }
                        else
                        {
                            listBox1.Items.Add("Ebben az évben minden nyelvből volt vizsgázó.");
                        }
                        break;

                    case 2011:
                        for (int i = 0; i < nyelv.Count; i++)
                        {
                            vizsgaszam = ev11[i][0] + ev11[i][1];
                            while (vizsgaszam == 0)
                            {
                                nincsVizsga.Add(i);

                                i++;
                                vizsgaszam = ev11[i][0] + ev11[i][1];

                            }
                            temp = (Convert.ToDouble(ev11[i][1]) / vizsgaszam) * 100;
                            if (temp > arany)
                            {
                                arany = temp;
                                index = i;
                            }

                        }
                        listBox1.Items.Add($"{nyelv[index]} nyelvből a vizsgázók {Math.Round(arany, 2)}% -a bukott meg 2011 -ben.");
                        if (nincsVizsga.Count != 0)
                        {
                            listBox1.Items.Add($"Továbbá ebben az écben nem volt vizsgázó a következő nyelv(ek) -ből: ");
                            foreach (var item in nincsVizsga)
                            {
                                listBox1.Items.Add($"   {nyelv[item]}");

                            }
                        }
                        else
                        {
                            listBox1.Items.Add("Ebben az évben minden nyelvből volt vizsgázó.");
                        }
                        break;

                    case 2012:
                        for (int i = 0; i < nyelv.Count; i++)
                        {
                            vizsgaszam = ev12[i][0] + ev12[i][1];
                            while (vizsgaszam == 0)
                            {
                                nincsVizsga.Add(i);

                                i++;
                                vizsgaszam = ev12[i][0] + ev12[i][1];

                            }
                            temp = (Convert.ToDouble(ev12[i][1]) / vizsgaszam) * 100;
                            if (temp > arany)
                            {
                                arany = temp;
                                index = i;
                            }

                        }
                        listBox1.Items.Add($"{nyelv[index]} nyelvből a vizsgázók {Math.Round(arany, 2)}% -a bukott meg 2012 -ben.");
                        if (nincsVizsga.Count != 0)
                        {
                            listBox1.Items.Add($"Továbbá ebben az écben nem volt vizsgázó a következő nyelv(ek) -ből: ");
                            foreach (var item in nincsVizsga)
                            {
                                listBox1.Items.Add($"   {nyelv[item]}");

                            }
                        }
                        else
                        {
                            listBox1.Items.Add("Ebben az évben minden nyelvből volt vizsgázó.");
                        }
                        break;

                    case 2013:
                        for (int i = 0; i < nyelv.Count; i++)
                        {
                            vizsgaszam = ev13[i][0] + ev13[i][1];
                            while (vizsgaszam == 0)
                            {
                                nincsVizsga.Add(i);

                                i++;
                                vizsgaszam = ev13[i][0] + ev13[i][1];

                            }
                            temp = (Convert.ToDouble(ev13[i][1]) / vizsgaszam) * 100;
                            if (temp > arany)
                            {
                                arany = temp;
                                index = i;
                            }

                        }
                        listBox1.Items.Add($"{nyelv[index]} nyelvből a vizsgázók {Math.Round(arany, 2)}% -a bukott meg 2013 -ban.");
                        if (nincsVizsga.Count != 0)
                        {
                            listBox1.Items.Add($"Továbbá ebben az écben nem volt vizsgázó a következő nyelv(ek) -ből: ");
                            foreach (var item in nincsVizsga)
                            {
                                listBox1.Items.Add($"   {nyelv[item]}");

                            }
                        }
                        else
                        {
                            listBox1.Items.Add("Ebben az évben minden nyelvből volt vizsgázó.");
                        }
                        break;

                    case 2014:
                        for (int i = 0; i < nyelv.Count; i++)
                        {
                            vizsgaszam = ev14[i][0] + ev14[i][1];
                            while (vizsgaszam == 0)
                            {
                                nincsVizsga.Add(i);

                                i++;
                                vizsgaszam = ev14[i][0] + ev14[i][1];

                            }
                            temp = (Convert.ToDouble(ev14[i][1]) / vizsgaszam) * 100;
                            if (temp > arany)
                            {
                                arany = temp;
                                index = i;
                            }

                        }
                        listBox1.Items.Add($"{nyelv[index]} nyelvből a vizsgázók {Math.Round(arany, 2)}% -a bukott meg 2014 -ben.");
                        if (nincsVizsga.Count != 0)
                        {
                            listBox1.Items.Add($"Továbbá ebben az écben nem volt vizsgázó a következő nyelv(ek) -ből: ");
                            foreach (var item in nincsVizsga)
                            {
                                listBox1.Items.Add($"   {nyelv[item]}");
                            }
                        }
                        else
                        {
                            listBox1.Items.Add("Ebben az évben minden nyelvből volt vizsgázó.");
                        }
                        break;

                    case 2015:
                        for (int i = 0; i < nyelv.Count; i++)
                        {
                            vizsgaszam = ev15[i][0] + ev15[i][1];
                            while (vizsgaszam == 0)
                            {
                                nincsVizsga.Add(i);

                                i++;
                                vizsgaszam = ev15[i][0] + ev15[i][1];

                            }
                            temp = (Convert.ToDouble(ev15[i][1]) / vizsgaszam) * 100;
                            if (temp > arany)
                            {
                                arany = temp;
                                index = i;
                            }

                        }
                        listBox1.Items.Add($"{nyelv[index]} nyelvből a vizsgázók {Math.Round(arany, 2)}% -a bukott meg 2015 -ben.");
                        if (nincsVizsga.Count != 0)
                        {
                            listBox1.Items.Add($"Továbbá ebben az écben nem volt vizsgázó a következő nyelv(ek) -ből: ");
                            foreach (var item in nincsVizsga)
                            {
                                listBox1.Items.Add($"   {nyelv[item]}");

                            }
                        }
                        else
                        {
                            listBox1.Items.Add("Ebben az évben minden nyelvből volt vizsgázó.");
                        }
                        break;

                    case 2016:
                        for (int i = 0; i < nyelv.Count; i++)
                        {
                            vizsgaszam = ev16[i][0] + ev16[i][1];
                            while (vizsgaszam == 0)
                            {
                                nincsVizsga.Add(i);

                                i++;
                                vizsgaszam = ev16[i][0] + ev16[i][1];

                            }
                            temp = (Convert.ToDouble(ev16[i][1]) / vizsgaszam) * 100;
                            if (temp > arany)
                            {
                                arany = temp;
                                index = i;
                            }

                        }
                        listBox1.Items.Add($"{nyelv[index]} nyelvből a vizsgázók {Math.Round(arany, 2)}% -a bukott meg 2016 -ban.");
                        if (nincsVizsga.Count != 0)
                        {
                            listBox1.Items.Add($"Továbbá ebben az écben nem volt vizsgázó a következő nyelv(ek) -ből: ");
                            foreach (var item in nincsVizsga)
                            {
                                listBox1.Items.Add($"   {nyelv[item]}");

                            }
                        }
                        else
                        {
                            listBox1.Items.Add("Ebben az évben minden nyelvből volt vizsgázó.");
                        }
                        break;

                    case 2017:
                        for (int i = 0; i < nyelv.Count; i++)
                        {
                            vizsgaszam = ev17[i][0] + ev17[i][1];
                            while (vizsgaszam == 0)
                            {
                                nincsVizsga.Add(i);

                                i++;
                                vizsgaszam = ev17[i][0] + ev17[i][1];

                            }
                            temp = (Convert.ToDouble(ev17[i][1]) / vizsgaszam) * 100;
                            if (temp > arany)
                            {
                                arany = temp;
                                index = i;
                            }

                        }
                        listBox1.Items.Add($"{nyelv[index]} nyelvből a vizsgázók {Math.Round(arany, 2)}% -a bukott meg 2017 -ben.");
                        if (nincsVizsga.Count != 0)
                        {
                            listBox1.Items.Add($"Továbbá ebben az écben nem volt vizsgázó a következő nyelv(ek) -ből: ");
                            foreach (var item in nincsVizsga)
                            {
                                listBox1.Items.Add($"   {nyelv[item]}");

                            }
                        }
                        else
                        {
                            listBox1.Items.Add("Ebben az évben minden nyelvből volt vizsgázó.");
                        }
                        break;

                    case 2018:
                        for (int i = 0; i < nyelv.Count; i++)
                        {
                            vizsgaszam = ev18[i][0] + ev18[i][1];
                            while (vizsgaszam == 0)
                            {
                                nincsVizsga.Add(i);

                                i++;
                                vizsgaszam = ev18[i][0] + ev18[i][1];

                            }
                            temp = (Convert.ToDouble(ev18[i][1]) / vizsgaszam) * 100;
                            if (temp > arany)
                            {
                                arany = temp;
                                index = i;
                            }

                        }
                        listBox1.Items.Add($"{nyelv[index]} nyelvből a vizsgázók {Math.Round(arany, 2)}% -a bukott meg 2018 -ban.");
                        if (nincsVizsga.Count != 0)
                        {
                            listBox1.Items.Add($"Továbbá ebben az écben nem volt vizsgázó a következő nyelv(ek) -ből: ");
                            foreach (var item in nincsVizsga)
                            {
                                listBox1.Items.Add($"   {nyelv[item]}");

                            }
                        }
                        else
                        {
                            listBox1.Items.Add("Ebben az évben minden nyelvből volt vizsgázó.");
                        }
                        break;

                }
            }
            else
            {
                //ha nem jó a megadott évszám
                MessageBox.Show("Nem megfelelő évszám. Próbáld újra. (2009 - 2018)","Hiba", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //összesítés button
        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < nyelv.Count; i++)
            {
                File.AppendAllText("E:\\osszesites.csv", $"{nyelv[i]};{osszember[i]};{Math.Round((Convert.ToDouble(osszSiker[i]) / osszember[i]) * 100, 2)}%;\n",Encoding.UTF8);
            }
        }
    }
}
