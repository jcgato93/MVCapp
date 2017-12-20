using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCapp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            BindGrid();
        }




        /// <summary>
        /// Binds the grid.
        /// </summary>
        public void BindGrid()
        {
            Datos.Controllers.CRUD.fillProducts(ref GridProducts);
        }



        /// <summary>
        /// Searches the date.
        /// </summary>
        public void searchDate()
        {
            string init = dateInit.Value.ToString("MM/dd/yyyy");
            string fin = dateFin.Value.ToString("MM/dd/yyyy");
            Datos.Controllers.CRUD.fillProducts(ref GridProducts, init, fin);
        }



   



        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProduct.Text))
            {
                try
                {
                    Datos.Entities.PRODUCTS product = new Datos.Entities.PRODUCTS();

                    product.PRODUCT = txtProduct.Text;
                    product.QUANTITY = (int)txtQuantity.Value;
                    product.MODIFIED_DATE = DateTime.Now.ToString("MM/dd/yyyy");

                    string result = Datos.Controllers.CRUD.InsertProduct(product);

                    MessageBox.Show(result);

                    BindGrid();

                    tabControl1.SelectTab(1);


                }
                catch (Exception)
                {

                    throw;
                }
            }

            else {
                MessageBox.Show("Missing Product Name");
            }
        }




        private void GridProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtProductId.Text = GridProducts.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtProducEdit.Text = GridProducts.Rows[e.RowIndex].Cells[1].Value.ToString();
            string Qt= GridProducts.Rows[e.RowIndex].Cells[2].Value.ToString();
            if (!Qt.Equals("0")) { txtQuantityEdit.Value = int.Parse(Qt); } else { txtQuantityEdit.Value = 0; }
            
        }
       


        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProductId.Text))
            {
                if (string.IsNullOrEmpty(txtProducEdit.Text)) { MessageBox.Show("Type the Product Name"); }
                else
                {
                    Datos.Entities.PRODUCTS product = new Datos.Entities.PRODUCTS();

                    product.ID = int.Parse(txtProductId.Text);
                    product.PRODUCT = txtProducEdit.Text;
                    product.QUANTITY = (int)txtQuantityEdit.Value;


                    string result= Datos.Controllers.CRUD.EditProduct(product,product.ID);

                    MessageBox.Show(result);

                    BindGrid();

                    txtProducEdit.Text = string.Empty;
                    txtQuantityEdit.Value = 0;
                    txtProductId.Text = string.Empty;

                }

            }
            else { MessageBox.Show("Please Select Product"); }

        }




        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProductId.Text))
            {
              DialogResult dialog= MessageBox.Show("Are you sure you want to delete it", "Delete", MessageBoxButtons.YesNo);

                if (dialog == DialogResult.Yes)
                {
                    string result = Datos.Controllers.CRUD.DeleteProduct(int.Parse(txtProductId.Text));

                    MessageBox.Show(result);

                    BindGrid();

                    txtProducEdit.Text = string.Empty;
                    txtQuantityEdit.Value = 0;
                    txtProductId.Text = string.Empty;
                }

            }            
            else { MessageBox.Show("Please Select Product"); }
        }


        private void txtProductSearch_TextChanged(object sender, EventArgs e)
        {
            Datos.Controllers.CRUD.fillProducts(ref GridProducts, txtProductSearch.Text);
        }

        private void txtQuantitySearch_ValueChanged(object sender, EventArgs e)
        {
            Datos.Controllers.CRUD.fillProducts(ref GridProducts, int.Parse(txtQuantitySearch.Value.ToString()));
        }

     

        private void dateInit_ValueChanged_1(object sender, EventArgs e)
        {
            searchDate();
        }

        private void dateFin_ValueChanged(object sender, EventArgs e)
        {
            searchDate();
        }
    }
}
