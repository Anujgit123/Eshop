#pragma checksum "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\Shared\Components\CheckoutOrderPreview\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fb745d9e8f335b7729beeec1df7c181ac5d9d575"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_CheckoutOrderPreview_Default), @"mvc.1.0.view", @"/Views/Shared/Components/CheckoutOrderPreview/Default.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\_ViewImports.cshtml"
using System;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\_ViewImports.cshtml"
using Ecommerce.Web.Mvc;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\_ViewImports.cshtml"
using Ecommerce.Web.Mvc.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\_ViewImports.cshtml"
using Ecommerce.Web.Mvc.Constants;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\_ViewImports.cshtml"
using Ecommerce.Application.Helpers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\_ViewImports.cshtml"
using Ecommerce.Application.Dto;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\_ViewImports.cshtml"
using Ecommerce.Domain.Enums;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\_ViewImports.cshtml"
using Ecommerce.Domain.Constants;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\_ViewImports.cshtml"
using Ecommerce.Domain.Identity.Constants;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\_ViewImports.cshtml"
using Ecommerce.Application.Extension;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\_ViewImports.cshtml"
using Ecommerce.Domain.Identity.Permissions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\_ViewImports.cshtml"
using System.Security.Claims;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\_ViewImports.cshtml"
using System.Text.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\_ViewImports.cshtml"
using Ecommerce.Application.Interfaces;

#line default
#line hidden
#nullable disable
#nullable restore
#line 23 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 26 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\_ViewImports.cshtml"
using Microsoft.Extensions.Configuration;

#line default
#line hidden
#nullable disable
#nullable restore
#line 29 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\_ViewImports.cshtml"
using System.Reflection;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fb745d9e8f335b7729beeec1df7c181ac5d9d575", @"/Views/Shared/Components/CheckoutOrderPreview/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a22bbf1aaa6d985f28ac5fe57be1fdf1e0c3b591", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared_Components_CheckoutOrderPreview_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CartDto>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\Shared\Components\CheckoutOrderPreview\Default.cshtml"
  
    decimal totalPrice = 0;
    GeneralConfigurationDto conGen = JsonSerializer.Deserialize<GeneralConfigurationDto>(K["GeneralConfiguration"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div id=\"checkOrderPreview\">\r\n");
#nullable restore
#line 9 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\Shared\Components\CheckoutOrderPreview\Default.cshtml"
     if (Model != null && Model.Count() != 0)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"table-responsive\">\r\n            <table class=\"table card-table table-bordered\" width=\"100%\">\r\n");
            WriteLiteral("                <tbody>\r\n");
#nullable restore
#line 22 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\Shared\Components\CheckoutOrderPreview\Default.cshtml"
                     foreach (var item in Model)
                    {
                        totalPrice += (item.Price * @item.Qty);

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td hidden>");
#nullable restore
#line 26 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\Shared\Components\CheckoutOrderPreview\Default.cshtml"
                                  Write(item.VariableId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td class=\"pl-0\">\r\n                                <span class=\"d-block\">");
#nullable restore
#line 28 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\Shared\Components\CheckoutOrderPreview\Default.cshtml"
                                                 Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                            </td>\r\n");
            WriteLiteral("                            <td>\r\n                                <span style=\"display: inline-block;white-space: nowrap;\"><span");
            BeginWriteAttribute("class", " class=\"", 1298, "\"", 1378, 1);
#nullable restore
#line 32 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\Shared\Components\CheckoutOrderPreview\Default.cshtml"
WriteAttributeValue("", 1306, conGen.CurrencyPosition == CurrencyPosition.End ? "currency-swap": "", 1306, 72, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><span>");
#nullable restore
#line 32 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\Shared\Components\CheckoutOrderPreview\Default.cshtml"
                                                                                                                                                                                 Write(conGen.CurrencySymbol);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><span>");
#nullable restore
#line 32 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\Shared\Components\CheckoutOrderPreview\Default.cshtml"
                                                                                                                                                                                                                    Write(item.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </span></span><span>x ");
#nullable restore
#line 32 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\Shared\Components\CheckoutOrderPreview\Default.cshtml"
                                                                                                                                                                                                                                                      Write(item.Qty);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></span>\r\n                            </td>\r\n");
            WriteLiteral("                            <td class=\"pr-0\">\r\n                                <span");
            BeginWriteAttribute("class", " class=\"", 1680, "\"", 1760, 1);
#nullable restore
#line 36 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\Shared\Components\CheckoutOrderPreview\Default.cshtml"
WriteAttributeValue("", 1688, conGen.CurrencyPosition == CurrencyPosition.End ? "currency-swap": "", 1688, 72, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><span>");
#nullable restore
#line 36 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\Shared\Components\CheckoutOrderPreview\Default.cshtml"
                                                                                                                        Write(conGen.CurrencySymbol);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><span>");
#nullable restore
#line 36 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\Shared\Components\CheckoutOrderPreview\Default.cshtml"
                                                                                                                                                            Write(item.Price * @item.Qty);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></span>\r\n                            </td>\r\n                        </tr>\r\n");
#nullable restore
#line 39 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\Shared\Components\CheckoutOrderPreview\Default.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                </tbody>
                <tfoot>
                    <tr class=""fw-bold"">
                        <td colspan=""2"" class=""pb-1 pl-0"">Subtotal</td>
                        <td colspan=""3"" class=""pb-1 pr-0"">
                            <span");
            BeginWriteAttribute("class", " class=\"", 2194, "\"", 2274, 1);
#nullable restore
#line 46 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\Shared\Components\CheckoutOrderPreview\Default.cshtml"
WriteAttributeValue("", 2202, conGen.CurrencyPosition == CurrencyPosition.End ? "currency-swap": "", 2202, 72, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><span>");
#nullable restore
#line 46 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\Shared\Components\CheckoutOrderPreview\Default.cshtml"
                                                                                                                    Write(conGen.CurrencySymbol);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><span class=\"subtotal-value\">");
#nullable restore
#line 46 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\Shared\Components\CheckoutOrderPreview\Default.cshtml"
                                                                                                                                                                              Write(totalPrice);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</span></span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan=""2"" class=""py-1 pl-0"">Delivery Charge</td>
                        <td colspan=""3"" class=""py-1 pr-0"">
                            <span");
            BeginWriteAttribute("class", " class=\"", 2624, "\"", 2704, 1);
#nullable restore
#line 52 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\Shared\Components\CheckoutOrderPreview\Default.cshtml"
WriteAttributeValue("", 2632, conGen.CurrencyPosition == CurrencyPosition.End ? "currency-swap": "", 2632, 72, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><span>");
#nullable restore
#line 52 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\Shared\Components\CheckoutOrderPreview\Default.cshtml"
                                                                                                                    Write(conGen.CurrencySymbol);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</span><span class=""deliverycharge-value"">0</span></span>
                        </td>
                    </tr>
                    <tr class=""fw-bold py-2"">
                        <td colspan=""2"" class=""pl-0"">Total</td>
                        <td colspan=""3"" style=""color: #40c39c;""");
            BeginWriteAttribute("class", " class=\"", 3026, "\"", 3111, 2);
            WriteAttributeValue("", 3034, "pr-0", 3034, 4, true);
#nullable restore
#line 57 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\Shared\Components\CheckoutOrderPreview\Default.cshtml"
WriteAttributeValue(" ", 3038, conGen.CurrencyPosition == CurrencyPosition.End ? "currency-swap": "", 3039, 72, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><span>");
#nullable restore
#line 57 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\Shared\Components\CheckoutOrderPreview\Default.cshtml"
                                                                                                                                                       Write(conGen.CurrencySymbol);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><span class=\"total-value\"></span></td>\r\n                    </tr>\r\n\r\n                </tfoot>\r\n            </table>\r\n        </div>\r\n");
#nullable restore
#line 63 "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\Shared\Components\CheckoutOrderPreview\Default.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IConfiguration Configuration { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IHttpContextAccessor context { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IKeyAccessor K { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IAuthorizationService AuthorizationService { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CartDto>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
