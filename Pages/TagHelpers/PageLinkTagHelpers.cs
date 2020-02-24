﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SparkAuto.Models;

namespace SparkAuto.Pages.TagHelpers
{
    public class PageLinkTagHelpers
    {
        [HtmlTargetElement("div", Attributes = "page-model")]
        public class PageLinkTagHelper : TagHelper

        {
            private IUrlHelperFactory urlHelperFactory;

            public PageLinkTagHelper(IUrlHelperFactory helperFactory)
            {
                urlHelperFactory = helperFactory;
            }

            //viewcontext provide access like httpcontext, request, response and so on
            //notbound this attribute you want to use in view context
            [ViewContext]
            [HtmlAttributeNotBound]
            public ViewContext ViewContext { get; set; }

            public  PagingInfo PageModel { get; set; }
            public string   PageAction { get; set; }
            public string  PageClass { get; set; }
            public string PageClassNormal { get; set; }
            public string PageClassSelected { get; set; }

            //override the process

            public override void Process(TagHelperContext context, TagHelperOutput output)
            {
                IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
                TagBuilder result = new TagBuilder("div");

                for (int i = 1; i <= PageModel.TotalPage; i++)
                {
                    TagBuilder tag = new TagBuilder("a");
                    string url = PageModel.UrlParam.Replace(":", i.ToString());
                    tag.Attributes["href"] = url;
                    tag.AddCssClass(PageClass);
                    tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected:PageClassNormal);
                    tag.InnerHtml.Append(i.ToString());
                    result.InnerHtml.AppendHtml(tag);
                }

                output.Content.AppendHtml(result.InnerHtml);
            }

        }
    }
}
