
using System;

namespace SeleniumCore.Helpers
{
    public class Constants
    {
        //url's
        public static string BASE_URL = ConfigurationRoot.GetApplicationConfiguration().Url;
        public static string API_BASE_URL = ConfigurationRoot.GetApplicationConfiguration().ApiUrl;
        public static string SEGMENTS_PAGE = BASE_URL + "/segments";
        public static string PROJECTS_PAGE = BASE_URL + "/projects";
        //

        public static string BROWSER = ConfigurationRoot.GetApplicationConfiguration().Browser;

        public static string USER_PASSWORD = ConfigurationRoot.GetApplicationConfiguration().UserPassword;

        public static string USER_EMAIL = ConfigurationRoot.GetApplicationConfiguration().UserEmail;

        public static string USER_NAME = ConfigurationRoot.GetApplicationConfiguration().UserName;

        public static double LOAD_TIME_SECONDS = ConfigurationRoot.GetApplicationConfiguration().LoadTimeSeconds;

        public static double WAIT_TIME_SECONDS = ConfigurationRoot.GetApplicationConfiguration().WaitTimeSeconds;

        public static bool RUN_LOCAL = ConfigurationRoot.GetApplicationConfiguration().RunLocal;

        public static string USER_COMPANY = ConfigurationRoot.GetApplicationConfiguration().UserCompany;

        public static string CURRENT_DATE = DateTime.Now.Date.ToString("d MMM yyyy");
    }
}
