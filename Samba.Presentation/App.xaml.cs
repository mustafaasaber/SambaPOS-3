using CommonServiceLocator;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using Samba.Infrastructure.Messaging;
using Samba.Infrastructure.Settings;
using Samba.Localization.Engine;
using Samba.Modules.AccountModule;
using Samba.Modules.AccountModule.ActionProcessors;
using Samba.Modules.AccountModule.Dashboard;
using Samba.Modules.AutomationModule;
using Samba.Modules.AutomationModule.ActionProcessors;
using Samba.Modules.AutomationModule.WidgetCreators;
using Samba.Modules.BasicReports;
using Samba.Modules.BasicReports.ActionProcessors;
using Samba.Modules.CidMonitor;
using Samba.Modules.DepartmentModule;
using Samba.Modules.EntityModule;
using Samba.Modules.EntityModule.ActionProcessors;
using Samba.Modules.EntityModule.Widgets.EntityButton;
using Samba.Modules.EntityModule.Widgets.EntityGrid;
using Samba.Modules.EntityModule.Widgets.EntitySearch;
using Samba.Modules.InventoryModule;
using Samba.Modules.LoginModule;
using Samba.Modules.ManagementModule;
using Samba.Modules.MarketModule;
using Samba.Modules.MenuModule;
using Samba.Modules.NavigationModule;
using Samba.Modules.PaymentModule;
using Samba.Modules.PosModule;
using Samba.Modules.PosModule.ActionProcessors;
using Samba.Modules.PrinterModule;
using Samba.Modules.PrinterModule.ActionProcessors;
using Samba.Modules.SettingsModule;
using Samba.Modules.SettingsModule.ActionProcessors;
using Samba.Modules.SettingsModule.Widgets.HtmlViewer;
using Samba.Modules.TaskModule;
using Samba.Modules.TaskModule.Widgets.TaskEditor;
using Samba.Modules.TicketModule;
using Samba.Modules.TicketModule.ActionProcessors;
using Samba.Modules.TicketModule.Widgets.TicketExplorer;
using Samba.Modules.TicketModule.Widgets.TicketLister;
using Samba.Modules.UserModule;
using Samba.Modules.WorkperiodModule;
using Samba.Persistance;
using Samba.Persistance.Implementations;
using Samba.Presentation.Common;
using Samba.Presentation.Common.ActionProcessors;
using Samba.Presentation.Common.ErrorReport;
using Samba.Presentation.Common.ModelBase;
using Samba.Presentation.Common.Services;
using Samba.Presentation.Common.Widgets;
using Samba.Presentation.Controls.ActionProcessors;
using Samba.Presentation.Controls.Interaction;
using Samba.Presentation.Services;
using Samba.Presentation.Services.Common;
using Samba.Presentation.Services.Common.DataGeneration;
using Samba.Presentation.Services.Implementations;
using Samba.Presentation.Services.Implementations.AutomationModule;
using Samba.Presentation.Services.Implementations.EntityModule;
using Samba.Presentation.Services.Implementations.InventoryModule;
using Samba.Presentation.Services.Implementations.TicketModule;
using Samba.Presentation.Services.Implementations.UserModule;
using Samba.Presentation.Services.Implementations.WorkPeriodModule;
using Samba.Presentation.ViewModels;
using Samba.Services;
using Samba.Services.Common;
using Samba.Services.Implementations;
using Samba.Services.Implementations.AccountModule;
using Samba.Services.Implementations.AutomationModule;
using Samba.Services.Implementations.DepartmentModule;
using Samba.Services.Implementations.EntityModule;
using Samba.Services.Implementations.ExpressionModule;
using Samba.Services.Implementations.MenuModule;
using Samba.Services.Implementations.Messaging;
using Samba.Services.Implementations.PrinterModule;
using Samba.Services.Implementations.PrinterModule.ValueChangers;
using Samba.Services.Implementations.SettingsModule;
using Samba.Services.Implementations.TicketModule;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Samba.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //RunInReleaseMode();
            ServiceLocator.Current.GetInstance<IApplicationState>().NotifyEvent(RuleEventNames.ApplicationStarted, new { Arguments = string.Join(" ", e.Args) });
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

            containerRegistry.GetContainer().RegisterType<ApplicationState>(new ContainerControlledLifetimeManager());
            containerRegistry.GetContainer().RegisterType<IApplicationState>(new InjectionFactory(c => c.Resolve<ApplicationState>()));
            containerRegistry.GetContainer().RegisterType<IApplicationStateSetter>(new InjectionFactory(c => c.Resolve<ApplicationState>()));

            containerRegistry.RegisterSingleton<IMethodQueue, MethodQueue>();

            containerRegistry.RegisterSingleton<IDepartmentService, DepartmentService>();
            containerRegistry.RegisterSingleton<ICacheDao, CacheDao>();
            containerRegistry.RegisterSingleton<ISettingService, SettingService>();
            containerRegistry.RegisterSingleton<ISettingDao, SettingDao>();
            containerRegistry.RegisterSingleton<ICacheService, CacheService>();

            containerRegistry.RegisterSingleton<IPrinterDao, PrinterDao>();
            containerRegistry.RegisterSingleton<IAutomationDao, AutomationDao>();

            containerRegistry.RegisterSingleton<IExpressionService, ExpressionService>();

            containerRegistry.RegisterSingleton<IEntityService, EntityService>();
            containerRegistry.RegisterSingleton<IEntityDao, EntityDao>();
            containerRegistry.RegisterSingleton<INotificationService, NotificationService>();

            containerRegistry.RegisterSingleton<PreCalculationValueChanger>();
            containerRegistry.RegisterSingleton<PostCalculationValueChanger>();

            containerRegistry.RegisterSingleton<ISettingService, SettingService>();
            containerRegistry.RegisterSingleton<IEntityService, EntityService>();
            containerRegistry.RegisterSingleton<ILogService, LogService>();
            containerRegistry.RegisterSingleton<TicketEntityValueChanger>();
            containerRegistry.RegisterSingleton<TicketValueChanger>();
            containerRegistry.RegisterSingleton<TicketFormatter>();

            containerRegistry.RegisterSingleton<FunctionRegistry>();
            containerRegistry.RegisterSingleton<TicketPrintTaskBuilder>();

            containerRegistry.RegisterSingleton<IPrinterService, PrinterService>();

            containerRegistry.RegisterSingleton<IAccountDao, AccountDao>();

            containerRegistry.RegisterSingleton<IUserInteraction, UserInteraction>();
            containerRegistry.RegisterSingleton<IMessagingService, MessagingService>();
            containerRegistry.RegisterSingleton<ITriggerService, TriggerService>();
            containerRegistry.RegisterSingleton<IModuleInitializationService, ModuleInitializationService>();
            containerRegistry.RegisterSingleton<IUserService, UserService>();
            containerRegistry.RegisterSingleton<IUserDao, UserDao>();
            containerRegistry.RegisterSingleton<IAccountService, AccountService>();
            containerRegistry.RegisterSingleton<IReportServiceClient, ReportServiceClient>();
            containerRegistry.RegisterSingleton<IReportService, ReportService>();
            containerRegistry.RegisterSingleton<IWorkPeriodService, WorkPeriodService>();
            containerRegistry.RegisterSingleton<IWorkPeriodDao, WorkPeriodDao>();
            containerRegistry.RegisterSingleton<IInventoryService, InventoryService>();
            containerRegistry.RegisterSingleton<IWorkPeriodProcessor, InventoryWorkperiodProcessor>();
            containerRegistry.RegisterSingleton<IInventoryDao, InventoryDao>();
            containerRegistry.RegisterSingleton<IMenuService, MenuService>();
            containerRegistry.RegisterSingleton<IMenuDao, MenuDao>();
            containerRegistry.RegisterSingleton<IAutomationService, AutomationService>();
            containerRegistry.RegisterSingleton<IDeviceService, DeviceService>();
            containerRegistry.RegisterSingleton<ITicketServiceBase, TicketServiceBase>();
            containerRegistry.RegisterSingleton<ITicketDao, TicketDao>();

            containerRegistry.RegisterSingleton<ITicketService, TicketService>();
            containerRegistry.RegisterSingleton<IEntityServiceClient, EntityServiceClient>();

            containerRegistry.RegisterSingleton<IPriceListService, PriceListService>();

            containerRegistry.RegisterSingleton<IPriceListDao, PriceListDao>();







            containerRegistry.RegisterSingleton<EntitySelectorViewModel>();
            containerRegistry.RegisterSingleton<ManagementViewModel>();

            //Views
            containerRegistry.RegisterSingleton<LoginView>();
            containerRegistry.RegisterSingleton<NavigationView>();

            containerRegistry.RegisterSingleton<TicketExplorerView>();

            //////containerRegistry.RegisterSingleton<WarehouseInventoryViewModel>();
            //////containerRegistry.RegisterSingleton<TicketTotalsViewModel>();
            //////containerRegistry.RegisterSingleton<WarehouseInventoryView>();


            //Inventory
            //ViewModels
            containerRegistry.RegisterSingleton<TicketOrdersViewModel>();
            containerRegistry.RegisterSingleton<WarehouseInventoryViewModel>();
            containerRegistry.RegisterSingleton<TicketListViewModel>();
            containerRegistry.RegisterSingleton<TicketViewModel>();
            containerRegistry.RegisterSingleton<MenuItemSelectorViewModel>();
            containerRegistry.RegisterSingleton<TicketTypeListViewModel>();
            containerRegistry.RegisterSingleton<TicketTotalsViewModel>();
            containerRegistry.RegisterSingleton<TicketTagListViewModel>();
            containerRegistry.RegisterSingleton<TicketInfoViewModel>();
            containerRegistry.RegisterSingleton<TicketEntityListViewModel>();
            containerRegistry.RegisterSingleton<PosViewModel>();

            //Views
            containerRegistry.RegisterSingleton<TicketOrdersView>();
            containerRegistry.RegisterSingleton<WarehouseInventoryView>();
            containerRegistry.RegisterSingleton<TicketListView>();
            containerRegistry.RegisterSingleton<TicketView>();
            containerRegistry.RegisterSingleton<MenuItemSelectorView>();
            containerRegistry.RegisterSingleton<TicketTypeListView>();
            containerRegistry.RegisterSingleton<TicketTotalsView>();
            containerRegistry.RegisterSingleton<TicketTagListView>();
            containerRegistry.RegisterSingleton<TicketInfoView>();
            containerRegistry.RegisterSingleton<TicketEntityListView>();
            containerRegistry.RegisterSingleton<PosView>();

            //Payment 
            //////View

            containerRegistry.RegisterSingleton<PaymentEditor>();

            containerRegistry.RegisterSingleton<PaymentEditorView>();
            containerRegistry.RegisterSingleton<TenderedValueView>();
            containerRegistry.RegisterSingleton<ForeignCurrencyButtonsView>();
            containerRegistry.RegisterSingleton<ChangePaymentTypeView>();

            containerRegistry.RegisterSingleton<CommandButtonsView>();
            containerRegistry.RegisterSingleton<ForeignCurrencyButtonsView>();
            containerRegistry.RegisterSingleton<NumberPadView>();
            containerRegistry.RegisterSingleton<OrderSelectorView>();
            containerRegistry.RegisterSingleton<PaymentButtonsView>();
            containerRegistry.RegisterSingleton<ReturningAmountView>();


            //////ViewModel

            containerRegistry.RegisterSingleton<PaymentEditorViewModel>();
            containerRegistry.RegisterSingleton<TenderedValueViewModel>();
            containerRegistry.RegisterSingleton<ForeignCurrencyButtonsViewModel>();
            containerRegistry.RegisterSingleton<ChangePaymentTypeViewModel>();


            containerRegistry.RegisterSingleton<CommandButtonsViewModel>();
            containerRegistry.RegisterSingleton<ForeignCurrencyButtonsViewModel>();
            containerRegistry.RegisterSingleton<NumberPadViewModel>();
            containerRegistry.RegisterSingleton<OrderSelectorViewModel>();
            containerRegistry.RegisterSingleton<PaymentButtonsViewModel>();
            containerRegistry.RegisterSingleton<ReturningAmountViewModel>();

            //Entity Module
            containerRegistry.RegisterSingleton<EntitySwitcherView>();
            containerRegistry.RegisterSingleton<EntitySelectorView>();
            containerRegistry.RegisterSingleton<EntitySearchView>();

            //Mamgement Module
            containerRegistry.RegisterSingleton<ManagementView>();

            //WorkPeriod Module
            containerRegistry.RegisterSingleton<WorkPeriodsView>();

            // Market Module
            containerRegistry.RegisterSingleton<MarketModuleView>();

            //Account Module
            containerRegistry.RegisterSingleton<AccountSelectorView>();
            containerRegistry.RegisterSingleton<AccountView>();
            //services 
            containerRegistry.RegisterSingleton<RuleActionTypeRegistry>();
            containerRegistry.RegisterSingleton<RuleActionView>();
            containerRegistry.RegisterSingleton<IEmailService, EMailService>();

            //Reports
            containerRegistry.RegisterSingleton<BasicReportView>();
            containerRegistry.RegisterSingleton<AccountDetailsView>();

            //Widgets
            containerRegistry.RegisterSingleton<IWidgetCreator, AutomationButtonWidgetCreator>();
            containerRegistry.RegisterSingleton<IWidgetCreator, EntityButtonWidgetCreator>();
            containerRegistry.RegisterSingleton<IWidgetCreator, EntityGridWidgetCreator>();
            containerRegistry.RegisterSingleton<IWidgetCreator, EntitySearchWidgetCreator>();
            containerRegistry.RegisterSingleton<IWidgetCreator, HtmlViewerWidgetCreator>();
            containerRegistry.RegisterSingleton<IWidgetCreator, TaskEditorWidgetCreator>();
            containerRegistry.RegisterSingleton<IWidgetCreator, TicketExplorerWidgetCreator>();
            containerRegistry.RegisterSingleton<IWidgetCreator, TicketListerWidgetCreator>();
            



            // ActionType>
            containerRegistry.RegisterSingleton<IActionType, UpdateTicketState>();
            containerRegistry.RegisterSingleton<IActionType, DisplayPopup>(nameof(DisplayPopup));

            containerRegistry.RegisterSingleton<IActionType, CreateBatchAccountTransactionDocument>(nameof(CreateBatchAccountTransactionDocument));
            containerRegistry.RegisterSingleton<IActionType, PrintAccountScreen>(nameof(PrintAccountScreen));
            containerRegistry.RegisterSingleton<IActionType, PrintAccountTransactionDocument>(nameof(PrintAccountTransactionDocument));
            containerRegistry.RegisterSingleton<IActionType, PrintAccountTransactions>(nameof(PrintAccountTransactions));
            containerRegistry.RegisterSingleton<IActionType, ExecuteScript>(nameof(ExecuteScript));
            containerRegistry.RegisterSingleton<IActionType, SendMessage>(nameof(SendMessage));
            containerRegistry.RegisterSingleton<IActionType, PrintReport>(nameof(PrintReport));
            containerRegistry.RegisterSingleton<IActionType, SaveReportToFile>(nameof(SaveReportToFile));
            containerRegistry.RegisterSingleton<IActionType, CreateEntity>(nameof(CreateEntity));
            containerRegistry.RegisterSingleton<IActionType, PrintEntity>(nameof(PrintEntity));
            containerRegistry.RegisterSingleton<IActionType, UdpateEntityData>(nameof(UdpateEntityData));
            containerRegistry.RegisterSingleton<IActionType, UpdateEntityState>(nameof(UpdateEntityState));
            containerRegistry.RegisterSingleton<IActionType, DisplayTicketList>(nameof(DisplayTicketList));

            containerRegistry.RegisterSingleton<IActionType, DisplayTicketLog>(nameof(DisplayTicketLog));
            containerRegistry.RegisterSingleton<IActionType, ExecutePrintJob>(nameof(ExecutePrintJob));
            containerRegistry.RegisterSingleton<IActionType, SetCurrentTerminal>(nameof(SetCurrentTerminal));
            containerRegistry.RegisterSingleton<IActionType, UpdateProgramSetting>(nameof(UpdateProgramSetting));

            containerRegistry.RegisterSingleton<IActionType, AddOrder>(nameof(AddOrder));
            containerRegistry.RegisterSingleton<IActionType, AddTicketLog>(nameof(AddTicketLog));
            containerRegistry.RegisterSingleton<IActionType, ChangeTicketEntity>(nameof(ChangeTicketEntity));
            containerRegistry.RegisterSingleton<IActionType, CloseActiveTicket>(nameof(CloseActiveTicket));
            containerRegistry.RegisterSingleton<IActionType, CreateTicket>(nameof(CreateTicket));
            containerRegistry.RegisterSingleton<IActionType, LoadLastOrder>(nameof(LoadLastOrder));
            containerRegistry.RegisterSingleton<IActionType, LoadTicket>(nameof(LoadTicket));

            containerRegistry.RegisterSingleton<IActionType, LockTicket>(nameof(LockTicket));
            containerRegistry.RegisterSingleton<IActionType, MarkTicketAsClosed>(nameof(MarkTicketAsClosed));
            containerRegistry.RegisterSingleton<IActionType, MoveTaggedOrders>(nameof(MoveTaggedOrders));
            containerRegistry.RegisterSingleton<IActionType, PayTicket>(nameof(PayTicket));
            containerRegistry.RegisterSingleton<IActionType, SelectAutomationCommand>(nameof(SelectAutomationCommand));
            containerRegistry.RegisterSingleton<IActionType, StopActiveTimers>(nameof(StopActiveTimers));
            containerRegistry.RegisterSingleton<IActionType, TagOrder>(nameof(TagOrder));
            containerRegistry.RegisterSingleton<IActionType, UnlockTicket>(nameof(UnlockTicket));
            containerRegistry.RegisterSingleton<IActionType, UntagOrder>(nameof(UntagOrder));
            containerRegistry.RegisterSingleton<IActionType, UpdateOrder>(nameof(UpdateOrder));
            containerRegistry.RegisterSingleton<IActionType, UpdateOrderState>(nameof(UpdateOrderState));
            containerRegistry.RegisterSingleton<IActionType, UpdateTicketCalculation>(nameof(UpdateTicketCalculation));

            containerRegistry.RegisterSingleton<IActionType, UpdateTicketTag>(nameof(UpdateTicketTag));
            containerRegistry.RegisterSingleton<IActionType, AddLineToTextFile>(nameof(AddLineToTextFile));
            containerRegistry.RegisterSingleton<IActionType, ModifyVariable>(nameof(ModifyVariable));
            containerRegistry.RegisterSingleton<IActionType, RefreshCache>(nameof(RefreshCache));

            containerRegistry.RegisterSingleton<IActionType, SendEmail>(nameof(SendEmail));
            containerRegistry.RegisterSingleton<IActionType, SetActiveTicketType>(nameof(SetActiveTicketType));
            containerRegistry.RegisterSingleton<IActionType, SetWidgetValue>(nameof(SetWidgetValue));
            containerRegistry.RegisterSingleton<IActionType, StartProcess>(nameof(StartProcess));

            containerRegistry.RegisterSingleton<IActionType, UpdatePriceTag>(nameof(UpdatePriceTag));
            containerRegistry.RegisterSingleton<IActionType, StartProcess>(nameof(StartProcess));
            containerRegistry.RegisterSingleton<IActionType, StartProcess>(nameof(StartProcess));
            containerRegistry.RegisterSingleton<IActionType, StartProcess>(nameof(StartProcess));
            containerRegistry.RegisterSingleton<IActionType, StartProcess>(nameof(StartProcess));
            containerRegistry.RegisterSingleton<IActionType, StartProcess>(nameof(StartProcess));



            //Keyboard 
            containerRegistry.RegisterSingleton<Samba.Presentation.Controls.VirtualKeyboard.KeyboardView>();






            //Widgets

            containerRegistry.RegisterSingleton<IWidgetCreator, AutomationButtonWidgetCreator>();

         
            //
            containerRegistry.RegisterSingleton<DepartmentListViewModel>();
            containerRegistry.Register<DepartmentViewModel>();

            //Cid Devices

            containerRegistry.RegisterSingleton<IDevice, CidShowDevice>();
            containerRegistry.RegisterSingleton<IDevice, CometDevice>(nameof(CometDevice));
            containerRegistry.RegisterSingleton<IDevice, GenericModemDevice>(nameof(GenericModemDevice));
            containerRegistry.RegisterSingleton<IDevice, HuginCallerIdDevice>(nameof(HuginCallerIdDevice));


        }
        protected override Window CreateShell()
        {
            return Container.Resolve<Shell>();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            MessagingClient.Stop();
            ServiceLocator.Current.GetInstance<ITriggerService>().CloseTriggers();
        }



        private static void RunInReleaseMode()
        {
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
        }

        private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        private static void HandleException(Exception ex)
        {
            if (ex == null) return;
            ExceptionReporter.Show(ex);
            Environment.Exit(1);
        }



        static readonly Mutex Mutex = new Mutex(true, "{aa77c8c4-b8c1-4e61-908f-48c6fb65227d}");

        //protected override IModuleCatalog CreateModuleCatalog()
        //{
        //    var a = new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        //    var b = new ConfigurationModuleCatalog();

        //    return new ModuleCatalog(a.Modules.OfType<ModuleInfo>().Concat(b.Modules).OfType<ModuleInfo>());
        //}
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<AutomationModule>();

            moduleCatalog.AddModule<AccountModule>();
            moduleCatalog.AddModule<BasicReportModule>();
            moduleCatalog.AddModule<InventoryModule>();
            moduleCatalog.AddModule<LoginModule>();
            moduleCatalog.AddModule<ManagementModule>();
            moduleCatalog.AddModule<MarketModule>();
            moduleCatalog.AddModule<NavigationModule>();
            moduleCatalog.AddModule<PaymentModule>();
            moduleCatalog.AddModule<PosModule>();
            moduleCatalog.AddModule<TicketModule>();
            moduleCatalog.AddModule<WorkPeriodsModule>();
            moduleCatalog.AddModule<CidMonitor>();
            moduleCatalog.AddModule<DepartmentModule>();
            moduleCatalog.AddModule<EntityModule>();
            moduleCatalog.AddModule<MenuModule>();
            moduleCatalog.AddModule<PrinterModule>();
            moduleCatalog.AddModule<SettingsModule>();
            moduleCatalog.AddModule<TaskModule>();
            moduleCatalog.AddModule<UserModule>();
            moduleCatalog.AddModule<PresentationModule>();
        }

        //        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        //        {
        //            base.ConfigureModuleCatalog(moduleCatalog);


        //            var path = System.IO.Path.GetDirectoryName(Application.ResourceAssembly.Location);
        //            if (path != null)
        //            {
        //                //moduleCatalog.Catalogs.Add(new DirectoryCatalog(path, "Samba.Persistance.dll"));

        //                //AggregateCatalog.Catalogs.Add(new DirectoryCatalog(path, "Samba.Modules*"));
        //                //AggregateCatalog.Catalogs.Add(new DirectoryCatalog(path, "Samba.Presentation*"));
        //                //AggregateCatalog.Catalogs.Add(new DirectoryCatalog(path, "Samba.Services*"));

        //                moduleCatalog.AddGroup(InitializationMode.OnDemand, "Samba.Modules*");
        //                moduleCatalog.AddGroup(InitializationMode.OnDemand, "Samba.Presentation*");
        //                moduleCatalog.AddGroup(InitializationMode.OnDemand, "Samba.Services*");

        //                var moduleCType = typeof(LoginModule);
        //                moduleCatalog.AddModule(new ModuleInfo()
        //                {
        //                    ModuleName = moduleCType.Name,
        //                    ModuleType = moduleCType.AssemblyQualifiedName,
        //                    InitializationMode = InitializationMode.OnDemand,
        //                });

        //            }
        //            LocalSettings.AppPath = path;



        //{
        //    Type moduleCType = typeof(ModuleC);
        //    ModuleCatalog.AddModule(new ModuleInfo()
        //    {
        //        ModuleName = moduleCType.Name,
        //        ModuleType = moduleCType.AssemblyQualifiedName,
        //    });
        //}
        //Note: If your application has a direct reference to the module type, you can add it by type as shown above; otherwise you need to provide the fully qualified type name and the location of the assembly.

        //To specify dependencies in code, use Prism supplied declarative attributes.

        //[Module(ModuleName = "ModuleA")]
        //[ModuleDependency("ModuleD")]
        //public class ModuleA : IModule
        //{
        //    ...
        //}
        //To specify on-demand loading in code, add the InitializationMOde property to yur new instance of ModuleInfo. Using the code wee above:

        //Type moduleCType = typeof(ModuleC);
        //ModuleCatalog.AddModule(new ModuleInfo()
        //{
        //    ModuleName = moduleCType.Name,
        //    ModuleType = moduleCType.AssemblyQualifiedName,
        //    InitializationMode = InitializationMode.OnDemand,
        //});
        //             * 
        //             * 
        //             * 
        //             */








        //        }

        protected override void InitializeModules()
        {
            base.InitializeModules();
            var moduleInitializationService = ServiceLocator.Current.GetInstance<IModuleInitializationService>();
            moduleInitializationService.Initialize();
        }

        protected override void InitializeShell(Window shell)
        {
#if DEBUG
            // Bypass Singleton check
#else
            if (!Mutex.WaitOne(TimeSpan.Zero, true) && !LocalSettings.AllowMultipleClients)
            {
                NativeWin32.PostMessage((IntPtr)NativeWin32.HWND_BROADCAST, NativeWin32.WM_SHOWSAMBAPOS, IntPtr.Zero, IntPtr.Zero);
                Environment.Exit(1);
            }
#endif
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            System.Net.ServicePointManager.Expect100Continue = false;

            LocalizeDictionary.ChangeLanguage(LocalSettings.CurrentLanguage);
            InteractionService.UserIntraction = ServiceLocator.Current.GetInstance<IUserInteraction>();
            InteractionService.UserIntraction.ToggleSplashScreen();

            ServiceLocator.Current.GetInstance<IApplicationState>().MainDispatcher = Application.Current.Dispatcher;
            var logger = ServiceLocator.Current.GetInstance<ILogService>();

            var messagingService = ServiceLocator.Current.GetInstance<IMessagingService>();
            messagingService.RegisterMessageListener(new MessageListener());

            if (LocalSettings.StartMessagingClient)
                messagingService.StartMessagingClient();

            PresentationServices.Initialize();

            base.InitializeShell(shell);

            try
            {
                var creationService = new DataCreationService();
                creationService.CreateData();
            }
            catch (Exception e)
            {
                if (!string.IsNullOrEmpty(LocalSettings.ConnectionString))
                {
                    var connectionString =
                        InteractionService.UserIntraction.GetStringFromUser(
                        "Connection String",
                        string.Format(Samba.Presentation.Properties.Resources.ConnectionStringError, e.Message),
                        LocalSettings.ConnectionString);



                    var cs = String.Join(" ", connectionString);

                    if (!string.IsNullOrEmpty(cs))
                        LocalSettings.ConnectionString = cs.Trim();

                    logger.LogError(e, Samba.Presentation.Properties.Resources.RestartAppError);
                }
                else
                {
                    logger.LogError(e);
                    LocalSettings.ConnectionString = "";
                }
                LocalSettings.SaveSettings();
                Environment.Exit(1);
            }

            var rm = Container.Resolve<IRegionManager>();
            rm.RegisterViewWithRegion("MessageRegion", typeof(WorkPeriodStatusView));
            rm.RegisterViewWithRegion("MessageRegion", typeof(MessageClientStatusView));
            //rm.RegisterViewWithRegion("KeyBoardViewRegion", typeof(Samba.Presentation.Controls.VirtualKeyboard.KeyboardView));

            Application.Current.MainWindow = (Shell)shell;

            if (LocalizeDictionary.Instance.Culture.TextInfo.IsRightToLeft)
            {
                Application.Current.MainWindow.FlowDirection = FlowDirection.RightToLeft;
            }

            ServiceLocator.Current.GetInstance<ITriggerService>().UpdateCronObjects();

            InteractionService.UserIntraction.ToggleSplashScreen();
            EntityCollectionSortManager.Load(LocalSettings.DocumentPath + "\\CollectionSort.txt");

            if (!string.IsNullOrEmpty(LocalSettings.CallerIdDeviceName))
            {
                ServiceLocator.Current.GetInstance<IDeviceService>().InitializeDevice(LocalSettings.CallerIdDeviceName);
            }

            Application.Current.MainWindow.Show();
            EventServiceFactory.EventService.PublishEvent(EventTopicNames.ShellInitialized);





            Mouse.UpdateCursor();
        }




    }
}
