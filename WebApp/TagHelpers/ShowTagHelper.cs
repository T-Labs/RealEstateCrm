using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Razor.TagHelpers;

namespace WebApp.TagHelpers
{
    [HtmlTargetElement(Attributes = "show-if, hide-if")]
    public class ShowTagHelper : TagHelper
    {
        public bool ShowIf { get; set; }
        public bool HideIf { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!ShowIf || HideIf)
            {
                output.SuppressOutput();
            }
        }
    }
}
