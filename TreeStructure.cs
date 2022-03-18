// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tubes_2_Stima
{
    class BFSDFS
    {
        public static (List<string> path, List<string> haveVisited, List<string> wayToPath) BFSSearch(string filename, TreeNode root, Boolean findAll)
        {
            Queue<TreeNode> strQ = new Queue<TreeNode>();
            List<string> haveVisited = new List<string>();
            List<string> path = new List<string>();
            List<string> wayToPath = new List<string>();

            strQ.Enqueue(root);
            while (strQ.Count > 0){
                string isiQueue = strQ.Peek().getFolderName();
                string result;
                result = Path.GetFileName(isiQueue);

                if (strQ.Peek().children.Count == 0){
                    if (result == filename){
                        path.Add(isiQueue);
                        if(findAll == false){   // find 1
                            strQ.Clear();
                        }else {                 // find all
                            strQ.Dequeue();
                        }
                    } else {
                        haveVisited.Add(strQ.Peek().getFolderName());
                        strQ.Dequeue();
                    }
                } else {
                    foreach (var child in strQ.Peek().children){
                        strQ.Enqueue(child);
                    }
                    haveVisited.Add(strQ.Peek().getFolderName());
                    strQ.Dequeue();
                }
            }

            if (path.Count == 0){
                path.Add("not found");
            } else {
                wayToPath = BreakPath(path, root.getFolderName());
            }

            return (path, haveVisited, wayToPath);
        }

        public static (List<string> path, List<string> visitedDirectory) DFSSearch(string folderName, TreeNode root, List<string> path, List<string> visitedDirectory, Boolean findAll) {

            if (findAll == true) {
                // Base of recursion
                string rootFullDirectory = root.getFolderName();
                if (Path.GetFileName(rootFullDirectory) == folderName) {
                    Console.WriteLine("Found folder!");
                    path.Add(rootFullDirectory);
                    return (path, visitedDirectory);
                }
                
                // Add root to visitedDirectory
                if (visitedDirectory.Count() == 0) {
                    visitedDirectory.Add(rootFullDirectory);
                }

                foreach (var child in root.children) {
                    // If the directory has been checked thus added to visitedDirectory, then skip it
                    if (!visitedDirectory.Contains(child.getFolderName())) {
                        // Add to the list of visitedDirectory and recurse the function
                        visitedDirectory.Add(child.getFolderName());
                        DFSSearch(folderName, child, path, visitedDirectory, findAll);
                    }
                }
                return (path, visitedDirectory);
            } else {
                // Base of recursion
                string rootFullDirectory = root.getFolderName();
                if (Path.GetFileName(rootFullDirectory) == folderName) {
                    Console.WriteLine("Found folder!");
                    path.Add(rootFullDirectory);
                    return (path, visitedDirectory);
                }
                
                // Add root to visitedDirectory
                if (visitedDirectory.Count() == 0) {
                    visitedDirectory.Add(rootFullDirectory);
                }

                foreach (var child in root.children) {
                    // If the directory has been checked thus added to visitedDirectory, then skip it
                    // Recurse only if the file hasn't been found yet
                    if ((!visitedDirectory.Contains(child.getFolderName())) && (path.Count() == 0)) {
                        // Add to the list of visitedDirectory and recurse the function
                        visitedDirectory.Add(child.getFolderName());
                        DFSSearch(folderName, child, path, visitedDirectory, findAll);
                    }
                }
                return (path, visitedDirectory);
            }
            
        }

        public static List<string> BreakPath(List<string> path, string directory)
        {
            List<string> pecahan = new List<string>();
            foreach (string dir in path)
            {
                string isiPecahan = dir;
                while (Path.GetDirectoryName(isiPecahan) != null)
                {
                    pecahan.Add(isiPecahan);
                    isiPecahan = Path.GetDirectoryName(isiPecahan);
                    if (isiPecahan == directory)
                    {
                        pecahan.Add(isiPecahan);
                        break;
                    }
                }
            }
            return pecahan;
        }
    }

    public class TreeStructure
    {
        public static TreeNode createTreeOfFiles(string directory, TreeNode root)
        {
            string[] files = Directory.GetFiles(directory);
            string[] directories = Directory.GetDirectories(directory);

            foreach (string file in files){
                root.AddChild(file);
            }

            foreach (string subDirectory in directories){
                // Call the same method on each directory.
                TreeNode rootChild2 = new TreeNode(subDirectory);
                root.AddChildTree(createTreeOfFiles(subDirectory, rootChild2));
            }

            return root;
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