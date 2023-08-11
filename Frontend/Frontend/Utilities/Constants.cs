using System.Dynamic;

namespace Frontend.Utilities
{
    public static class Constants
    {
        public struct AppSettings
        {
            public const string BaseApiUrl = "AppSettings:BaseApiUrl";
        }
        public struct SelectOption
        {
            public static string SelectAll = "--- Select All ---";
            public static string SelectOne = "--- Please Select ---";
        }
        public struct SessionKey
        {
            public const string sessionLogin = "sessionLogin";
            public const string accessToken = "JwtToken";
        }
        public struct UrlApi
        {
            public const string Login = "Authentication/Login";
            public const string Register = "Authentication/Register";
        }
        public struct UrlAction
        {
            public struct Home
            {
                public const string Login = "~/Home/Login";
            }
            public struct Product
            {
                public const string GetList = "~/Product/GetList";
                public const string Save = "~/Product/Save";
                public const string Delete = "~/Product/Delete";
            }
            public struct Menu
            {
                public const string GetList = "~/Menu/GetList";
                public const string Save = "~/Menu/Save";
                public const string Delete = "~/Menu/Delete";
            }
        }
        public struct MessageError
        {
            public const string CallAPI = "Error calling API";
        }

        public struct CategoryDDL
        {
            public const string Code = "Id";
            public const string Text = "Name";
        }
        public struct StatusListDDl
        {
            public const string Code = "CODE";
            public const string Text = "TEXT";
        }

        public struct Action
        {
            public const string New = "New";
            public const string Edit = "Edit";
            public const string View = "View";
            public const string Add = "Add";
            public const string Approved = "Approved";
            public const string Delete = "Del";
            public const string Send = "Send";
            public const string Save = "Save";
            public const string Reject = "Reject";
            public const string Upload = "Upload";
        }
    }
}
