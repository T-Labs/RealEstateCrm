using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApp.TagHelpers
{
    [HtmlTargetElement("pager", Attributes = "total-pages, current-page, link-url, query-params")]
    public class PagingTagHelper : TagHelper
    {
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        
        public string LinkUrl { get; set; }

        public IQueryCollection QueryParams { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (TotalPages < 2)
            {
                return;
            }

            output.TagName = "div";
         
            var items = new StringBuilder();
            items.Append("<span>Страницы:</span>");
            for (var page = 1; page <= TotalPages; page++)
            {
                if (CurrentPage == page)
                {
                    items.Append($"<a class=\"ui blue label\">{page}</a>");
                }
                else
                {
                    var paramsUrl = QueryParams.ToList().ToDictionary(x => x.Key, x => x.Value[0]);

                    if (!paramsUrl.ContainsKey("page"))
                    {
                        paramsUrl.Add("page", page.ToString());
                    }
                    else
                    {
                        paramsUrl["page"] = page.ToString();
                    }
         
                    var queryParams = paramsUrl.Aggregate("", (x, y) => x + $"&{y.Key}={y.Value}").Trim('&');
                    
                    items.Append($"<a class=\"ui label\" href=\"{LinkUrl}?{queryParams}\">{page}</a>");
                }

            }
            output.Content.SetHtmlContent(items.ToString());
            output.Attributes.Clear();
            output.Attributes.Add("class", "ui circular labels");
        }
    }
}
