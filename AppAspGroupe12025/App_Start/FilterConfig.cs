﻿using System.Web;
using System.Web.Mvc;

namespace AppAspGroupe12025
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
