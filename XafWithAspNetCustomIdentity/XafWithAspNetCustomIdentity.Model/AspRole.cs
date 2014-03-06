using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace XafWithAspNetCustomIdentity.Model
{
    [Persistent]
    [DefaultClassOptions]
    public class AspRole : XPObject
    {


        public AspRole(Session session)
            : base(session)
        {
        }

        [Association("ASP-User-Role")]
        public XPCollection<AspUser> Users
        {
            get
            {
                return GetCollection<AspUser>("Users");
            }
        }

        private string _Name;
        [DevExpress.ExpressApp.DC.XafDisplayName("Name")]
        public string Name
        {
            get { return _Name; }
            set { SetPropertyValue("Name", ref _Name, value); }
        }

    }

}