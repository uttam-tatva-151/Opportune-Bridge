namespace Core.Beans
{
    public static class Constants
    {
        #region  Success Messages
        public const string SUCCESS_READ = "{0} fetch successfully";
        public const string SUCCESS_ADDED = "{0} added successfully.";
        public const string SUCCESS_UPDATED = "{0} updated successfully.";
        public const string SUCCESS_DELETED = "{0} deleted successfully.";
        public const string GENERAL_SUCCESS = "Operation completed successfully.";
        public const string SUCCESS_LOGIN = "Login successfully.";
        public const string SUCCESS_LOGOUT = "Logout successfully.";
        public const string SUCCESS_FORGOT_PASSWORD = "Password reset link sent to your email.";
        public const string SUCCESS_PASSWORD_CHANGED = "Password changed successfully.";
        public const string SUCCESS_EMAIL_SENT = "Email sent successfully.";
        public const string SUCCESS_TOKEN_REFRESHED = "Token refreshed successfully.";
        public const string ANALYSIS_COMPLETED = "Records analysis has been successfully completed.";
        public const string LOGOUT_CONFIRMATION = "Are you sure you want to Logout this ID?";

        #endregion
        #region  Error Messages
        public const string ERROR_ADDING = "An error occurred while adding {0}. Please try again.";
        public const string ERROR_UPDATING = "An error occurred while updating {0}. Please try again.";
        public const string ERROR_DELETING = "An error occurred while deleting {0}. Please try again.";
        public const string ERROR_NOT_FOUND = "{0} not found.";
        public const string GENERAL_ERROR = "An error occurred. Please try again.";
        public const string ERROR_LOGIN = "Login failed. Please check your Email.";
        public const string ERROR_PASSWORD_MISMATCH = "Password is Incorrect.";
        public const string ERROR_FORGOT_PASSWORD = "An error occurred while sending the password reset link. Please try again.";
        public const string ERROR_EMAIL_SENDING = "An error occurred while sending the email. Please try again.";
        public const string ERROR_SENDING_LOGIN_DETAILS_EMAIL = "Error to send Login Details to user's MailId.";
        public const string ERROR_MISSING_DB_CONNECTION = "Database connection string is missing or empty in configuration.";
        public const string ERROR_MISSING_JWT_SECTION = "JWT configuration section is missing or invalid.";
        public const string ERROR_INVALID_JWT_VALUES = "JWT configuration values (Key, Issuer, Audience) must not be null or empty.";
        public const string ACCOUNT_LOCKED = "User account is locked due to multiple failed login attempts. Please try again after {0} Minutes.";

        #endregion

        #region  Warning Messages
        public const string WARNING_INVALID_INPUT = "Invalid input provided for {0}.";
        public const string WARNING_INVALID_CREDENTIALS = "Invalid credentials.";
        public const string WARNING_ALL_READY_EXISTS = "{0} already exists.";
        public const string WARNING_DELETE_CONFIRMATION = "Are you sure want to delete this {0}?";
        public const string WARNING_MULTIPLE_DELETE_CONFIRMATION = "Are you sure want to delete this selected {0}s?";
        public const string WARNING_EMAIL_NOT_FOUND = "Email not found.";
        public const string WARNING_EMAIL_ALL_READY_EXISTS = "Email already exists.";
        public const string WARNING_EDITOR_ID_NOT_FOUND = "Editor ID not found.";
        public const string WARNING_NOTHING_SELECTED = "Please select at least one {0}.";
        public const string WARNING_ACTION_NOT_ALLOWED = "Action not allowed for you.";
        public const string WARNING_RESET_TOKEN_EXPIRED = "Reset Password Link Expired";
        #endregion
        #region  Info Messages
        public const string INFO_LOADING = "Loading {0}...";
        public const string INFO_NO_RECORDS_FOUND = "No {0} records found.";
        #endregion
        #region  Module/Entity Name
        public const string USER = "User";
        public const string ROLE = "Role";
        public const string PASSWORD = "Password";
        public const string USER_LIST = "User List";
        public const string ROLE_LIST = "Role List";
        public const string RESET_PASSWORD_TOKEN = "Reset Password Token";
        public const string WAITING_TOKEN = "Waiting Token";
        public const string PERMISSION_LIST = "Permission List";
        public const string MAPPING_RELATIONS = "Mapping Relations";
        public const string DEFAULT_ENTITY = "Entity";
        public const string PAGINATION = "Pagination Details";
        #endregion
        #region  Email Subject
        public const string EMAIL_SUBJECT_FORGOT_PASSWORD = "Password Reset Request";
        public const string EMAIL_SUBJECT_ADD_USER = "New User Registration";
        #endregion

        #region Column Order 
        public const string ASC_ORDER = "asc";
        public const string DESC_ORDER = "desc";
        #endregion

        #region  Layouts
        public const string LOGIN_LAYOUT = "_LoginLayout";
        public const string MAIN_LAYOUT = "_Layout";
        public const string ORDER_APP_LAYOUT = "_OrderAppLayout";
        public const string LAYOUT_VARIABLE_NAME = "LayoutName";
        #endregion

        #region  Tokens
        public const string ACCESS_TOKEN = "AccessToken";
        public const string REFRESH_TOKEN = "RefreshToken";
        public const string INVALID_ACCESS_TOKEN = "Invalid access token. Possible tampering detected.";
        public const string INVALID_REFRESH_TOKEN = "Invalid refresh token";
        public const string SESSION_EMAIL = "Email";
        public const string SESSION_USERNAME = "UserName";

        #endregion

        #region Views and Controller

        public const string DASHBOARD_VIEW = "Index";
        public const string USER_LIST_VIEW = "UserList";
        public const string LOGIN_VIEW = "Index";
        public const string PERMISSION_VIEW = "Permissions";
        public const string ERROR_VIEW = "HttpStatusCodeHandler";
        public const string HOME_CONTROLLER = "Home";
        public const string ROLE_CONTROLLER = "RoleAndPermissions";
        public const string LOGIN_CONTROLLER = "Login";
        public const string USER_CONTROLLER = "User";
        public const string ERROR_CONTROLLER = "ErrorHandler";

        public const string ERROR_HANDLER_HTTP_STATUS_CODE_HANDLER_ROUTE = "/ErrorHandler/HttpStatusCodeHandler/{0}";
        public const string ERROR_HANDLER_HTTP_STATUS_CODE_500_ROUTE = "/ErrorHandler/HttpStatusCodeHandler/500";
        public const string ERROR_HANDLER_HTTP_STATUS_CODE_404_ROUTE = "/ErrorHandler/HttpStatusCodeHandler/404";
        public const string ERROR_HANDLER_ROUTE = "/ErrorHandler";

        #endregion

        #region Partial Views
        public const string PARTIAL_DASHBOARD_GRID = "_parial_Dashboard_Grid";
        public const string PARTIAL_USER_GRID = "_partial_UserGrid";
        #endregion


        #region Authorization Permissions

        public const string CREATE_PERMISSION = "can_create";
        public const string EDIT_PERMISSION = "can_edit";
        public const string VIEW_PERMISSION = "can_view";
        public const string DELETE_PERMISSION = "can_delete";

        #endregion

        #region  Module Names

        public const string USERS_MODULE = "Users";
        public const string ROLE_AND_PERMISSION_MODULE = "RoleAndPermission";

        #endregion

        #region  Static Files Related

        public const string IMAGE_TYPE = "image/jpeg";
        public const string FORGOT_PASSWORD_FILE = "ForgotPasswordFormat.html";
        public const string EXPORT_FILE_GENERATION_ERROR = "An error occurred while generating the export file. Please try again later.";
        public const string EXPORT_FILE_GENERATION_SUCCESS = "The export file was generated successfully.";

        public const string TEMPLATE_NOT_FOUND = "Template not found.";

        public const string DATE_FORMATE = "yyyy-MM-dd";
        public const string PDF_CONTENT_TYPE = "application/pdf";
        public const string IMAGE_FORMATE = "data:image/jpeg;base64";
        #endregion

        #region  Roles
        public const string ADMIN_ROLE = "admin";
        public const string CHEF_ROLE = "chef";
        public const string ACCOUNT_MANAGER_ROLE = "account manager";
        public const string GUEST_ROLE = "Guest"; // Default role
        #endregion

        #region sort column
        public const string SORT_BY_DATE = "Date";


        #endregion

        #region Configuration Strings
        public const string JWT_CONFIG = "JwtConfig";
        public const string EMAIL_CONFIG = "EmailSettings";
        public const string DATABASE_DEFAULT_CONNECTION = "DefaultConnection";
        public const string MAX_FILE_UPLOAD_SIZE = "FileUpload:MaxMultipartBodyLengthInBytes";
        public const string DEFAULT_ROUTE_CONFIG = "RouteSettings";
        public const int SESSION_IDLE_TIME_OUT_HOURS = 10;
        public const int MAX_FAILED_ATTEMPTS = 10; // Maximum failed login attempts before locking the account for some times
        public const int LOCK_DURATION_MINUTES = 30;
        #endregion

        #region Swagger JWT Auth
        public const string SWAGGER_SECURITY_SCHEME = "Bearer";
        public const string SWAGGER_SECURITY_DESCRIPTION = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'";
        public const string SWAGGER_SECURITY_NAME = "Authorization";
        public const string SWAGGER_SECURITY_SCHEME_TYPE = "bearer";
        public const string SWAGGER_SECURITY_BEARER_FORMAT = "JWT";
        #endregion


    }
}

