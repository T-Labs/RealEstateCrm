using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Razor.TagHelpers;

namespace WebApp.TagHelpers
{
    [HtmlTargetElement(Attributes = "hide-if")]
    public class HideTagHelper : TagHelper
    {
        public bool HideIf { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (HideIf)
            {
                output.SuppressOutput();
            }
        }
    }
}
