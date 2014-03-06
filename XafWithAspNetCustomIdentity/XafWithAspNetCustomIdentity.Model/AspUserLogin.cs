using DevExpress.Xpo;

namespace XafWithAspNetCustomIdentity.Model
{

    [Persistent]
    public class AspUserLogin : XPObject
    {
        public AspUserLogin(Session session)
            : base(session)
        {
        }

        private AspUser _User;
        [Association("ASP-User-Login")]
        public AspUser User
        {
            get { return _User; }
            set { SetPropertyValue("User", ref _User, value); }
        }

        private string _LoginProvider;
        public string LoginProvider
        {
            get { return _LoginProvider; }
            set { SetPropertyValue("LoginProvider", ref _LoginProvider, value); }
        }


        private string _ProviderKey;
        public string ProviderKey
        {
            get { return _ProviderKey; }
            set { SetPropertyValue("ProviderKey", ref _ProviderKey, value); }
        }
    }

}