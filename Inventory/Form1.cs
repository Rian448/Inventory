using System.Text.RegularExpressions;
using System;

namespace Inventory
{
    public partial class frmAddProduct : Form
    {
        private string _ProductName, _Category, _MfgDate, _ExpDate, _Description;
        private int _Quantity;
        private double _SellPrice;
        private BindingSource showProductList;
        public frmAddProduct()
        {
            InitializeComponent();

            showProductList = new BindingSource();
        }

        public string Product_Name(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || !Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
            {
                throw new ArgumentException("Product name must contain only letters and spaces.");
            }
            return name;
        }

        public int Quantity(string qty)
        {
            if (string.IsNullOrWhiteSpace(qty) || !Regex.IsMatch(qty, @"^\d+$"))
            {
                // Throw exception if invalid
                throw new ArgumentException("Quantity must be a non-negative integer.");
            }
            return Convert.ToInt32(qty);
        }

        // Validate Selling Price - must be a valid double number
        public double SellingPrice(string price)
        {
            if (string.IsNullOrWhiteSpace(price) || !Regex.IsMatch(price, @"^(\d+(\.\d{1,2})?)$"))
            {
                // Throw exception if invalid
                throw new ArgumentException("Selling price must be a valid number (e.g., 10.99).");
            }
            return Convert.ToDouble(price);
        }
        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory = new string[]
              {
                "Beverages",
                "Bread/Bakery",
                "Canned/Jarred Goods",
                "Dairy",
                "Frozen Goods",
                "Meat",
                "Personal Care",
                "Other"
             };

            // Add categories to ComboBox
            foreach (string category in ListOfProductCategory)
            {
                cbCategory.Items.Add(category);
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            _ProductName = Product_Name(txtProductName.Text);
            _Category = cbCategory.Text;
            _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
            _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
            _Description = richTxtDescription.Text;
            _Quantity = Quantity(txtQuantity.Text);
            _SellPrice = SellingPrice(txtSellPrice.Text);
            showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate,
            _ExpDate, _SellPrice, _Quantity, _Description));
            gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridViewProductList.DataSource = showProductList;
        }
    }
}

  