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
            using (FileStream fs = new FileStream(@"c:\temp\student.dat", FileMode.OpenOrCreate))
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

            string name, sid, subs, sclass_name;
            bool gender;
            name = this.textBox1.Text;
            sid = this.textBox3.Text;
            subs = this.textBox5.Text;
            sclass_name = this.textBox2.Text;
            if (radioButton1.Checked == true)
            {
                gender = true;
            }
            else gender = false;


            Student student = new Student(name, sid, sclass_name, gender, subs);


            //List<Student> last_list = new List<Student>();
            //last_list = ReserializeMethod();
            delete_list = ReserializeMethod();

            if (changeOrDeleteFlag == false)
            {
                foreach (Student item in delete_list)//遍历集合中的元素
                {
                    if (student.sid == item.sid)
                    {
                        //delete node
                        delete_list.Remove(item);
                        changeOrDeleteFlag = true;
                        list.Add(student);
                        AddToRoute(student);
                        MessageBox.Show("修改成功！");
                        break;
                    }
                   
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
            if (radioButton1.Checked == true)
            {
                gender = true;
            }
            else gender = false;
            Student student = new Student(name, sid, sclass_name,gender,subs);
            list.Add(student);
            AddToRoute(student);
  
            


          //  if (sclass_name == "计算机科学与技术141")
          //treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(MyNodes);  //到计算机141层
          //  else  treeView1.Nodes[0].Nodes[0].Nodes[1].Nodes.Add(MyNodes);   //计算机142层

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changeOrDeleteFlag == false)
            {
                List<Student> last_list = new List<Student>();
                last_list = ReserializeMethod();
                foreach (Student item in last_list)//遍历集合中的元素
                {
                    list.Add(item);
                }
                SerializeMethod(list);
            }
            else
            {
                foreach (Student item in delete_list)//遍历集合中的元素
                {
                    list.Add(item);
                }
                SerializeMethod(list);

            }
           
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
                        if (student.gender == true) this.radioButton1.Checked = true;
                        else this.radioButton2.Checked = true;
                    }
                }
            }
            
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //delete
           // TreeView treeView = (TreeView)sender;
            //TreeNode selectedNode = treeView.SelectedNode;
            //treeView.SelectedNode.Remove();
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
                        MessageBox.Show("删除成功！");
                        break;
                    }
                }
                if (changeOrDeleteFlag == false) MessageBox.Show("无法删除！");
            }
          
            //this.Hide();   //先隐藏主窗体
            //Form1 form1 = new Form1(); //重新实例化此窗体
            //form1.ShowDialog();//已模式窗体的方法重新打开
            //this.Close();//原窗体关闭
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
