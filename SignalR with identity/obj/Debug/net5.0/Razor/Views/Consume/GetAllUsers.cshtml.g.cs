#pragma checksum "D:\auth by tutorial\SignalR with identity\SignalR with identity\Views\Consume\GetAllUsers.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ca77e74ab68bc8503e325486d1d3811a198e86c1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Consume_GetAllUsers), @"mvc.1.0.view", @"/Views/Consume/GetAllUsers.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ca77e74ab68bc8503e325486d1d3811a198e86c1", @"/Views/Consume/GetAllUsers.cshtml")]
    #nullable restore
    public class Views_Consume_GetAllUsers : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<SignalR_with_identity.Models.UserApp>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\auth by tutorial\SignalR with identity\SignalR with identity\Views\Consume\GetAllUsers.cshtml"
  
    ViewData["Title"] = "GetAllUsers";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>GetAllUsers</h1>\r\n\r\n<p>\r\n    <a asp-action=\"RegisterUser\">Create New</a>\r\n</p>\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 16 "D:\auth by tutorial\SignalR with identity\SignalR with identity\Views\Consume\GetAllUsers.cshtml"
           Write(Html.DisplayNameFor(model => model.Username));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 19 "D:\auth by tutorial\SignalR with identity\SignalR with identity\Views\Consume\GetAllUsers.cshtml"
           Write(Html.DisplayNameFor(model => model.Password));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 22 "D:\auth by tutorial\SignalR with identity\SignalR with identity\Views\Consume\GetAllUsers.cshtml"
           Write(Html.DisplayNameFor(model => model.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 28 "D:\auth by tutorial\SignalR with identity\SignalR with identity\Views\Consume\GetAllUsers.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
#nullable restore
#line 31 "D:\auth by tutorial\SignalR with identity\SignalR with identity\Views\Consume\GetAllUsers.cshtml"
           Write(Html.DisplayFor(modelItem => item.Username));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 34 "D:\auth by tutorial\SignalR with identity\SignalR with identity\Views\Consume\GetAllUsers.cshtml"
           Write(Html.DisplayFor(modelItem => item.Password));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 37 "D:\auth by tutorial\SignalR with identity\SignalR with identity\Views\Consume\GetAllUsers.cshtml"
           Write(Html.DisplayFor(modelItem => item.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                <button>");
#nullable restore
#line 40 "D:\auth by tutorial\SignalR with identity\SignalR with identity\Views\Consume\GetAllUsers.cshtml"
                   Write(Html.ActionLink("Edit", "updateUser","Consume", new { email = item.Email }));

#line default
#line hidden
#nullable disable
            WriteLiteral("</button>\r\n\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 44 "D:\auth by tutorial\SignalR with identity\SignalR with identity\Views\Consume\GetAllUsers.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 48 "D:\auth by tutorial\SignalR with identity\SignalR with identity\Views\Consume\GetAllUsers.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
            }
            );
        }
        #pragma warning restore 1998
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<SignalR_with_identity.Models.UserApp>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591