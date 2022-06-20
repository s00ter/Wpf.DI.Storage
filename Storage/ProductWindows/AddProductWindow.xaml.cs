﻿using Storage.Database.Entities;
using System;
using System.Linq;
using System.Windows;
using Storage.Database.Enums;

namespace Storage.ProductWindows
{
    public partial class AddProductWindow
    {
        private readonly int _currentId;

        public Product Result { get; set; }


        public AddProductWindow()
        {
            InitializeComponent();

            InitializeProductOwner(ProductOwner.Простор);
            InitializeProductType(ProductType.Расходник);
        }

        public AddProductWindow(Product product)
        {
            InitializeComponent();

            NameTextBox.Text = product.Name;
            CostTextBox.Text = product.Cost.ToString("f2");

            ComingPicker.SelectedDate = DateTime.Parse(product.Coming);

            AmountTextBox.Text = product.Amount.ToString();
            DimensionTypeTextBox.Text = product.DimensionType;
            VendorCodeTextBox.Text = product.VendorCode;
            InfoTextBox.Text = product.Info;
            ProviderTextBox.Text = product.Provider;
            _currentId = product.Id;

            InitializeProductType(product.ProductType);
            InitializeProductOwner(product.ProductOwner);
        }

        private void InitializeProductType(ProductType productType)
        {
            var items = Enum.GetValues<ProductType>();
            ProductTypeComboBox.ItemsSource = items;

            ProductTypeComboBox.SelectedItem = items.First(x => x == productType);
        }

        private void InitializeProductOwner(ProductOwner productOwner)
        {
            var items = Enum.GetValues<ProductOwner>();
            ProductOwnerComboBox.ItemsSource = items;

            ProductOwnerComboBox.SelectedItem = items.First(x => x == productOwner);
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(CostTextBox.Text, out var cost))
            {
                MessageBox.Show("Стоимость должна быть числом");
                return;
            }

            DateTime? date = null;
            if (ComingPicker.SelectedDate is null)
            {
                var datePickerAns = MessageBox.Show("Не выбрана дата. Выбрать сегодня?", "Выбор", MessageBoxButton.YesNo);

                if (datePickerAns == MessageBoxResult.No)
                {
                    return;
                }
                date = DateTime.Now;
            }

            if (!int.TryParse(AmountTextBox.Text, out var amount))
            {
                MessageBox.Show("Количество должно быть числом");
                return;
            }

            Result = new Product
            {
                Id = _currentId,
                Name = NameTextBox.Text,
                Cost = cost,
                Coming = date == null
                    ? ComingPicker.SelectedDate.Value.ToString("d")
                    : date.Value.ToString("d"),
                Amount = amount,
                DimensionType = DimensionTypeTextBox.Text,
                VendorCode = VendorCodeTextBox.Text,
                Status = amount == 0
                    ? ProductStatus.Закончилось
                    : ProductStatus.Наличие,
                Info = InfoTextBox.Text,
                Provider = ProviderTextBox.Text,
                ProductType = (ProductType)ProductTypeComboBox.SelectedItem,
                ProductOwner = (ProductOwner)ProductOwnerComboBox.SelectedItem
            };

            DialogResult = true;
        }
    }
}