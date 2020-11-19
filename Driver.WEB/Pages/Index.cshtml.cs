using Driver.CORE.Entities;
using Driver.WEB.Interfaces;
using Driver.WEB.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Driver.WEB.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IIndexService indexService;

        public IndexModel(IIndexService indexService)
        {
            this.indexService = indexService;
        }

        [BindProperty]
        public IndexVm IndexInfo { get; set; } = new IndexVm() { Assessments = new List<AssessmentVm>() };
        [BindProperty]
        public List<SelectListItem> CompanyInfo { get; set; } = new List<SelectListItem>();

        //========================
        // GET
        //========================
        public void OnGet()
        {
            GetPageInfo();
        }

        //========================
        // POST
        //========================
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                GetPageInfo();
            }

            return Page();
        }

        //========================
        // PRIVATE
        //========================
        private void GetPageInfo()
        {
            CompanyInfo = indexService.GetAllCompanies();

            IndexInfo = indexService.GetAllPassed(IndexInfo.CompanyId);
        }
    }
}
