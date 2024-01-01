using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WarehouseManagementSystem.Models;

namespace TaskManagementApp
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Package> Packages { get; set; }
        private readonly WarehouseManager WarehouseManager;
        private IPackageHandler _handlerChain;
        private PackageMemento _lastState;
        private readonly PackagePool _packagePool;

        public MainWindow()
        {
            var _storages = new Dictionary<string, (int, int)>
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
            _packagePool = new PackagePool();
            UpdateHandlers();
        }

        private void BtnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateHandlers();
            var package = _packagePool.GetPackage();
            package.Name = txtPackageName.Text;
            package.Description = txtDescription.Text;
            package.Storage = txtStorage.Text;
            var isValid = _handlerChain.Handle(package);
            if (isValid)
            {
                _lastState = new PackageMemento(Packages, WarehouseManager._storages);
                Packages.Add(package);
                WarehouseManager.AddPackageToWarehouse(package.Storage);
                txtPackageName.Clear();
                txtDescription.Clear();
                txtStorage.Clear();
            }
            else
            {
                _packagePool.ReleasePackage(package);
            }
        }

        private void UndoLastAction_Click(object sender, RoutedEventArgs e)
        {
            if (_lastState != null)
            {
                var lastPackage = Packages.Last();
                Packages = _lastState.PackagesState;
                WarehouseManager._storages = _lastState.WarehouseState;
                lvTasks.ItemsSource = Packages;
                _packagePool.ReleasePackage(lastPackage);
            }
        }

        private void UpdateHandlers()
        {
            _handlerChain = new NameValidationHandler(Packages);
            var descriptionHandler = new DescriptionValidationHandler();
            var storageHandler = new StorageValidationHandler(WarehouseManager);
            _handlerChain.SetNext(descriptionHandler);
            descriptionHandler.SetNext(storageHandler);
        }


        private void Show_Click(object sender, RoutedEventArgs e)
        {
            foreach (var x in _packagePool.inUse)
            {
                Console.WriteLine($"In use!!! Name: {x.Name}, Description: {x.Description}, Storage: {x.Storage}");
            }

            foreach (var x in _packagePool.available)
            {
                Console.WriteLine($"Avaible!!! Name: {x.Name}, Description: {x.Description}, Storage: {x.Storage}");
            }
        }
    }
}