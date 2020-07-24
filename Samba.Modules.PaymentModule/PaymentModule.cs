using System.ComponentModel.Composition;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Samba.Domain.Models.Tickets;
using Samba.Modules.PaymentModule.ActionProcessors;
using Samba.Presentation.Common;
using Samba.Presentation.Services.Common;
using Samba.Services.Common;

namespace Samba.Modules.PaymentModule
{
    [Module(ModuleName = "PaymentModule")]
  public  class PaymentModule : VisibleModuleBase
    {
        private readonly IRegionManager _regionManager;
        private readonly PaymentEditorView _paymentEditorView;
        private readonly TenderedValueView _tenderedValueView;
        private readonly ReturningAmountView _returningAmountView;
        private readonly ChangeTemplatesView _changeTemplatesView;

        
        public PaymentModule(IRegionManager regionManager, PaymentEditorView paymentEditorView, TenderedValueView tenderedValueView,
            ReturningAmountView returningAmountView, NumberPadViewModel numberPadViewModel, ChangeTemplatesView changeTemplatesView)
            : base(regionManager, AppScreens.PaymentView)
        {
            _regionManager = regionManager;
            _paymentEditorView = paymentEditorView;
            _tenderedValueView = tenderedValueView;
            _returningAmountView = returningAmountView;
            _changeTemplatesView = changeTemplatesView;
            numberPadViewModel.TypedValueChanged += NumberPadViewModelTypedValueChanged;
        }

        void NumberPadViewModelTypedValueChanged(object sender, System.EventArgs e)
        {
            ActivateTenderedAmount();
        }

        public override object GetVisibleView()
        {
            return _paymentEditorView;
        }

        protected override void OnInitialization()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(PaymentEditorView));
            _regionManager.RegisterViewWithRegion(RegionNames.PaymentNumberPadRegion, typeof(NumberPadView));
            _regionManager.RegisterViewWithRegion(RegionNames.PaymentOrderSelectorRegion, typeof(OrderSelectorView));
            _regionManager.RegisterViewWithRegion(RegionNames.PaymentForeignCurrencyRegion, typeof(ForeignCurrencyButtonsView));
            _regionManager.RegisterViewWithRegion(RegionNames.PaymentButtonsRegion, typeof(PaymentButtonsView));
            _regionManager.RegisterViewWithRegion(RegionNames.PaymentCommandButtonsRegion, typeof(CommandButtonsView));
            _regionManager.RegisterViewWithRegion(RegionNames.PaymentTotalsRegion, typeof(PaymentTotalsView));
            _regionManager.RegisterViewWithRegion(RegionNames.PaymentTenderedValueRegion, typeof(TenderedValueView));
            _regionManager.RegisterViewWithRegion(RegionNames.PaymentTenderedValueRegion, typeof(ReturningAmountView));
            _regionManager.RegisterViewWithRegion(RegionNames.PaymentTenderedValueRegion, typeof(ChangeTemplatesView));

            EventServiceFactory.EventService.GetEvent<GenericEvent<ReturningAmountViewModel>>().Subscribe(
                x =>
                {
                    if (x.Topic == EventTopicNames.Activate)
                        ActivateReturningAmount();
                });

            EventServiceFactory.EventService.GetEvent<GenericEvent<ChangeTemplatesViewModel>>().Subscribe(
                x =>
                {
                    if (x.Topic == EventTopicNames.Activate)
                        ActivateChangeTemplates();
                });

            EventServiceFactory.EventService.GetEvent<GenericEvent<Ticket>>().Subscribe(
             x =>
             {
                 if (x.Topic == EventTopicNames.MakePayment)
                 {
                     ((PaymentEditorViewModel)_paymentEditorView.DataContext).Prepare(x.Value);
                     ActivateTenderedAmount();
                     Activate();
                 }
             });
        }

        public void ActivateReturningAmount()
        {
            _regionManager.ActivateRegion(RegionNames.PaymentTenderedValueRegion, _returningAmountView);
        }

        public void ActivateTenderedAmount()
        {
            _regionManager.ActivateRegion(RegionNames.PaymentTenderedValueRegion, _tenderedValueView);
        }

        public void ActivateChangeTemplates()
        {
            _regionManager.ActivateRegion(RegionNames.PaymentTenderedValueRegion, _changeTemplatesView);
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IActionType, DisplayPaymentScreen>(nameof(DisplayPaymentScreen));

        }
    }
}
