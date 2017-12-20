using Datos.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Datos.Controllers
{
    

    public static class CRUD
    {
        static MVCAPPEntities con = new MVCAPPEntities();



        /// <summary>
        /// Inserts the product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public static string  InsertProduct(PRODUCTS product)
        {
            try
            {
                con.PRODUCTS.Add(product);
                con.SaveChanges();

                return "Successful";
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }



        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public static List<PRODUCTS> GetProducts()
        {
            try
            {
                var x = (from i in con.PRODUCTS
                         select i).ToList();


                return x;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }



        /// <summary>
        /// Fills the products.
        /// </summary>
        /// <param name="gridView">The grid view.</param>
        /// <exception cref="System.Exception"></exception>
        public static void fillProducts(ref DataGridView gridView)
        {
            try
            {
                var x = (from i in con.PRODUCTS
                         select i).ToList();


                gridView.DataSource = x;
                
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }




        /// <summary>
        /// Edits the product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public static string EditProduct(PRODUCTS product,int productId)
        {
            try
            {
                var x = (from i in con.PRODUCTS
                         where i.ID == productId
                         select i).First();

                x.PRODUCT = product.PRODUCT;
                x.QUANTITY = product.QUANTITY;
                x.MODIFIED_DATE = DateTime.Now.ToString("MM/dd/yyyy");

                int result =con.SaveChanges();

                if (result > 0)
                {
                    return "Successful";
                }
                else
                {
                    return "Failed Operation";
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }



        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public static string DeleteProduct(int productId)
        {

            try
            {
                var x = (from i in con.PRODUCTS
                         where i.ID == productId
                         select i).First();


                con.PRODUCTS.Remove(x);
                int result= con.SaveChanges();

                if (result > 0)
                {
                    return "Successful";
                }
                else
                {
                    return "Failed Operation";
                }



            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }



        /// <summary>
        /// Fills the products.
        /// </summary>
        /// <param name="gridView">The grid view.</param>
        /// <param name="productName">Name of the product.</param>
        /// <exception cref="System.Exception"></exception>
        public static void fillProducts(ref DataGridView gridView, string productName)
        {
            try
            {
                var x = (from i in con.PRODUCTS
                         where i.PRODUCT==productName
                         select i).ToList();


                gridView.DataSource = x;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }




        /// <summary>
        /// Fills the products.
        /// </summary>
        /// <param name="gridView">The grid view.</param>
        /// <param name="Quantity">The quantity.</param>
        /// <exception cref="System.Exception"></exception>
        public static void fillProducts(ref DataGridView gridView, int Quantity)
        {
            try
            {
                var x = (from i in con.PRODUCTS
                         where i.QUANTITY == Quantity
                         select i).ToList();


                gridView.DataSource = x;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }


        /// <summary>
        /// Fills the products.
        /// </summary>
        /// <param name="gridView">The grid view.</param>
        /// <param name="dateInit">The date initialize.</param>
        /// <param name="dateFin">The date fin.</param>
        /// <exception cref="System.Exception"></exception>
        public static void fillProducts(ref DataGridView gridView, string dateInit,string dateFin)
        {
            string hh = @"Data Source=tcp:DESKTOP-NH9SF1J,49172;Initial Catalog=MVCAPP; User Id=dariavish; Password=Winters1";

            string query = "SELECT * FROM  PRODUCTS WHERE MODIFIED_DATE BETWEEN '" + dateInit + "' AND  '" + dateFin + "'";

            SqlConnection con = new SqlConnection(hh);
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable table = new DataTable();

                con.Open();
                adapter.Fill(table);
                con.Close();

                if (table.Rows.Count > 0)
                {
                    gridView.DataSource = table;
                }
                else
                {
                    gridView.DataSource = null;
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally { con.Close(); }

        }
    }
}
