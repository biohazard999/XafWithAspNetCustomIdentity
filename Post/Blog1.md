# How to run an ASP.NET MVC5 project from an XAF Application #

I try to provide you an step by step model, based on a new project, so everybody can follow.

## Creating the XAF-Winforms-only project ##

![](http://i.imgur.com/HbCZBDY.png)

In the `App.config` Use your connection string:

	<add name="ConnectionString" connectionString="Integrated Security=SSPI;Pooling=false;Data Source=.\SQLEXPRESS;Initial Catalog=XafWithAspNetCustomIdentity"/> 



Lets run the application:

![](http://i.imgur.com/YI0gaLB.png)

Great!


Let'S create another project called `XafWithAspNetCustomIdentity.Model`:

![](http://i.imgur.com/LtthBaA.png)


Our project structure should look something like this:

![](http://i.imgur.com/RiXlI7W.png)


Add a model class for a User, Roles, Claims, and UserLogins:

User:

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

Role:

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

Claim:

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

And User Login:

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