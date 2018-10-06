using System;
using System.Collections.Generic;
using System.Text;

namespace PetBellies.Model
{
    public class SMTPUser
    {
        /// <summary>
        /// Gets or sets the SMTP client host.
        /// </summary>
        public string SMTPCLientHost
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the SMTP server port.
        /// </summary>
        public int SMTPServerPort
        {
            get;
            set;
        }

        /// <summary>
        /// Your E-Mail address.
        /// </summary>
        public string SMTPEmail
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the SMTP username.
        /// </summary>
        public string SMTPUsername
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the SMTP password.
        /// </summary>
        public string SMTPPassword
        {
            get;
            set;
        }
    }
}
