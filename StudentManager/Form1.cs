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
using System.Runtime.Serialization.Formatters.Binary;
/*
 todo
 删除后刷新
 添加后双击
 */
namespace StudentManager
{
    public partial class Form1 : Form
    {
        public List<Student> list = new List<Student>();
        public List<Sclass> class_list = new List<Sclass>();
        public List<Student> delete_list = new List<Student>();
        public bool changeOrDeleteFlag = false;
        //Sclass ssclass = new Sclass("机电学院","计算机科学与技术141");
        //Sclass ssclass = new Sclass("机电学院", "计算机科学与技术142");
        //Sclass ssclass = new Sclass("数理学院", "信息与科学技术141");
       // List<Student> delete_list = ReserializeMethod();
        public Form1()
        {
            InitializeComponent();
            panel1.Visible = false;
    
            List<Student> last_list = ReserializeMethod();//调用反序列化的方法  其方法返回值是一个List集合
            foreach (Student item in last_list)//遍历集合中的元素
            {
                AddToRoute(item);
            }
         
        }
        public static void SerializeMethod(List<Student> list)
        {
            using (FileStream fs = new FileStream(@"c:\temp\student.dat", FileMode.Create))  //@"d:\temp\student.dat"
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, list);
            }
        }


        public static List<Student> ReserializeMethod() 
        {
            using (FileStream fs = new FileStream(@"c:\temp\student.dat", FileMode.OpenOrCreate))//@"c:\temp\student.dat"
            {
                BinaryFormatter bf = new BinaryFormatter();
                List<Student> list = (List<Student>)bf.Deserialize(fs);
                return list;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        public void onFormCloseing() 
        { }
        private void button1_Click(object sender, EventArgs e)
        {
            
            //change

            string name, sid, subs, sclass_name,collageName;
            bool gender;
            name = this.textBox1.Text;
            sid = this.textBox3.Text;
            subs = this.textBox5.Text;
            sclass_name = this.textBox2.Text;
            collageName = this.comboBox1.Text;
            if (radioButton1.Checked == true)
            {
                gender = true;
            }
            else gender = false;


            Student student = new Student(name, sid, sclass_name, gender, subs,collageName);


    
            delete_list = ReserializeMethod();

           
                foreach (Student item in delete_list)//遍历集合中的元素
                {
                    if (student.sid == item.sid)
                    {
                       
                        delete_list.Remove(item);
                        changeOrDeleteFlag = true;
                        list.Add(student);
                        AddToRoute(student);
                        this.treeView1.SelectedNode.Remove(); //delete node
                        SaveChange(delete_list, list);
                        list.Clear();
                        MessageBox.Show("修改成功！");
                        break;
                    }
                   
                
                 if(changeOrDeleteFlag == false) MessageBox.Show("学号无法修改！");
            }
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //add
            string name, sid, subs, sclass_name;
            bool gender;
            name = this.textBox1.Text;
            sid = this.textBox3.Text;
            subs = this.textBox5.Text;
            sclass_name = this.textBox2.Text;
            string collageName;
            collageName = this.comboBox1.Text;
            if (radioButton1.Checked == true)
            {
                gender = true;
            }
                
            else gender = false;
            if (name == "" || sid == "")
            {
                MessageBox.Show("姓名，学号不能为空！");
            
            }
            else
            {
                Student student = new Student(name, sid, sclass_name, gender, subs, collageName);
                list.Add(student);
                AddToRoute(student);

                List<Student> last_list = new List<Student>();
                last_list = ReserializeMethod();
                foreach (Student item in last_list)//遍历集合中的元素
                {
                    list.Add(item);
                }
                
                SerializeMethod(list);
                list.Clear();
            }
          



          //  if (sclass_name == "计算机科学与技术141")
          //treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(MyNodes);  //到计算机141层
          //  else  treeView1.Nodes[0].Nodes[0].Nodes[1].Nodes.Add(MyNodes);   //计算机142层

        }

       
        public bool SaveDeleteChange(List<Student> listssss)
        {
            SerializeMethod(listssss);

            return true;
        }
        public bool SaveChange(List<Student> lista, List<Student> listb)
        {
            foreach (Student item in lista)//遍历集合中的元素
            {
                listb.Add(item);
            }
            SerializeMethod(listb);
            return true;
        }
        public void AddToRoute(Student student)
        {
            TreeNode MyNodes = new TreeNode();
            MyNodes.Text = student.name;

            switch (student.sclass)
            {
                case "计算机科学与技术141":
                    treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(MyNodes);
                    break;
                case "计算机科学与技术142":
                    treeView1.Nodes[0].Nodes[0].Nodes[1].Nodes.Add(MyNodes);
                    break;
                case "信息与计算科学141":
                    treeView1.Nodes[0].Nodes[1].Nodes[0].Nodes.Add(MyNodes);
                    break;
                case "机械":
                    treeView1.Nodes[0].Nodes[0].Nodes[3].Nodes.Add(MyNodes);
                    break;
                case "信息管理141":
                    treeView1.Nodes[0].Nodes[0].Nodes[2].Nodes.Add(MyNodes);
                    break;
                //default:
                //    MessageBox.Show("Error");
                //    break;
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeView treeView = (TreeView)sender;
            
            TreeNode selectedNode=treeView.SelectedNode;
            if (selectedNode.Level == 3) 
            {
                List<Student> student_list = ReserializeMethod();
                foreach (Student student in student_list)
                {
                    if (student.name == selectedNode.Text)
                    {
                        this.textBox1.Text = student.name;
                        this.textBox2.Text = student.sclass;
                        this.textBox3.Text = student.sid;
                        this.textBox5.Text = student.subs;
                        if (student.collage != null) this.comboBox1.Text = student.collage;
                        if (student.gender == true) this.radioButton1.Checked = true;
                        else this.radioButton2.Checked = true;
                    }
                }
            }
            
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //delete

     
            
            string sid = this.textBox3.Text;
            if (sid == "") MessageBox.Show("双击选中删除的项目！");
            else
            {
                delete_list = ReserializeMethod();
                foreach (Student item in delete_list)
                {
                    if (sid == item.sid)
                    {
                        delete_list.Remove(item);
                        changeOrDeleteFlag = true;
                        this.treeView1.SelectedNode.Remove();
                        MessageBox.Show("删除成功！");
                        SaveDeleteChange(delete_list);
                        this.textBox1.Text = "";
                        this.textBox2.Text = "";
                        this.textBox3.Text = "";
                        this.textBox5.Text = "";
                        this.comboBox1.Text = "";
                        break;
                    }
                }
                if (changeOrDeleteFlag == false) MessageBox.Show("无法删除！");
            }
          
          
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeView treeView = (TreeView)sender;
            if (e.Node.Level == 0)
            {
                panel1.Visible = false;
          
               
            }
            else { panel1.Visible = true;  }
            
            if (e.Node.Level== 2)                 //还是得看MSDN靠谱
            {
                
                this.textBox2.Text = e.Node.Text;

                this.textBox1.Text = "";
                this.textBox3.Text = "";
                this.textBox5.Text = "";
                this.radioButton1.Checked = false;
                this.radioButton2.Checked = false;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
