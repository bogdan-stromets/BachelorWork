using AccountingSystem.Model;
using AccountingSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace AccountingSystem.ViewModel
{
    class ChangeProductVM : ViewModelBase
    {
        private DB dB;
        private DeviceModel targetProduct;
        private string _name, _description,_price,_count, _pictureURL,categoryStr,manufacturerStr;
        private ManufacturerModel _productManufacturer;
        private CategoryModel _productCategory;
        private List<ManufacturerModel> manufacturers;
        private List<CategoryModel> categories;
        private List<string> manufacturersStr, categoriesStr;

        public string ProductName
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); } 
        }
        public string ProductDescription
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }
        public ManufacturerModel ProductManufacturer
        {
            get => manufacturers.Find(x => x.name == ProductManufacturerString);
        }
        public string ProductPrice
        {
            get => _price;
            set { _price = value; OnPropertyChanged(); }
        }
        public CategoryModel ProductCategory
        {
            get => categories.Find(x =>x.name == ProductCategoryString);
        }        
        public string ProductCategoryString
        {
            get => categoryStr;
            set { categoryStr = value; OnPropertyChanged(nameof(ProductCategory)); }
        }     
        public string ProductManufacturerString
        {
            get => manufacturerStr;
            set { manufacturerStr = value; OnPropertyChanged(nameof(ProductManufacturer)); }
        }
        public string ProductCount
        {
            get => _count;
            set { _count = value; OnPropertyChanged(); }
        }
        public string ProductPictureURL
        {
            get => _pictureURL;
            set { _pictureURL = value; OnPropertyChanged(nameof(picturePreview)); }
        }
        public BitmapImage picturePreview
        {
            get => GetPicture();
        }
        public List<string> manufacturersList { get => manufacturersStr; set { manufacturersStr = value; OnPropertyChanged(); } }
        public List<string> categoriesList { get => categoriesStr; set { categoriesStr = value; OnPropertyChanged(); } }

        public ICommand ChangeCommand { get; set; }

        public ChangeProductVM()
        {
            targetProduct = NavigationVM.CurrentObject as DeviceModel;
            ProductName = targetProduct.name;
            ProductDescription = targetProduct.description;
            ProductManufacturerString = targetProduct.manufacturer.name;
            ProductPrice = targetProduct.price.ToString();
            ProductCategoryString = targetProduct.category.name;
            ProductCount = targetProduct.stock_size.ToString();
            ProductPictureURL = targetProduct.pictureURL;


            dB = new DB();
            ChangeCommand = new RelayCommand(ChangeProduct);
            FillList();
        }

        private void FillList()
        {
            manufacturersList = new List<string>();
            categoriesList = new List<string>();
            GetManufacturers();
            GetCategories();
            manufacturers.ForEach(m => manufacturersList.Add(m.name));
            categories.ForEach(c => categoriesList.Add(c.name));
        }
        private void GetManufacturers()
        {
            manufacturers = new List<ManufacturerModel>();
            DataTable dt = dB.GetTableData(TableName.manufacturers.ToString());
            foreach (DataRow dr in dt.Rows)
                manufacturers.Add(new ManufacturerModel(dr));
        }
        private void GetCategories()
        {
            categories = new List<CategoryModel>();
            DataTable dt = dB.GetTableData(TableName.categories.ToString());
            foreach (DataRow dr in dt.Rows)
                categories.Add(new CategoryModel(dr));
        }
        private BitmapImage GetPicture()
        {
            try
            {
                BitmapImage _picture = new BitmapImage();
                _picture.BeginInit();
                _picture.UriSource = new Uri(ProductPictureURL);
                _picture.EndInit();
                return _picture;
            }
            catch (Exception)
            {
                BitmapImage _picture = new BitmapImage();
                string noPictureImg = "https://static.vecteezy.com/system/resources/previews/005/337/799/non_2x/icon-image-not-found-free-vector.jpg";
                _picture.BeginInit();
                _picture.UriSource = new Uri(noPictureImg);
                _picture.EndInit();
                return _picture;
            }
        }
        private void ChangeProduct(object obj)
        {
            try
            {
                dB.ExecuteSQLCommand(targetProduct.UpdateAllCommand(ProductName,ProductDescription,ProductManufacturer.id, Convert.ToDecimal(ProductPrice),ProductCategory.id,ProductCount,ProductPictureURL));
                NavigationVM.ExecuteProductCommand();
            }
            catch (Exception)
            {
                MessageBox.Show("Not all values are filled!","Error!",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
        }
    }
}
