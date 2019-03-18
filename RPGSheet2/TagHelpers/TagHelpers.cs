using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RPGSheet2.TagHelpers
{
    [HtmlTargetElement("desc", Attributes = "asp-for")]
    public class DescTagHelper : TagHelper
    {
        public DescTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }
        public override int Order { get; }
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        protected IHtmlGenerator Generator { get; }



        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "";

            TagBuilder tag = new TagBuilder("span");

            tag.TagRenderMode = TagRenderMode.Normal;
            tag.InnerHtml.SetContent(For.ModelExplorer.Metadata.Description);
            if (output.Attributes.ContainsName("class"))
            {
                TagHelperAttribute insert;
                if (output.Attributes.TryGetAttribute("class", out insert))
                    tag.AddCssClass(insert.Value.ToString());
            }
            else
            {
                tag.AddCssClass("text-info");
            }

            using (var writer = new StringWriter())
            {
                output.Content.AppendHtml("<br />");
                tag.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
                output.Content.AppendHtml(writer.ToString());
            }
        }
    }
}
