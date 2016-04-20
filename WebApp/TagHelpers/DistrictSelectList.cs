using System.Linq;
using System.Text;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Razor.TagHelpers;
using WebApp.Models;

namespace WebApp.TagHelpers
{
    [HtmlTargetElement("district-list", Attributes = "city-id")]
    public class DistrictSelectListTagHelper : TagHelper
    {
        public int CityId { get; set; }

        public int DistrictId { get; set; }

        private ApplicationDbContext DbContext;

        public DistrictSelectListTagHelper([FromServices] ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "select";

            var items = new StringBuilder();
            var list = DbContext.Districts.Where(x => x.CityId == CityId).OrderBy(x => x.Name).ToList();

            items.Append("<option value=\"\">Все районы</option>");
            foreach (var item in list)
            {
                if (item.Id == DistrictId)
                {
                    items.Append($"<option value=\"{item.Id}\" selected=\"true\">{item.Name}</option>");
                }
                else
                {
                    items.Append($"<option value=\"{item.Id}\">{item.Name}</option>");
                }
            }
        
            output.Content.SetHtmlContent(items.ToString());
         
            output.Attributes.Add("class", "ui fluid dropdown");
        }
    }
}
