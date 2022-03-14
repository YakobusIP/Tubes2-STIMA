// See https://aka.ms/new-console-template for more information
using System;

namespace TreeStructure
{
    class TreeStructure {
        static void Main(string[] args) {
            // Ini testing aja buat ngetes tree structure-nya
            TreeNode root = new TreeNode("Root Folder");
            root.AddChild("A");
            root.AddChild("B");
            root.AddChild("C");
            TreeNode rootChild1 = root.GetChild(0);
            TreeNode rootChild2 = root.GetChild(1);
            TreeNode rootChild3 = root.GetChild(2);

            rootChild1.AddChild("D");
            rootChild1.AddChild("E");
            rootChild1.AddChild("F");

            rootChild2.AddChild("G");

            rootChild3.AddChild("H");
            rootChild3.AddChild("I");

            TreeNode child1Child = rootChild2.GetChild(0);
            child1Child.AddChild("J");

            root.displayTree(0);
        }
    }

    class TreeNode {
        public string folderName;
        public List<TreeNode> children;

        public TreeNode(string folderName) {
            this.folderName = folderName;
            this.children = new List<TreeNode>();
        }

        public void AddChild(string folderName) {
            this.children.Add(new TreeNode(folderName));
        }

        public TreeNode GetChild(int index) {
            return this.children[index];
        }

        public int childCount() {
            return this.children.Count;
        }

        public void displayTree(int level) {
            for (int i = 0; i < level; i++) {
                Console.Write("\t");
            }
            Console.WriteLine(this.folderName);
            foreach (var child in this.children) {
                child.displayTree(level + 1);
            }
        } 
    }
}
