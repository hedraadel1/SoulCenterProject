//-----------------------------------------------------------------------
// <copyright file="DirectoryTree.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.IO;
using System.Windows.Forms;

namespace SoulCenterProject.Helpers.Utils
{
    /// <summary>
    /// Class for traversing directories and populating a TreeView control with folders and files.
    /// </summary>
    public class DirectoryTree
    {
        /// <summary>
        /// Helper method for recursively traversing directories and adding nodes to the TreeView.
        /// </summary>
        /// <param name="node">The current TreeNode being populated.</param>
        /// <param name="path">The current directory path.</param>
        /// <param name="includeFileExtensions">Optional parameter (default: true) indicating whether to include file extensions in node names.</param>
        /// <param name="fileFilter">Optional parameter for filtering files based on extension (e.g., "*.txt").</param>
        private void PopulateTreeNode(
            TreeNode node,
            string path,
            bool includeFileExtensions = true,
            string fileFilter = null)
        {
            try
            {
                // Get directories and files (optionally filtered)
                var directories = Directory.EnumerateDirectories(path);
                var files = Directory.EnumerateFiles(path, fileFilter ?? "*.*"); // Default to all files

                // Add child nodes for directories
                foreach(var directory in directories)
                {
                    var childNode = new TreeNode(Path.GetFileName(directory));
                    node.Nodes.Add(childNode);
                    PopulateTreeNode(childNode, directory, includeFileExtensions, fileFilter);
                }

                // Add file nodes (with or without extensions)
                foreach(var file in files)
                {
                    string fileName;
                    if(includeFileExtensions)
                    {
                        fileName = Path.GetFileName(file); // Get the entire filename (including extension)
                    } else
                    {
                        fileName = Path.GetFileNameWithoutExtension(file); // Without extension
                    }
                    node.Nodes.Add(new TreeNode(fileName));
                }
            } catch(Exception ex)
            {
                // Handle exceptions (e.g., invalid path, access denied)
                // Consider providing more specific error messages here
                MessageBox.Show($"Error accessing directory: {ex.Message}");
            }
        }

        /// <summary>
        /// Populates a new TreeNode with the contents of the specified folder path.
        /// </summary>
        /// <param name="folderPath">The path to the directory to be traversed.</param>
        /// <param name="includeFileExtensions">Optional parameter (default: true) indicating whether to include file extensions in node names.</param>
        /// <param name="fileFilter">Optional parameter for filtering files based on extension (e.g., "*.txt").</param>
        /// <returns>A TreeNode representing the root of the directory structure.</returns>
        public TreeNode PopulateTree(string folderPath, bool includeFileExtensions = true, string fileFilter = null)
        {
            var rootNode = new TreeNode(folderPath);
            PopulateTreeNode(rootNode, folderPath, includeFileExtensions, fileFilter);
            return rootNode;
        }
    }
}
