using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WebApp.Models;

namespace WebApp.TagHelpers
{
    [OutputElementHint("select")]
    [HtmlTargetElement("housiong-type-list", Attributes = "house-type-id")]
    public class HousingTypeListTagHelper : TagHelper
    {
        public int HouseTypeId { get; set; }

        private ApplicationDbContext DbContext;

        public HousingTypeListTagHelper([FromServices] ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output){

            output.TagName = "select";

            var items = new StringBuilder();
            var list = DbContext.TypesHousing.OrderBy(x => x.Name).ToList();

            items.Append("<option value=\"\"Все типы жилья</option>");
            foreach (var item in list)
            {
                if (item.Id == HouseTypeId)
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
