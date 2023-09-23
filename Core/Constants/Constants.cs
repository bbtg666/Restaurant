namespace Core.Constants
{
    public static class Constants
    {
        public struct SESSION
        {
            public const string SESSION_KEY = "SessionKey";
        }

        public struct MESSAGE
        {
            public const string ERROR_KEY = "ErrorMessage";
            public const string ADD_ITEM_FAILED = "Create task failed!";
            public const string UPDATE_ITEM_FAILED = "Edit task failed!";
            public const string DELETE_ITEM_FAILED = "Delete task failed!";
            public const string LOGIN_FAILED = "User name or Password isn't correct!";
            public const string SIGNUP_FAILED = "Sign up failed";

            public const string SUCCESS_KEY = "SuccessMessage";
            public const string ADD_ITEM_SUCCESS = "Add successfully!";
            public const string UPDATE_ITEM_SUCCESS = "Edit successfully!";
            public const string DELETE_ITEM_SUCCESS = "Delete successfully!";
        }

        public struct FEILD_SEARCH
        {
            public const string TITLE = "Title";
            public const string CONTENT = "Content";
            public const string STATUS = "Status";
            public const string CREATED_DATE = "CreatedDate";
            public const string UPDATED_DATE = "UpdatedDate";
        }

        public struct ROLE
        {
            public const string ADMIN = "Admin";
            public const string MEMBER = "Member";
        }

        public struct AREA
        {
            public const string ADMIN = "Admin";
        }
    }
}
