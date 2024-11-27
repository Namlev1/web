using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiProj.Models
{
    public partial class Product : ObservableObject
    {
        [ObservableProperty]
        public int id;
        [ObservableProperty]
        public string name;
        [ObservableProperty]
        public string description;
        [ObservableProperty]
        public decimal price;
        [ObservableProperty]
        public Category category = new Category(); //= new Category { Id = 1, Name = "testCat1" };
        [ObservableProperty]
        public ObservableCollection<Tag>? tags = [];// = new ObservableCollection<Tag> { new Tag {Id=1, Name="testTag1" }, new Tag {Id=2, Name="testTag2" } };
        [ObservableProperty]
        public ProductDetails details = new ProductDetails();// = new ProductDetails {Id=1, Manufacturer="testManufacturer", Warranty="testWarranty" };
    }
}
