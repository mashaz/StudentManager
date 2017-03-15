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
        Sclass ssclass = new Sclass("计算机科学与技术143", "机电学院");
        
        //Sclass ssclass = new Sclass("机电学院","计算机科学与技术141");
        //Sclass ssclass = new Sclass("机电学院", "计算机科学与技术142");
       // Sclass ssclass = new Sclass("法学院");
        public Form1()
        {
            InitializeComponent();
            panel1.Visible = false;
            
            class_list = ReserializeClassMethod();
            foreach (Sclass item in class_list)
            {
                if (item.className == null) this.treeView1.Nodes[0].Nodes.Add(item.collageName);//only collage
              
            }
        
            foreach (Sclass item in class_list) //item=[collagename,classname]
            {
                if (item.className != null)
                {
                    

                    foreach (TreeNode tn in this.treeView1.Nodes[0].Nodes)
                    {
                       
                        if (tn.Text == item.collageName)
                        {
                            TreeNode newnodes = new TreeNode();
                            newnodes.Text = item.className;
                            tn.Nodes.Add(newnodes);
                            break;
                        }
                    }
                }

            }

            List<Student> last_list = ReserializeMethod();//调用反序列化的方法  其方法返回值是一个List集合
            //MessageBox.Show(last_list.Count().ToString());
            foreach (Student item in last_list)//遍历集合中的元素
            {
                AddToRoute(item);
            }
            
         
           
         
        }
        public static void SerializeMethod(List<Student> list)  //序列化
        {
            using (FileStream fs = new FileStream(@"c:\temp\student.dat", FileMode.Create))  //@"d:\temp\student.dat"
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, list);
            }
        }


        public static List<Student> ReserializeMethod()  //反序列化
        {
            using (FileStream fs = new FileStream(@"c:\temp\student.dat", FileMode.OpenOrCreate))//@"c:\temp\student.dat"
            {
                BinaryFormatter bf = new BinaryFormatter();
                List<Student> list = (List<Student>)bf.Deserialize(fs);
                return list;
            }
        }

        public static void SerializeClassMethod(List<Sclass> list)
        {
            using (FileStream fs = new FileStream(@"c:\temp\classes.dat", FileMode.Create))  //@"d:\temp\student.dat"
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, list);
            }
        }


        public static List<Sclass> ReserializeClassMethod()
        {
            using (FileStream fs = new FileStream(@"c:\temp\classes.dat", FileMode.OpenOrCreate))//@"c:\temp\student.dat"
            {
                BinaryFormatter bf = new BinaryFormatter();
                List<Sclass> list = (List<Sclass>)bf.Deserialize(fs);
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
            //add student
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
            if (name == "" || sid == "" ||collageName =="")
            {
                MessageBox.Show("姓名，学号,系别不能为空！");
            
            }
            else
            {

                Student student = new Student(name, sid, sclass_name, gender, subs, collageName);
           

                List<Student> last_list = new List<Student>();
                last_list = ReserializeMethod();
                int flag = 1;

                foreach (Student item in last_list)//遍历集合中的元素
                {
                  
                    if (student.sid == item.sid)
                    {
                        MessageBox.Show("已存在此学号！");
                        flag = 0;
                        break;
                    }

                }
                if (flag == 1)
                {
                    AddToRoute(student);
                    last_list.Add(student);
                    SerializeMethod(last_list);
                    MessageBox.Show("添加成功！");
                }
            }
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

           
                foreach(TreeNode  tn in this.treeView1.Nodes[0].Nodes)
                {
                    if (tn.Text == student.collage)
                    {
                        foreach (TreeNode tnx in tn.Nodes)
                        {
                            if (tnx.Text == student.sclass)
                            {
                                tnx.Nodes.Add(MyNodes);
                            }
                        }
                    }
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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            InputBox input = new InputBox();
            DialogResult dr = input.ShowDialog();
            if (dr == DialogResult.OK)
            {
               // MessageBox.Show(input.GetMsg());
                AddClass(input.GetMsg());
            }
        }
        private void AddClass(string name)
        {
            //add class
            //todo 序列化和新建后立刻添加学生的处理
            TreeNode td = new TreeNode();
            td.Text = name;
            this.treeView1.SelectedNode.Nodes.Add(td);
            
            Sclass s = new Sclass(name,this.treeView1.SelectedNode.Text); //todos
            class_list = ReserializeClassMethod();
            class_list.Add(s);
            SerializeClassMethod(class_list);
            class_list.Clear();
                
            
        }
        private void AddCollage(string name)
        {
            //add class
            //todo 序列化和新建后立刻添加学生的处理
            TreeNode td = new TreeNode();
            td.Text = name;
            this.treeView1.SelectedNode.Nodes.Add(td);
            Sclass s = new Sclass(name);
            class_list = ReserializeClassMethod();
            class_list.Add(s);
            SerializeClassMethod(class_list);
            class_list.Clear();


        }
        private void treeView1_MouseDown(object sender, MouseEventArgs e) //treeview右击菜单选择事件
        {
            if (e.Button == MouseButtons.Right)
            {
                Point ClickPoint = new Point(e.X, e.Y);
                TreeNode CurrentNode = treeView1.GetNodeAt(ClickPoint);
                if (CurrentNode != null)//判断你点的是不是一个节点
                {
                    switch (CurrentNode.Level)//根据不同节点显示不同的右键菜单，当然你可以让它显示一样的菜单
                    {
                        case 1:
                            CurrentNode.ContextMenuStrip = contextMenuStrip1;
                            break;
                        case 0:
                             CurrentNode.ContextMenuStrip = contextMenuStrip2;
                            break;
                        case 2:
                            CurrentNode.ContextMenuStrip = contextMenuStrip3;
                            break;
                    }
                    treeView1.SelectedNode = CurrentNode;//选中这个节点
                }
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //delete collage
            class_list = ReserializeClassMethod();
            foreach (Sclass sclass in class_list) 
            {
                if (sclass.collageName == this.treeView1.SelectedNode.Text && sclass.className == null)
                {
                    class_list.Remove(sclass);
                    break;
                }
            }
            SerializeClassMethod(class_list);
       
             
            this.treeView1.SelectedNode.Remove();

        }

        private void addCollageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputBox input = new InputBox();
            DialogResult dr = input.ShowDialog();
            if (dr == DialogResult.OK)
            {
                // MessageBox.Show(input.GetMsg());
                AddCollage(input.GetMsg());
            }
        }

        private void DeleteClassContextMenuStrip(object sender, EventArgs e)
        {
            //delete class
            if (this.treeView1.SelectedNode.Nodes.Count != 0)
            {
                DialogResult dialogResult = MessageBox.Show("该节点非空，确认删除？", "Warnning", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    class_list = ReserializeClassMethod();
                    foreach (Sclass sclass in class_list)
                    {
                        if (sclass.className == this.treeView1.SelectedNode.Text && sclass.collageName != null)
                        {
                            class_list.Remove(sclass);
                            break;
                        }
                    }
                    SerializeClassMethod(class_list);


                    this.treeView1.SelectedNode.Remove();
                }

            }
            else
            {
                class_list = ReserializeClassMethod();
                foreach (Sclass sclass in class_list)
                {
                    if (sclass.className == this.treeView1.SelectedNode.Text && sclass.collageName != null)
                    {
                        class_list.Remove(sclass);
                        break;
                    }
                }
                SerializeClassMethod(class_list);


                this.treeView1.SelectedNode.Remove();
            }
          
        }

        //private void contextMenuStrip2_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Right)
        //    {
        //        Point ClickPoint = new Point(e.X, e.Y);
        //        TreeNode CurrentNode = treeView1.GetNodeAt(ClickPoint);
        //        if (CurrentNode != null)//判断你点的是不是一个节点
        //        {
        //            switch (CurrentNode.Level)//根据不同节点显示不同的右键菜单，当然你可以让它显示一样的菜单
        //            {
        //                case 1:
        //                    CurrentNode.ContextMenuStrip = contextMenuStrip1;
        //                    break;
        //                case 0:
        //                    CurrentNode.ContextMenuStrip = contextMenuStrip2;
        //                    break;
        //            }
        //            treeView1.SelectedNode = CurrentNode;//选中这个节点
        //        }
        //    }
        //}

      

   
     
    }
}
