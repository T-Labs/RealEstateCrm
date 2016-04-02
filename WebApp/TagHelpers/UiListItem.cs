using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Razor.TagHelpers;

namespace WebApp.TagHelpers
{
    [HtmlTargetElement("ui-list-item", Attributes = "icon, content")]
    public class UiListItem : TagHelper
    {
        public string Icon { get; set; }
        public string Content { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            if (output.Attributes["class"].IsNull())
            {
                output.Attributes["class"] = "item";
            }
            else
            {
                output.Attributes["class"].Value += " item";
            }
            /*
            <div class="item">
                        <i class="marker icon"></i>
                        <div class="content">@Html.DisplayFor(x => item.FullAddress)</div>
                    </div>
            */

            var html = new StringBuilder();

            
            html.Append($"<i class='{Icon} icon'></i>");
            html.Append($"<div class='content'>{Content}</div>");
            

            output.Content.SetHtmlContent(html.ToString());
        }
    }
}
