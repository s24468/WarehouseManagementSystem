using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Packaging;
using System.Windows;
using WarehouseManagementSystem.Models;

namespace TaskManagementApp
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Package> Packages { get; set; }
        private WarehouseManager WarehouseManager;
        private IPackageHandler _handlerChain;

        public MainWindow()
        {
            var _storages =new Dictionary<string, (int, int)>
            {
                { "A", (4, 0) },
                { "B", (2, 0) },
                { "C", (100, 0) },
                { "D", (20, 0) },
            };
            InitializeComponent();
            Packages = new ObservableCollection<Package>();
            lvTasks.ItemsSource = Packages;
            WarehouseManager = new WarehouseManager(_storages); 
            SetupHandlers();
        }

        private void BtnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            var package = new Package(txtPackageName.Text, txtDescription.Text, txtStorage.Text);
            var isValid = _handlerChain.Handle(package);
            if (isValid)
            {
                Packages.Add(package);
                WarehouseManager.AddPackageToWarehouse(package.Storage);
                txtPackageName.Clear();
                txtDescription.Clear();
                txtStorage.Clear();
            }
        }

        private void SetupHandlers()
        {
            _handlerChain = new NameValidationHandler(Packages);
            var descriptionHandler = new DescriptionValidationHandler();
            var storageHandler = new StorageValidationHandler(WarehouseManager);
            _handlerChain.SetNext(descriptionHandler);
            descriptionHandler.SetNext(storageHandler);
        }
    }
}