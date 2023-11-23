using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Add_Branch : System.Web.UI.Page
{
    admin a = new admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            if (Request.QueryString["id"] != null)
            {

                int pid = Convert.ToInt32(Request.QueryString["id"].ToString());
                SqlDataReader rd = a.GetDatareader("select cattegoryname from tbl_Category where id='" + pid + "'");
                while (rd.Read())
                {
                   
                    txtlast.Text = rd["cattegoryname"].ToString();                  
                }
                rd.Close();

               
            }
        }

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        cmd.Connection = conn;
        string insqry = "update tbl_Category set cattegoryname=@cattegoryname where id='" + Request.QueryString["id"].ToString() + "'";
        cmd = new SqlCommand(insqry, conn);      
        cmd.Parameters.AddWithValue("@cattegoryname", txtlast.Text);      
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
       Response.Redirect("View-Category-Detail.aspx");
    }
}