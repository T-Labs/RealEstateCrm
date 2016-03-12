using System.Linq;
using System.Text;
using Microsoft.AspNet.Razor.TagHelpers;

namespace RealEstateCrm.TagHelpers
{
    [HtmlTargetElement("pager", Attributes = "total-pages, current-page, link-url, query-params")]
    public class PagingTagHelper : TagHelper
    {
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        
        public string LinkUrl { get; set; }

        public Microsoft.AspNet.Http.IReadableStringCollection QueryParams { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {/*
            <div class="ui circular labels">
                <a class="ui label">Первая</a>
                <a class="ui blue label">1</a>
                <a class="ui label">2</a>
                <a class="ui label">3</a>
                <a class="ui label">Последняя</a>
            </div>
            */
            output.TagName = "div";
           // output.PreContent.SetContent("<ul class=\"link-list\">");
            
            
            var items = new StringBuilder();
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
                    //Where(x => x.Key != "page").Aggregate("", (x, y) => x + $"&{y.Key}={y.Value[0]}")
                    var queryParams = paramsUrl.Aggregate("", (x, y) => x + $"&{y.Key}={y.Value}").Trim('&');
                    
                    items.Append($"<a class=\"ui label\" href=\"{LinkUrl}?{queryParams}\">{page}</a>");
                }

            }
            output.Content.SetHtmlContent(items.ToString());
            //output.PostContent.SetContent("</ul>");
            output.Attributes.Clear();
            output.Attributes.Add("class", "ui circular labels");
        }
    }
}
