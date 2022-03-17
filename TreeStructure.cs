// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.IO;

namespace Tubes_2_Stima
{
    public class TreeStructure
    {
        /*static void Main(string[] args)
        {
            // Ini testing bat tree struct si folder, jadi ganti ke directory computer masing2 :D
            // saran, pilih folder yang ga banyak anaknya gt deh
            // tadi pake folder stima yg ada si overdrive, agak shock panjang bgt treenya AHAHAHHA
            string directory = @"C:\Kuliah\(4)Semester4\Stima\A";
            TreeNode root = new TreeNode(directory);

            // bikin tree file
            root = crateTreeOfFiles(directory, root);
            root.displayTree(0);

            // find folder BFS style
            (List<string> path, List<string> haveVisited) = myBFSMethod("siangg.txt", root, true);

            // find folder DFS
            List<string> visitedDirectory = new List<string>();
            List<string> pathIn = new List<string>();
            (List<string> path2, List<string> visitedFolder) = DFSSearch("itb.txt", root, pathIn, visitedDirectory);

            //print hasil BFS path ketemunya
            Console.WriteLine("file ketemu pake BFS di:");
            foreach (string dir in path)
            {
                Console.WriteLine(dir);
            }

            //print yang udah dikunjungin BFS
            Console.WriteLine("yang udah dikunjungin BFS:");
            foreach (string dir in haveVisited)
            {
                Console.WriteLine(dir);
            }

            Console.WriteLine("ketemu pake DFS:");
            foreach (string dir in path2)
            {
                Console.WriteLine(dir);
            }

            Console.WriteLine("yang DFS kunjungi:");
            foreach (string dir in visitedFolder)
            {
                Console.WriteLine(dir);
            }
        }*/

        public static TreeNode crateTreeOfFiles(string directory, TreeNode root)
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

        public static (List<string> path, List<string> haveVisited) myBFSMethod(string filename, TreeNode root, Boolean findAll)
        {
            Queue<TreeNode> strQ = new Queue<TreeNode>();
            List<string> haveVisited = new List<string>();
            List<string> path = new List<string>();

            strQ.Enqueue(root);
            while (strQ.Count > 0)
            {
                string isiQueue = strQ.Peek().getFolderName();
                string result;
                result = Path.GetFileName(isiQueue);

                if (strQ.Peek().children.Count == 0)
                {
                    if (result == filename)
                    {
                        path.Add(isiQueue);
                        if (findAll == false)
                        {   // find 1
                            strQ.Clear();
                        }
                        else
                        {                 // find all
                            strQ.Dequeue();
                        }
                    }
                    else
                    {
                        haveVisited.Add(strQ.Peek().getFolderName());
                        strQ.Dequeue();
                    }
                }
                else
                {
                    foreach (var child in strQ.Peek().children)
                    {
                        strQ.Enqueue(child);
                    }
                    haveVisited.Add(strQ.Peek().getFolderName());
                    strQ.Dequeue();
                }
            }

            if (path.Count == 0)
            {
                path.Add("not found");
            }

            return (path, haveVisited);
        }

        static (List<string> path, List<string> visitedDirectory) DFSSearch(string folderName, TreeNode root, List<string> path, List<string> visitedDirectory)
        {
            // Base of recursion
            string rootFullDirectory = root.getFolderName();
            if (Path.GetFileName(rootFullDirectory) == folderName)
            {
                path.Add(rootFullDirectory);
                return (path, visitedDirectory);
            }

            foreach (var child in root.children)
            {
                // If the directory has been checked thus added to visitedDirectory, then skip it
                if (!visitedDirectory.Contains(child.getFolderName()))
                {
                    // Add to the list of visitedDirectory and recurse the function
                    visitedDirectory.Add(child.getFolderName());
                    DFSSearch(folderName, child, path, visitedDirectory);
                }
            }
            return (path, visitedDirectory);
        }
    }

    public class TreeNode
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

        public string getFolderName()
        {
            return this.folderName;
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