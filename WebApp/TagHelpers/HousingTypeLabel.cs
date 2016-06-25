using System.Linq;
using System.Text;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Razor.TagHelpers;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.TagHelpers
{
    [HtmlTargetElement("housing-label", Attributes = "housing-type-id")]
    public class HousingTypeLabelTagHelper : TagHelper
    {

        public int HousingTypeId { get; set; }

        private AddressService DbContext;

        public HousingTypeLabelTagHelper([FromServices] AddressService dbContext)
        {
            DbContext = dbContext;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "label";

            var item = DbContext.CachedHousingTypeList().FirstOrDefault(x => x.Id == HousingTypeId);
            if (item == null)
            {
                output.SuppressOutput();
            }
            else
            {
                output.Content.SetHtmlContent(item.Name);
            }
        }
    }
}
