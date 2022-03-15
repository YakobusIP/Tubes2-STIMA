// See https://aka.ms/new-console-template for more information
using System;

namespace src
{
    class TreeStructure
    {
        static void Main(string[] args)
        {
            // Ini testing bat tree struct si folder, jadi ganti ke directory computer masing2 :D
            // saran, pilih folder yang ga banyak anaknya gt deh
            // tadi pake folder stima yg ada si overdrive, agak shock panjang bgt treenya AHAHAHHA
            string directory = @"C:\Kuliah\(4)Semester4\OOP";
            TreeNode root = new TreeNode(directory);

            root = crateTreeOfFiles(directory, root);
            root.displayTree(0);
        }

        static TreeNode crateTreeOfFiles(string directory, TreeNode root)
        {
            string[] files = Directory.GetFiles(directory);
            string[] directories = Directory.GetDirectories(directory);


            foreach (string file in files)
            {
                root.AddChild(file);
            }


            foreach (string subDirectory in directories)
            {
                // Call the same method on each directory.
                TreeNode rootChild2 = new TreeNode(subDirectory);
                root.AddChildTree(crateTreeOfFiles(subDirectory, rootChild2));
            }

            return root;
        }
    }

    class TreeNode
    {
        public string folderName;
        public List<TreeNode> children;

        public TreeNode(string folderName)
        {
            this.folderName = folderName;
            this.children = new List<TreeNode>();
        }

        public void AddChild(string folderName)
        {
            this.children.Add(new TreeNode(folderName));
        }

        public void AddChildTree(TreeNode folderName)
        {
            this.children.Add(folderName);
        }

        public TreeNode GetChild(int index)
        {
            return this.children[index];
        }

        public int childCount()
        {
            return this.children.Count;
        }

        public void displayTree(int level)
        {
            for (int i = 0; i < level; i++)
            {
                Console.Write("\t");
            }
            Console.WriteLine(this.folderName);
            foreach (var child in this.children)
            {
                child.displayTree(level + 1);
            }
        }
    }
}