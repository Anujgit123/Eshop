#pragma checksum "C:\Users\anujy\Downloads\E_shop\Eshop_Ecommerce_AspNetCore\source-code\eshop_ecommerce_main\Ecommerce\Ecommerce.Web.Mvc\Views\Order\_AddPaymentPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1419458f2696be8daeade77ddad380cd0c871fdb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Order__AddPaymentPartial), @"mvc.1.0.view", @"/Views/Order/_AddPaymentPartial.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1419458f2696be8daeade77ddad380cd0c871fdb", @"/Views/Order/_AddPaymentPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a22bbf1aaa6d985f28ac5fe57be1fdf1e0c3b591", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Order__AddPaymentPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
