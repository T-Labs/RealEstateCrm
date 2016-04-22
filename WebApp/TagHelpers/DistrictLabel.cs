using System.Linq;
using System.Text;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Razor.TagHelpers;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.TagHelpers
{
    [HtmlTargetElement("district", Attributes = "district-id")]
    public class DistrictLabelTagHelper : TagHelper
    {

        public int DistrictId { get; set; }

        private AddressService DbContext;

        public DistrictLabelTagHelper([FromServices] AddressService dbContext)
        {
            DbContext = dbContext;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "label";

            var item = DbContext.CachedDistrictList().FirstOrDefault(x => x.Id == DistrictId);
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
