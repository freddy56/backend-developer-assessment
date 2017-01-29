using Autofac;
using Autofac.Integration.WebApi;
using MusicStore.Data;
using MusicStore.Handler;
using MusicStore.IHandler;
using MusicStore.IRepository;
using MusicStore.MusicBrainzAPI.Service;
using MusicStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace MusicStore.WebApi
{
    public static class Bootstrapper
    {
        public static void InitialiseDependencies()
        {
            var builder = new ContainerBuilder();
            // You can register controllers all at once using assembly scanning...
            builder.RegisterInstance(new ArtistRepository(new ArtistDBEntities()))
            .As<IArtistRepository>();
            builder.RegisterInstance(new ArtistHandler(new ArtistRepository(new ArtistDBEntities()),
                new RestClientService("http://musicbrainz.org/ws/2")))
            .As<IArtistHandler>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(ArtistRepository)))
            //.Where(t => t.Name.EndsWith("Repository"))
            //.AsImplementedInterfaces();
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(ArtistHandler)))
            //.Where(t => t.Name.EndsWith("Handler"))
            //.AsImplementedInterfaces();
            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());
        }
    }
}