﻿// <copyright file="SettingsWindow.xaml.cs" company="AAllard">License: http://www.gnu.org/licenses/gpl.html GPL version 3.</copyright>

namespace FileConverter
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.ComponentModel;
    using System.Windows.Controls;

    using FileConverter.Annotations;

    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class SettingsWindow : Window, INotifyPropertyChanged
    {
        private ConversionPreset selectedPreset;

        public SettingsWindow()
        {
            this.InitializeComponent();
            
            Application application = Application.Current as Application;
            this.PresetList.ItemsSource = application.Settings.ConversionPresets;

            OutputType[] outputTypes = new[]
                                           {
                                               OutputType.Flac, 
                                               OutputType.Mp3, 
                                               OutputType.Ogg, 
                                               OutputType.Wav, 
                                           };

            this.OutputFormats.ItemsSource = outputTypes;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ConversionPreset SelectedPreset
        {
            get
            {
                return this.selectedPreset;
            }

            set
            {
                this.selectedPreset = value;
                this.OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as System.Windows.Controls.CheckBox;
            string inputFormat = checkBox.Content as string;

            if (!this.selectedPreset.InputTypes.Contains(inputFormat))
            {
                this.selectedPreset.InputTypes.Add(inputFormat);
            }
        }

        private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as System.Windows.Controls.CheckBox;
            string inputFormat = checkBox.Content as string;

            this.selectedPreset.InputTypes.Remove(inputFormat);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Load previous preset in order to cancel changes.
            Application application = Application.Current as Application;
            application.Settings.Load();

            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Save changes.
            Application application = Application.Current as Application;
            application.Settings.Save();

            this.Close();
        }
    }
}