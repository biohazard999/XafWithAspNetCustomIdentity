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


Write a class that the XAF Module can use to register the exported types:

> Note you can skip this, if you work with the designer, but it will come handy later
	
	using System;
	using System.Collections.Generic;
	
	namespace XafWithAspNetCustomIdentity.Model._Configuration
	{
	    public static class ExportedTypesProvider
	    {
	        public static IEnumerable<Type> ExportedTypes
	        {
	            get
	            {
	                return new[]
	                {
	                    typeof(AspClaim),
	                    typeof(AspRole),
	                    typeof(AspUser),
	                    typeof(AspUserLogin),
	                };
	            }
	        }
	    }
	}

In the `Module.cs` of the `XafWithAspNetCustomIdentity.Module` overwrite the `GetDeclaredExportedTypes` method:

	using System;
	using System.Linq;
	using DevExpress.ExpressApp;
	using System.Collections.Generic;
	using DevExpress.ExpressApp.Updating;
	using XafWithAspNetCustomIdentity.Model._Configuration;
	
	namespace XafWithAspNetCustomIdentity.Module
	{
	    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppModuleBasetopic.
	    public sealed partial class XafWithAspNetCustomIdentityModule : ModuleBase
	    {
	        public XafWithAspNetCustomIdentityModule()
	        {
	            InitializeComponent();
	        }
	
	        protected override IEnumerable<Type> GetDeclaredExportedTypes()
	        {
	            return base.GetDeclaredExportedTypes().Concat(ExportedTypesProvider.ExportedTypes);
	        }
	
	        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
	        {
	            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
	            return new ModuleUpdater[] { updater };
	        }
	        public override void Setup(XafApplication application)
	        {
	            base.Setup(application);
	            // Manage various aspects of the application UI and behavior at the module level.
	        }
	    }
	}

Lets run the application and see what happens:

![](http://i.imgur.com/9RBlOFD.png)

Perfect, we have now our model completed.
Lets go on with the ASP.NET part.

## ASP.NET ##

Let's add another project:
![](http://i.imgur.com/F6QWFR6.png)

> Note: This sample uses `.NET4.5` but i think this is also possible with earlier versions of `.NET` but maybe not that easy ;)


We create a default **MVC** with **Individual Accounts** project. You can mix the technologies, but for now let everything like it is.

![](http://i.imgur.com/aYQIQhG.png)  