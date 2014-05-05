using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Autofac;
using Caliburn.Micro;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TwitterDataBase;
using CweetTweetApp.ViewModels;

namespace CweetTweetApp
{
    public class AppBootstrapper : PhoneBootstrapper
    {
        private IContainer _container;

        protected override void Configure()
        {
            var builder = new ContainerBuilder();
            ConfigureContainer(builder);

            _container = builder.Build();

            AddCustomConventions();
        }

        protected void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterInstance(new FrameAdapter(RootFrame)).As<INavigationService>();
            builder.RegisterInstance(new PhoneApplicationServiceAdapter(PhoneApplicationService.Current, RootFrame)).As<IPhoneService>();

            builder.RegisterType<TwitterApi.TwitterApi>();
            builder.RegisterType<DataContextProvider>();

            builder.RegisterType<TwitterFeedViewModel>().PropertiesAutowired();
            builder.RegisterType<DatabaseViewModel>();
            builder.RegisterType<TweetDetailsViewModel>();

            builder.RegisterType<MainPageViewModel>();
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.Resolve(typeof(IEnumerable<>).MakeGenericType(service)) as IEnumerable<object>;
        }

        protected override void BuildUp(object instance)
        {
            _container.InjectProperties(instance);
        }

        protected override object GetInstance(Type service, string key)
        {
            object instance;
            if (string.IsNullOrEmpty(key))
            {
                if (_container.TryResolve(service, out instance))
                    return instance;
            }
            else
            {
                if (_container.TryResolveNamed(key, service, out instance))
                    return instance;
            }
            throw new Exception(string.Format("Could not locate any instances of contract {0}.", key ?? service.Name));
        }

        protected void AddCustomConventions()
        {
            Func<Type, string, PropertyInfo, FrameworkElement, ElementConvention, bool> applyBindingFunc =
                (viewModelType, path, property, element, convention) =>
                {
                    if (ConventionManager
                        .GetElementConvention(typeof(ItemsControl))
                        .ApplyBinding(viewModelType, path, property, element, convention))
                    {
                        ConventionManager
                            .ConfigureSelectedItem(element, Pivot.SelectedItemProperty, viewModelType, path);
                        ConventionManager
                            .ApplyHeaderTemplate(element, Pivot.HeaderTemplateProperty, null, viewModelType);
                        return true;
                    }

                    return false;
                };

            ConventionManager.AddElementConvention<Pivot>(ItemsControl.ItemsSourceProperty, "SelectedItem", "SelectionChanged")
                                         .ApplyBinding = applyBindingFunc;
        }

    }
}
