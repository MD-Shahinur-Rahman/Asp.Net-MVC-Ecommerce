﻿using System.Web;
using System.Web.Mvc;

namespace E_commerceProject_1280721
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
