//-----------------------------------------------------------------------
// <copyright file="ProjectTreeview.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions; // Added for parsing
using System.Windows.Forms;
using SoulCenterProject.Helpers.Utils;

namespace SoulCenterProject.SoulControls
{
    public partial class ProjectTreeview : UserControl
    {
        private string _fileFilter;
        private string _folderPath;
        private ImageList _imageList; // Added for icons
        private bool _includeFileExtensions = true;

        public ProjectTreeview()
        {
            InitializeComponent();
            _imageList = new ImageList();
            _imageList.Images.Add(Properties.Resources.FolderIcon); // Replace with folder icon
            _imageList.Images.Add(Properties.Resources.cClassIcon); // Replace with class icon
            _imageList.Images.Add(Properties.Resources.model); // Replace with model icon
            _imageList.Images.Add(Properties.Resources.ViewIcon); // Replace with view icon
            treeView1.ImageList = _imageList;
        }

        private Type GetClassTypeWithAttribute(string filePath)
        {
            try
            {
                // Read the source code content
                string sourceCode = File.ReadAllText(filePath);

                // Look for ClassInfoAttribute using regular expressions (example)
                var match = Regex.Match(sourceCode, @"\[ClassInfo\s*:\s*(.*?)\]");
                if(match.Success)
                {
                    // Extract the type information from the attribute
                    string typeName = match.Groups[1].Value.Trim();

                    // You can load the assembly if needed for further processing (optional)
                    // Assembly assembly = Assembly.LoadFile(Path.Combine(Path.GetDirectoryName(filePath), Path.ChangeExtension(filePath, ".dll")));
                    // Type type = assembly.GetType(typeName);

                    return Type.GetType(typeName); // Return the extracted type information (optional loading)
                }
            } catch(Exception)
            {
                // Handle potential errors while reading the file
            }

            return null;
        }

        private void PopulateTreeNode(TreeNode node, string path)
        {
            var directories = Directory.EnumerateDirectories(path);
            var files = Directory.EnumerateFiles(path, _fileFilter ?? "*.cs"); // Use filter if provided

            foreach(var directory in directories)
            {
                var childNode = new TreeNode(Path.GetFileName(directory));
                childNode.ImageIndex = 0; // Set folder icon
                childNode.SelectedImageIndex = 0; // Maintain icon on selection
                node.Nodes.Add(childNode);
                PopulateTreeNode(childNode, directory);
            }

            foreach(var file in files)
            {
                string fileName = Path.GetFileName(file);
                int imageIndex = 1; // Default to class icon

                // Check for corresponding DLL or EXE file
                if(File.Exists(Path.Combine(path, fileName + ".dll")) ||
                    File.Exists(Path.Combine(path, fileName + ".exe")))
                {
                    // Try to get class type and attribute
                    var type = GetClassTypeWithAttribute(file);
                    if(type != null)
                    {
                        var attribute = type.GetCustomAttributes<ClassInfoAttribute>(false).FirstOrDefault();
                        if(attribute != null)
                        {
                            imageIndex = SetIconBasedOnType(attribute.Type); // Set icon based on type
                        }
                    }
                }
                string finalFileName;
                if(_includeFileExtensions)
                {
                    finalFileName = fileName + Path.GetExtension(file);
                } else
                {
                    finalFileName = fileName;
                }
                node.Nodes.Add(new TreeNode(finalFileName, imageIndex, imageIndex));

                //node.Nodes.Add(new TreeNode(fileName + (_includeFileExtensions ? Path.GetExtension(file) : ""), imageIndex, imageIndex));
            }

            // Expand nodes automatically
            node.ExpandAll();
        }

        private void PopulateTreeView()
        {
            try
            {
                if(!string.IsNullOrEmpty(_folderPath) && Directory.Exists(_folderPath))
                {
                    treeView1.Nodes.Clear();
                    var rootNode = new TreeNode(_folderPath);
                    PopulateTreeNode(rootNode, _folderPath);
                    treeView1.Nodes.Add(rootNode);
                }
            } catch(Exception ex)
            {
                MessageBox.Show($"Error accessing directory: {ex.Message}");
            }
        }


private int SetIconBasedOnType(string type)
{
    switch(type)
    {
        case "Model":
            return 1; // Model icon index
        case "View":
            return 3; // View icon index
        default:
            return 0; // Default class icon index
    }
}

        /// <summary>
        /// Gets or sets a filter for files based on extension (e.g., "*.txt").
        /// </summary>
        [Browsable(true)]
        [Category("Data")]
        [Description("Filter for files based on extension (e.g., '*.txt').")]
        public string FileFilter
        {
            get { return _fileFilter; }
            set
            {
                if(_fileFilter != value)
                {
                    _fileFilter = value;
                    PopulateTreeView();
                }
            }
        }

        /// <summary>
        /// Gets or sets the folder path to be displayed in the TreeView.
        /// </summary>
        [Browsable(true)]
        [Category("Data")]
        [Description("The path to the directory to be traversed.")]
        public string FolderPath
        {
            get { return _folderPath; }
            set
            {
                if(_folderPath != value)
                {
                    _folderPath = value;
                    PopulateTreeView();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to include file extensions in node names.
        /// </summary>
        [Browsable(true)]
        [Category("Data")]
        [Description("Indicates whether to include file extensions in node names.")]
        public bool IncludeFileExtensions
        {
            get { return _includeFileExtensions; }
            set
            {
                if(_includeFileExtensions != value)
                {
                    _includeFileExtensions = value;
                    PopulateTreeView();
                }
            }
        }
    }
}
