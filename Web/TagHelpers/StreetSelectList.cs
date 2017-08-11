using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WebApp.Models;

namespace WebApp.TagHelpers
{
    [HtmlTargetElement("street-list", Attributes = "city-id")]
    public class StreetSelectListTagHelper : TagHelper
    {
        public int CityId { get; set; }

        public int StreetId { get; set; }

        private ApplicationDbContext DbContext;

        public StreetSelectListTagHelper([FromServices] ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "select";

            var items = new StringBuilder();
            var list = DbContext.Streets.Where(x => x.CityId == CityId).OrderBy(x => x.Name).ToList();

            items.Append("<option value=\"\">Все улицы</option>");
            foreach (var item in list)
            {
                if (item.Id == StreetId)
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
