// See https://aka.ms/new-console-template for more information
using System;
using System.IO;

namespace src
{
    class TreeStructure
    {
        static void Main(string[] args)
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
            (List<string> path, List<string> haveVisited) = myBFSMethod("siangg.txt", root);

            // find folder DFS
            List<string> visitedDirectory = new List<string>();
            List<string> pathIn = new List<string>();
            (List<string> path2, List<string> visitedFolder) = DFSSearch("itb.txt", root, pathIn, visitedDirectory);

            //print hasil path ketemunya
            Console.WriteLine("file ketemu di");
            foreach (string dir in path)
            {
                Console.WriteLine(dir);
            }

            //print yang udah dikunjungin
            Console.WriteLine("yang udah dikunjungin");
            foreach (string dir in haveVisited)
            {
                Console.WriteLine(dir);
            }

            Console.WriteLine("ketemu pake DFS:");
            foreach (string dir in path2) {
                Console.WriteLine(dir);
            }

            Console.WriteLine("yang DFS kunjungi:");
            foreach (string dir in visitedFolder)
            {
                Console.WriteLine(dir);
            }
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
        static (List<string> path, List<string> haveVisited) myBFSMethod(string filename, TreeNode root)
        {
            Queue<TreeNode> strQ = new Queue<TreeNode> ();
            List<string> haveVisited = new List<string> ();
            List<string> path = new List<string> ();

            strQ.Enqueue(root);

            // root.children.Count == 0

            while(strQ.Count > 0)
            {
                /*
                Console.WriteLine(strQ.Peek().getFolderName());
                Console.WriteLine(strQ.Count());
                */
                string isiQueue = strQ.Peek().getFolderName();
                string result;
                result = Path.GetFileName(isiQueue);
                if(strQ.Peek().children.Count == 0)
                {
                    if (result == filename)
                    {
                        path.Add(isiQueue);
                        strQ.Clear();
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
                        strQ.Enqueue (child);
                    }
                    haveVisited.Add(strQ.Peek().getFolderName());
                    strQ.Dequeue();
                }
            }

            if(path.Count == 0)
            {
                path.Add("not found");
            }


           //onsole.WriteLine(strQ.Peek());

            return (path, haveVisited);
        }

        static (List<string> path, List<string> visitedDirectory) DFSSearch(string folderName, TreeNode root, List<string> path, List<string> visitedDirectory) {
            // Base of recursion
            string rootFullDirectory = root.getFolderName();
            if (Path.GetFileName(rootFullDirectory) == folderName) {
                path.Add(rootFullDirectory);
                return (path, visitedDirectory);
            }

            foreach (var child in root.getChildren()) {
                // If the directory has been checked thus added to visitedDirectory, then skip it
                if (!visitedDirectory.Contains(child.getFolderName())) {
                    // Add to the list of visitedDirectory and recurse the function
                    visitedDirectory.Add(child.getFolderName());
                    DFSSearch(folderName, child, path, visitedDirectory);
                }
            }
            return (path, visitedDirectory);
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