using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CupOfSugar.WebSite.Services;
using CupOfSugar.WebSite.Models;

namespace CupOfSugar.Pages
{
    /// <summary>
    /// LendModel class
    /// Will be developed further to Lend page - Create in CRUDi associated with Lend form
    /// </summary>
    public class LendModel : PageModel
    {
        // Data middletier
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="productService"></param>
        public LendModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        [BindProperty]
        public CupOfSugar.WebSite.Models.Product Product { get; set; }  // The data to show, bind to it for the post
    }
}
