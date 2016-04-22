using System;
using System.Linq;
using System.Text;
using Microsoft.AspNet.Razor.TagHelpers;
using Microsoft.SqlServer.Server;

namespace WebApp.TagHelpers
{
    [HtmlTargetElement("alphabet-pager", Attributes = "letter, link-url, query-params")]
    public class AlphabetPagingTagHelper : TagHelper
    {
        public char? Letter { get; set; }
        
        public string LinkUrl { get; set; }

        public Microsoft.AspNet.Http.IReadableStringCollection QueryParams { get; set; }

        private const string AplhabetList = "0123456789АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            char letter = Letter.HasValue ? Letter.Value : '*';

            if (Char.IsLower(letter))
            {
                letter = Char.ToUpper(letter);
            }

            var items = new StringBuilder();
            for (var index = 0; index < AplhabetList.Length; index++)
            {
                if (AplhabetList[index] == letter)
                {
                    items.Append($"<a class=\"ui blue label\">{AplhabetList[index]}</a>");
                }
                else
                {
                    var paramsUrl = QueryParams.ToList().ToDictionary(x => x.Key, x => x.Value[0]);

                    if (!paramsUrl.ContainsKey("letter"))
                    {
                        paramsUrl.Add("letter", AplhabetList[index].ToString());
                    }
                    else
                    {
                        paramsUrl["letter"] = AplhabetList[index].ToString();
                    }
         
                    var queryParams = paramsUrl.Aggregate("", (x, y) => x + $"&{y.Key}={y.Value}").Trim('&');
                    
                    items.Append($"<a class=\"ui label\" href=\"{LinkUrl}?{queryParams}\">{AplhabetList[index]}</a>");
                }

            }
            output.Content.SetHtmlContent(items.ToString());
            if (output.Attributes["class"] != null)
            {
                output.Attributes["class"].Value += " ui circular labels";
            }
            else
            {
                output.Attributes.Add("class", "ui circular labels");
            }
        }
    }
}
