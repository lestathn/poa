﻿using Application.Institucion.Services;
using Application.Poa.Services;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Domain.Institucion;
using Domain.Poa;
using Infrastructure.Institucion.Repositories;
using Infrastructure.NHibernate;
using Infrastructure.Poa.Repositories;
using NHibernate;
using System.Web.Mvc;

namespace Presentation.CastleWindsor
{
    public class CommonDependencyInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.ComponentRegistered += Kernel_ComponentRegistered;

            container
                .Register(Classes.FromThisAssembly()
                .BasedOn<IController>()
                .LifestyleTransient());

            container
                .Register(Component.For<IProgramaEstrategicoService>()
                .ImplementedBy<ProgramaEstrategicoService>()
                .LifeStyle.Transient);

            container
                .Register(Component.For<IProgramaEstrategicoRepository>()
                .ImplementedBy<ProgramaEstrategicoRepository>()
                .LifeStyle.Transient);

            container
                .Register(Component.For<IProductoService>()
                .ImplementedBy<ProductoService>()
                .LifeStyle.Transient);

            container
                .Register(Component.For<IProductoRepository>()
                .ImplementedBy<ProductoRepository>()
                .LifeStyle.Transient);

            container
                .Register(Component.For<IUsuarioService>()
                .ImplementedBy<UsuarioService>()
                .LifeStyle.Transient);

            container
                .Register(Component.For<IUsuarioRepository>()
                .ImplementedBy<UsuarioRepository>()
                .LifeStyle.Transient);

            container
                .Register(Component.For<IDependenciaService>()
                .ImplementedBy<DependenciaService>()
                .LifeStyle.Transient);

            container
                .Register(Component.For<IDependenciaRepository>()
                .ImplementedBy<DependenciaRepository>()
                .LifeStyle.Transient);

            container
                .Register(Component.For<ISessionFactory>()
                .UsingFactoryMethod(NhSessionFactory.Create)
                .LifeStyle.Singleton);

            container
                .Register(Component.For<NhUnitOfWorkInterceptor>()
                .LifeStyle.Transient);
        }

        private static void Kernel_ComponentRegistered(string key, IHandler handler)
        {
            if (UnitOfWorkHelper.IsRepositoryClass(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(NhUnitOfWorkInterceptor)));
            }

            foreach (var method in handler.ComponentModel.Implementation.GetMethods())
            {
                if (UnitOfWorkHelper.HasUnitOfWorkAttribute(method))
                {
                    handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(NhUnitOfWorkInterceptor)));
                    return;
                }
            }
        }
    }
}