//-----------------------------------------------------------------------
// <copyright file="AichatViewModel.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.ComponentModel;

namespace SoulCenterProject.Modules.Ai.ViewModel
{
    public class AichatViewModel : INotifyPropertyChanged
    {
        // Add other properties as needed

        public AichatViewModel()
        {
            // Initialize properties
            SomeData = "Hello, World!";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string SomeData { get; set; }
    }
}
