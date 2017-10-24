namespace TenMinutePlatformIntegration2.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Primary : DbContext
    {
        // Your context has been configured to use a 'Primary' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'TenMinutePlatformIntegration2.Models.Primary' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Primary' 
        // connection string in the application configuration file.
        public Primary()
            : base("name=Primary")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

         public virtual DbSet<Request> Requests { get; set; }
    }

  
}