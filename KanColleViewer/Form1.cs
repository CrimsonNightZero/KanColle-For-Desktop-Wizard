using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Threading;


namespace KanColleViewer
{
    public partial class Form1 : Form
    {
        Character character = new Character();
        Furniture furniture = new Furniture();
        Btn_Select btn_select = new Btn_Select();
        Thread mediaplay;
        Image a2;
        Image a;
        public Form1()
        {
            InitializeComponent();
            //this.Opacity = 10;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
            Closing += new CancelEventHandler(Form1_Closing);

               a=Image.FromFile(@"C: \Users\foryou\Documents\Visual Studio 2015\Projects\KanColleViewer\KanColleViewer\bin\Debug\Furniture\wall\009.png");
               a2 = Image.FromFile(@"C: \Users\foryou\Documents\Visual Studio 2015\Projects\KanColleViewer\KanColleViewer\bin\Debug\Furniture\floor\003.png");
               Bitmap bmp = new Bitmap(a2,1000,1000);
               Graphics g2 = Graphics.FromImage(bmp);
               Graphics g = Graphics.FromImage(bmp);
               g2.DrawImage(a, 0, -100,1000,700);
               g.DrawImage(pictureBox1.Image,0,0);
               //  g.Clear(Color.White);
               g.Dispose();
               pictureBox1.Image = bmp;


            //  Graphics g = Graphics.FromImage(pictureBox1.Image);
            // pictureBox1.Image = ;

            /* SolidBrush blueBrush = new SolidBrush(Color.Blue);

             // Create rectangle for region.
             Rectangle fillRect = new Rectangle(100, 100, 200, 200);

             // Create region for fill.
             Region fillRegion = new Region(fillRect);


             Bitmap bit = new Bitmap(pictureBox1.Width, pictureBox1.Height);
             Graphics g3 = Graphics.FromImage(bit);

            // g3.FillRectangle(new SolidBrush(Color.Red), 0, 0, pictureBox1.Width, pictureBox1.Height);
             g3.FillRectangle(new SolidBrush(Color.Transparent), 20, 20, 20, 20);//这块变透明
             pictureBox1.Image = bit;
             g3.Clear(Color.Transparent);*/

            //Intersect(交差部分)によりrect2を追加
            /*   Rectangle re = new Rectangle();
               re.Location = pictureBox1.Location;
               Rectangle re2 = new Rectangle();
               re2.Location = pictureBox2.Location;
               Bitmap bit = new Bitmap(pictureBox1.Width, pictureBox1.Height);
               Graphics g3 = Graphics.FromImage(bit);
               Region rgn = new Region(re2);
               rgn.Intersect(re);
               //出来上がったRegionを黒で描画
               g3.FillRegion(Brushes.Black, rgn);
              pictureBox1.BackColor=*/
            //  pictureBox1.Image = bit;
            // pictureBox3.Controls.Add(pictureBox2);
            // Fill region to screen.


            /*  Bitmap bmpPic2 = new Bitmap(this.pictureBox2.Image);
               Graphics g2 = Graphics.FromImage(bmpPic2);
               g2.DrawImage(this.pictureBox3.Image, new Point(0, 0));
               g2.FillRegion(blueBrush, fillRegion);
               pictureBox2.Image = (Image)bmpPic2;
               Bitmap bmpPic1 = new Bitmap(this.pictureBox2.Image);
                 Graphics g = Graphics.FromImage(bmpPic1);
               g.DrawImage(this.pictureBox1.Image, new Rectangle(200, 0,405,462));

                 pictureBox2.Image = (Image)bmpPic1;

               pictureBox1.Location = new Point(200, 0);*/

             pictureBox1.Parent = pictureBox2;
    pictureBox3.Parent = pictureBox2;
    pictureBox4.Parent = pictureBox2;
    pictureBox5.Parent = pictureBox2;
    pictureBox6.Parent = pictureBox2;
    pictureBox7.Parent = pictureBox2;
            string[] path = new string[] {
             System.Windows.Forms.Application.StartupPath + @"\Character\DD\hibiki1.png",
             System.Windows.Forms.Application.StartupPath + @"\Furniture\chest\001.png",
             System.Windows.Forms.Application.StartupPath + @"\Furniture\desk\001.png",
             System.Windows.Forms.Application.StartupPath + @"\Furniture\floor\001.png",
             System.Windows.Forms.Application.StartupPath + @"\Furniture\object\001.png",
             System.Windows.Forms.Application.StartupPath + @"\Furniture\wall\001.png",
             System.Windows.Forms.Application.StartupPath + @"\Furniture\window\001.png",
              };
             furniture.Type = new string[6];
             furniture.Text  = new string[6];
             furniture.Name  = new string[6];
             string file = "Initial.xml";
             XmlDocument XmlDoc = new XmlDocument();

             if (System.IO.File.Exists(file))
             {
                 XmlDoc.Load(file);
                 XmlNodeList NodeList = XmlDoc.SelectNodes("/kanColleViewer/Type");
                 int i = 0;
                 foreach (XmlNode OneNode in NodeList)
                 {

                     if (i==0)
                     {
                         character.Type = OneNode.Attributes["type"].Value;
                         path[0] = OneNode.Attributes["path"].Value;
                         character.Text = XmlDoc.SelectNodes("//Name")[0].InnerText;
                     }
                     else
                     {
                         furniture.Type[i-1] = OneNode.Attributes["type"].Value;
                         path[i] = OneNode.Attributes["path"].Value;
                         furniture.Text[i-1] = XmlDoc.SelectNodes("//Name")[i].InnerText;

                     }

                     i++;
                 }
             }
             else
             {
                 XmlDeclaration xmldecl;
                 xmldecl = XmlDoc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
                 XmlElement kanColleViewer = XmlDoc.CreateElement("kanColleViewer");
                 XmlDoc.AppendChild(kanColleViewer);
                 XmlDoc.InsertBefore(xmldecl, kanColleViewer);

                 string[] type_xml = new string[] {"DD","chest","desk","floor","object","wall","window"};
                 string[] name_xml = new string[] {"響","001", "001", "001", "001", "001", "001" };
                 for (int i = 0; i < 7; i++)
                 {
                     XmlElement type = XmlDoc.CreateElement("Type");
                     type.SetAttribute("type", type_xml[i]);
                     type.SetAttribute("path", path[i]);
                     kanColleViewer.AppendChild(type);
                     XmlElement name = XmlDoc.CreateElement("Name");
                     name.InnerText = name_xml[i];
                     type.AppendChild(name);
                 }

                 XmlDoc.Save(@"C:\Users\foryou\Documents\Visual Studio 2015\Projects\KanColleViewer\KanColleViewer\bin\Debug\" + "Initial.xml");

             }

             for (int i=0; i < 7; i++)
             {
                 string[] image_temp = new string[2];
                 if (i == 0)
                 {
                     image_temp[0] = Path.GetFileNameWithoutExtension(path[i]);
                     image_temp[1] = character.Text;

                     picture_image(character.Type, path[i], image_temp);
                 }
                 else
                 {
                     image_temp[0] = Path.GetFileNameWithoutExtension(path[i]);
                     image_temp[1] = furniture.Text[i-1];
                     picture_image(furniture.Type[i-1], path[i], image_temp);
                 }
             }

             InitializeTime();
             mediaplay = new Thread(new ParameterizedThreadStart(music_play));
             mediaplay.Start(character.Text);
           pictureBox1.Image = bmp;
        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void xml_updata(string type, string image_path)
        {
            XmlDocument XmlDoc = new XmlDocument();
            string file = "Initial.xml";
            
            if (System.IO.File.Exists(file))
            {
                XmlDoc.Load(file);
                XmlNodeList NodeList = XmlDoc.SelectNodes("/kanColleViewer/Type");
                int i = 0;
                foreach (XmlNode OneNode in NodeList)
                {
                    XmlElement on = (XmlElement)OneNode;
                    if (i == 0)
                    {
                        on.SetAttribute("type", character.Type);
                        on.SetAttribute("path", image_path);
                        on.SelectNodes("//Name")[0].InnerText = character.Text;
                    }
                    else if(OneNode.Attributes["type"].Value.Equals(type))
                    {
                            on.SetAttribute("type", furniture.Type[i - 1]);
                            on.SetAttribute("path", image_path);
                            on.SelectNodes("//Name")[i].InnerText = furniture.Text[i - 1];
                        
                    }
                    i++;
                }
                XmlDoc.Save(@"C:\Users\foryou\Documents\Visual Studio 2015\Projects\KanColleViewer\KanColleViewer\bin\Debug\" + "Initial.xml");
            }
        }

        private void InitializeTime()
        {
            timer1.Interval = 10000000;
            timer1.Enabled = true;
            timer1.Tick += new EventHandler(timer1_Tick);
        }
        private string select_xml(string xx, string x, string lan)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load("Data.xml");
            XmlNodeList NodeList = XmlDoc.SelectNodes("/kanColleViewer" + x.Replace("\\", "/") + "/Type");

            if (lan.Equals("type_ch"))
                foreach (XmlNode OneNode in NodeList)
                {
                    //string aa = XmlDoc.SelectSingleNode("/kanColleViewer/Character/Type").Attributes["type"].Value;
                    //  MessageBox.Show(OneNode.Attributes["type"].Value);
                    try
                    {
                        if (xx.Equals(OneNode.Attributes["type"].Value))
                        {
                            return OneNode.Attributes["ch"].Value;
                        }
                    }
                    catch (Exception ex)
                    {
                        return xx;
                    }

                }

            if (lan.Equals("type_en"))
                foreach (XmlNode OneNode in NodeList)
                {
                    //string aa = XmlDoc.SelectSingleNode("/kanColleViewer/Character/Type").Attributes["type"].Value;

                    try
                    {
                        if (xx.Equals(OneNode.Attributes["ch"].Value))
                        {
                            return OneNode.Attributes["type"].Value;
                        }
                    }
                    catch (Exception ex)
                    {
                        return xx;
                    }
                }

            NodeList = XmlDoc.SelectNodes("/kanColleViewer/Character/Type/Name");
            if (lan.Equals("char_ch"))
                foreach (XmlNode OneNode in NodeList)
                {
                    //string aa = XmlDoc.SelectSingleNode("/kanColleViewer/Character/Type").Attributes["type"].Value;

                    try
                    {
                        if (xx.Equals(OneNode.Attributes["en"].Value))
                        {
                            return OneNode.InnerText;
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        return xx;
                    }


                    //MessageBox.Show(OneNode.Attributes["kanColleViewer"].Value + "");//+""+OneNode.InnerText);
                    /* String StrAttrName = OneNode.Attributes.Name;
                     String StrAttrValue = OneNode.Attributes[" MyAttr1 "].Value;
                     String StrAttrValue = OneNode.InnerText;*/

                }
            return xx;
        }
        private void select_picture(string x, string filename)
        {
            flowLayoutPanel1.Controls.Clear();
            string folderName = System.Windows.Forms.Application.StartupPath + x;
            string folderName2 = System.Windows.Forms.Application.StartupPath + x + "\\" + filename;
            //  DirectoryInfo dirInfo = new DirectoryInfo(folderiName);
            //  int  count=dirInfo.GetFiles("*.png").Length;
            //MessageBox.Show(folderName2);
            string[] filenames = Directory.GetFiles(folderName2); //获取该文件夹下面的所有文件名
            string[] folderNames = Directory.GetDirectories(folderName);
            string[] data = new string[folderNames.Length];
            int i = 0;
            int temp = 0;
            foreach (string fname in folderNames)
            {
                data[i] = Path.GetFileName(fname);
                data[i] = select_xml(data[i], x, "type_ch");
                if (filename.Equals(data[i]))
                    temp = i;

                i++;

            }
            

            ComboBox cb = new ComboBox();
            flowLayoutPanel1.Controls.Add(cb);
            cb.DataSource = data;
            cb.SelectedIndex = temp;
            btn_select.Path = filenames;
            btn_select.Type = select_xml(cb.SelectedValue.ToString(), x, "type_en");

            i = 0;
            foreach (string fname in filenames)
            {
                Button btn = new Button();
                btn.Name = Path.GetFileNameWithoutExtension(fname);
                btn.Text = select_xml(Path.GetFileNameWithoutExtension(fname), x, "char_ch");
                btn.Tag = i;
                btn.Click += new EventHandler(btn_Click);
                flowLayoutPanel1.Controls.Add(btn);
                i++;
            }
            
            cb.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            flowLayoutPanel1.Visible = true;
        }

        //type: 選擇類型 image_path: 圖片位置 image_temp:name 英文名字 text 中文名字
        private void picture_image(string type, string image_path, string[] image_temp)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(image_path);

            switch (type)
            {
                case "chest":
                    pictureBox5.Image = Image.FromStream(fs);
                    furniture.Type[0]= type;
                    furniture.Name[0]= image_temp[0];
                    furniture.Text[0]= image_temp[1];
                    break;
                case "desk":
                    pictureBox7.Image = Image.FromStream(fs);
                    furniture.Type[1] = type;
                    furniture.Name[1] = image_temp[0];
                    furniture.Text[1] = image_temp[1];
                    break;
                case "floor":
                    pictureBox3.Image = Image.FromStream(fs);
                    furniture.Type[2] = type;
                    furniture.Name[2] = image_temp[0];
                    furniture.Text[2] = image_temp[1];
                    a2 = Image.FromFile(@"C: \Users\foryou\Documents\Visual Studio 2015\Projects\KanColleViewer\KanColleViewer\bin\Debug\Furniture\floor\002.png");
                    a = Image.FromFile(@"C: \Users\foryou\Documents\Visual Studio 2015\Projects\KanColleViewer\KanColleViewer\bin\Debug\Furniture\wall\009.png");

                    Bitmap bmp = new Bitmap(pictureBox3.Image, 1000, 1000);
                    Graphics g2 = Graphics.FromImage(bmp);
                    Graphics g = Graphics.FromImage(bmp);
                    // g2.DrawImage(a, 0, -100, 1000, 700);
                    g.DrawImage(pictureBox1.Image, 0, 0);
                    g2.DrawImage(pictureBox2.Image, 0, 0);
                    //  g.Clear(Color.White);
                    //g.Dispose();
                    pictureBox1.Image = bmp;
                    break;
                case "object":
                    pictureBox6.Image = Image.FromStream(fs);
                    furniture.Type[3] = type;
                    furniture.Name[3] = image_temp[0];
                    furniture.Text[3] = image_temp[1];
                    break;
                case "wall":
                    pictureBox2.Image = Image.FromStream(fs);
                    furniture.Type[4] = type;
                    furniture.Name[4] = image_temp[0];
                    furniture.Text[4] = image_temp[1];
                    a2 = Image.FromFile(@"C: \Users\foryou\Documents\Visual Studio 2015\Projects\KanColleViewer\KanColleViewer\bin\Debug\Furniture\floor\002.png");
                    a = Image.FromFile(@"C: \Users\foryou\Documents\Visual Studio 2015\Projects\KanColleViewer\KanColleViewer\bin\Debug\Furniture\wall\009.png");

                     bmp = new Bitmap(a2, 1000, 1000);
                    // Graphics g2 = Graphics.FromImage(pictureBox2.Image);
                   g = Graphics.FromImage(bmp);
                    // g2.DrawImage(a, 0, -100, 1000, 700);
                    g.DrawImage(pictureBox1.Image, 0, 0);
                    g.DrawImage(pictureBox2.Image, 0, 0);
                    //  g.Clear(Color.White);
                    //g.Dispose();
                    pictureBox1.Image = bmp;
                    break;
                case "window":
                    pictureBox4.Image = Image.FromStream(fs);
                    furniture.Type[5] = type;
                    furniture.Name[5] = image_temp[0];
                    furniture.Text[5] = image_temp[1];
                    break;
                default:
                    pictureBox1.Image = Image.FromStream(fs);
                    character.Type = type;
                    character.Name = image_temp[0];
                    character.Text = image_temp[1];
                    mediaplay = new Thread(new ParameterizedThreadStart(music_play));
                    mediaplay.Start(image_temp[1]);
                    a2 = Image.FromFile(@"C: \Users\foryou\Documents\Visual Studio 2015\Projects\KanColleViewer\KanColleViewer\bin\Debug\Furniture\floor\002.png");
                    a = Image.FromFile(@"C: \Users\foryou\Documents\Visual Studio 2015\Projects\KanColleViewer\KanColleViewer\bin\Debug\Furniture\wall\009.png");

                     bmp = new Bitmap(a2, 1000, 1000);
                    // Graphics g2 = Graphics.FromImage(pictureBox2.Image);
                    g = Graphics.FromImage(bmp);
                    // g2.DrawImage(a, 0, -100, 1000, 700);
                    g.DrawImage(pictureBox2.Image, 0, 0);
                    g.DrawImage(pictureBox1.Image, 0, 0);
                    //  g.Clear(Color.White);
                    //g.Dispose();
                    pictureBox1.Image = bmp;
                    break;
            }

            fs.Close();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        void btn_Click(object sender, EventArgs e)
        {
            //btn屬性 圖片位址 艦娘類型
            int number = (int)((Button)sender).Tag;
            string image_path = btn_select.Path[number];
            string[] image_temp = new string[2];
            image_temp[0] = ((Button)sender).Name.ToString();
            image_temp[1] = ((Button)sender).Text.ToString();
            picture_image(btn_select.Type, image_path, image_temp);
            xml_updata(btn_select.Type, image_path);

         

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

           // MessageBox.Show(((ComboBox)sender).SelectedItem.ToString());
            switch (((ComboBox)sender).SelectedItem.ToString())
            {
                case "chest":
                case "desk":
                case "floor":
                case "object":
                case "wall":
                case "window":
                    string item = select_xml(((ComboBox)sender).SelectedItem.ToString(), @"\Furniture", "type_en");
                    select_picture(@"\Furniture", item);
                    break;
                default:
                    item = select_xml(((ComboBox)sender).SelectedItem.ToString(), @"\Character", "type_en");
                    select_picture(@"\Character", item);
                    break;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            mediaplay = new Thread(new ParameterizedThreadStart(music_play));
            mediaplay.Start(character.Text);

            select_picture(@"\Character", character.Type);
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            a2 = Image.FromFile(@"C: \Users\foryou\Documents\Visual Studio 2015\Projects\KanColleViewer\KanColleViewer\bin\Debug\Furniture\floor\002.png");
          //  pictureBox1.Image = Image.FromFile(@"C:\Users\foryou\Documents\Visual Studio 2015\Projects\KanColleViewer\KanColleViewer\bin\Debug\Character\DD\akatsuki1.png");
            Bitmap bmp = new Bitmap(a2, 1000, 1000);
            Graphics g2 = Graphics.FromImage(bmp);
            Graphics g = Graphics.FromImage(bmp);
            g2.DrawImage(a, 0, -100, 1000, 700);
            g.DrawImage(pictureBox1.Image, 0, 0);
            
            pictureBox1.Image = bmp;
          //  pictureBox1.Image = Image.FromFile(@"C:\Users\foryou\Documents\Visual Studio 2015\Projects\KanColleViewer\KanColleViewer\bin\Debug\Character\DD\akatsuki1.png");
           // g.Dispose();
            select_picture(@"\Furniture", "wall");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            select_picture(@"\Furniture", "window");
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            select_picture(@"\Furniture", "desk");
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            select_picture(@"\Furniture", "object");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            select_picture(@"\Furniture", "chest");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            select_picture(@"\Furniture", "floor");
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            mediaplay = new Thread(new ParameterizedThreadStart(music_play));
            mediaplay.Start(character.Text);

        }

        private void music_play(Object aa)
        {
            string folderName = System.Windows.Forms.Application.StartupPath + @"\Music";
            string[] folderNames = Directory.GetDirectories(folderName);
            string folder = null;
            /*string up_text = ((string)aa).Substring(0, 1);
            string text = ((string)aa).ToLower();
            /*string name =text.Substring(0,1).Replace(up_text.Substring(0, 1), up_text.ToUpper())+text.Substring(1,text.Length-1);
            MessageBox.Show(name.Substring(0, name.Length - 1)+text);
            foreach (string folder_temp in folderNames)
            {
                if (folder_temp.Contains(name)|| folder_temp.Contains(name.Substring(0, name.Length-1)) || folder_temp.Contains(name.Substring(0, name.Length - 2))|| folder_temp.Contains(name.Substring(0, name.Length - 3)))
                {
                    folder = folder_temp;
                    break;
                }
            }*/

            foreach (string folder_temp in folderNames)
            {
                //MessageBox.Show(((string)aa)+","+ folder_temp+ folder_temp.Split('\\').Length);
                if (((string)aa).Contains(folder_temp.Split('\\')[folder_temp.Split('\\').Length - 1]))
                {
                    folder = folder_temp;
                    break;
                }
            }

            string folderName2 = folder + @"\母港_詳細閱覽";
            if (System.IO.Directory.Exists(folderName2))
            {
                string[] folderFiles = Directory.GetFiles(folderName2);
                string[] temp = new string[folderFiles.Length];
                int i = 0;
                foreach (string folder_temp in folderFiles)
                {
                    temp[i] = folder_temp;
                    i++;
                }
                Random r = new Random();
                string folderName3 = temp[r.Next(0, temp.Length - 1)];

                //MessageBox.Show(folderName3);
                axWindowsMediaPlayer1.URL = folderName3;
            }

        }
    }
    public class Character
    {
        public Character() { }

        // The following constructor has parameters for two of the three 
        // properties. 
        public Character(string type, string name)
        { 
          
            Type = type;
            Name = name;
        }

        // Properties.
        public string Type { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return Type + "  ";
        }

       
    }
    public class _Chest : Character { }
    public class _Desk : Character { }
    public class _Floor : Character { }
    public class _Object : Character { }
    public class _Wall : Character { }
    public class _Window : Character { }
    /* namespace ss{
          class aq
         {
             public string aa{ get; set; }
             class az
             {

             }
         }
     }*/
   public class Furniture {
         /*  string Text1;
             public Furniture chest { get { return new Furniture("chest", Type); } }
             public Furniture desk { get { return new Furniture("desk", Type); } }
             public Furniture floor { get { return new Furniture("floor", Type); } }
             public Furniture object1 { get { return new Furniture("object1", Type); } }
             public Furniture wall { get { return new Furniture("wall", Type); } }
             public Furniture windows { get { return new Furniture("windows", Type); } set{ Type = value; } }

          public string Text(string type , string name)
          {
              switch (type)
              {
                  case "chest":
                      chest=name;
                      break;
                  case "desk":
                      chest = name;
                      break;
                  case "floor":

                      break;
                  case "object":

                      break;
                  case "wall":

                      break;
                  case "window":

                      break;
              }
              return "";
          }


          public string Type(string x) {
               switch (type)
               {
                   case "chest":
                       chest = value;

                       break;
                   case "desk":

                       break;
                   case "floor":

                       break;
                   case "object":

                       break;
                   case "wall":

                       break;
                   case "window":

                       break;
               }

           }*/
         public Furniture()
         {

         }
         public string []Name { get; set; }

         public string []Type { get; set; }

         public string []Text { get; set; }
         
        }

    public class Btn_Select
    {
        public Btn_Select() { }
        public string[] Path { get; set; }
        public string Type { get; set; }
    }
}


