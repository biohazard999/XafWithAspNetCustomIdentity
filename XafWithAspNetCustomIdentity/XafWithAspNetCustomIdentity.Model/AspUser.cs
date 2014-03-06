using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace XafWithAspNetCustomIdentity.Model
{
    [Persistent]
    [DefaultClassOptions]
    public class AspUser : XPObject
    {
        public AspUser(Session session) : base(session)
        {
        }

        private string _Username;

        public string Username
        {
            get { return _Username; }
            set { SetPropertyValue("Username", ref _Username, value); }
        }


        private string _PasswordHash;

        [Custom("AllowEdit", "false")]
        public string PasswordHash
        {
            get { return _PasswordHash; }
            set { SetPropertyValue("Password", ref _PasswordHash, value); }
        }

        [Association("ASP-User-Claim")]
        public XPCollection<AspClaim> Claims
        {
            get { return GetCollection<AspClaim>("Claims"); }
        }

        [Association("ASP-User-Login")]
        public XPCollection<AspUserLogin> Logins
        {
            get { return GetCollection<AspUserLogin>("Logins"); }
        }

        [Association("ASP-User-Role")]
        public XPCollection<AspRole> Roles
        {
            get { return GetCollection<AspRole>("Roles"); }
        }
    }
}
