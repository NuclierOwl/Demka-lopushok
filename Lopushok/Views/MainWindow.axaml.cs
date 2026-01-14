using Avalonia.Controls;
using Avalonia.Interactivity;
using Lopushok.Hardik.Connector;
using Lopushok.Hardik.Dao;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lopushok.Views;

public partial class MainWindow : Window
{
    private int currentPage = 1;
    private int pageSize = 20;
    private List<PRODUCT> allProducts = new List<PRODUCT>();

    public MainWindow()
    {
        InitializeComponent();
        LoadProducts();
    }

    private void LoadProducts()
    {
        using (var db = new dbBaza())
        {
            allProducts = db.PRODUCTs.ToList();
            ApplyFilters();
        }
    }

    private void ApplyFilters()
    {
        List<PRODUCT> filteredProducts = allProducts;

        if (SerchBox != null && !string.IsNullOrEmpty(SerchBox.Text))
        {
            filteredProducts = filteredProducts
                .Where(p => p.NAME_PRODUCT != null &&
                           p.NAME_PRODUCT.ToLower().Contains(SerchBox.Text.ToLower()))
                .ToList();
        }

        if (ComboFilter != null)
        {
            switch (ComboFilter.SelectedIndex)
            {
                case 1:
                    filteredProducts = filteredProducts
                        .OrderByDescending(p => p.MIN_PRICE)
                        .ToList();
                    break;
                case 2:
                    filteredProducts = filteredProducts
                        .OrderBy(p => p.MIN_PRICE)
                        .ToList();
                    break;
                default:
                    filteredProducts = filteredProducts
                        .OrderBy(p => p.NAME_PRODUCT)
                        .ToList();
                    break;
            }
        }

        UpdatePagination(filteredProducts);
        DisplayCurrentPage(filteredProducts);
    }

    private void UpdatePagination(List<PRODUCT> products)
    {
        PaginationPanel.Children.Clear();

        int totalPages = (int)Math.Ceiling(products.Count / (double)pageSize);

        var prevButton = new Button
        {
            Content = "←",
            IsEnabled = currentPage > 1
        };
        prevButton.Click += (s, e) =>
        {
            if (currentPage > 1)
            {
                currentPage--;
                ApplyFilters();
            }
        };
        PaginationPanel.Children.Add(prevButton);

        for (int i = 1; i <= totalPages; i++)
        {
            var pageButton = new Button
            {
                Content = i.ToString(),
                Width = 30,
                IsEnabled = i != currentPage
            };

            int pageNumber = i;
            pageButton.Click += (s, e) =>
            {
                currentPage = pageNumber;
                ApplyFilters();
            };

            PaginationPanel.Children.Add(pageButton);
        }

        var nextButton = new Button
        {
            Content = "→",
            IsEnabled = currentPage < totalPages
        };
        nextButton.Click += (s, e) =>
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                ApplyFilters();
            }
        };
        PaginationPanel.Children.Add(nextButton);
    }

    private void DisplayCurrentPage(List<PRODUCT> products)
    {
        int skip = (currentPage - 1) * pageSize;
        var pageProducts = products.Skip(skip).Take(pageSize).ToList();
        ProductsContainer.ItemsSource = pageProducts;
    }

    private void SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        currentPage = 1;
        ApplyFilters();
    }

    private void TextChanged(object sender, TextChangedEventArgs e)
    {
        currentPage = 1;
        ApplyFilters();
    }
}