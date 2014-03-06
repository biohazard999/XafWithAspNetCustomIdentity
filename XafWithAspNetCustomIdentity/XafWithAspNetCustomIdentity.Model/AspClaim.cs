using DevExpress.Xpo;

namespace XafWithAspNetCustomIdentity.Model
{

    [Persistent]
    public class AspClaim : XPObject
    {

        public AspClaim(Session session)
            : base(session)
        {
        }

        private AspUser _User;
        [Association("ASP-User-Claim")]
        public AspUser User
        {
            get { return _User; }
            set { SetPropertyValue("User", ref _User, value); }
        }

        private string _Value;
        public string Value
        {
            get { return _Value; }
            set { SetPropertyValue("Value", ref _Value, value); }
        }

        private string _Type;
        public string Type
        {
            get { return _Type; }
            set { SetPropertyValue("Type", ref _Type, value); }
        }

    }

}