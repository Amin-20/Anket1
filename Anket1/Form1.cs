using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TheArtOfDevHtmlRenderer.Adapters.RGraphicsPath;

namespace Anket1
{

    public partial class Form1 : Form
    {
        List<Human> humans = new List<Human>();
        Human human;
        Human user;

        List<Human> GetAllPersons()
        {
            List<Human> persons = new List<Human>();
            var files = Directory.GetFiles(".");
            foreach (var item in files)
            {
                if (item.EndsWith(".json"))
                {
                    var obj = JsonConvert.DeserializeObject<Human>(File.ReadAllText(item));
                    persons.Add(obj);
                }
            }
            return persons;
        }

        public Form1()
        {
            InitializeComponent();
            personListb.Items.AddRange(humans.ToArray());
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                                                              Color.FromArgb(0, 79, 249),
                                                              Color.FromArgb(255, 249, 76),
                                                              45F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            human = new Human();
            human.Name = nameTxtb.Text;
            human.Filename = human.Name;
            human.Surname = surnameTxtb.Text;
            human.Phone = phoneTxtb.Text;
            human.Email = emailTxtb.Text;
            human.BirthDay = gunaBirthDayTxtb.Text;
            fileNameTxtb.Text = human.Filename + ".json";
            personListb.Items.Add(human);
            personListb.DisplayMember = nameof(Human.Name);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            FileHelper.WriteJsonHuman(human);
            nameTxtb.Text = "";
            surnameTxtb.Text = "";
            emailTxtb.Text = "";
            phoneTxtb.Text = "";
            gunaBirthDayTxtb.Text = "";
            fileNameTxtb.Text = "";
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            if (fileNameTxtb.Text == "")
            {
                var persons = GetAllPersons();
                personListb.Items.Clear();
                personListb.Items.AddRange(persons.ToArray());
                personListb.DisplayMember = nameof(human.Name);
            }
            if (fileNameTxtb.Text != "" && !fileNameTxtb.Text.Contains(".json"))
            {
                fileNameTxtb.Text += ".json";
            }
            if (File.Exists(fileNameTxtb.Text))
            {
                user = FileHelper.ReadJsonHuman(fileNameTxtb.Text);
                nameTxtb.Text = user.Name;
                surnameTxtb.Text = user.Surname;
                emailTxtb.Text = user.Email;
                phoneTxtb.Text = user.Phone;
                gunaBirthDayTxtb.Text = user.BirthDay;
            }
        }

        private void personListb_SelectedIndexChanged(object sender, EventArgs e)
        {
            var human = personListb.SelectedItem as Human;
            fileNameTxtb.Text = human.Filename;

            nameTxtb.Text = human.Name;
            surnameTxtb.Text = human.Surname;
            emailTxtb.Text = human.Email;
            phoneTxtb.Text = human.Phone;
            gunaBirthDayTxtb.Text = human.BirthDay;
        }

        private void changeBtn_Click(object sender, EventArgs e)
        {
            try
            {
                fileNameTxtb.Text = user.Filename + ".json";
                if (nameTxtb.Text != user.Name)
                {
                    user.Name = nameTxtb.Text;
                }
                if (human.Name != user.Name)
                {
                    human.Filename = user.Filename;
                }
                else
                {
                    fileNameTxtb.Text = user.Filename + ".json";
                }
                if (surnameTxtb.Text != user.Surname)
                {
                    user.Surname = surnameTxtb.Text;
                }
                if (emailTxtb.Text != user.Email)
                {
                    user.Email = emailTxtb.Text;
                }
                if (phoneTxtb.Text != user.Phone)
                {
                    user.Phone = phoneTxtb.Text;
                }
                if (gunaBirthDayTxtb.Text != user.BirthDay)
                {
                    user.BirthDay = gunaBirthDayTxtb.Value.ToString();
                }
                human = user;
                FileHelper.WriteJsonHuman(human);
            }
            catch (Exception)
            {


            }
        }
    }


}