using Structure.Core.Enums;
using Structure.Model.Custom;
using Structure.Model.Poco;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Composing;

namespace Structure.Core.Utilities
{
    public static class ExtensionMethods
    {
        private static UmbracoHelper Umbraco
        {
            get
            {

                // return new UmbracoHelper(Current.);
                return Current.UmbracoHelper;
            }
        }
        #region IPublishedContent - Methods   

        #region Other datatypes

        /// <summary>
        /// Return the strings that has been filled in.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public static GoogleMaps GetGoogleMapsValues(this IPublishedContent content, string alias)
        {
            char[] splitChar = { ',', ';' };
            var values = content.Value<string>(alias).Split(splitChar, StringSplitOptions.RemoveEmptyEntries);

            if (!values.Any())
            {
                return null;
            }

            //Return the Google Maps values with the properties set.
            return
            (
                new GoogleMaps()
                {
                    Lat = values[0],
                    Lng = values[1],
                    Zoom = Convert.ToInt32(values[2])
                }
            );
        }

        #endregion

        #region Property

        /// <summary>
        /// Return the node where default settings are stored.
        /// </summary>
        public static IPublishedContent TopPage(this IPublishedContent content)
        {
            var topPage = (IPublishedContent)System.Web.HttpContext.Current.Items["TopPage"];

            if (topPage == null)
            {
                topPage = content.AncestorOrSelf(1);
                System.Web.HttpContext.Current.Items["TopPage"] = topPage;
            }

            return topPage;
        }

        #endregion

        #endregion

        #region UmbracoHelper - Methods

        #region Email

        /// <summary>
        /// Send the e-mail.
        /// </summary>
        /// <param name="umbraco"> </param>
        /// <param name="emailFrom"></param>
        /// <param name="emailFromName"> </param>
        /// <param name="emailTo"></param>
        /// <param name="emailCc"></param>
        /// <param name="emailBcc"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachments"></param>
        /// <param name="emailType">Type of email to set in the db</param>
        /// <returns></returns>
        public static void SendEmail(this UmbracoHelper umbraco, string emailFrom, string emailFromName, string emailTo, string subject, string body, string emailCc = "", string emailBcc = "", EmailType? emailType = null)
        {

            //Make the MailMessage and set the e-mail address.
            MailMessage message = new MailMessage();
            message.From = new MailAddress(emailFrom, emailFromName);

            //Split all the e-mail addresses on , or ;.
            char[] splitChar = { ',', ';' };

            //Remove all spaces.
            emailTo = emailTo.Trim();
            emailCc = emailCc.Trim();
            emailBcc = emailBcc.Trim();

            //Split emailTo.
            string[] toArray = emailTo.Split(splitChar, StringSplitOptions.RemoveEmptyEntries);
            foreach (string to in toArray)
            {
                //Check if the e-mail is valid.
                if (umbraco.IsValidEmail(to))
                {
                    //Loop through all e-mail addressen in toArray and add them in the to.
                    message.To.Add(new MailAddress(to));
                }
            }

            //Split emailCc.
            string[] ccArray = emailCc.Split(splitChar, StringSplitOptions.RemoveEmptyEntries);
            foreach (string cc in ccArray)
            {
                //Check if the e-mail is valid.
                if (umbraco.IsValidEmail(cc))
                {
                    //Loop through all e-mail addressen in ccArray and add them in the cc.
                    message.CC.Add(new MailAddress(cc));
                }
            }

            //Split emailBcc.
            string[] bccArray = emailBcc.Split(splitChar, StringSplitOptions.RemoveEmptyEntries);
            foreach (string bcc in bccArray)
            {
                //Check if the e-mail is valid.
                if (umbraco.IsValidEmail(bcc))
                {
                    //Loop through all e-mail addressen in bccArray and add them in the bcc.
                    message.Bcc.Add(new MailAddress(bcc));
                }
            }

            //Set the rest of the e-mail data.
            message.Subject = subject;
            message.SubjectEncoding = Encoding.UTF8;
            message.Body = body;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;

            //Only send the email if there is a to address.
            if (message.To.Any())
            {
                if (emailType.HasValue)
                {
                    try
                    {
                        //Create the email object and set all properties.
                        var emailPoco = new EmailPoco()
                        {
                            Type = emailType.Value.ToString(),
                            FromName = emailFromName,
                            FromEmail = emailFrom,
                            ToEmail = emailTo,
                            CCEmail = emailCc,
                            BCCEmail = emailBcc,
                            Date = DateTime.Now,
                            Subject = subject,
                            Message = body
                        };
                        using (var scope = Current.ScopeProvider.CreateScope(autoComplete: true))
                        {
                            //Insert the email into the database.
                            scope.Database.Insert(emailPoco);
                        }

                    }
                    catch (Exception ex)
                    {
                        Umbraco.LogException(ex);
                    }
                }

                //Make the SmtpClient.
                SmtpClient smtpClient = new SmtpClient();

                //Send the e-mail.
                smtpClient.Send(message);
            }

            //Clear the resources.
            message.Dispose();
        }

        #endregion

        #region Validation

        /// <summary>
        /// Checks if the e-mail is valid.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(this UmbracoHelper umbraco, string email)
        {
            Regex r = new Regex(@"([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})");
            return r.IsMatch(email);
        }

        #endregion

        #region Error

        /// <summary>
        /// Log an exception and send an email.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="nodeId"></param>
        /// <param name="type"></param>
        public static void LogException(this UmbracoHelper umbraco, Exception ex)
        {
            try
            {
                int nodeId = -1;
                if (System.Web.HttpContext.Current.Items["pageID"] != null)
                {
                    int.TryParse(System.Web.HttpContext.Current.Items["pageID"].ToString(), out nodeId);
                }

                StringBuilder comment = new StringBuilder();
                StringBuilder commentHtml = new StringBuilder();

                commentHtml.AppendFormat("<p><strong>Url:</strong><br/>{0}</p>", System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
                commentHtml.AppendFormat("<p><strong>Node id:</strong><br/>{0}</p>", nodeId);

                //Add the exception.
                comment.AppendFormat("Exception: {0} - StackTrace: {1}", ex.Message, ex.StackTrace);
                commentHtml.AppendFormat("<p><strong>Exception:</strong><br/>{0}</p>", ex.Message);
                commentHtml.AppendFormat("<p><strong>StackTrace:</strong><br/>{0}</p>", ex.StackTrace);

                if (ex.InnerException != null)
                {
                    //Add the inner exception.
                    comment.AppendFormat("- InnerException: {0} - InnerStackTrace: {1}", ex.InnerException.Message, ex.InnerException.StackTrace);
                    commentHtml.AppendFormat("<p><strong>InnerException:</strong><br/>{0}</p>", ex.InnerException.Message);
                    commentHtml.AppendFormat("<p><strong>InnerStackTrace:</strong><br/>{1}</p>", ex.InnerException.StackTrace);
                }
                //Log the Exception into Umbraco.                
                Current.Logger.Error(System.Reflection.MethodBase.GetCurrentMethod().GetType(), comment.ToString(), ex);
                //Send an email with the exception.
                umbraco.SendEmail(umbraco.Config().ErrorFrom, umbraco.Config().ErrorFromName, umbraco.Config().ErrorTo, "Error log", commentHtml.ToString());
            }
            catch
            {
                //Do nothing because nothing can be done with this exception.
            }
        }

        #endregion

        #region Property

        /// <summary>
        /// Return an instance of the Configuration class.
        /// </summary>
        public static Configuration Config(this UmbracoHelper umbraco)
        {
            return Configuration.Instance;
        }

        #endregion

        #region Pager

        /// <summary>
        /// Return all fields required for paging.
        /// </summary>
        /// <param name="itemsPerPage"></param>
        /// <param name="numberOfItems"></param>
        /// <returns></returns>
        public static Pager GetPager(this UmbracoHelper umbraco, int itemsPerPage, int numberOfItems)
        {
            // paging calculations
            int currentPage;
            if (!int.TryParse(HttpContext.Current.Request.QueryString["Page"], out currentPage))
            {
                currentPage = 1;
            }
            var numberOfPages = numberOfItems % itemsPerPage == 0 ? Math.Ceiling((decimal)(numberOfItems / itemsPerPage)) : Math.Ceiling((decimal)(numberOfItems / itemsPerPage)) + 1;
            var pages = Enumerable.Range(1, (int)numberOfPages);

            return new Pager()
            {
                NumberOfItems = numberOfItems,
                ItemsPerPage = itemsPerPage,
                CurrentPage = currentPage,
                Pages = pages
            };
        }

        #endregion

        #region Other

        /// <summary>
        /// Appends or updates a query string value to the current Url
        /// </summary>
        /// <param name="key">The query string key</param>
        /// <param name="value">The query string value</param>
        /// <returns>The updated Url</returns>
        public static string AppendOrUpdateQueryString(this UmbracoHelper umbraco, string key, string value)
        {
            return umbraco.AppendOrUpdateQueryString(HttpContext.Current.Request.RawUrl, key, value);
        }

        /// <summary>
        /// Appends or updates a query string value to supplied Url
        /// </summary>
        /// <param name="url">The Url to update</param>
        /// <param name="key">The query string key</param>
        /// <param name="value">The query string value</param>
        /// <returns>The updated Url</returns>
        public static string AppendOrUpdateQueryString(this UmbracoHelper umbraco, string url, string key, string value)
        {
            var q = '?';

            if (url.IndexOf(q) == -1)
            {
                return string.Concat(url, q, key, '=', HttpUtility.UrlEncode(value));
            }

            var baseUrl = url.Substring(0, url.IndexOf(q));
            var queryString = url.Substring(url.IndexOf(q) + 1);
            var match = false;
            var kvps = HttpUtility.ParseQueryString(queryString);

            foreach (var queryStringKey in kvps.AllKeys)
            {
                if (queryStringKey == key)
                {
                    kvps[queryStringKey] = value;
                    match = true;
                    break;
                }
            }

            if (!match)
            {
                kvps.Add(key, value);
            }

            return string.Concat(baseUrl, q, ConstructQueryString(kvps, null, false));
        }

        /// <summary>
        /// Constructs a NameValueCollection into a query string.
        /// </summary>
        /// <remarks>Consider this method to be the opposite of "System.Web.HttpUtility.ParseQueryString"</remarks>
        /// <param name="parameters">The NameValueCollection</param>
        /// <param name="delimiter">The String to delimit the key/value pairs</param>
        /// <param name="omitEmpty">Boolean to chose whether to omit empty values</param>
        /// <returns>A key/value structured query string, delimited by the specified String</returns>      
        private static string ConstructQueryString(NameValueCollection parameters, string delimiter, bool omitEmpty)
        {
            if (string.IsNullOrEmpty(delimiter))
                delimiter = "&";

            var equals = '=';
            var items = new List<string>();

            for (var i = 0; i < parameters.Count; i++)
            {
                foreach (var value in parameters.GetValues(i))
                {
                    var addValue = omitEmpty ? !string.IsNullOrEmpty(value) : true;
                    if (addValue)
                    {
                        items.Add(string.Concat(parameters.GetKey(i), equals, HttpUtility.UrlEncode(value)));
                    }
                }
            }

            return string.Join(delimiter, items.ToArray());
        }

        #endregion

        #endregion
    }
}
