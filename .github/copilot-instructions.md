# Project Overview

This is a Dynamics 365 CE plugins project that contains custom business logic to be executed in response to specific events within the Dynamics 365 Customer Engagement platform. The project is structured to facilitate easy development, testing, and deployment of plugins.

# Project Architecture & Structure

- Plugins are in folder **Plugins/**
  Conventions for plugin name, class name, and namespace:
  - Plugin Name: [EntityName][Event][Action]Plugin (e.g., AccountCreateSteTitlePlugin)
  - Class Name: [EntityName][Event][Action]Plugin (e.g., AccountCreateSetTitlePlugin)
  - Namespace: CompanyName.ProjectName.Plugins (e.g., Contoso.Dynamics.Plugins)

Plugin Class Structure:

```csharp

namespace CXE.Sample.Account
{

    [
        CrmPluginRegistration(
         MessageNameEnum.Update,
         "account",
         StageEnum.PostOperation,
         ExecutionModeEnum.Synchronous,
         "msdyn_recordstage",
         "CXE.Sample.Account - On Update Account PostSync - Set Department",
         1,
         IsolationModeEnum.Sandbox
     )
        ]
    public class AccountUpdateSetContactDeptPlugin : PluginBase
    {
        private AccountService _accountService;
        public override void InitializeController() => _accountService = new AccountService(
            this.ServiceClient,
            new AccountModel()
            {
                Id = this.TargetEntity.Id,
                LogicalName = this.TargetEntity.LogicalName,
                Attributes = this.TargetEntity.Attributes
            },
            this.Logger);
    }
}
```

- Services are in folder **Services/**
  Use these conventions for service name, class name, and namespace:
  - Service Name: [EntityName]Service (e.g., AccountService)
  - Class Name: [EntityName]Service (e.g., AccountService)
  - Namespace: CompanyName.ProjectName.Services (e.g., Contoso.Dynamics.Services)
    Use attribute [DataverseTable] to map the service to the Dataverse table.
    Get name parameter for [DataverseTable(Name = <Table_LogicalName>)] from from #CXE-IN Dev02 mcp connector
    Inherit it from EntityBase.
    Example
    :

```csharp
[DataverseTable("account")]
	public partial class AccountModel : EntityBase
	{
   }
```

- Models are in folder **Models/**
  Use these conventions for model name, class name, and namespace:
- Model Name: [EntityName]Model (e.g., AccountModel)
- Class Name: [EntityName]Model (e.g., AccountModel)
- Namespace: CompanyName.ProjectName.Models (e.g., Contoso.Dynamics.Models)
  Use attribute [DataverseColumn] to map the model to the Dataverse table.
  Get the attribute logical names for [DataverseColumn(Name = <Attribute_LogicalName>)] from #CXE-IN Dev02 mcp connector
```csharp
[DataverseTable("accountid")]
public partial class AccountModel : EntityBase
{
      [DataverseColumn("name")]
	    public string Name
	    {
		    get => GetValue<string>();
		    set => SetValue(value);
	    }
}
```

both attributes [DataverseTable] and [DataverseColumn] are in the shared project CXE.CoreFx and both take logical name as parameter.
