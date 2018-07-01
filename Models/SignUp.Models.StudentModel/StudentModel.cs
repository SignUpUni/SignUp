using System;

namespace SignUp.Models.StudentModel
{
    public class StudentModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id
        {
            get;
            set;
        } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>The gender.</value>
        public string Gender
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>The email address.</value>
        public string EmailAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the university.
        /// </summary>
        /// <value>The university.</value>
        public string University
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the synched status.
        /// </summary>
        /// <value>The synched.</value>
        public SyncStatus SyncStatus
        {
            get;
            set;
        }
    }
}
