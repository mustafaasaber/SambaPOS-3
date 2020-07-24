namespace Samba.Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialStart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountTypeId = c.Int(nullable: false),
                        ForeignCurrencyId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AccountScreens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Filter = c.Int(nullable: false),
                        DisplayAsTree = c.Boolean(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        AutomationCommandMapData = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AccountScreenValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountScreenId = c.Int(nullable: false),
                        AccountTypeId = c.Int(nullable: false),
                        AccountTypeName = c.String(),
                        DisplayDetails = c.Boolean(nullable: false),
                        HideZeroBalanceAccounts = c.Boolean(nullable: false),
                        SortOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.AccountScreenId })
                .ForeignKey("dbo.AccountScreens", t => t.AccountScreenId, cascadeDelete: true)
                .Index(t => t.AccountScreenId);
            
            CreateTable(
                "dbo.AccountTransactionDocuments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        DocumentTypeId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AccountTransactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountTransactionDocumentId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 16, scale: 2),
                        ExchangeRate = c.Decimal(nullable: false, precision: 16, scale: 2),
                        AccountTransactionTypeId = c.Int(nullable: false),
                        SourceAccountTypeId = c.Int(nullable: false),
                        TargetAccountTypeId = c.Int(nullable: false),
                        IsReversed = c.Boolean(nullable: false),
                        Reversable = c.Boolean(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.AccountTransactionDocumentId })
                .ForeignKey("dbo.AccountTransactionDocuments", t => t.AccountTransactionDocumentId, cascadeDelete: true)
                .Index(t => t.AccountTransactionDocumentId);
            
            CreateTable(
                "dbo.AccountTransactionValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountTransactionId = c.Int(nullable: false),
                        AccountTransactionDocumentId = c.Int(nullable: false),
                        AccountTypeId = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Debit = c.Decimal(nullable: false, precision: 16, scale: 2),
                        Credit = c.Decimal(nullable: false, precision: 16, scale: 2),
                        Exchange = c.Decimal(nullable: false, precision: 16, scale: 2),
                        Name = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.AccountTransactionId, t.AccountTransactionDocumentId })
                .ForeignKey("dbo.AccountTransactions", t => new { t.AccountTransactionId, t.AccountTransactionDocumentId }, cascadeDelete: true)
                .Index(t => new { t.AccountTransactionId, t.AccountTransactionDocumentId });
            
            CreateTable(
                "dbo.AccountTransactionDocumentTypeMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountTransactionDocumentTypeId = c.Int(nullable: false),
                        TerminalId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        UserRoleId = c.Int(nullable: false),
                        TicketTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountTransactionDocumentTypes", t => t.AccountTransactionDocumentTypeId, cascadeDelete: true)
                .Index(t => t.AccountTransactionDocumentTypeId);
            
            CreateTable(
                "dbo.AccountTransactionDocumentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ButtonHeader = c.String(),
                        ButtonColor = c.String(),
                        MasterAccountTypeId = c.Int(nullable: false),
                        DefaultAmount = c.String(),
                        DescriptionTemplate = c.String(),
                        ExchangeTemplate = c.String(),
                        BatchCreateDocuments = c.Boolean(nullable: false),
                        Filter = c.Int(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        PrinterTemplateId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AccountTransactionDocumentAccountMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountTransactionDocumentTypeId = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                        AccountName = c.String(),
                        MappedAccountId = c.Int(nullable: false),
                        MappedAccountName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountTransactionDocumentTypes", t => t.AccountTransactionDocumentTypeId, cascadeDelete: true)
                .Index(t => t.AccountTransactionDocumentTypeId);
            
            CreateTable(
                "dbo.AccountTransactionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        SourceAccountTypeId = c.Int(nullable: false),
                        TargetAccountTypeId = c.Int(nullable: false),
                        DefaultSourceAccountId = c.Int(nullable: false),
                        DefaultTargetAccountId = c.Int(nullable: false),
                        ForeignCurrencyId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AccountTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DefaultFilterType = c.Int(nullable: false),
                        WorkingRule = c.Int(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        Tags = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ActionContainers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppActionId = c.Int(nullable: false),
                        AppRuleId = c.Int(nullable: false),
                        Name = c.String(),
                        ParameterValues = c.String(),
                        CustomConstraint = c.String(),
                        SortOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppRules", t => t.AppRuleId, cascadeDelete: true)
                .Index(t => t.AppRuleId);
            
            CreateTable(
                "dbo.AutomationCommandMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AutomationCommandId = c.Int(nullable: false),
                        DisplayOnTicket = c.Boolean(nullable: false),
                        DisplayOnPayment = c.Boolean(nullable: false),
                        DisplayOnOrders = c.Boolean(nullable: false),
                        DisplayOnTicketList = c.Boolean(nullable: false),
                        DisplayUnderTicket = c.Boolean(nullable: false),
                        DisplayUnderTicket2 = c.Boolean(nullable: false),
                        DisplayOnCommandSelector = c.Boolean(nullable: false),
                        EnabledStates = c.String(),
                        VisibleStates = c.String(),
                        TerminalId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        UserRoleId = c.Int(nullable: false),
                        TicketTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AutomationCommands", t => t.AutomationCommandId, cascadeDelete: true)
                .Index(t => t.AutomationCommandId);
            
            CreateTable(
                "dbo.AutomationCommands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ButtonHeader = c.String(),
                        Color = c.String(),
                        FontSize = c.Int(nullable: false),
                        Values = c.String(),
                        ToggleValues = c.Boolean(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Calculations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketId = c.Int(nullable: false),
                        Name = c.String(),
                        Order = c.Int(nullable: false),
                        CalculationTypeId = c.Int(nullable: false),
                        AccountTransactionTypeId = c.Int(nullable: false),
                        CalculationType = c.Int(nullable: false),
                        IncludeTax = c.Boolean(nullable: false),
                        DecreaseAmount = c.Boolean(nullable: false),
                        UsePlainSum = c.Boolean(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 16, scale: 2),
                        CalculationAmount = c.Decimal(nullable: false, precision: 16, scale: 2),
                    })
                .PrimaryKey(t => new { t.Id, t.TicketId })
                .ForeignKey("dbo.Tickets", t => t.TicketId, cascadeDelete: true)
                .Index(t => t.TicketId);
            
            CreateTable(
                "dbo.CalculationSelectors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ButtonHeader = c.String(),
                        ButtonColor = c.String(),
                        FontSize = c.Int(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CalculationSelectorMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CalculationSelectorId = c.Int(nullable: false),
                        TerminalId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        UserRoleId = c.Int(nullable: false),
                        TicketTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CalculationSelectors", t => t.CalculationSelectorId, cascadeDelete: true)
                .Index(t => t.CalculationSelectorId);
            
            CreateTable(
                "dbo.CalculationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        CalculationMethod = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 16, scale: 2),
                        MaxAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IncludeTax = c.Boolean(nullable: false),
                        DecreaseAmount = c.Boolean(nullable: false),
                        UsePlainSum = c.Boolean(nullable: false),
                        ToggleCalculation = c.Boolean(nullable: false),
                        Name = c.String(),
                        AccountTransactionType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountTransactionTypes", t => t.AccountTransactionType_Id)
                .Index(t => t.AccountTransactionType_Id);
            
            CreateTable(
                "dbo.ChangePayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketId = c.Int(nullable: false),
                        ChangePaymentTypeId = c.Int(nullable: false),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        AccountTransactionId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.Int(nullable: false),
                        AccountTransaction_Id = c.Int(),
                        AccountTransaction_AccountTransactionDocumentId = c.Int(),
                    })
                .PrimaryKey(t => new { t.Id, t.TicketId })
                .ForeignKey("dbo.AccountTransactions", t => new { t.AccountTransaction_Id, t.AccountTransaction_AccountTransactionDocumentId })
                .ForeignKey("dbo.Tickets", t => t.TicketId, cascadeDelete: true)
                .Index(t => t.TicketId)
                .Index(t => new { t.AccountTransaction_Id, t.AccountTransaction_AccountTransactionDocumentId });
            
            CreateTable(
                "dbo.ChangePaymentTypeMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChangePaymentTypeId = c.Int(nullable: false),
                        TerminalId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        UserRoleId = c.Int(nullable: false),
                        TicketTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangePaymentTypes", t => t.ChangePaymentTypeId, cascadeDelete: true)
                .Index(t => t.ChangePaymentTypeId);
            
            CreateTable(
                "dbo.ChangePaymentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        Name = c.String(),
                        Account_Id = c.Int(),
                        AccountTransactionType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .ForeignKey("dbo.AccountTransactionTypes", t => t.AccountTransactionType_Id)
                .Index(t => t.Account_Id)
                .Index(t => t.AccountTransactionType_Id);
            
            CreateTable(
                "dbo.CostItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WarehouseConsumptionId = c.Int(nullable: false),
                        PeriodicConsumptionId = c.Int(nullable: false),
                        MenuItemId = c.Int(nullable: false),
                        PortionId = c.Int(nullable: false),
                        PortionName = c.String(),
                        Quantity = c.Decimal(nullable: false, precision: 16, scale: 3),
                        CostPrediction = c.Decimal(nullable: false, precision: 16, scale: 2),
                        Cost = c.Decimal(nullable: false, precision: 16, scale: 2),
                        Name = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.WarehouseConsumptionId, t.PeriodicConsumptionId })
                .ForeignKey("dbo.WarehouseConsumptions", t => new { t.PeriodicConsumptionId, t.WarehouseConsumptionId }, cascadeDelete: true)
                .Index(t => new { t.PeriodicConsumptionId, t.WarehouseConsumptionId });
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        PriceTag = c.String(maxLength: 10),
                        WarehouseId = c.Int(nullable: false),
                        TicketTypeId = c.Int(nullable: false),
                        ScreenMenuId = c.Int(nullable: false),
                        TicketCreationMethod = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Entities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityTypeId = c.Int(nullable: false),
                        LastUpdateTime = c.DateTime(nullable: false),
                        SearchString = c.String(),
                        CustomData = c.String(),
                        AccountId = c.Int(nullable: false),
                        WarehouseId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EntityCustomFields",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FieldType = c.Int(nullable: false),
                        EditingFormat = c.String(),
                        ValueSource = c.String(),
                        Hidden = c.Boolean(nullable: false),
                        Name = c.String(),
                        EntityType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EntityTypes", t => t.EntityType_Id)
                .Index(t => t.EntityType_Id);
            
            CreateTable(
                "dbo.EntityScreenItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityScreenId = c.Int(nullable: false),
                        Name = c.String(),
                        EntityId = c.Int(nullable: false),
                        EntityState = c.String(),
                        SortOrder = c.Int(nullable: false),
                        LastUpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.EntityScreenId })
                .ForeignKey("dbo.EntityScreens", t => t.EntityScreenId, cascadeDelete: true)
                .Index(t => t.EntityScreenId);
            
            CreateTable(
                "dbo.EntityScreens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketTypeId = c.Int(nullable: false),
                        EntityTypeId = c.Int(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        DisplayMode = c.Int(nullable: false),
                        BackgroundColor = c.String(),
                        BackgroundImage = c.String(),
                        FontSize = c.Int(nullable: false),
                        PageCount = c.Int(nullable: false),
                        RowCount = c.Int(nullable: false),
                        ColumnCount = c.Int(nullable: false),
                        ButtonHeight = c.Int(nullable: false),
                        DisplayState = c.String(),
                        StateFilter = c.String(),
                        AskTicketType = c.Boolean(nullable: false),
                        SearchValueReplacePattern = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EntityScreenMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityScreenId = c.Int(nullable: false),
                        TerminalId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        UserRoleId = c.Int(nullable: false),
                        TicketTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EntityScreens", t => t.EntityScreenId, cascadeDelete: true)
                .Index(t => t.EntityScreenId);
            
            CreateTable(
                "dbo.Widgets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EntityScreenId = c.Int(nullable: false),
                        XLocation = c.Int(nullable: false),
                        YLocation = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        Width = c.Int(nullable: false),
                        CornerRadius = c.Int(nullable: false),
                        Angle = c.Double(nullable: false),
                        Scale = c.Double(nullable: false),
                        Properties = c.String(),
                        CreatorName = c.String(),
                        AutoRefresh = c.Boolean(nullable: false),
                        AutoRefreshInterval = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EntityScreens", t => t.EntityScreenId, cascadeDelete: true)
                .Index(t => t.EntityScreenId);
            
            CreateTable(
                "dbo.EntityStateValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityId = c.Int(nullable: false),
                        EntityStates = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EntityTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        EntityName = c.String(),
                        AccountTypeId = c.Int(nullable: false),
                        WarehouseTypeId = c.Int(nullable: false),
                        AccountNameTemplate = c.String(),
                        PrimaryFieldName = c.String(),
                        PrimaryFieldFormat = c.String(),
                        DisplayFormat = c.String(),
                        AccountBalanceDisplayFormat = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ForeignCurrencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CurrencySymbol = c.String(),
                        ExchangeRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rounding = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InventoryItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupCode = c.String(),
                        BaseUnit = c.String(),
                        TransactionUnit = c.String(),
                        TransactionUnitMultiplier = c.Int(nullable: false),
                        Warehouse = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InventoryTransactionDocumentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SourceEntityTypeId = c.Int(nullable: false),
                        TargetEntityTypeId = c.Int(nullable: false),
                        DefaultSourceEntityId = c.Int(nullable: false),
                        DefaultTargetEntityId = c.Int(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        Name = c.String(),
                        AccountTransactionType_Id = c.Int(),
                        InventoryTransactionType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountTransactionTypes", t => t.AccountTransactionType_Id)
                .ForeignKey("dbo.InventoryTransactionTypes", t => t.InventoryTransactionType_Id)
                .Index(t => t.AccountTransactionType_Id)
                .Index(t => t.InventoryTransactionType_Id);
            
            CreateTable(
                "dbo.InventoryTransactionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SourceWarehouseTypeId = c.Int(nullable: false),
                        TargetWarehouseTypeId = c.Int(nullable: false),
                        DefaultSourceWarehouseId = c.Int(nullable: false),
                        DefaultTargetWarehouseId = c.Int(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InventoryTransactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InventoryTransactionDocumentId = c.Int(nullable: false),
                        InventoryTransactionTypeId = c.Int(nullable: false),
                        SourceWarehouseId = c.Int(nullable: false),
                        TargetWarehouseId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Unit = c.String(),
                        Multiplier = c.Int(nullable: false),
                        Quantity = c.Decimal(nullable: false, precision: 16, scale: 3),
                        Price = c.Decimal(nullable: false, precision: 16, scale: 2),
                        InventoryItem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InventoryItems", t => t.InventoryItem_Id)
                .ForeignKey("dbo.InventoryTransactionDocuments", t => t.InventoryTransactionDocumentId, cascadeDelete: true)
                .Index(t => t.InventoryTransactionDocumentId)
                .Index(t => t.InventoryItem_Id);
            
            CreateTable(
                "dbo.InventoryTransactionDocuments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MenuItemPortions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MenuItemId = c.Int(nullable: false),
                        Multiplier = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuItems", t => t.MenuItemId, cascadeDelete: true)
                .Index(t => t.MenuItemId);
            
            CreateTable(
                "dbo.MenuItemPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuItemPortionId = c.Int(nullable: false),
                        PriceTag = c.String(maxLength: 10),
                        Price = c.Decimal(nullable: false, precision: 16, scale: 2),
                    })
                .PrimaryKey(t => new { t.Id, t.MenuItemPortionId })
                .ForeignKey("dbo.MenuItemPortions", t => t.MenuItemPortionId, cascadeDelete: true)
                .Index(t => t.MenuItemPortionId);
            
            CreateTable(
                "dbo.MenuItemPriceDefinitions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PriceTag = c.String(maxLength: 10),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MenuItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupCode = c.String(),
                        Barcode = c.String(),
                        Tag = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Numerators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastUpdateTime = c.Binary(fixedLength: true, storeType: "timestamp"),
                        Number = c.Int(nullable: false),
                        NumberFormat = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketId = c.Int(nullable: false),
                        WarehouseId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        MenuItemId = c.Int(nullable: false),
                        MenuItemName = c.String(),
                        PortionName = c.String(),
                        Price = c.Decimal(nullable: false, precision: 16, scale: 2),
                        Quantity = c.Decimal(nullable: false, precision: 16, scale: 3),
                        PortionCount = c.Int(nullable: false),
                        Locked = c.Boolean(nullable: false),
                        CalculatePrice = c.Boolean(nullable: false),
                        DecreaseInventory = c.Boolean(nullable: false),
                        IncreaseInventory = c.Boolean(nullable: false),
                        OrderNumber = c.Int(nullable: false),
                        CreatingUserName = c.String(),
                        CreatedDateTime = c.DateTime(nullable: false),
                        AccountTransactionTypeId = c.Int(nullable: false),
                        ProductTimerValueId = c.Int(),
                        PriceTag = c.String(),
                        Tag = c.String(),
                        Taxes = c.String(),
                        OrderTags = c.String(),
                        OrderStates = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.TicketId })
                .ForeignKey("dbo.ProductTimerValues", t => t.ProductTimerValueId)
                .ForeignKey("dbo.Tickets", t => t.TicketId, cascadeDelete: true)
                .Index(t => t.TicketId)
                .Index(t => t.ProductTimerValueId);
            
            CreateTable(
                "dbo.ProductTimerValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductTimerId = c.Int(nullable: false),
                        PriceType = c.Int(nullable: false),
                        PriceDuration = c.Decimal(nullable: false, precision: 16, scale: 2),
                        MinTime = c.Decimal(nullable: false, precision: 16, scale: 2),
                        TimeRounding = c.Decimal(nullable: false, precision: 16, scale: 2),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderTagGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        ColumnCount = c.Int(nullable: false),
                        ButtonHeight = c.Int(nullable: false),
                        FontSize = c.Int(nullable: false),
                        ButtonColor = c.String(),
                        MaxSelectedItems = c.Int(nullable: false),
                        MinSelectedItems = c.Int(nullable: false),
                        AddTagPriceToOrderPrice = c.Boolean(nullable: false),
                        FreeTagging = c.Boolean(nullable: false),
                        SaveFreeTags = c.Boolean(nullable: false),
                        GroupTag = c.String(),
                        TaxFree = c.Boolean(nullable: false),
                        Hidden = c.Boolean(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderTagMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderTagGroupId = c.Int(nullable: false),
                        MenuItemGroupCode = c.String(),
                        MenuItemId = c.Int(nullable: false),
                        TerminalId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        UserRoleId = c.Int(nullable: false),
                        TicketTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderTagGroups", t => t.OrderTagGroupId, cascadeDelete: true)
                .Index(t => t.OrderTagGroupId);
            
            CreateTable(
                "dbo.OrderTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SortOrder = c.Int(nullable: false),
                        OrderTagGroupId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 16, scale: 2),
                        MenuItemId = c.Int(nullable: false),
                        MaxQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderTagGroups", t => t.OrderTagGroupId, cascadeDelete: true)
                .Index(t => t.OrderTagGroupId);
            
            CreateTable(
                "dbo.PaidItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketId = c.Int(nullable: false),
                        Key = c.String(),
                        Quantity = c.Decimal(nullable: false, precision: 16, scale: 3),
                    })
                .PrimaryKey(t => new { t.Id, t.TicketId })
                .ForeignKey("dbo.Tickets", t => t.TicketId, cascadeDelete: true)
                .Index(t => t.TicketId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketId = c.Int(nullable: false),
                        PaymentTypeId = c.Int(nullable: false),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        AccountTransactionId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 16, scale: 2),
                        UserId = c.Int(nullable: false),
                        AccountTransaction_Id = c.Int(),
                        AccountTransaction_AccountTransactionDocumentId = c.Int(),
                    })
                .PrimaryKey(t => new { t.Id, t.TicketId })
                .ForeignKey("dbo.AccountTransactions", t => new { t.AccountTransaction_Id, t.AccountTransaction_AccountTransactionDocumentId })
                .ForeignKey("dbo.Tickets", t => t.TicketId, cascadeDelete: true)
                .Index(t => t.TicketId)
                .Index(t => new { t.AccountTransaction_Id, t.AccountTransaction_AccountTransactionDocumentId });
            
            CreateTable(
                "dbo.PaymentTypeMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaymentTypeId = c.Int(nullable: false),
                        TerminalId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        UserRoleId = c.Int(nullable: false),
                        TicketTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PaymentTypes", t => t.PaymentTypeId, cascadeDelete: true)
                .Index(t => t.PaymentTypeId);
            
            CreateTable(
                "dbo.PaymentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        ButtonColor = c.String(),
                        FontSize = c.Int(nullable: false),
                        Name = c.String(),
                        Account_Id = c.Int(),
                        AccountTransactionType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .ForeignKey("dbo.AccountTransactionTypes", t => t.AccountTransactionType_Id)
                .Index(t => t.Account_Id)
                .Index(t => t.AccountTransactionType_Id);
            
            CreateTable(
                "dbo.PeriodicConsumptionItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WarehouseConsumptionId = c.Int(nullable: false),
                        PeriodicConsumptionId = c.Int(nullable: false),
                        InventoryItemId = c.Int(nullable: false),
                        InventoryItemName = c.String(),
                        UnitName = c.String(),
                        UnitMultiplier = c.Decimal(nullable: false, precision: 16, scale: 2),
                        InStock = c.Decimal(nullable: false, precision: 16, scale: 3),
                        Added = c.Decimal(nullable: false, precision: 16, scale: 3),
                        Removed = c.Decimal(nullable: false, precision: 16, scale: 3),
                        Consumption = c.Decimal(nullable: false, precision: 16, scale: 3),
                        PhysicalInventory = c.Decimal(precision: 16, scale: 3),
                        Cost = c.Decimal(nullable: false, precision: 16, scale: 2),
                    })
                .PrimaryKey(t => new { t.Id, t.WarehouseConsumptionId, t.PeriodicConsumptionId })
                .ForeignKey("dbo.WarehouseConsumptions", t => new { t.PeriodicConsumptionId, t.WarehouseConsumptionId }, cascadeDelete: true)
                .Index(t => new { t.PeriodicConsumptionId, t.WarehouseConsumptionId });
            
            CreateTable(
                "dbo.PeriodicConsumptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkPeriodId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        LastUpdateTime = c.DateTime(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WarehouseConsumptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PeriodicConsumptionId = c.Int(nullable: false),
                        WarehouseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.PeriodicConsumptionId })
                .ForeignKey("dbo.PeriodicConsumptions", t => t.PeriodicConsumptionId, cascadeDelete: true)
                .Index(t => t.PeriodicConsumptionId);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        UserRoleId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserRoles", t => t.UserRoleId, cascadeDelete: true)
                .Index(t => t.UserRoleId);
            
            CreateTable(
                "dbo.PrinterMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrintJobId = c.Int(nullable: false),
                        MenuItemGroupCode = c.String(),
                        MenuItemId = c.Int(nullable: false),
                        PrinterId = c.Int(nullable: false),
                        PrinterTemplateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PrintJobs", t => t.PrintJobId, cascadeDelete: true)
                .Index(t => t.PrintJobId);
            
            CreateTable(
                "dbo.Printers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShareName = c.String(),
                        PrinterType = c.Int(nullable: false),
                        CodePage = c.Int(nullable: false),
                        CharsPerLine = c.Int(nullable: false),
                        PageHeight = c.Int(nullable: false),
                        CustomPrinterName = c.String(),
                        CustomPrinterData = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PrinterTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Template = c.String(),
                        MergeLines = c.Boolean(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PrintJobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WhatToPrint = c.Int(nullable: false),
                        UseForPaidTickets = c.Boolean(nullable: false),
                        ExcludeTax = c.Boolean(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductTimers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PriceType = c.Int(nullable: false),
                        PriceDuration = c.Decimal(nullable: false, precision: 16, scale: 2),
                        MinTime = c.Decimal(nullable: false, precision: 16, scale: 2),
                        TimeRounding = c.Decimal(nullable: false, precision: 16, scale: 2),
                        StartTime = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProdcutTimerMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductTimerId = c.Int(nullable: false),
                        MenuItemGroupCode = c.String(),
                        MenuItemId = c.Int(nullable: false),
                        TerminalId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        UserRoleId = c.Int(nullable: false),
                        TicketTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductTimers", t => t.ProductTimerId, cascadeDelete: true)
                .Index(t => t.ProductTimerId);
            
            CreateTable(
                "dbo.ProgramSettingValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 250),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RecipeItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecipeId = c.Int(nullable: false),
                        Quantity = c.Decimal(nullable: false, precision: 16, scale: 3),
                        InventoryItem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InventoryItems", t => t.InventoryItem_Id)
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId)
                .Index(t => t.InventoryItem_Id);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FixedCost = c.Decimal(nullable: false, precision: 16, scale: 2),
                        Name = c.String(),
                        Portion_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuItemPortions", t => t.Portion_Id)
                .Index(t => t.Portion_Id);
            
            CreateTable(
                "dbo.AppActions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActionType = c.String(),
                        Parameter = c.String(),
                        SortOrder = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppRules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventName = c.String(),
                        EventConstraints = c.String(),
                        CustomConstraint = c.String(),
                        RuleConstraints = c.String(),
                        ConstraintMatch = c.Int(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppRuleMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppRuleId = c.Int(nullable: false),
                        TerminalId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        UserRoleId = c.Int(nullable: false),
                        TicketTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppRules", t => t.AppRuleId, cascadeDelete: true)
                .Index(t => t.AppRuleId);
            
            CreateTable(
                "dbo.ScreenMenuCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SortOrder = c.Int(nullable: false),
                        ScreenMenuId = c.Int(nullable: false),
                        MostUsedItemsCategory = c.Boolean(nullable: false),
                        ColumnCount = c.Int(nullable: false),
                        MenuItemButtonHeight = c.Int(nullable: false),
                        MenuItemButtonColor = c.String(),
                        MenuItemFontSize = c.Double(nullable: false),
                        WrapText = c.Boolean(nullable: false),
                        PageCount = c.Int(nullable: false),
                        MainButtonHeight = c.Int(nullable: false),
                        MainButtonColor = c.String(),
                        MainFontSize = c.Double(nullable: false),
                        SubButtonHeight = c.Int(nullable: false),
                        SubButtonRows = c.Int(nullable: false),
                        SubButtonColorDef = c.String(),
                        NumeratorType = c.Int(nullable: false),
                        NumeratorValues = c.String(),
                        AlphaButtonValues = c.String(),
                        ImagePath = c.String(),
                        MaxItems = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ScreenMenus", t => t.ScreenMenuId, cascadeDelete: true)
                .Index(t => t.ScreenMenuId);
            
            CreateTable(
                "dbo.ScreenMenuItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ScreenMenuCategoryId = c.Int(nullable: false),
                        MenuItemId = c.Int(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        AutoSelect = c.Boolean(nullable: false),
                        ButtonColor = c.String(),
                        Quantity = c.Int(nullable: false),
                        ImagePath = c.String(),
                        FontSize = c.Double(nullable: false),
                        SubMenuTag = c.String(),
                        ItemPortion = c.String(),
                        OrderTags = c.String(),
                        OrderStates = c.String(),
                        AutomationCommand = c.String(),
                        AutomationCommandValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ScreenMenuCategories", t => t.ScreenMenuCategoryId, cascadeDelete: true)
                .Index(t => t.ScreenMenuCategoryId);
            
            CreateTable(
                "dbo.ScreenMenus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryColumnCount = c.Int(nullable: false),
                        CategoryColumnWidthRate = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Scripts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HandlerName = c.String(),
                        Code = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                        StateType = c.Int(nullable: false),
                        Color = c.String(),
                        ShowOnEndOfDayReport = c.Boolean(nullable: false),
                        ShowOnProductReport = c.Boolean(nullable: false),
                        ShowOnTicket = c.Boolean(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TaskTokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaskId = c.Int(nullable: false),
                        Caption = c.String(),
                        Value = c.String(),
                        Type = c.Int(nullable: false),
                        ReferenceTypeId = c.Int(nullable: false),
                        ReferenceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.TaskId })
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaskTypeId = c.Int(nullable: false),
                        Content = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CustomData = c.String(),
                        Completed = c.Boolean(nullable: false),
                        LastUpdateTime = c.DateTime(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TaskTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TaskCustomFields",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaskTypeId = c.Int(nullable: false),
                        FieldType = c.Int(nullable: false),
                        EditingFormat = c.String(),
                        DisplayFormat = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.TaskTypeId })
                .ForeignKey("dbo.TaskTypes", t => t.TaskTypeId, cascadeDelete: true)
                .Index(t => t.TaskTypeId);
            
            CreateTable(
                "dbo.TaxTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 16, scale: 2),
                        Rounding = c.Int(nullable: false),
                        Name = c.String(),
                        AccountTransactionType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountTransactionTypes", t => t.AccountTransactionType_Id)
                .Index(t => t.AccountTransactionType_Id);
            
            CreateTable(
                "dbo.TaxTemplateMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaxTemplateId = c.Int(nullable: false),
                        MenuItemGroupCode = c.String(),
                        MenuItemId = c.Int(nullable: false),
                        TerminalId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        UserRoleId = c.Int(nullable: false),
                        TicketTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaxTemplates", t => t.TaxTemplateId, cascadeDelete: true)
                .Index(t => t.TaxTemplateId);
            
            CreateTable(
                "dbo.Terminals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDefault = c.Boolean(nullable: false),
                        AutoLogout = c.Boolean(nullable: false),
                        ReportPrinterId = c.Int(nullable: false),
                        TransactionPrinterId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TicketEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityTypeId = c.Int(nullable: false),
                        EntityId = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                        AccountTypeId = c.Int(nullable: false),
                        EntityName = c.String(),
                        EntityCustomData = c.String(),
                        Ticket_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tickets", t => t.Ticket_Id)
                .Index(t => t.Ticket_Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastUpdateTime = c.DateTime(nullable: false),
                        TicketNumber = c.String(),
                        Date = c.DateTime(nullable: false),
                        LastOrderDate = c.DateTime(nullable: false),
                        LastPaymentDate = c.DateTime(nullable: false),
                        IsClosed = c.Boolean(nullable: false),
                        IsLocked = c.Boolean(nullable: false),
                        RemainingAmount = c.Decimal(nullable: false, precision: 16, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 16, scale: 2),
                        DepartmentId = c.Int(nullable: false),
                        TicketTypeId = c.Int(nullable: false),
                        Note = c.String(),
                        LastModifiedUserName = c.String(),
                        TicketTags = c.String(),
                        TicketStates = c.String(),
                        TicketLogs = c.String(),
                        ExchangeRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxIncluded = c.Boolean(nullable: false),
                        Name = c.String(),
                        TransactionDocument_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountTransactionDocuments", t => t.TransactionDocument_Id)
                .Index(t => t.TransactionDocument_Id);
            
            CreateTable(
                "dbo.TicketTagGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        FreeTagging = c.Boolean(nullable: false),
                        SaveFreeTags = c.Boolean(nullable: false),
                        ButtonColorWhenTagSelected = c.String(),
                        ButtonColorWhenNoTagSelected = c.String(),
                        ForceValue = c.Boolean(nullable: false),
                        AskBeforeCreatingTicket = c.Boolean(nullable: false),
                        DataType = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TicketTagMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketTagGroupId = c.Int(nullable: false),
                        TerminalId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        UserRoleId = c.Int(nullable: false),
                        TicketTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TicketTagGroups", t => t.TicketTagGroupId, cascadeDelete: true)
                .Index(t => t.TicketTagGroupId);
            
            CreateTable(
                "dbo.TicketTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketTagGroupId = c.Int(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TicketTagGroups", t => t.TicketTagGroupId, cascadeDelete: true)
                .Index(t => t.TicketTagGroupId);
            
            CreateTable(
                "dbo.TicketTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScreenMenuId = c.Int(nullable: false),
                        TaxIncluded = c.Boolean(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        Name = c.String(),
                        OrderNumerator_Id = c.Int(),
                        SaleTransactionType_Id = c.Int(),
                        TicketNumerator_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Numerators", t => t.OrderNumerator_Id)
                .ForeignKey("dbo.AccountTransactionTypes", t => t.SaleTransactionType_Id)
                .ForeignKey("dbo.Numerators", t => t.TicketNumerator_Id)
                .Index(t => t.OrderNumerator_Id)
                .Index(t => t.SaleTransactionType_Id)
                .Index(t => t.TicketNumerator_Id);
            
            CreateTable(
                "dbo.EntityTypeAssignments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketTypeId = c.Int(nullable: false),
                        EntityTypeId = c.Int(nullable: false),
                        EntityTypeName = c.String(),
                        AskBeforeCreatingTicket = c.Boolean(nullable: false),
                        State = c.String(),
                        CopyToNewTickets = c.Boolean(nullable: false),
                        SortOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.TicketTypeId })
                .ForeignKey("dbo.TicketTypes", t => t.TicketTypeId, cascadeDelete: true)
                .Index(t => t.TicketTypeId);
            
            CreateTable(
                "dbo.MenuAssignments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketTypeId = c.Int(nullable: false),
                        TerminalId = c.Int(nullable: false),
                        MenuId = c.Int(nullable: false),
                        TerminalName = c.String(),
                        SortOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.TicketTypeId })
                .ForeignKey("dbo.TicketTypes", t => t.TicketTypeId, cascadeDelete: true)
                .Index(t => t.TicketTypeId);
            
            CreateTable(
                "dbo.Triggers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Expression = c.String(),
                        LastTrigger = c.DateTime(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsAdmin = c.Boolean(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PinCode = c.String(),
                        Name = c.String(),
                        UserRole_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserRoles", t => t.UserRole_Id)
                .Index(t => t.UserRole_Id);
            
            CreateTable(
                "dbo.Warehouses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WarehouseTypeId = c.Int(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WarehouseTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkPeriods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        StartDescription = c.String(),
                        EndDescription = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AccountTransactionDocumentTypeAccountTransactionTypes",
                c => new
                    {
                        AccountTransactionDocumentType_Id = c.Int(nullable: false),
                        AccountTransactionType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AccountTransactionDocumentType_Id, t.AccountTransactionType_Id })
                .ForeignKey("dbo.AccountTransactionDocumentTypes", t => t.AccountTransactionDocumentType_Id, cascadeDelete: true)
                .ForeignKey("dbo.AccountTransactionTypes", t => t.AccountTransactionType_Id, cascadeDelete: true)
                .Index(t => t.AccountTransactionDocumentType_Id)
                .Index(t => t.AccountTransactionType_Id);
            
            CreateTable(
                "dbo.CalculationSelectorCalculationTypes",
                c => new
                    {
                        CalculationSelector_Id = c.Int(nullable: false),
                        CalculationType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CalculationSelector_Id, t.CalculationType_Id })
                .ForeignKey("dbo.CalculationSelectors", t => t.CalculationSelector_Id, cascadeDelete: true)
                .ForeignKey("dbo.CalculationTypes", t => t.CalculationType_Id, cascadeDelete: true)
                .Index(t => t.CalculationSelector_Id)
                .Index(t => t.CalculationType_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserRole_Id", "dbo.UserRoles");
            DropForeignKey("dbo.Permissions", "UserRoleId", "dbo.UserRoles");
            DropForeignKey("dbo.TicketTypes", "TicketNumerator_Id", "dbo.Numerators");
            DropForeignKey("dbo.TicketTypes", "SaleTransactionType_Id", "dbo.AccountTransactionTypes");
            DropForeignKey("dbo.TicketTypes", "OrderNumerator_Id", "dbo.Numerators");
            DropForeignKey("dbo.MenuAssignments", "TicketTypeId", "dbo.TicketTypes");
            DropForeignKey("dbo.EntityTypeAssignments", "TicketTypeId", "dbo.TicketTypes");
            DropForeignKey("dbo.TicketTags", "TicketTagGroupId", "dbo.TicketTagGroups");
            DropForeignKey("dbo.TicketTagMaps", "TicketTagGroupId", "dbo.TicketTagGroups");
            DropForeignKey("dbo.Tickets", "TransactionDocument_Id", "dbo.AccountTransactionDocuments");
            DropForeignKey("dbo.TicketEntities", "Ticket_Id", "dbo.Tickets");
            DropForeignKey("dbo.Payments", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.PaidItems", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.Orders", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.ChangePayments", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.Calculations", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.TaxTemplateMaps", "TaxTemplateId", "dbo.TaxTemplates");
            DropForeignKey("dbo.TaxTemplates", "AccountTransactionType_Id", "dbo.AccountTransactionTypes");
            DropForeignKey("dbo.TaskCustomFields", "TaskTypeId", "dbo.TaskTypes");
            DropForeignKey("dbo.TaskTokens", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.ScreenMenuCategories", "ScreenMenuId", "dbo.ScreenMenus");
            DropForeignKey("dbo.ScreenMenuItems", "ScreenMenuCategoryId", "dbo.ScreenMenuCategories");
            DropForeignKey("dbo.AppRuleMaps", "AppRuleId", "dbo.AppRules");
            DropForeignKey("dbo.ActionContainers", "AppRuleId", "dbo.AppRules");
            DropForeignKey("dbo.RecipeItems", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.Recipes", "Portion_Id", "dbo.MenuItemPortions");
            DropForeignKey("dbo.RecipeItems", "InventoryItem_Id", "dbo.InventoryItems");
            DropForeignKey("dbo.ProdcutTimerMaps", "ProductTimerId", "dbo.ProductTimers");
            DropForeignKey("dbo.PrinterMaps", "PrintJobId", "dbo.PrintJobs");
            DropForeignKey("dbo.WarehouseConsumptions", "PeriodicConsumptionId", "dbo.PeriodicConsumptions");
            DropForeignKey("dbo.PeriodicConsumptionItems", new[] { "PeriodicConsumptionId", "WarehouseConsumptionId" }, "dbo.WarehouseConsumptions");
            DropForeignKey("dbo.CostItems", new[] { "PeriodicConsumptionId", "WarehouseConsumptionId" }, "dbo.WarehouseConsumptions");
            DropForeignKey("dbo.PaymentTypeMaps", "PaymentTypeId", "dbo.PaymentTypes");
            DropForeignKey("dbo.PaymentTypes", "AccountTransactionType_Id", "dbo.AccountTransactionTypes");
            DropForeignKey("dbo.PaymentTypes", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Payments", new[] { "AccountTransaction_Id", "AccountTransaction_AccountTransactionDocumentId" }, "dbo.AccountTransactions");
            DropForeignKey("dbo.OrderTags", "OrderTagGroupId", "dbo.OrderTagGroups");
            DropForeignKey("dbo.OrderTagMaps", "OrderTagGroupId", "dbo.OrderTagGroups");
            DropForeignKey("dbo.Orders", "ProductTimerValueId", "dbo.ProductTimerValues");
            DropForeignKey("dbo.MenuItemPortions", "MenuItemId", "dbo.MenuItems");
            DropForeignKey("dbo.MenuItemPrices", "MenuItemPortionId", "dbo.MenuItemPortions");
            DropForeignKey("dbo.InventoryTransactions", "InventoryTransactionDocumentId", "dbo.InventoryTransactionDocuments");
            DropForeignKey("dbo.InventoryTransactions", "InventoryItem_Id", "dbo.InventoryItems");
            DropForeignKey("dbo.InventoryTransactionDocumentTypes", "InventoryTransactionType_Id", "dbo.InventoryTransactionTypes");
            DropForeignKey("dbo.InventoryTransactionDocumentTypes", "AccountTransactionType_Id", "dbo.AccountTransactionTypes");
            DropForeignKey("dbo.EntityCustomFields", "EntityType_Id", "dbo.EntityTypes");
            DropForeignKey("dbo.Widgets", "EntityScreenId", "dbo.EntityScreens");
            DropForeignKey("dbo.EntityScreenItems", "EntityScreenId", "dbo.EntityScreens");
            DropForeignKey("dbo.EntityScreenMaps", "EntityScreenId", "dbo.EntityScreens");
            DropForeignKey("dbo.ChangePaymentTypeMaps", "ChangePaymentTypeId", "dbo.ChangePaymentTypes");
            DropForeignKey("dbo.ChangePaymentTypes", "AccountTransactionType_Id", "dbo.AccountTransactionTypes");
            DropForeignKey("dbo.ChangePaymentTypes", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.ChangePayments", new[] { "AccountTransaction_Id", "AccountTransaction_AccountTransactionDocumentId" }, "dbo.AccountTransactions");
            DropForeignKey("dbo.CalculationSelectorCalculationTypes", "CalculationType_Id", "dbo.CalculationTypes");
            DropForeignKey("dbo.CalculationSelectorCalculationTypes", "CalculationSelector_Id", "dbo.CalculationSelectors");
            DropForeignKey("dbo.CalculationTypes", "AccountTransactionType_Id", "dbo.AccountTransactionTypes");
            DropForeignKey("dbo.CalculationSelectorMaps", "CalculationSelectorId", "dbo.CalculationSelectors");
            DropForeignKey("dbo.AutomationCommandMaps", "AutomationCommandId", "dbo.AutomationCommands");
            DropForeignKey("dbo.AccountTransactionDocumentTypeAccountTransactionTypes", "AccountTransactionType_Id", "dbo.AccountTransactionTypes");
            DropForeignKey("dbo.AccountTransactionDocumentTypeAccountTransactionTypes", "AccountTransactionDocumentType_Id", "dbo.AccountTransactionDocumentTypes");
            DropForeignKey("dbo.AccountTransactionDocumentTypeMaps", "AccountTransactionDocumentTypeId", "dbo.AccountTransactionDocumentTypes");
            DropForeignKey("dbo.AccountTransactionDocumentAccountMaps", "AccountTransactionDocumentTypeId", "dbo.AccountTransactionDocumentTypes");
            DropForeignKey("dbo.AccountTransactions", "AccountTransactionDocumentId", "dbo.AccountTransactionDocuments");
            DropForeignKey("dbo.AccountTransactionValues", new[] { "AccountTransactionId", "AccountTransactionDocumentId" }, "dbo.AccountTransactions");
            DropForeignKey("dbo.AccountScreenValues", "AccountScreenId", "dbo.AccountScreens");
            DropIndex("dbo.CalculationSelectorCalculationTypes", new[] { "CalculationType_Id" });
            DropIndex("dbo.CalculationSelectorCalculationTypes", new[] { "CalculationSelector_Id" });
            DropIndex("dbo.AccountTransactionDocumentTypeAccountTransactionTypes", new[] { "AccountTransactionType_Id" });
            DropIndex("dbo.AccountTransactionDocumentTypeAccountTransactionTypes", new[] { "AccountTransactionDocumentType_Id" });
            DropIndex("dbo.Users", new[] { "UserRole_Id" });
            DropIndex("dbo.MenuAssignments", new[] { "TicketTypeId" });
            DropIndex("dbo.EntityTypeAssignments", new[] { "TicketTypeId" });
            DropIndex("dbo.TicketTypes", new[] { "TicketNumerator_Id" });
            DropIndex("dbo.TicketTypes", new[] { "SaleTransactionType_Id" });
            DropIndex("dbo.TicketTypes", new[] { "OrderNumerator_Id" });
            DropIndex("dbo.TicketTags", new[] { "TicketTagGroupId" });
            DropIndex("dbo.TicketTagMaps", new[] { "TicketTagGroupId" });
            DropIndex("dbo.Tickets", new[] { "TransactionDocument_Id" });
            DropIndex("dbo.TicketEntities", new[] { "Ticket_Id" });
            DropIndex("dbo.TaxTemplateMaps", new[] { "TaxTemplateId" });
            DropIndex("dbo.TaxTemplates", new[] { "AccountTransactionType_Id" });
            DropIndex("dbo.TaskCustomFields", new[] { "TaskTypeId" });
            DropIndex("dbo.TaskTokens", new[] { "TaskId" });
            DropIndex("dbo.ScreenMenuItems", new[] { "ScreenMenuCategoryId" });
            DropIndex("dbo.ScreenMenuCategories", new[] { "ScreenMenuId" });
            DropIndex("dbo.AppRuleMaps", new[] { "AppRuleId" });
            DropIndex("dbo.Recipes", new[] { "Portion_Id" });
            DropIndex("dbo.RecipeItems", new[] { "InventoryItem_Id" });
            DropIndex("dbo.RecipeItems", new[] { "RecipeId" });
            DropIndex("dbo.ProdcutTimerMaps", new[] { "ProductTimerId" });
            DropIndex("dbo.PrinterMaps", new[] { "PrintJobId" });
            DropIndex("dbo.Permissions", new[] { "UserRoleId" });
            DropIndex("dbo.WarehouseConsumptions", new[] { "PeriodicConsumptionId" });
            DropIndex("dbo.PeriodicConsumptionItems", new[] { "PeriodicConsumptionId", "WarehouseConsumptionId" });
            DropIndex("dbo.PaymentTypes", new[] { "AccountTransactionType_Id" });
            DropIndex("dbo.PaymentTypes", new[] { "Account_Id" });
            DropIndex("dbo.PaymentTypeMaps", new[] { "PaymentTypeId" });
            DropIndex("dbo.Payments", new[] { "AccountTransaction_Id", "AccountTransaction_AccountTransactionDocumentId" });
            DropIndex("dbo.Payments", new[] { "TicketId" });
            DropIndex("dbo.PaidItems", new[] { "TicketId" });
            DropIndex("dbo.OrderTags", new[] { "OrderTagGroupId" });
            DropIndex("dbo.OrderTagMaps", new[] { "OrderTagGroupId" });
            DropIndex("dbo.Orders", new[] { "ProductTimerValueId" });
            DropIndex("dbo.Orders", new[] { "TicketId" });
            DropIndex("dbo.MenuItemPrices", new[] { "MenuItemPortionId" });
            DropIndex("dbo.MenuItemPortions", new[] { "MenuItemId" });
            DropIndex("dbo.InventoryTransactions", new[] { "InventoryItem_Id" });
            DropIndex("dbo.InventoryTransactions", new[] { "InventoryTransactionDocumentId" });
            DropIndex("dbo.InventoryTransactionDocumentTypes", new[] { "InventoryTransactionType_Id" });
            DropIndex("dbo.InventoryTransactionDocumentTypes", new[] { "AccountTransactionType_Id" });
            DropIndex("dbo.Widgets", new[] { "EntityScreenId" });
            DropIndex("dbo.EntityScreenMaps", new[] { "EntityScreenId" });
            DropIndex("dbo.EntityScreenItems", new[] { "EntityScreenId" });
            DropIndex("dbo.EntityCustomFields", new[] { "EntityType_Id" });
            DropIndex("dbo.CostItems", new[] { "PeriodicConsumptionId", "WarehouseConsumptionId" });
            DropIndex("dbo.ChangePaymentTypes", new[] { "AccountTransactionType_Id" });
            DropIndex("dbo.ChangePaymentTypes", new[] { "Account_Id" });
            DropIndex("dbo.ChangePaymentTypeMaps", new[] { "ChangePaymentTypeId" });
            DropIndex("dbo.ChangePayments", new[] { "AccountTransaction_Id", "AccountTransaction_AccountTransactionDocumentId" });
            DropIndex("dbo.ChangePayments", new[] { "TicketId" });
            DropIndex("dbo.CalculationTypes", new[] { "AccountTransactionType_Id" });
            DropIndex("dbo.CalculationSelectorMaps", new[] { "CalculationSelectorId" });
            DropIndex("dbo.Calculations", new[] { "TicketId" });
            DropIndex("dbo.AutomationCommandMaps", new[] { "AutomationCommandId" });
            DropIndex("dbo.ActionContainers", new[] { "AppRuleId" });
            DropIndex("dbo.AccountTransactionDocumentAccountMaps", new[] { "AccountTransactionDocumentTypeId" });
            DropIndex("dbo.AccountTransactionDocumentTypeMaps", new[] { "AccountTransactionDocumentTypeId" });
            DropIndex("dbo.AccountTransactionValues", new[] { "AccountTransactionId", "AccountTransactionDocumentId" });
            DropIndex("dbo.AccountTransactions", new[] { "AccountTransactionDocumentId" });
            DropIndex("dbo.AccountScreenValues", new[] { "AccountScreenId" });
            DropTable("dbo.CalculationSelectorCalculationTypes");
            DropTable("dbo.AccountTransactionDocumentTypeAccountTransactionTypes");
            DropTable("dbo.WorkPeriods");
            DropTable("dbo.WarehouseTypes");
            DropTable("dbo.Warehouses");
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Triggers");
            DropTable("dbo.MenuAssignments");
            DropTable("dbo.EntityTypeAssignments");
            DropTable("dbo.TicketTypes");
            DropTable("dbo.TicketTags");
            DropTable("dbo.TicketTagMaps");
            DropTable("dbo.TicketTagGroups");
            DropTable("dbo.Tickets");
            DropTable("dbo.TicketEntities");
            DropTable("dbo.Terminals");
            DropTable("dbo.TaxTemplateMaps");
            DropTable("dbo.TaxTemplates");
            DropTable("dbo.TaskCustomFields");
            DropTable("dbo.TaskTypes");
            DropTable("dbo.Tasks");
            DropTable("dbo.TaskTokens");
            DropTable("dbo.States");
            DropTable("dbo.Scripts");
            DropTable("dbo.ScreenMenus");
            DropTable("dbo.ScreenMenuItems");
            DropTable("dbo.ScreenMenuCategories");
            DropTable("dbo.AppRuleMaps");
            DropTable("dbo.AppRules");
            DropTable("dbo.AppActions");
            DropTable("dbo.Recipes");
            DropTable("dbo.RecipeItems");
            DropTable("dbo.ProgramSettingValues");
            DropTable("dbo.ProdcutTimerMaps");
            DropTable("dbo.ProductTimers");
            DropTable("dbo.PrintJobs");
            DropTable("dbo.PrinterTemplates");
            DropTable("dbo.Printers");
            DropTable("dbo.PrinterMaps");
            DropTable("dbo.Permissions");
            DropTable("dbo.WarehouseConsumptions");
            DropTable("dbo.PeriodicConsumptions");
            DropTable("dbo.PeriodicConsumptionItems");
            DropTable("dbo.PaymentTypes");
            DropTable("dbo.PaymentTypeMaps");
            DropTable("dbo.Payments");
            DropTable("dbo.PaidItems");
            DropTable("dbo.OrderTags");
            DropTable("dbo.OrderTagMaps");
            DropTable("dbo.OrderTagGroups");
            DropTable("dbo.ProductTimerValues");
            DropTable("dbo.Orders");
            DropTable("dbo.Numerators");
            DropTable("dbo.MenuItems");
            DropTable("dbo.MenuItemPriceDefinitions");
            DropTable("dbo.MenuItemPrices");
            DropTable("dbo.MenuItemPortions");
            DropTable("dbo.InventoryTransactionDocuments");
            DropTable("dbo.InventoryTransactions");
            DropTable("dbo.InventoryTransactionTypes");
            DropTable("dbo.InventoryTransactionDocumentTypes");
            DropTable("dbo.InventoryItems");
            DropTable("dbo.ForeignCurrencies");
            DropTable("dbo.EntityTypes");
            DropTable("dbo.EntityStateValues");
            DropTable("dbo.Widgets");
            DropTable("dbo.EntityScreenMaps");
            DropTable("dbo.EntityScreens");
            DropTable("dbo.EntityScreenItems");
            DropTable("dbo.EntityCustomFields");
            DropTable("dbo.Entities");
            DropTable("dbo.Departments");
            DropTable("dbo.CostItems");
            DropTable("dbo.ChangePaymentTypes");
            DropTable("dbo.ChangePaymentTypeMaps");
            DropTable("dbo.ChangePayments");
            DropTable("dbo.CalculationTypes");
            DropTable("dbo.CalculationSelectorMaps");
            DropTable("dbo.CalculationSelectors");
            DropTable("dbo.Calculations");
            DropTable("dbo.AutomationCommands");
            DropTable("dbo.AutomationCommandMaps");
            DropTable("dbo.ActionContainers");
            DropTable("dbo.AccountTypes");
            DropTable("dbo.AccountTransactionTypes");
            DropTable("dbo.AccountTransactionDocumentAccountMaps");
            DropTable("dbo.AccountTransactionDocumentTypes");
            DropTable("dbo.AccountTransactionDocumentTypeMaps");
            DropTable("dbo.AccountTransactionValues");
            DropTable("dbo.AccountTransactions");
            DropTable("dbo.AccountTransactionDocuments");
            DropTable("dbo.AccountScreenValues");
            DropTable("dbo.AccountScreens");
            DropTable("dbo.Accounts");
        }
    }
}
