using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RedditWrapper
{
    public class ApiException : Exception
    {
        private readonly HttpStatusCode _status;

        public ApiException(HttpStatusCode status) : base("The API returned an error.")
        {
            _status = status;
        }

        public HttpStatusCode Status
        {
            get { return _status; }
        }
    }
}
