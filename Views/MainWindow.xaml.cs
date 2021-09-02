﻿using MahApps.Metro.Controls;
using System;
using System.Windows;

namespace PrismSample.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow() => InitializeComponent();

        ~MainWindow() => Console.WriteLine($"{GetType().Name}");
    }
}
