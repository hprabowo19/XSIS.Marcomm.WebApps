using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Threading.Tasks;
using XSIS.Marcomm.Models;
using XSIS.Marcomm.ViewModels;

namespace XSIS.Marcomm.Repositories
{
    public class DashboardRepository
    {
        public int TotalCompany()
        {
            int totalCompany = 0;

            using (MarcommEntities db = new MarcommEntities())
            {
                var getCompany = db.m_company.Count();

                if (getCompany == 0)
                {
                    totalCompany = 0;
                }
                else
                {
                    totalCompany = getCompany;
                }
            }

            return totalCompany;
        }

        public int TotalEmployee()
        {
            int totalEmployee = 0;

            using (MarcommEntities db = new MarcommEntities())
            {
                var getEmployee = db.m_employee.Count();

                if (getEmployee == 0)
                {
                    totalEmployee = 0;
                }
                else
                {
                    totalEmployee = getEmployee;
                }
            }

            return totalEmployee;
        }

        public int TotalUser()
        {
            int totalUser = 0;

            using (MarcommEntities db = new MarcommEntities())
            {
                var getUser = db.m_user.Count();

                if (getUser == 0)
                {
                    totalUser = 0;
                }
                else
                {
                    totalUser = getUser;
                }
            }

            return totalUser;
        }

        public int TotalProduct()
        {
            int totalProduct = 0;

            using (MarcommEntities db = new MarcommEntities())
            {
                var getProduct = db.m_product.Count();

                if (getProduct == 0)
                {
                    totalProduct = 0;
                }
                else
                {
                    totalProduct = getProduct;
                }
            }

            return totalProduct;
        }
    }
}
