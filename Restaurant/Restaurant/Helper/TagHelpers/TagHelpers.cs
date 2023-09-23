//using Core.Constants;
//using Microsoft.AspNetCore.Razor.TagHelpers;

//namespace Restaurant.Helper.TagHelpers
//{
//    public class StatusTagHelper : TagHelper
//    {
//        [HtmlAttributeName("value")]
//        public int Status { get; set; }
//        public override void Process(TagHelperContext context, TagHelperOutput output)
//        {
//            output.TagName = "td";
//            output.TagMode = TagMode.StartTagAndEndTag;
//            output.Attributes.SetAttribute("class", GenerateStatusClass());
//            output.Content.SetContent(Enums.GetDescription((Enums.Status)Status));
//        }

//        private string GenerateStatusClass()
//        {
//            switch (Status)
//            {
//                case (int)Enums.Status.NotStarted:
//                    return "table-light";
//                case (int)Enums.Status.InProgress:
//                    return "table-secondary";
//                case (int)Enums.Status.Pending:
//                    return "table-primary";
//                case (int)Enums.Status.Done:
//                    return "table-warning";
//                case (int)Enums.Status.Canceled:
//                    return "table-danger";
//                default:
//                    return "table-light";
//            }
//        }
//    }
//}
