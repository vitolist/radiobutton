using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace radiobutton
{
    public partial class Form1 : Form
    {
        int brojGrupa = 2;
        public Form1()
        {
            InitializeComponent();
        }

        Point pozicijaZadnjeg()
        {
            var grupe = Controls.OfType<GroupBox>().OrderBy(x => x.Name).ToArray();
            GroupBox zadnja = grupe[grupe.Length - 1];
            Point pozicija = zadnja.FindForm().PointToClient(zadnja.Parent.PointToScreen(zadnja.Location));
            return pozicija;
        }

        void dodajGrupu()
        {
            GroupBox novaGrupa = new GroupBox();
            novaGrupa.Name = "grupa" + brojGrupa.ToString();

            TextBox noviFaktor = new TextBox();
            noviFaktor.Name = "faktor" + (brojGrupa + 1).ToString();

            RadioButton add = new RadioButton();
            add.Text = "+";
            add.Name = "add" + brojGrupa.ToString();
            add.Location = new Point(6, 31);

            RadioButton sub = new RadioButton();
            sub.Text = "-";
            sub.Name = "sub" + brojGrupa.ToString();
            sub.Location = new Point(6, 54);

            RadioButton div = new RadioButton();
            div.Text = "/";
            div.Name = "div" + brojGrupa.ToString();
            div.Location = new Point(6, 77);

            RadioButton mul = new RadioButton();
            mul.Text = "*";
            mul.Name = "mul" + brojGrupa.ToString();
            mul.Location = new Point(6, 100);

            novaGrupa.Controls.Add(add);
            novaGrupa.Controls.Add(sub);
            novaGrupa.Controls.Add(div);
            novaGrupa.Controls.Add(mul);

            Point pozicija = pozicijaZadnjeg();

            novaGrupa.Location = new Point(pozicija.X + 125, pozicija.Y);
            novaGrupa.Size = new Size(115, 136);

            noviFaktor.Location = new Point(pozicija.X + 125, pozicija.Y - 26);
            noviFaktor.Size = new Size(100, 20);

            Controls.Add(novaGrupa);
            Controls.Add(noviFaktor);

            brojGrupa++;
        }

        private void submit_Click(object sender, EventArgs e)
        {
            ArrayList operatori = new ArrayList();

            var faktori = Controls.OfType<TextBox>().OrderBy(x => x.Name).ToArray();
            var grupe = Controls.OfType<GroupBox>().OrderBy(x => x.Name).ToArray();

            foreach (GroupBox grupa in grupe)
            {
                var checkedButton = grupa.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
                operatori.Add(checkedButton.Text);
            }

            DataTable dt = new DataTable();

            int i = 0;
            string izraz = "";

            foreach (TextBox faktor in faktori)
            {
                if (faktor.Name.StartsWith("faktor"))
                {
                    izraz += faktor.Text;
                    if (i < operatori.Count)
                        izraz += operatori[i];
                    i++;
                }
            }
            racun.Text = izraz;

            Console.WriteLine(izraz);
            var rez = dt.Compute(izraz, "");
            result.Text = rez.ToString();
        }

        private void dodaj_Click(object sender, EventArgs e)
        {
            dodajGrupu();
        }
    }
}
