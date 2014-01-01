// ----------------------------------------------------------------------------------
// Microsoft Developer & Platform Evangelism
// 
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// ----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
// ----------------------------------------------------------------------------------

namespace CloudSurvey
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;
    using CloudSurvey.Repositories;

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SurveyContext, Migrations.Configuration>());
            //// Database.SetInitializer<SurveyContext>(new DropCreateDatabaseIfModelChanges<SurveyContext>()); 

            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleDependencyResolver();

            using (var context = new SurveyContext())
            {
                var dummy = context.Surveys.FirstOrDefault();
            }

            InitializeSecurity();
        }

        private static void InitializeSecurity()
        {
            if (Membership.FindUsersByName("admin").Cast<MembershipUser>().FirstOrDefault() == null)
            {
                Membership.CreateUser("admin", "password", "admin@contoso.com");
            }
        }
    }
}